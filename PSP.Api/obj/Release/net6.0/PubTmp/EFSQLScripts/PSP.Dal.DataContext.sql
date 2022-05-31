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

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [RoleClaims] (
        [Id] int NOT NULL IDENTITY,
        [RoleId] nvarchar(max) NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_RoleClaims] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [Roles] (
        [Id] nvarchar(450) NOT NULL,
        [Name] nvarchar(max) NULL,
        [NormalizedName] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        CONSTRAINT [PK_Roles] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [UserClaims] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(max) NULL,
        [ClaimType] nvarchar(max) NULL,
        [ClaimValue] nvarchar(max) NULL,
        CONSTRAINT [PK_UserClaims] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [UserLogins] (
        [UserId] nvarchar(450) NOT NULL,
        [LoginProvider] nvarchar(max) NULL,
        [ProviderKey] nvarchar(max) NULL,
        [ProviderDisplayName] nvarchar(max) NULL,
        CONSTRAINT [PK_UserLogins] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [UserProfiles] (
        [UserProfileId] uniqueidentifier NOT NULL,
        [IdentityId] nvarchar(max) NOT NULL,
        [BasicInfo_FirstName] nvarchar(max) NOT NULL,
        [BasicInfo_LastName] nvarchar(max) NOT NULL,
        [BasicInfo_UserName] nvarchar(max) NOT NULL,
        [BasicInfo_EmailAddress] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_UserProfiles] PRIMARY KEY ([UserProfileId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [UserRoles] (
        [RoleId] nvarchar(450) NOT NULL,
        [UserId] nvarchar(max) NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY ([RoleId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [Users] (
        [Id] nvarchar(450) NOT NULL,
        [UserName] nvarchar(max) NULL,
        [NormalizedUserName] nvarchar(max) NULL,
        [Email] nvarchar(max) NULL,
        [NormalizedEmail] nvarchar(max) NULL,
        [EmailConfirmed] bit NOT NULL,
        [PasswordHash] nvarchar(max) NULL,
        [SecurityStamp] nvarchar(max) NULL,
        [ConcurrencyStamp] nvarchar(max) NULL,
        [PhoneNumber] nvarchar(max) NULL,
        [PhoneNumberConfirmed] bit NOT NULL,
        [TwoFactorEnabled] bit NOT NULL,
        [LockoutEnd] datetimeoffset NULL,
        [LockoutEnabled] bit NOT NULL,
        [AccessFailedCount] int NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([Id])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [UserTokens] (
        [UserId] nvarchar(max) NULL,
        [LoginProvider] nvarchar(max) NULL,
        [Name] nvarchar(max) NULL,
        [Value] nvarchar(max) NULL
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [Projects] (
        [ProjectId] uniqueidentifier NOT NULL,
        [UserProfileId] uniqueidentifier NOT NULL,
        [ProjectContent] nvarchar(max) NOT NULL,
        CONSTRAINT [PK_Projects] PRIMARY KEY ([ProjectId]),
        CONSTRAINT [FK_Projects_UserProfiles_UserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfiles] ([UserProfileId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [ProjectComment] (
        [CommentId] uniqueidentifier NOT NULL,
        [ProjectId] uniqueidentifier NOT NULL,
        [Text] nvarchar(max) NOT NULL,
        [UserProfileId] uniqueidentifier NOT NULL,
        CONSTRAINT [PK_ProjectComment] PRIMARY KEY ([CommentId]),
        CONSTRAINT [FK_ProjectComment_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE TABLE [ProjectInteraction] (
        [InteractionId] uniqueidentifier NOT NULL,
        [ProjectId] uniqueidentifier NOT NULL,
        [InteractionType] int NOT NULL,
        CONSTRAINT [PK_ProjectInteraction] PRIMARY KEY ([InteractionId]),
        CONSTRAINT [FK_ProjectInteraction_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId]) ON DELETE CASCADE
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE INDEX [IX_ProjectComment_ProjectId] ON [ProjectComment] ([ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE INDEX [IX_ProjectInteraction_ProjectId] ON [ProjectInteraction] ([ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    CREATE INDEX [IX_Projects_UserProfileId] ON [Projects] ([UserProfileId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220512102803_Initial')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220512102803_Initial', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524124901_deneme')
BEGIN
    ALTER TABLE [ProjectInteraction] ADD [UserProfileId] uniqueidentifier NULL;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524124901_deneme')
BEGIN
    CREATE INDEX [IX_ProjectInteraction_UserProfileId] ON [ProjectInteraction] ([UserProfileId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524124901_deneme')
BEGIN
    ALTER TABLE [ProjectInteraction] ADD CONSTRAINT [FK_ProjectInteraction_UserProfiles_UserProfileId] FOREIGN KEY ([UserProfileId]) REFERENCES [UserProfiles] ([UserProfileId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524124901_deneme')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220524124901_deneme', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524133437_project_domain_changed')
BEGIN
    ALTER TABLE [Projects] ADD [Category] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524133437_project_domain_changed')
BEGIN
    ALTER TABLE [Projects] ADD [ProjectName] nvarchar(max) NOT NULL DEFAULT N'';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220524133437_project_domain_changed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220524133437_project_domain_changed', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525081956_add_date_created_modified')
BEGIN
    ALTER TABLE [UserProfiles] ADD [DateCreated] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525081956_add_date_created_modified')
BEGIN
    ALTER TABLE [UserProfiles] ADD [LastModified] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525081956_add_date_created_modified')
BEGIN
    ALTER TABLE [Projects] ADD [CreatedDate] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525081956_add_date_created_modified')
BEGIN
    ALTER TABLE [Projects] ADD [LastModified] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525081956_add_date_created_modified')
BEGIN
    ALTER TABLE [ProjectComment] ADD [DateCreated] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525081956_add_date_created_modified')
BEGIN
    ALTER TABLE [ProjectComment] ADD [LastModified] datetime2 NOT NULL DEFAULT '0001-01-01T00:00:00.0000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220525081956_add_date_created_modified')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220525081956_add_date_created_modified', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530142019_add_category')
BEGIN
    DECLARE @var0 sysname;
    SELECT @var0 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Projects]') AND [c].[name] = N'Category');
    IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Projects] DROP CONSTRAINT [' + @var0 + '];');
    ALTER TABLE [Projects] DROP COLUMN [Category];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530142019_add_category')
BEGIN
    CREATE TABLE [Categories] (
        [CategoryId] uniqueidentifier NOT NULL,
        [CategoryName] nvarchar(max) NOT NULL,
        [CategoryDescription] nvarchar(max) NOT NULL,
        [CreatedDate] datetime2 NOT NULL,
        [LastModified] datetime2 NOT NULL,
        [ProjectId] uniqueidentifier NULL,
        CONSTRAINT [PK_Categories] PRIMARY KEY ([CategoryId]),
        CONSTRAINT [FK_Categories_Projects_ProjectId] FOREIGN KEY ([ProjectId]) REFERENCES [Projects] ([ProjectId])
    );
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530142019_add_category')
BEGIN
    CREATE INDEX [IX_Categories_ProjectId] ON [Categories] ([ProjectId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530142019_add_category')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220530142019_add_category', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530193522_add_category_to_project')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220530193522_add_category_to_project', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530221525_project_domain_typo_fixed')
BEGIN
    EXEC sp_rename N'[Projects].[CreatedDate]', N'DateCreated', N'COLUMN';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530221525_project_domain_typo_fixed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220530221525_project_domain_typo_fixed', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530223512_category_debug')
BEGIN
    ALTER TABLE [Categories] DROP CONSTRAINT [FK_Categories_Projects_ProjectId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530223512_category_debug')
BEGIN
    DROP INDEX [IX_Categories_ProjectId] ON [Categories];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530223512_category_debug')
BEGIN
    DECLARE @var1 sysname;
    SELECT @var1 = [d].[name]
    FROM [sys].[default_constraints] [d]
    INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
    WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Categories]') AND [c].[name] = N'ProjectId');
    IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Categories] DROP CONSTRAINT [' + @var1 + '];');
    ALTER TABLE [Categories] DROP COLUMN [ProjectId];
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530223512_category_debug')
BEGIN
    ALTER TABLE [Projects] ADD [CategoryId] uniqueidentifier NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530223512_category_debug')
BEGIN
    CREATE INDEX [IX_Projects_CategoryId] ON [Projects] ([CategoryId]);
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530223512_category_debug')
BEGIN
    ALTER TABLE [Projects] ADD CONSTRAINT [FK_Projects_Categories_CategoryId] FOREIGN KEY ([CategoryId]) REFERENCES [Categories] ([CategoryId]) ON DELETE CASCADE;
END;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530223512_category_debug')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220530223512_category_debug', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530225902_category_fixed')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220530225902_category_fixed', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530230511_deneme123')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220530230511_deneme123', N'6.0.5');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS(SELECT * FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20220530232904_add_key_project_config')
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20220530232904_add_key_project_config', N'6.0.5');
END;
GO

COMMIT;
GO

