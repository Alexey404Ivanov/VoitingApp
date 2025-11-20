const modalBackdrop = document.getElementById("modalBackdrop");
const modal = document.getElementById("modal");
const btnAdd = document.getElementById("btnAdd");
const modalClose = document.getElementById("modalClose");


btnAdd.addEventListener("click", () => {
    modalBackdrop.style.display = "flex";
    // modal.style.display = "flex";
});

modalClose.addEventListener("click", () => {
    modalBackdrop.style.display = "none";
    // modal.style.display = "none";
});

// Закрытие при клике по фону
window.addEventListener("click", (e) => {
    if (e.target === modal) {
        modal.display = "none";
    }
});

function openPoll(id) {
    alert("Открыть опрос " + id);
}