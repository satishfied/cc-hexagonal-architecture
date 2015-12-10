CREATE TABLE [dbo].[ScreeningAspect]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
	[ScreeningId] INT,
    [Name] NVARCHAR(500) NULL, 
    [AspectType] TINYINT NOT NULL DEFAULT 0, 
    [Remark] NVARCHAR(MAX) NULL, 
    [Score] INT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_ScreeningAspect_Screening] FOREIGN KEY ([ScreeningId]) REFERENCES [Screening]([Id])
)
