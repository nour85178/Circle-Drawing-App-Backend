CREATE TABLE [dbo].[Circles] (
    [Id]               INT            IDENTITY (1, 1) NOT NULL,
    [TimeOfSubmission] DATETIME2 (7)  NULL,
    [X]                INT            NULL,
    [Y]                INT            NULL,
    [Diameter]         FLOAT (53)     NULL,
    [Color]            NVARCHAR (MAX) NULL,
    [SetId]            NVARCHAR (MAX) NULL,
    CONSTRAINT [PK_Circles] PRIMARY KEY CLUSTERED ([Id] ASC)
);

