CREATE TABLE [dbo].[AppMessageLog] (
    [ID]                   BIGINT         IDENTITY (1, 1) NOT NULL,
    [AppMessageEntryId]    NVARCHAR (MAX) NOT NULL,
    [ApplicationName]      NVARCHAR (500) NOT NULL,
    [User]                 NVARCHAR (500) NOT NULL,
    [Machine]              NVARCHAR (MAX) NULL,
    [RequestIpAddress]     NVARCHAR (MAX) NULL,
    [RequestContentType]   NVARCHAR (MAX) NULL,
    [RequestContentBody]   NVARCHAR (MAX) NULL,
    [RequestUri]           NVARCHAR (MAX) NULL,
    [RequestMethod]        NVARCHAR (MAX) NULL,
    [RequestRouteTemplate] NVARCHAR (MAX) NULL,
    [RequestRouteData]     NVARCHAR (MAX) NULL,
    [RequestHeaders]       NVARCHAR (MAX) NULL,
    [RequestTimestamp]     DATETIME       NULL,
    [ResponseContentType]  NVARCHAR (MAX) NULL,
    [ResponseContentBody]  NVARCHAR (MAX) NULL,
    [ResponseStatusCode]   INT            NULL,
    [ResponseHeaders]      NVARCHAR (MAX) NULL,
    [ResponseTimestamp]    DATETIME       NULL,
    CONSTRAINT [PK_AppMessageLog] PRIMARY KEY NONCLUSTERED ([ID] ASC)
);

