
document.addEventListener('DOMContentLoaded', function () {
    const voteBtn = document.getElementById('voteBtn');
    if (!voteBtn) return;

// Функция создаёт кнопку "Отменить голос"
function createCancelButton(original) {
    const btn = document.createElement('button');
    btn.type = 'button';
    btn.id = 'cancelBtn';
    // сохраняем классы, чтобы размер/отступы совпадали
    btn.className = original.className + ' cancel-button';
    btn.textContent = 'Отменить голос';
    
    // при нажатии отменяем — возвращаем исходную кнопку
    btn.addEventListener('click', function () {
    btn.replaceWith(original);
});

    return btn;
}

voteBtn.addEventListener('click', function (e) {
    // предотвращаем немедленную отправку формы — только визуальная замена
    e.preventDefault();

    // создаём кнопку отмены и меняем местами
    const cancel = createCancelButton(voteBtn);
    voteBtn.replaceWith(cancel);
});
});
