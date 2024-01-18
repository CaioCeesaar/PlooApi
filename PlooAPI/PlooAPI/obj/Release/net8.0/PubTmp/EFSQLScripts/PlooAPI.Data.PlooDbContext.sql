IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240113231512_Initial'
)
BEGIN
    CREATE TABLE [Equipes] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [DataCriacao] datetime2 NOT NULL,
        [DataAtualizacao] datetime2 NOT NULL,
        [Ativo] bit NOT NULL,
        CONSTRAINT [PK_Equipes] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240113231512_Initial'
)
BEGIN
    CREATE TABLE [Perfis] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [DataCriacao] datetime2 NOT NULL,
        [DataAtualizacao] datetime2 NOT NULL,
        [Ativo] bit NOT NULL,
        CONSTRAINT [PK_Perfis] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240113231512_Initial'
)
BEGIN
    CREATE TABLE [Usuarios] (
        [Id] int NOT NULL IDENTITY,
        [Nome] nvarchar(max) NOT NULL,
        [Email] nvarchar(max) NOT NULL,
        [DataNascimento] datetime2 NOT NULL,
        [NumeroCasa] nvarchar(max) NOT NULL,
        [Cep] nvarchar(max) NOT NULL,
        [Logradouro] nvarchar(max) NOT NULL,
        [Complemento] nvarchar(max) NOT NULL,
        [Bairro] nvarchar(max) NOT NULL,
        [Localidade] nvarchar(max) NOT NULL,
        [Uf] nvarchar(max) NOT NULL,
        [Ibge] nvarchar(max) NOT NULL,
        [Gia] nvarchar(max) NOT NULL,
        [Ddd] nvarchar(max) NOT NULL,
        [Siafi] nvarchar(max) NOT NULL,
        [PerfilId] int NOT NULL,
        [DataCriacao] datetime2 NOT NULL,
        [DataAtualizacao] datetime2 NOT NULL,
        [Ativo] bit NOT NULL,
        CONSTRAINT [PK_Usuarios] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240113231512_Initial'
)
BEGIN
    CREATE TABLE [EquipeUsuario] (
        [EquipesId] int NOT NULL,
        [UsuariosId] int NOT NULL,
        CONSTRAINT [PK_EquipeUsuario] PRIMARY KEY ([EquipesId], [UsuariosId]),
        CONSTRAINT [FK_EquipeUsuario_Equipes_EquipesId] FOREIGN KEY ([EquipesId]) REFERENCES [Equipes] ([Id]) ON DELETE CASCADE,
        CONSTRAINT [FK_EquipeUsuario_Usuarios_UsuariosId] FOREIGN KEY ([UsuariosId]) REFERENCES [Usuarios] ([Id]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240113231512_Initial'
)
BEGIN
    CREATE INDEX [IX_EquipeUsuario_UsuariosId] ON [EquipeUsuario] ([UsuariosId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20240113231512_Initial'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20240113231512_Initial', N'8.0.1');
END;
GO

COMMIT;
GO

