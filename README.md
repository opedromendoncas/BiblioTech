## BiblioTech Sistema de Gestão de Biblioteca - TCC

![Demonstração do Projeto](https://media.giphy.com/media/v1.Y2lkPTc5MGI3NjExN3ZoMGsxMG1weGQ5bzE0eTN0dXQxanVsZDF3NTYwNTJsODkxZ3RmciZlcD12MV9pbnRlcm5hbF9naWZfYnlfaWQmY3Q9Zw/oxULF33e2surHF84rF/giphy.gif)

Este projeto foi desenvolvido como Trabalho de Conclusão de Curso (TCC) na Instituição de Ensino ETEC Irmã Agostina, pelo curso técnico em Desenvolvimento de Sistemas, tendo como objetivo otimizar de forma eficiente as operações diárias da biblioteca da ETEC, tais como o cadastro, a organização e a consulta do acervo e dos empréstimos de forma intuitiva.

## Funcionalidades:

* Cadastro e edição de livros.
* Busca avançada por gênero e palavra-chave.
* Geração de relatórios personalizados.
* Empréstimo e devolução de livros.

## Tecnologias Utilizadas:

* ASP.NET Core
* Bootstrap
* C#
* CSS
* IIS (Internet Information Services)
* jQuery
* SQL Server

## Instalação

Para configurar e rodar o projeto localmente, siga os passos abaixo:

### Pré-requisitos

1. **Visual Studio**: Certifique-se de ter o Visual Studio instalado com os seguintes componentes:
    - ASP.NET e desenvolvimento web
    - Desenvolvimento para a plataforma .NET

2. **SQL Server**: Tenha o SQL Server instalado e rodando em sua máquina.

3. **.NET Core SDK**: Instale o .NET Core SDK a partir do [site oficial](https://dotnet.microsoft.com/download).

4. **IIS (Internet Information Services)**: Ative o IIS no Windows:
    - Abra o "Painel de Controle".
    - Vá para "Programas" > "Ativar ou desativar recursos do Windows".
    - Marque a opção "Serviços de Informações da Internet" e clique em "OK".
    - Aguarde a instalação e reinicie o computador se necessário.

### Passos para a Instalação

#### Opção 1: Baixar pelo botão de download do GitHub

1. Acesse o repositório do projeto no GitHub.
2. Clique no botão "Code" e depois em "Download ZIP".
3. Descompacte o arquivo em qualquer local de sua preferência.

#### Opção 2: Clonar o repositório usando Git

1. Abra o terminal ou prompt de comando.
2. Execute o seguinte comando para clonar o repositório:
    ```sh
    git clone https://github.com/opedromendoncas/BiblioTech.git
    cd BiblioTech
    ```

### Configuração do Banco de Dados

1. Copie os arquivos da pasta `banco` para `C:\Users\SeuUsuario\Documents`.
2. Certifique-se de que os arquivos `bancoBiblio.sql.mdf` e `banco.mdf` estão na pasta correta.

### Atualize a String de Conexão

1. Abra o arquivo `web.config` na pasta do projeto.
2. Atualize as strings de conexão para apontar para a localização dos arquivos do banco de dados:
    ```xml
    <connectionStrings>
        <add name="DefaultConnection" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=&quot;C:\Users\SeuUsuario\Documents\bancoBiblio.sql.mdf&quot;;Integrated Security=True;Connect Timeout=30" providerName="System.Data.SqlClient" />
        <add name="AnotherConnection" connectionString="Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=&quot;C:\Users\SeuUsuario\Documents\banco.mdf&quot;;Integrated Security=True;Connect Timeout=30" providerName="System.Data.SqlClient" />
    </connectionStrings>
    ```
    - Substitua `SeuUsuario` pelo nome do seu usuário no Windows.

### Compilar e Rodar o Projeto

1. Abra o Visual Studio.
2. Navegue até o local onde descompactou ou clonou o projeto e abra o arquivo de solução `Bibliotcc.sln`.
3. Defina `bibliotcc` como o projeto de inicialização.
4. Pressione `F5` para compilar e executar o projeto.

Agora seu ambiente de desenvolvimento deve estar configurado e você deve ser capaz de acessar o projeto localmente.

## Colaboradores:

* André de Sousa Neves
* Beatriz Santos Lima
* Joyce Rufino Pereira
* Juliana Sabino Lourenço Silva
* Luiz Henrique Lemos Oliveira
* Pedro Augusto Mendonça da Silva
* Raquell Dezotti Tristão de Oliveira
