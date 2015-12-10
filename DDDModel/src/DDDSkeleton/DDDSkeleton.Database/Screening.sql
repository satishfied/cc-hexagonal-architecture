CREATE TABLE [dbo].[Screening]
(
	[Id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [Candidate] NVARCHAR(500) NOT NULL, 
    [Recruiter] NVARCHAR(500) NOT NULL, 
    [Date] SMALLDATETIME NULL, 
    [Location] NVARCHAR(500) NULL, 
    [Remark] NVARCHAR(MAX) NULL, 
    [GlobalEvaluation] NVARCHAR(MAX) NULL
)
