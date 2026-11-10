CREATE TABLE [dbo].[Product] (
    [Id]         INT             IDENTITY (1, 1) NOT NULL,
    [ExtGuid]    CHAR(36) NULL, 
    [Name]       NVARCHAR (50)   NULL,
    [Description] NVARCHAR (250)  NULL,
    [Image]      VARCHAR (250)   NULL,
    [Category]   VARCHAR (50)    NULL,
    [Price]      DECIMAL (18, 2) NULL,
    [Amount]     INT             NULL,
    [RowVersion] ROWVERSION      NULL,
    CONSTRAINT [PK_Product] PRIMARY KEY CLUSTERED ([Id] ASC)
);

