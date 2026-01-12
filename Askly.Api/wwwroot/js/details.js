document.addEventListener('DOMContentLoaded', function () {
    const voteBtn = document.getElementById('voteBtn');
    const toastContainer = document.getElementById('toastContainer');

    if (!voteBtn) return;

    function showToast(message, timeout = 4000) {
        if (!toastContainer) return () => {};

        const toast = document.createElement('div');
        toast.className = 'toast';
        toast.innerHTML = '<div class="toast-text"></div>';

        const text = toast.querySelector('.toast-text');
        text.textContent = message;

        const closeBtn = document.createElement('button');
        closeBtn.className = 'toast-close';
        closeBtn.setAttribute('aria-label', 'Закрыть');
        closeBtn.innerHTML = '&times;';

        closeBtn.addEventListener('click', () => hideToast(toast));
        toast.appendChild(closeBtn);

        toastContainer.prepend(toast);

        requestAnimationFrame(() => {
            requestAnimationFrame(() => toast.classList.add('show'));
        });

        const removeTimer = setTimeout(() => hideToast(toast), timeout);

        function hideToast(node) {
            clearTimeout(removeTimer);
            node.classList.remove('show');
            node.classList.add('hide');
            node.addEventListener('transitionend', () => {
                if (node.parentNode) node.parentNode.removeChild(node);
            }, { once: true });
        }

        return () => hideToast(toast);
    }

    function setInputsDisabled(formScope, disabled) {
        const inputs = formScope.querySelectorAll('input[type="radio"], input[type="checkbox"]');
        inputs.forEach(i => i.disabled = !!disabled);
        // опционально можно добавить/удалить класс для визуального состояния
        const wrappers = formScope.querySelectorAll('.option-item');
        wrappers.forEach(w => {
            if (disabled) w.classList.add('option-disabled');
            else w.classList.remove('option-disabled');
        });
    }

    function applyResultsToOptions(results, formScope) {
        if (!results || !Array.isArray(results.options)) return;

        const totalVotes = results.options.reduce(
            (sum, o) => sum + (o.votesCount || 0),
            0
        );

        const safeTotal = totalVotes === 0 ? 1 : totalVotes;
        results.options.forEach(option => {
            const optionId = option.id;
            const votes = option.votesCount || 0;
            const percent = Math.round((votes / safeTotal) * 100);

            const input = formScope.querySelector('input[value="' + optionId + '"]');
            if (!input) return;

            const wrapper = input.closest('.option-item') || input.parentElement;
            if (!wrapper) return;
            wrapper.classList.add('option-with-results');

            let resultSpan = wrapper.querySelector('.option-result');
            if (!resultSpan) {
                resultSpan = document.createElement('span');
                resultSpan.className = 'option-result';
                wrapper.appendChild(resultSpan);
            }

            resultSpan.textContent = votes + ' (' + percent + '%)';
        });
    }

    function createCancelButton(original, selectedIdsAtVote, pollId, formScope) {
        const btn = document.createElement('button');
        btn.type = 'button';
        btn.id = 'cancelBtn';
        btn.className = original.className + ' cancel-button';
        btn.textContent = 'Отменить голос';

        btn.addEventListener('click', function () {
            
            fetch(`/api/polls/${pollId}/vote`, {
                method: "DELETE",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify(selectedIdsAtVote)
            })
                .then(r => {
                    if (!r.ok) throw new Error("Ошибка голосования");
                })
            // Логируем и показываем тост
            // console.log('Cancel payload:', JSON.stringify(payload));
            showToast('Голос отменен');

            // Убираем результаты с интерфейса: удаляем .option-result и сбрасываем классы
            try {
                const resultSpans = formScope.querySelectorAll('.option-result');
                resultSpans.forEach(s => s.remove());

                const withResults = formScope.querySelectorAll('.option-with-results');
                withResults.forEach(w => w.classList.remove('option-with-results'));
            } catch (e) {
                console.error('Ошибка при очистке результатов:', e);
            }

            // Восстанавливаем состояние: включаем inputs и возвращаем кнопку
            setInputsDisabled(formScope, false);
            btn.replaceWith(original);
        });

        return btn;
    }

    voteBtn.addEventListener('click', function (e) {
        e.preventDefault();

        const formScope = voteBtn.closest('form') || document;
        const anyChecked = formScope.querySelector('input[type="radio"]:checked, input[type="checkbox"]:checked');

        if (!anyChecked) {
            showToast('Выберите хотя бы один вариант ответа');
            return;
        }
        
        const pollInput = formScope.querySelector('input[name="Id"], input[id="Id"], input[type="hidden"]');
        const pollId = pollInput ? String(pollInput.value) : '';
        
        let optionsIds = [];
        const checkedCheckboxes = Array.from(formScope.querySelectorAll('input.option-checkbox:checked'));
        if (checkedCheckboxes.length > 0) {
            optionsIds = checkedCheckboxes.map(i => String(i.value));
        } else {
            const checkedRadio = formScope.querySelector('input[type="radio"]:checked');
            if (checkedRadio) optionsIds.push(String(checkedRadio.value));
        }

        // Сохраняем выбранные id на момент голосования
        const selectedAtVote = optionsIds.slice();

        // Отключаем возможность менять выбор
        setInputsDisabled(formScope, true);

        // Создаём кнопку "Отменить голос" с замыканием выбранных id
        const cancelBtn = createCancelButton(voteBtn, selectedAtVote, pollId, formScope);
        voteBtn.replaceWith(cancelBtn);
        showToast('Голос принят');

        // Отправляем голос и запрашиваем результаты (опционально можем ожидать и только потом менять UI)
        fetch(`/api/polls/${pollId}/vote`, {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify(optionsIds)
        })
            .then(r => {
                if (!r.ok) throw new Error("Ошибка голосования");
                return fetch(`/api/polls/${pollId}/results`, {
                    method: "GET",
                    headers: { "Content-Type": "application/json" }
                });
            })
            .then(r => {
                if (!r.ok) throw new Error("Ошибка получения результатов");
                return r.json();
            })
            .then(results => {
                applyResultsToOptions(results, formScope);
            })
            .catch(err => {
                console.error(err);
                showToast('Произошла ошибка при голосовании');

                // при ошибке восстанавливаем интерактивность и кнопку
                try {
                    const currentCancel = formScope.querySelector('#cancelBtn');
                    if (currentCancel && currentCancel.parentNode) currentCancel.replaceWith(voteBtn);
                } finally {
                    setInputsDisabled(formScope, false);
                }
            });
    });
});
