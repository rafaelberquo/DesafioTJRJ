# Desafio TJ-RJ

## Sumário da Aplicação

### Descrição da Aplicação

- **Tecnologia**: .NET 8
- **Banco de Dados**: SQL Server local

### Camadas e Tecnologias Utilizadas

- **Camada de Apresentação**: MVC Core com Bootstrap e jQuery, Componente EPPlus para criação de relatórios
- **Camada de Persistência**: Entity Framework Core
- **Injeção de Dependência**: Container padrão do .NET
- **Testes unitários**: xUnit

### Estrutura do Banco de Dados

- **Tabelas Principais**:
  - **Autor**: Armazena informações sobre autores.
  - **Livro**: Armazena detalhes dos livros.
  - **Assunto**: Armazena assuntos relacionados aos livros.
  - **LivroAutor**: Relaciona livros aos autores.
  - **LivroAssunto**: Relaciona livros aos assuntos.
  - **FormaCompra**: Define as formas de compra dos livros.
  - **LivroPrecoFormaCompra**: Armazena preços dos livros por forma de compra.

- **View Criada**:
  - **vw_LivroAutorAssunto**: Agrega dados dos livros, autores e assuntos, agrupados por autor, mostrando a relação entre livros, autores e assuntos.

### Criação e Configuração do Banco de Dados

- **Script de Criação**:
  - O script SQL cria o banco de dados `DesafioTJRJ`.
  - Define as tabelas e suas relações.
  - Realiza uma carga inicial de dados na tabela `FormaCompra`.
  - Cria uma view `vw_LivroAutorAssunto` para agregar informações sobre livros, autores e assuntos.

- **Criação de Usuário para Utilização**:
  - O script SQL também cria um usuário `desafio_app` com a senha `desafio_app`.

- **Permissões Concedidas**:
  - **Leitura**: Permissões para realizar consultas (SELECT) em todas as tabelas e views no esquema `dbo`.
  - **Escrita**: Permissões para inserir (INSERT), atualizar (UPDATE) e excluir (DELETE) registros nas tabelas do esquema `dbo`.

### Publicação e Execução da Aplicação

- **Publicação**:
  - Compilar a aplicação em modo Release.
  - Configurar o banco de dados de conexão no arquivo `appsettings.json`.
  - Publicar a aplicação em um servidor web ou ambiente de hospedagem.

- **Execução Local**:
  - Executar a aplicação localmente para desenvolvimento e testes.
  - Verificar a conexão com o banco de dados e testar todas as funcionalidades.
