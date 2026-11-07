CREATE TABLE [dbo].[Category] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [ExtGuid] UNIQUEIDENTIFIER NULL, 
    [Name]       NVARCHAR (50) NULL,
    [Image]      VARCHAR (250) NULL,
    [RowVersion] ROWVERSION    NULL,
    CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED ([Id] ASC)
);

