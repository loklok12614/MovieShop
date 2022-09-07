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

CREATE TABLE [Movies] (
    [Id] int NOT NULL IDENTITY,
    [Title] nvarchar(max) NOT NULL,
    [Overview] nvarchar(max) NOT NULL,
    [Budget] decimal(18,2) NULL,
    [Revenue] decimal(18,2) NULL,
    [ImdbUrl] nvarchar(max) NOT NULL,
    [TmdbUrl] nvarchar(max) NOT NULL,
    [PosterUrl] nvarchar(max) NOT NULL,
    [BackdropUrl] nvarchar(max) NOT NULL,
    [OriginalLanguage] nvarchar(max) NOT NULL,
    [ReleaseDate] datetime2 NULL,
    [RunTime] int NULL,
    [Price] decimal(18,2) NULL,
    [CreatedDate] datetime2 NULL,
    [UpdatedDate] datetime2 NULL,
    [UpdatedBy] nvarchar(max) NULL,
    [CreatedBy] nvarchar(max) NULL,
    [Rating] decimal(18,2) NULL,
    CONSTRAINT [PK_Movies] PRIMARY KEY ([Id])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824195850_InitialMigration', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Title');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Title] nvarchar(256) NOT NULL;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'OriginalLanguage');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Movies] ALTER COLUMN [OriginalLanguage] nvarchar(64) NOT NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824203137_ChangingMovieTable', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Rating');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Movies] DROP COLUMN [Rating];
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'UpdatedBy');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Movies] ALTER COLUMN [UpdatedBy] nvarchar(512) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'TmdbUrl');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var4 + '];');
ALTER TABLE [Movies] ALTER COLUMN [TmdbUrl] nvarchar(2084) NOT NULL;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Title');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Title] nvarchar(512) NOT NULL;
GO

DECLARE @var6 sysname;
SELECT @var6 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Revenue');
IF @var6 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var6 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Revenue] decimal(18,4) NULL;
ALTER TABLE [Movies] ADD DEFAULT 9.9 FOR [Revenue];
GO

DECLARE @var7 sysname;
SELECT @var7 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Price');
IF @var7 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var7 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Price] decimal(5,2) NULL;
ALTER TABLE [Movies] ADD DEFAULT 9.9 FOR [Price];
GO

DECLARE @var8 sysname;
SELECT @var8 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'PosterUrl');
IF @var8 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var8 + '];');
ALTER TABLE [Movies] ALTER COLUMN [PosterUrl] nvarchar(2084) NOT NULL;
GO

DECLARE @var9 sysname;
SELECT @var9 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'ImdbUrl');
IF @var9 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var9 + '];');
ALTER TABLE [Movies] ALTER COLUMN [ImdbUrl] nvarchar(2084) NOT NULL;
GO

DECLARE @var10 sysname;
SELECT @var10 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'CreatedDate');
IF @var10 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var10 + '];');
ALTER TABLE [Movies] ADD DEFAULT (getdate()) FOR [CreatedDate];
GO

DECLARE @var11 sysname;
SELECT @var11 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'CreatedBy');
IF @var11 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var11 + '];');
ALTER TABLE [Movies] ALTER COLUMN [CreatedBy] nvarchar(512) NULL;
GO

DECLARE @var12 sysname;
SELECT @var12 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'Budget');
IF @var12 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var12 + '];');
ALTER TABLE [Movies] ALTER COLUMN [Budget] decimal(18,4) NULL;
ALTER TABLE [Movies] ADD DEFAULT 9.9 FOR [Budget];
GO

DECLARE @var13 sysname;
SELECT @var13 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Movies]') AND [c].[name] = N'BackdropUrl');
IF @var13 IS NOT NULL EXEC(N'ALTER TABLE [Movies] DROP CONSTRAINT [' + @var13 + '];');
ALTER TABLE [Movies] ALTER COLUMN [BackdropUrl] nvarchar(2084) NOT NULL;
GO

CREATE TABLE [Genres] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(24) NOT NULL,
    CONSTRAINT [PK_Genres] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Movies_Budget] ON [Movies] ([Budget]);
GO

CREATE INDEX [IX_Movies_Price] ON [Movies] ([Price]);
GO

CREATE INDEX [IX_Movies_Revenue] ON [Movies] ([Revenue]);
GO

