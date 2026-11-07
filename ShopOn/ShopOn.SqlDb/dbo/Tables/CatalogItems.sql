CREATE TABLE [dbo].[CatalogItems] (
    [Id]       INT          IDENTITY (1, 1) NOT NULL,
    [ExtGuid] CHAR(36) NULL, 
    [StatusId] VARCHAR (50) NULL,
    [RowVersion] ROWVERSION    NULL,   
    CONSTRAINT [PK_CatalogItems] PRIMARY KEY CLUSTERED ([Id] ASC)
);

