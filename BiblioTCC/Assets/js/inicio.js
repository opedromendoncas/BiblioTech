var myDiv = document.getElementById("cadastro");

cadastro.addEventListener("click", function () {
    window.location.href = "CadastroLivros.aspx";
});

var emprestimo = document.getElementById("emprestimo");

emprestimo.addEventListener("click", function () {
    window.location.href = "CadastroEmprestimo.aspx";
});

window.onload = function () {
    AlterarCorTituloDaPagina();
}

function AlterarCorTituloDaPagina() {
    let tituloDaPagina = document.querySelector("#form1 > nav > div.nav-list > ul > li:nth-child(1) > a");
    tituloDaPagina.style.color = "#FFD369";
    tituloDaPagina.style.borderBottom = "3px solid #FFD369";
}
