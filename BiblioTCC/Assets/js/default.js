function AbrirModal(titulo, mensagem) {
    document.getElementById("modal").classList.add("active");
    document.getElementById("tituloModal").innerText = titulo;
    document.getElementById("mensagemModal").innerText = mensagem;
}
function FecharModal() {
    document.getElementById("modal").classList.remove("active");
}