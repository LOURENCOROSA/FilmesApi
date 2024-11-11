
FilmesApi
A FilmesApi é uma API REST para gerenciar um catálogo de filmes. Ela permite adicionar, consultar, atualizar (total ou parcialmente) e excluir filmes de um banco de dados. A API foi desenvolvida com .NET Core e utiliza o AutoMapper para facilitar o mapeamento de DTOs.

Tecnologias Usadas
.NET Core
Entity Framework Core
AutoMapper
Swashbuckle para documentação com Swagger
Banco de dados MySQL
Instalação
Clone o repositório:

bash
Copiar código
git clone https://github.com/seu_usuario/FilmesApi.git
Navegue para o diretório do projeto:

bash
Copiar código
cd FilmesApi
Configure a conexão com o banco de dados MySQL em appsettings.json:

json
Copiar código
"ConnectionStrings": {
  "FilmeConnection": "server=localhost;database=filme;user=root;password=root"
}
Execute as migrações para criar as tabelas:

bash
Copiar código
dotnet ef database update
Execute a aplicação:

bash
Copiar código
dotnet run
A API estará disponível em https://localhost:5001 por padrão.

Endpoints
Abaixo estão os endpoints disponíveis com exemplos e explicações dos parâmetros esperados.

Adicionar Filme
Endpoint: POST /filme
Descrição: Adiciona um novo filme ao banco de dados.
Parâmetros no Body (JSON):
json
Copiar código
{
  "titulo": "O Poderoso Chefão",
  "diretor": "Francis Ford Coppola",
  "anoLancamento": 1972,
  "genero": "Drama"
}
Resposta de Sucesso: 201 Created
Listar Filmes
Endpoint: GET /filme
Descrição: Retorna uma lista de filmes com paginação.
Parâmetros de Query (Opcional):
skip: Número de filmes a pular (padrão: 0).
take: Número de filmes a retornar (padrão: 50).
Resposta de Sucesso: 200 OK
Buscar Filme por ID
Endpoint: GET /filme/{id}
Descrição: Retorna os detalhes de um filme específico.
Parâmetros de Rota:
id: ID do filme.
Resposta de Sucesso: 200 OK
Atualizar Filme
Endpoint: PUT /filme/{id}
Descrição: Atualiza todos os dados de um filme.
Parâmetros de Rota:
id: ID do filme.
Parâmetros no Body (JSON):
json
Copiar código
{
  "titulo": "Novo Título",
  "diretor": "Novo Diretor",
  "anoLancamento": 2000,
  "genero": "Novo Gênero"
}
Resposta de Sucesso: 204 No Content
Atualizar Parcialmente um Filme
Endpoint: PATCH /filme/{id}
Descrição: Atualiza parcialmente os dados de um filme usando JSON Patch.
Parâmetros de Rota:
id: ID do filme.
Exemplo de Requisição (JSON Patch):
json
Copiar código
[
  { "op": "replace", "path": "/titulo", "value": "Título Atualizado" }
]
Resposta de Sucesso: 204 No Content
Excluir Filme
Endpoint: DELETE /filme/{id}
Descrição: Exclui um filme do banco de dados.
Parâmetros de Rota:
id: ID do filme.
Resposta de Sucesso: 204 No Content
Documentação da API
A API utiliza o Swagger para documentação. Após iniciar o projeto, você pode acessar a documentação interativa em:

bash
Copiar código
https://localhost:5001/swagger
