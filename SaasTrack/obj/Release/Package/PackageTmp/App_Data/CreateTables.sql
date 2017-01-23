USE "dbname";

CREATE TABLE [dbo].[Account] (
    [Id] [int] NOT NULL IDENTITY,
    [_id]                     NVARCHAR (MAX) NULL,
    [_item]                   NVARCHAR (MAX) NULL,
    [balance_available]       NVARCHAR (MAX) NULL,
    [balance_current]         NVARCHAR (MAX) NULL,
    [institution_type]        NVARCHAR (MAX) NULL,
    [meta_name]               NVARCHAR (MAX) NULL,
    [meta_number]             NVARCHAR (MAX) NULL,
    [subtype]                 NVARCHAR (MAX) NULL,
    [type]                    NVARCHAR (MAX) NULL,
    [enabled]                 BIT            NOT NULL,
    [Card_Id]                 INT            NULL,
    [lastdateprocessed]       DATETIME       NULL,
    [lastdateadded]           DATETIME       NULL,
    [Monthly]                 FLOAT (53)     NULL,
    [MonthlyIncreaseDecrease] FLOAT (53)     NULL,
    [Annual]                  FLOAT (53)     NULL,
    [AnnualIncreaseDecrease]  FLOAT (53)     NULL,
    [Subs]                    INT            NULL,
    CONSTRAINT [PK_dbo.Account] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Card] (
    [Id] [int] NOT NULL IDENTITY,
    [AccessToken] [nvarchar](max),
    [PublicToken] [nvarchar](max),
    CONSTRAINT [PK_dbo.Card] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[Company] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [Address] [nvarchar](max),
    [CompanyEmail] [nvarchar](max),
    [CompanyPhone] [nvarchar](max),
    [CompanyCity] [nvarchar](max),
    [CompanyState] [nvarchar](max),
    [CompanyCountry] [nvarchar](max),
    CONSTRAINT [PK_dbo.Company] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[CompanyUser] (
    [id] [int] NOT NULL IDENTITY,
    [subscription_id] [int] NOT NULL,
    [type] [nvarchar](max),
    [updated_at] [nvarchar](max),
    [created_at] [nvarchar](max),
    [firstName] [nvarchar](max),
    [lastName] [nvarchar](max),
    [email] [nvarchar](max),
    [Company_Id] [int],
    CONSTRAINT [PK_dbo.CompanyUser] PRIMARY KEY ([id])
)
CREATE TABLE [dbo].[Plan] (
    [id] [int] NOT NULL IDENTITY,
    [name] [nvarchar](max),
    [private_topics] [bit] NOT NULL,
    [sites] [int] NOT NULL,
    [topics_per_sites] [int] NOT NULL,
    [embed_player] [bit] NOT NULL,
    [datecreated] [datetime] NOT NULL,
    CONSTRAINT [PK_dbo.Plan] PRIMARY KEY ([id])
)
CREATE TABLE [dbo].[SubscriptionItem] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [CCDescription] [nvarchar](max),
    [LogoUrl] [nvarchar](max),
    CONSTRAINT [PK_dbo.SubscriptionItem] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[UserSubscription] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [IsActive] [bit] NOT NULL,
    [NextBillingDate] [datetime] NOT NULL,
    [PreviousBillingDate] [datetime] NOT NULL,
    [motherSubscriptionId] [int],
    CONSTRAINT [PK_dbo.UserSubscription] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[UserDashboard] (
    [Id] [int] NOT NULL IDENTITY,
    [Monthly] [float],
    [Annual] [float],
    [Subs] [int],    
    CONSTRAINT [PK_dbo.UserDashboard] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[UserSubscriptionPayment] (
    [Id] [int] NOT NULL IDENTITY,
    [Amount] [float] NOT NULL,
	[TransactionId] [nvarchar](max),
    [Date] [datetime] NOT NULL,
    [UserSubscription_Id] [int],
    CONSTRAINT [PK_dbo.UserSubscriptionPayment] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Card_Id] ON [dbo].[Account]([Card_Id])
CREATE INDEX [IX_Company_Id] ON [dbo].[CompanyUser]([Company_Id])
CREATE INDEX [IX_UserSubscription_Id] ON [dbo].[UserSubscriptionPayment]([UserSubscription_Id])
ALTER TABLE [dbo].[Account] ADD CONSTRAINT [FK_dbo.Account_dbo.Card_Card_Id] FOREIGN KEY ([Card_Id]) REFERENCES [dbo].[Card] ([Id])
ALTER TABLE [dbo].[CompanyUser] ADD CONSTRAINT [FK_dbo.CompanyUser_dbo.Company_Company_Id] FOREIGN KEY ([Company_Id]) REFERENCES [dbo].[Company] ([Id])
ALTER TABLE [dbo].[UserSubscriptionPayment] ADD CONSTRAINT [FK_dbo.UserSubscriptionPayment_dbo.UserSubscription_UserSubscription_Id] FOREIGN KEY ([UserSubscription_Id]) REFERENCES [dbo].[UserSubscription] ([Id])