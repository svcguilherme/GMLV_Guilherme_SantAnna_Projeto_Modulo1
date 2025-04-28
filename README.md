Gestão de Mini Loja Virtual com Cadastro de Produtos e Categorias

Autor: Guilherme Sant'Anna do Valle Carreiro

📚 Proposta do Projeto

Aplicação MVC:Interface web para vendedores gerenciarem seus produtos, categorias e dados pessoais dentro de um Market Place.

API RESTful:Exposição dos recursos de produtos, categorias e vendedores associados ao usuário do sistema, permitindo integração com outras aplicações ou criação de front-ends alternativos.

Autenticação e Autorização:Controle de acesso implementado para vendedores. Outros usuários podem visualizar a Home com todos os produtos disponíveis.

Acesso a Dados:Implementação via Entity Framework Core com suporte a SQLite e compatibilidade futura com SQL Server.

🚀 Tecnologias Utilizadas

Linguagem: C#

Frameworks:

ASP.NET Core MVC

ASP.NET Core Web API

Entity Framework Core

Autenticação:

ASP.NET Core Identity (Customizada)

JWT (JSON Web Token) para autenticação na API

Front-end:

Razor Pages/Views

HTML5 / CSS3

Bootstrap 5

Documentação da API:

Swagger (OpenAPI)

Banco de Dados:

SQLite (default)

Compatível com SQL Server

📈 Roadmap de Melhorias Futuras

Filtro de produtos por categoria no front-end.

Filtro de produtos por vendedor no front-end.

Ordenação dos produtos por preço, quantidade em estoque e categoria.

Organização do Program.cs dos projetos (MVC e API) para melhorar a clareza e modularização.

Permitir desativação de vendedores e ocultar seus produtos.

Melhorar mensagens de validação nas APIs e MVC.

Implementar CI/CD com GitHub Actions para estudos.

🛠️ Requisitos para implementar

Compartilhamento de banco SQLite entre a API e a aplicação MVC.

Melhorar suporte de globalização para valores decimais, considerando as diferenças entre ponto e vírgula.

✅ Funcionalidades Implementadas

CRUD completo de Produtos.

CRUD completo de Categorias.

Edição de Dados de Vendedor.

Cada vendedor gerencia apenas seus próprios produtos.

Restrição de exclusão de Categorias/Vendedores com produtos associados.

Criei as roles de Vendedor e Associei aos vendedores.

Autenticação de usuários e controle de acesso baseado em perfis.

Documentação automática da API via Swagger.

🛠️ Como Executar o Projeto

Pré-requisitos

.NET SDK 8.0 ou superior

SQLite

Visual Studio 2022 ou superior (ou outra IDE compatível)

Git instalado

Passos para execução

# Clone o repositório
git clone https://github.com/svcguilherme/GMLV_Guilherme_SantAnna_Projeto_Modulo1.git

# Acesse a pasta do projeto
cd GMLV_Guilherme_SantAnna_Projeto_Modulo1

Banco de Dados

O appsettings.json já está configurado para utilizar SQLite.

Ao rodar o projeto, a base será criada e populada automaticamente via Seed.

💻 Executar a Aplicação MVC

cd GLMV.AppWeb
dotnet run

Acesse em: https://localhost:7088/

📡 Executar a API

cd GLMV.API
dotnet run

Acesse a documentação da API em: https://localhost:7277/swagger/index.html

🔑 Instruções de Configuração - JWT API

As chaves JWT estão localizadas em:

// appsettings.json
"JwtSettings": {
  "Segredo": "sua-chave-secreta-aqui",
  "Emissor": "GLMV.API",
  "Audiencia": "GLMV.Client",
  "ExpiracaoHoras": 2
}

📄 Documentação da API

Após iniciar a API, acesse:

https://localhost:7277/swagger/index.html