CREATE INDEX [IX_Movies_Title] ON [Movies] ([Title]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824204945_UpdateMovieTable-CreatingGenreTable', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Movies] ADD [Tagline] nvarchar(512) NOT NULL DEFAULT N'';
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824205153_AddColumnTaglineToMovieTable', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [MovieGenres] (
    [MovieId] int NOT NULL,
    [GenreId] int NOT NULL,
    CONSTRAINT [PK_MovieGenres] PRIMARY KEY ([MovieId], [GenreId]),
    CONSTRAINT [FK_MovieGenres_Genres_GenreId] FOREIGN KEY ([GenreId]) REFERENCES [Genres] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieGenres_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieGenres_GenreId] ON [MovieGenres] ([GenreId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824205946_CreatingMovieGenre', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Trailers] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(2084) NOT NULL,
    [TrailerUrl] nvarchar(2084) NOT NULL,
    [MovieId] int NOT NULL,
    CONSTRAINT [PK_Trailers] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Trailers_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Trailers_MovieId] ON [Trailers] ([MovieId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824210354_CreatingTrailerTable', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Casts] (
    [Id] int NOT NULL IDENTITY,
    [Gender] nvarchar(16) NOT NULL,
    [Name] nvarchar(128) NOT NULL,
    [ProfilePath] nvarchar(2084) NOT NULL,
    [TmdbUrl] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Casts] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [MovieCasts] (
    [CastId] int NOT NULL,
    [MovieId] int NOT NULL,
    [Character] nvarchar(450) NOT NULL,
    CONSTRAINT [PK_MovieCasts] PRIMARY KEY ([CastId], [MovieId], [Character]),
    CONSTRAINT [FK_MovieCasts_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_MovieCasts_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_MovieCasts_MovieId] ON [MovieCasts] ([MovieId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824224227_CreatingMovieCastAndCastTables', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Users] (
    [Id] int NOT NULL IDENTITY,
    [DateOfBirth] datetime2 NULL,
    [Email] nvarchar(256) NOT NULL,
    [FirstName] nvarchar(128) NOT NULL,
    [LastName] nvarchar(128) NOT NULL,
    [PhoneNumber] nvarchar(16) NOT NULL,
    [ProfilePictureUrl] nvarchar(max) NOT NULL,
    [HashedPassword] nvarchar(1024) NOT NULL,
    [IsLocked] bit NOT NULL,
    [Salt] nvarchar(1024) NOT NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
);
GO

CREATE INDEX [IX_Users_FirstName] ON [Users] ([FirstName]);
GO

CREATE INDEX [IX_Users_LastName] ON [Users] ([LastName]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824232630_CreatingUserTable', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Favorites] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    CONSTRAINT [PK_Favorites] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [FK_Favorites_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Favorites_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Favorites_UserId] ON [Favorites] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824233728_CreatingFavoritesTable', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var14 sysname;
SELECT @var14 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'IsLocked');
IF @var14 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var14 + '];');
ALTER TABLE [Users] ADD DEFAULT CAST(0 AS bit) FOR [IsLocked];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824234812_UpdateUserTableIsLocked', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Reviews] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    [CreatedDate] datetime2 NULL DEFAULT (getdate()),
    [Rating] decimal(3,2) NULL DEFAULT 0.0,
    [ReviewText] nvarchar(max) NOT NULL,
    CONSTRAINT [PK_Reviews] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [FK_Reviews_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Reviews_UserId] ON [Reviews] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220824235919_CreatingReviewTable', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Purchases] (
    [MovieId] int NOT NULL,
    [UserId] int NOT NULL,
    [PurchaseDateTime] datetime2 NULL DEFAULT (getdate()),
    [PurchaseNumber] nvarchar(max) NOT NULL,
    [TotalPrice] decimal(5,2) NULL DEFAULT 0.0,
    CONSTRAINT [PK_Purchases] PRIMARY KEY ([MovieId], [UserId]),
    CONSTRAINT [FK_Purchases_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_Purchases_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_Purchases_UserId] ON [Purchases] ([UserId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825033432_CreatingPurchaseTable', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Roles] (
    [Id] int NOT NULL IDENTITY,
    [Name] nvarchar(20) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
);
GO

CREATE TABLE [UserRoles] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY ([UserId], [RoleId]),
    CONSTRAINT [FK_UserRoles_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([Id]) ON DELETE CASCADE,
    CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_UserRoles_RoleId] ON [UserRoles] ([RoleId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220825040939_CreatingUserAndUserRolesTables', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [MovieCasts] DROP CONSTRAINT [FK_MovieCasts_Casts_CastId];
GO

ALTER TABLE [Casts] DROP CONSTRAINT [PK_Casts];
GO

EXEC sp_rename N'[Casts]', N'Cast';
GO

ALTER TABLE [Cast] ADD CONSTRAINT [PK_Cast] PRIMARY KEY ([Id]);
GO

ALTER TABLE [MovieCasts] ADD CONSTRAINT [FK_MovieCasts_Cast_CastId] FOREIGN KEY ([CastId]) REFERENCES [Cast] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220827033443_ChangeCastTableName', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [MovieCasts] DROP CONSTRAINT [FK_MovieCasts_Cast_CastId];
GO

ALTER TABLE [Cast] DROP CONSTRAINT [PK_Cast];
GO

EXEC sp_rename N'[Cast]', N'Casts';
GO

ALTER TABLE [Casts] ADD CONSTRAINT [PK_Casts] PRIMARY KEY ([Id]);
GO

ALTER TABLE [MovieCasts] ADD CONSTRAINT [FK_MovieCasts_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220827035454_ChangeCastTableNameBack', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Favorites] DROP CONSTRAINT [FK_Favorites_Users_UserId];
GO

ALTER TABLE [Purchases] DROP CONSTRAINT [FK_Purchases_Users_UserId];
GO

ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_Users_UserId];
GO

ALTER TABLE [Trailers] DROP CONSTRAINT [FK_Trailers_Movies_MovieId];
GO

ALTER TABLE [UserRoles] DROP CONSTRAINT [FK_UserRoles_Users_UserId];
GO

ALTER TABLE [Users] DROP CONSTRAINT [PK_Users];
GO

ALTER TABLE [Trailers] DROP CONSTRAINT [PK_Trailers];
GO

EXEC sp_rename N'[Users]', N'User';
GO

EXEC sp_rename N'[Trailers]', N'Trailer';
GO

EXEC sp_rename N'[User].[IX_Users_LastName]', N'IX_User_LastName', N'INDEX';
GO

EXEC sp_rename N'[User].[IX_Users_FirstName]', N'IX_User_FirstName', N'INDEX';
GO

EXEC sp_rename N'[Trailer].[IX_Trailers_MovieId]', N'IX_Trailer_MovieId', N'INDEX';
GO

ALTER TABLE [User] ADD CONSTRAINT [PK_User] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Trailer] ADD CONSTRAINT [PK_Trailer] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Favorites] ADD CONSTRAINT [FK_Favorites_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Purchases] ADD CONSTRAINT [FK_Purchases_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Reviews] ADD CONSTRAINT [FK_Reviews_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Trailer] ADD CONSTRAINT [FK_Trailer_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [UserRoles] ADD CONSTRAINT [FK_UserRoles_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220827035742_ChangeTableNames', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [MovieCasts] DROP CONSTRAINT [FK_MovieCasts_Casts_CastId];
GO

ALTER TABLE [MovieCasts] DROP CONSTRAINT [FK_MovieCasts_Movies_MovieId];
GO

ALTER TABLE [Purchases] DROP CONSTRAINT [FK_Purchases_Movies_MovieId];
GO

ALTER TABLE [Purchases] DROP CONSTRAINT [FK_Purchases_User_UserId];
GO

ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_Movies_MovieId];
GO

ALTER TABLE [Reviews] DROP CONSTRAINT [FK_Reviews_User_UserId];
GO

ALTER TABLE [Reviews] DROP CONSTRAINT [PK_Reviews];
GO

ALTER TABLE [Purchases] DROP CONSTRAINT [PK_Purchases];
GO

ALTER TABLE [MovieCasts] DROP CONSTRAINT [PK_MovieCasts];
GO

EXEC sp_rename N'[Reviews]', N'Review';
GO

EXEC sp_rename N'[Purchases]', N'Purchase';
GO

EXEC sp_rename N'[MovieCasts]', N'MovieCast';
GO

EXEC sp_rename N'[Review].[IX_Reviews_UserId]', N'IX_Review_UserId', N'INDEX';
GO

EXEC sp_rename N'[Purchase].[IX_Purchases_UserId]', N'IX_Purchase_UserId', N'INDEX';
GO

EXEC sp_rename N'[MovieCast].[IX_MovieCasts_MovieId]', N'IX_MovieCast_MovieId', N'INDEX';
GO

ALTER TABLE [Review] ADD CONSTRAINT [PK_Review] PRIMARY KEY ([MovieId], [UserId]);
GO

ALTER TABLE [Purchase] ADD CONSTRAINT [PK_Purchase] PRIMARY KEY ([MovieId], [UserId]);
GO

ALTER TABLE [MovieCast] ADD CONSTRAINT [PK_MovieCast] PRIMARY KEY ([CastId], [MovieId], [Character]);
GO

ALTER TABLE [MovieCast] ADD CONSTRAINT [FK_MovieCast_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [MovieCast] ADD CONSTRAINT [FK_MovieCast_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Purchase] ADD CONSTRAINT [FK_Purchase_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Purchase] ADD CONSTRAINT [FK_Purchase_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Review] ADD CONSTRAINT [FK_Review_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Review] ADD CONSTRAINT [FK_Review_User_UserId] FOREIGN KEY ([UserId]) REFERENCES [User] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220827040040_ChangeTableNames02', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Favorites] DROP CONSTRAINT [FK_Favorites_User_UserId];
GO

ALTER TABLE [MovieCast] DROP CONSTRAINT [FK_MovieCast_Casts_CastId];
GO

ALTER TABLE [MovieCast] DROP CONSTRAINT [FK_MovieCast_Movies_MovieId];
GO

ALTER TABLE [Purchase] DROP CONSTRAINT [FK_Purchase_Movies_MovieId];
GO

ALTER TABLE [Purchase] DROP CONSTRAINT [FK_Purchase_User_UserId];
GO

ALTER TABLE [Review] DROP CONSTRAINT [FK_Review_Movies_MovieId];
GO

ALTER TABLE [Review] DROP CONSTRAINT [FK_Review_User_UserId];
GO

ALTER TABLE [Trailer] DROP CONSTRAINT [FK_Trailer_Movies_MovieId];
GO

ALTER TABLE [UserRoles] DROP CONSTRAINT [FK_UserRoles_User_UserId];
GO

ALTER TABLE [User] DROP CONSTRAINT [PK_User];
GO

ALTER TABLE [Trailer] DROP CONSTRAINT [PK_Trailer];
GO

ALTER TABLE [Review] DROP CONSTRAINT [PK_Review];
GO

ALTER TABLE [Purchase] DROP CONSTRAINT [PK_Purchase];
GO

ALTER TABLE [MovieCast] DROP CONSTRAINT [PK_MovieCast];
GO

EXEC sp_rename N'[User]', N'Users';
GO

EXEC sp_rename N'[Trailer]', N'Trailers';
GO

EXEC sp_rename N'[Review]', N'Reviews';
GO

EXEC sp_rename N'[Purchase]', N'Purchases';
GO

EXEC sp_rename N'[MovieCast]', N'MovieCasts';
GO

EXEC sp_rename N'[Users].[IX_User_LastName]', N'IX_Users_LastName', N'INDEX';
GO

EXEC sp_rename N'[Users].[IX_User_FirstName]', N'IX_Users_FirstName', N'INDEX';
GO

EXEC sp_rename N'[Trailers].[IX_Trailer_MovieId]', N'IX_Trailers_MovieId', N'INDEX';
GO

EXEC sp_rename N'[Reviews].[IX_Review_UserId]', N'IX_Reviews_UserId', N'INDEX';
GO

EXEC sp_rename N'[Purchases].[IX_Purchase_UserId]', N'IX_Purchases_UserId', N'INDEX';
GO

EXEC sp_rename N'[MovieCasts].[IX_MovieCast_MovieId]', N'IX_MovieCasts_MovieId', N'INDEX';
GO

ALTER TABLE [Users] ADD CONSTRAINT [PK_Users] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Trailers] ADD CONSTRAINT [PK_Trailers] PRIMARY KEY ([Id]);
GO

ALTER TABLE [Reviews] ADD CONSTRAINT [PK_Reviews] PRIMARY KEY ([MovieId], [UserId]);
GO

ALTER TABLE [Purchases] ADD CONSTRAINT [PK_Purchases] PRIMARY KEY ([MovieId], [UserId]);
GO

ALTER TABLE [MovieCasts] ADD CONSTRAINT [PK_MovieCasts] PRIMARY KEY ([CastId], [MovieId], [Character]);
GO

ALTER TABLE [Favorites] ADD CONSTRAINT [FK_Favorites_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [MovieCasts] ADD CONSTRAINT [FK_MovieCasts_Casts_CastId] FOREIGN KEY ([CastId]) REFERENCES [Casts] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [MovieCasts] ADD CONSTRAINT [FK_MovieCasts_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Purchases] ADD CONSTRAINT [FK_Purchases_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Purchases] ADD CONSTRAINT [FK_Purchases_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Reviews] ADD CONSTRAINT [FK_Reviews_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Reviews] ADD CONSTRAINT [FK_Reviews_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [Trailers] ADD CONSTRAINT [FK_Trailers_Movies_MovieId] FOREIGN KEY ([MovieId]) REFERENCES [Movies] ([Id]) ON DELETE CASCADE;
GO

ALTER TABLE [UserRoles] ADD CONSTRAINT [FK_UserRoles_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([Id]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220827041916_ChangeTableNames02Back', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DROP INDEX [IX_Users_FirstName] ON [Users];
GO

DROP INDEX [IX_Users_LastName] ON [Users];
GO

DECLARE @var15 sysname;
SELECT @var15 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'ProfilePictureUrl');
IF @var15 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var15 + '];');
ALTER TABLE [Users] ALTER COLUMN [ProfilePictureUrl] nvarchar(max) NULL;
GO

DECLARE @var16 sysname;
SELECT @var16 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'PhoneNumber');
IF @var16 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var16 + '];');
ALTER TABLE [Users] ALTER COLUMN [PhoneNumber] nvarchar(16) NULL;
GO

DECLARE @var17 sysname;
SELECT @var17 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'IsLocked');
IF @var17 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var17 + '];');
ALTER TABLE [Users] ALTER COLUMN [IsLocked] bit NULL;
ALTER TABLE [Users] ADD DEFAULT CAST(0 AS bit) FOR [IsLocked];
GO

DECLARE @var18 sysname;
SELECT @var18 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Roles]') AND [c].[name] = N'Name');
IF @var18 IS NOT NULL EXEC(N'ALTER TABLE [Roles] DROP CONSTRAINT [' + @var18 + '];');
ALTER TABLE [Roles] ALTER COLUMN [Name] nvarchar(64) NOT NULL;
GO

CREATE UNIQUE INDEX [IX_Users_Email] ON [Users] ([Email]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220829174120_ModifyUserAndRoleTables', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var19 sysname;
SELECT @var19 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'Rating');
IF @var19 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var19 + '];');
ALTER TABLE [Reviews] ALTER COLUMN [Rating] decimal(3,2) NOT NULL;
ALTER TABLE [Reviews] ADD DEFAULT 0.0 FOR [Rating];
GO

DECLARE @var20 sysname;
SELECT @var20 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Reviews]') AND [c].[name] = N'CreatedDate');
IF @var20 IS NOT NULL EXEC(N'ALTER TABLE [Reviews] DROP CONSTRAINT [' + @var20 + '];');
ALTER TABLE [Reviews] ALTER COLUMN [CreatedDate] datetime2 NOT NULL;
ALTER TABLE [Reviews] ADD DEFAULT (getdate()) FOR [CreatedDate];
GO

DECLARE @var21 sysname;
SELECT @var21 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Purchases]') AND [c].[name] = N'PurchaseNumber');
IF @var21 IS NOT NULL EXEC(N'ALTER TABLE [Purchases] DROP CONSTRAINT [' + @var21 + '];');
ALTER TABLE [Purchases] ALTER COLUMN [PurchaseNumber] nvarchar(max) NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220906163200_ModifyPurchaseTable', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220906164856_ModifyPurchaseTable1', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220906172036_ModifyPurchaseTable2', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220906205259_ModifyPurchaseTable3', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var22 sysname;
SELECT @var22 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Purchases]') AND [c].[name] = N'PurchaseNumber');
IF @var22 IS NOT NULL EXEC(N'ALTER TABLE [Purchases] DROP CONSTRAINT [' + @var22 + '];');
ALTER TABLE [Purchases] ALTER COLUMN [PurchaseNumber] uniqueidentifier NOT NULL;
ALTER TABLE [Purchases] ADD DEFAULT '00000000-0000-0000-0000-000000000000' FOR [PurchaseNumber];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20220906205711_ModifyPurchaseTable4', N'7.0.0-preview.7.22376.2');
GO

COMMIT;
GO

