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

