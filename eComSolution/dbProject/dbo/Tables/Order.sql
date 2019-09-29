CREATE TABLE [dbo].[Order] (
    [Id]            INT           IDENTITY (1, 1) NOT NULL,
    [OrderNumber]   NVARCHAR (50) NOT NULL,
    [TotalItemCost] INT           NOT NULL,
    [DeliveryCost]  INT           NOT NULL,
    [Status]        NVARCHAR (50) NOT NULL,
    [UserId]        NVARCHAR (50) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);



