function AbrirModal(titulo, mensagem) {
    document.getElementById("modal").classList.add("active");
    document.getElementById("tituloModal").innerText = titulo;
    document.getElementById("mensagemModal").innerText = mensagem;
}
function FecharModal() {
    document.getElementById("modal").classList.remove("active");
}

function AbrirModal2() {
    document.getElementById("modal2").classList.add("active");
}
function FecharModal2() {
    document.getElementById("modal2").classList.remove("active");
}

window.onload = function () {
    AlterarCorTituloDaPagina();
}

function AlterarCorTituloDaPagina() {
    let tituloDaPagina = document.querySelector("#form1 > nav > div.nav-list > ul > li:nth-child(2) > a");
    tituloDaPagina.style.color = "#FFD369";
    tituloDaPagina.style.borderBottom = "3px solid #FFD369";
}
