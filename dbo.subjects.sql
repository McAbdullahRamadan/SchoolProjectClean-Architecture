CREATE TABLE [dbo].[subjects] (
    [SubID]         INT            IDENTITY (1, 1) NOT NULL,
    [SubjectNameAr] NVARCHAR (500) NULL,
    [SubjectNameEn] NVARCHAR (MAX) NULL,
    [Period]        INT  NULL,
    CONSTRAINT [PK_subjects] PRIMARY KEY CLUSTERED ([SubID] ASC)
);

