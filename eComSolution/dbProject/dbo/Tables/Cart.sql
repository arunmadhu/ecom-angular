CREATE TABLE [dbo].[Cart] (
    [Id]        INT           IDENTITY (1, 1) NOT NULL,
    [UserId]    NVARCHAR (50) NOT NULL,
    [ProductId] INT           NOT NULL,
    [Quantity]  INT           NOT NULL,
    [Price]     INT           NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_Cart_ToTable] FOREIGN KEY ([ProductId]) REFERENCES [dbo].[Product] ([Id])
);



