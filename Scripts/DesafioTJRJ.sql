CREATE DATABASE DesafioTJRJ;
GO

USE DesafioTJRJ;
GO

CREATE TABLE Autor (
    CodAu INT PRIMARY KEY IDENTITY(1,1),
    Nome NVARCHAR(40) NOT NULL
);
GO

CREATE TABLE Livro (
    CodL INT PRIMARY KEY IDENTITY(1,1),
    Titulo NVARCHAR(40) NOT NULL,
    Editora NVARCHAR(40) NOT NULL,
    Edicao INT NOT NULL,
    AnoPublicacao CHAR(4) NOT NULL
);
GO

CREATE TABLE Assunto (
    CodAs INT PRIMARY KEY IDENTITY(1,1),
    Descricao NVARCHAR(20) NOT NULL
);
GO

CREATE TABLE LivroAutor (
    CodL INT NOT NULL,
    CodAu INT NOT NULL,
    PRIMARY KEY (CodL, CodAu),
    FOREIGN KEY (CodL) REFERENCES Livro(CodL) ON DELETE CASCADE,
    FOREIGN KEY (CodAu) REFERENCES Autor(CodAu) ON DELETE CASCADE
);
GO

CREATE TABLE LivroAssunto (
    CodL INT NOT NULL,
    CodAs INT NOT NULL,
    PRIMARY KEY (CodL, CodAs),
    FOREIGN KEY (CodL) REFERENCES Livro(CodL) ON DELETE CASCADE,
    FOREIGN KEY (CodAs) REFERENCES Assunto(CodAs) ON DELETE CASCADE
);
GO


CREATE TABLE FormaCompra (
    CodFormaCompra INT PRIMARY KEY IDENTITY(1,1),
    Descricao NVARCHAR(40) NOT NULL
);
GO

CREATE TABLE LivroPrecoFormaCompra (
    CodL INT NOT NULL,
    CodFormaCompra INT NOT NULL,
    Preco DECIMAL(10, 2) NOT NULL,
    PRIMARY KEY (CodL, CodFormaCompra),
    FOREIGN KEY (CodL) REFERENCES Livro(CodL) ON DELETE CASCADE,
    FOREIGN KEY (CodFormaCompra) REFERENCES FormaCompra(CodFormaCompra) ON DELETE CASCADE
);
GO

-- Carga inicial de formas de compra
INSERT INTO FormaCompra (Descricao) VALUES ('Balcão');
INSERT INTO FormaCompra (Descricao) VALUES ('Self-Service');
INSERT INTO FormaCompra (Descricao) VALUES ('Internet');
INSERT INTO FormaCompra (Descricao) VALUES ('Evento');
GO


CREATE VIEW vw_LivroAutorAssunto
AS
SELECT
    a.CodAu AS AutorId,
    a.Nome AS NomeAutor,
    l.CodL AS LivroId,
    l.Titulo AS TituloLivro,
    l.Editora,
    l.Edicao,
    l.AnoPublicacao,
    (
        SELECT 
            (
                SELECT 
                    ass.Descricao + ', '
                FROM 
                    dbo.LivroAssunto AS las
                    INNER JOIN dbo.Assunto AS ass ON las.CodAs = ass.CodAs
                WHERE 
                    las.CodL = l.CodL
                ORDER BY 
                    ass.Descricao
                FOR XML PATH(''), TYPE
            ).value('.', 'NVARCHAR(MAX)')
    ) AS DescricaoAssunto
FROM 
    dbo.Livro AS l
    INNER JOIN dbo.LivroAutor AS la ON l.CodL = la.CodL
    INNER JOIN dbo.Autor AS a ON la.CodAu = a.CodAu
GO

CREATE LOGIN desafio_app WITH PASSWORD = 'desafio_app01';
GO

CREATE USER desafio_app FOR LOGIN desafio_app;
GO

GRANT SELECT ON SCHEMA::dbo TO desafio_app;

GRANT INSERT, UPDATE, DELETE ON SCHEMA::dbo TO desafio_app;
GO