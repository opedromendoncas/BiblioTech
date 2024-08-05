window.onload = function () {
    AlterarCorTituloDaPagina();
  
}

function AlterarCorTituloDaPagina() {
    let tituloDaPagina = document.querySelector("#form1 > nav > div.nav-list > ul > li:nth-child(5)> a");
    tituloDaPagina.style.color = "#FFD369";
    tituloDaPagina.style.borderBottom = "3px solid #FFD369";
}

function RedirecionarParaTime(IdLivro) {
    window.location.replace("Livro.aspx?livro=" + IdLivro)
}