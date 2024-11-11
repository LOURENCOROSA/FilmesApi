FilmesApi
A FilmesApi é uma API REST para gerenciar um catálogo de filmes. Ela permite adicionar, consultar, atualizar (total ou parcialmente) e excluir filmes de um banco de dados. A API foi desenvolvida com .NET Core e utiliza o AutoMapper para facilitar o mapeamento de DTOs.

Tecnologias Usadas
<ul> <li>.NET Core</li> <li>Entity Framework Core</li> <li>AutoMapper</li> <li>Swashbuckle para documentação com Swagger</li> <li>Banco de dados MySQL</li> </ul>
Instalação
<ol> <li>Clone o repositório: <pre><code>git clone https://github.com/seu_usuario/FilmesApi.git</code></pre> </li> <li>Navegue para o diretório do projeto: <pre><code>cd FilmesApi</code></pre> </li> <li>Configure a conexão com o banco de dados MySQL em <code>appsettings.json</code>: <pre><code> "ConnectionStrings": { "FilmeConnection": "server=localhost;database=filme;user=root;password=root" } </code></pre> </li> <li>Execute as migrações para criar as tabelas: <pre><code>dotnet ef database update</code></pre> </li> <li>Execute a aplicação: <pre><code>dotnet run</code></pre> </li> </ol> <p>A API estará disponível em <code>https://localhost:5001</code> por padrão.</p>
Endpoints
Abaixo estão os endpoints disponíveis com exemplos e explicações dos parâmetros esperados.

Adicionar Filme
<ul> <li><strong>Endpoint:</strong> <code>POST /filme</code></li> <li><strong>Descrição:</strong> Adiciona um novo filme ao banco de dados.</li> <li><strong>Parâmetros no Body (JSON):</strong> <pre><code> { "titulo": "O Poderoso Chefão", "diretor": "Francis Ford Coppola", "anoLancamento": 1972, "genero": "Drama" } </code></pre> </li> <li><strong>Resposta de Sucesso:</strong> <code>201 Created</code></li> </ul>
Listar Filmes
<ul> <li><strong>Endpoint:</strong> <code>GET /filme</code></li> <li><strong>Descrição:</strong> Retorna uma lista de filmes com paginação.</li> <li><strong>Parâmetros de Query (Opcional):</strong></li> <ul> <li><code>skip</code>: Número de filmes a pular (padrão: 0).</li> <li><code>take</code>: Número de filmes a retornar (padrão: 50).</li> </ul> <li><strong>Resposta de Sucesso:</strong> <code>200 OK</code></li> </ul>
Buscar Filme por ID
<ul> <li><strong>Endpoint:</strong> <code>GET /filme/{id}</code></li> <li><strong>Descrição:</strong> Retorna os detalhes de um filme específico.</li> <li><strong>Parâmetros de Rota:</strong></li> <ul> <li><code>id</code>: ID do filme.</li> </ul> <li><strong>Resposta de Sucesso:</strong> <code>200 OK</code></li> </ul>
Atualizar Filme
<ul> <li><strong>Endpoint:</strong> <code>PUT /filme/{id}</code></li> <li><strong>Descrição:</strong> Atualiza todos os dados de um filme.</li> <li><strong>Parâmetros de Rota:</strong></li> <ul> <li><code>id</code>: ID do filme.</li> </ul> <li><strong>Parâmetros no Body (JSON):</strong> <pre><code> { "titulo": "Novo Título", "diretor": "Novo Diretor", "anoLancamento": 2000, "genero": "Novo Gênero" } </code></pre> </li> <li><strong>Resposta de Sucesso:</strong> <code>204 No Content</code></li> </ul>
Atualizar Parcialmente um Filme
<ul> <li><strong>Endpoint:</strong> <code>PATCH /filme/{id}</code></li> <li><strong>Descrição:</strong> Atualiza parcialmente os dados de um filme usando JSON Patch.</li> <li><strong>Parâmetros de Rota:</strong></li> <ul> <li><code>id</code>: ID do filme.</li> </ul> <li><strong>Exemplo de Requisição (JSON Patch):</strong> <pre><code> [ { "op": "replace", "path": "/titulo", "value": "Título Atualizado" } ] </code></pre> </li> <li><strong>Resposta de Sucesso:</strong> <code>204 No Content</code></li> </ul>
Excluir Filme
<ul> <li><strong>Endpoint:</strong> <code>DELETE /filme/{id}</code></li> <li><strong>Descrição:</strong> Exclui um filme do banco de dados.</li> <li><strong>Parâmetros de Rota:</strong></li> <ul> <li><code>id</code>: ID do filme.</li> </ul> <li><strong>Resposta de Sucesso:</strong> <code>204 No Content</code></li> </ul>
Documentação da API
<p>A API utiliza o Swagger para documentação. Após iniciar o projeto, você pode acessar a documentação interativa em:</p> <pre><code>https://localhost:5001/swagger</code></pre>
