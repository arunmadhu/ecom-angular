CREATE TABLE [dbo].[Product] (
    [Id]           INT           IDENTITY (1, 1) NOT NULL,
    [Name]         NVARCHAR (50) NOT NULL,
    [Description ] TEXT          NOT NULL,
    [Category]     NCHAR (30)    NOT NULL,
    [UnitPrice]    INT           NOT NULL,
    [Manufacturer] NVARCHAR (50) NULL,
    [InStock]      INT           NOT NULL,
    [StarRating]   INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



