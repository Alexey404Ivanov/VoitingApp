document.addEventListener('DOMContentLoaded',(function () {
    const form = document.getElementById('loginForm');
    const toastContainer = document.getElementById('toastContainer');

    if (!form) return;

    function showToast(message, timeout = 4000) {
        if (!toastContainer) return;
        const toast = document.createElement('div');
        toast.className = 'toast';
        toast.innerHTML = '<div class="toast-text"></div>';
        toast.querySelector('.toast-text').textContent = message;

        const closeBtn = document.createElement('button');
        closeBtn.className = 'toast-close';
        closeBtn.setAttribute('aria-label', 'Закрыть');
        closeBtn.innerHTML = '&times;';
        closeBtn.addEventListener('click', () => hideToast(toast));
        toast.appendChild(closeBtn);

        toastContainer.prepend(toast);
        requestAnimationFrame(() => requestAnimationFrame(() => toast.classList.add('show')));

        const timer = setTimeout(() => hideToast(toast), timeout);
        function hideToast(node) {
            clearTimeout(timer);
            node.classList.remove('show');
            node.classList.add('hide');
            node.addEventListener('transitionend', () => node.parentNode && node.parentNode.removeChild(node), { once: true });
        }
    }

    form.addEventListener('submit', async function (e) {
        e.preventDefault(); 
        
        const email = document.getElementById('loginEmail').value.trim();
        const password = document.getElementById('loginPassword').value.trim();

        if (!email || !password) {
            showToast('Не все поля заполнены');
            return;
        }

        try {
            await fetch("/api/users/login", {
                method: "POST",
                headers: { "Content-Type": "application/json" },
                body: JSON.stringify({ email, password }),
                credentials: "include" // важно для cookie
            });

            window.location.href = "/polls";
        }
        catch (err) {
            console.error(err);
            showToast("Ошибка входа");
        }
    });

}));