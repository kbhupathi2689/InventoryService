CREATE TABLE [dbo].[Domain] (
    [ID]              BIGINT           IDENTITY (1, 1) NOT NULL,
    [Name]            NVARCHAR (MAX)   NOT NULL,
    [LastUpdatedDate] DATETIME         NOT NULL,
    [LastUpdatedUser] NVARCHAR (MAX)   NOT NULL,
    [RowGuid]         UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_Domain] PRIMARY KEY CLUSTERED ([ID] ASC)
);

