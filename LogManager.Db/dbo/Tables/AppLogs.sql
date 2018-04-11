CREATE TABLE [dbo].[AppLogs] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
    [Date]      DATETIME       NOT NULL,
    [Thread]    VARCHAR (255)  NOT NULL,
    [Level]     VARCHAR (50)   NOT NULL,
    [Logger]    VARCHAR (255)  NOT NULL,
    [Message]   NVARCHAR (MAX) NOT NULL,
    [Exception] NVARCHAR (MAX) NULL,
    [Context]   NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Log] PRIMARY KEY CLUSTERED ([Id] ASC)
);

