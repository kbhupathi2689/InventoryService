﻿CREATE TABLE [dbo].[Log] (
    [Id]              INT                IDENTITY (1, 1) NOT NULL,
    [Message]         NVARCHAR (MAX)     NULL,
    [MessageTemplate] NVARCHAR (MAX)     NULL,
    [Level]           NVARCHAR (128)     NULL,
    [TimeStamp]       DATETIMEOFFSET (7) NOT NULL,
    [Exception]       NVARCHAR (MAX)     NULL,
    [Properties]      XML                NULL,
    [LogEvent]        NVARCHAR (MAX)     NULL,
    CONSTRAINT [PK_SeriLog] PRIMARY KEY CLUSTERED ([Id] ASC)
);

