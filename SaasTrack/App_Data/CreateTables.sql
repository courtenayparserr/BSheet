USE "dbname";

CREATE TABLE [dbo].[Account] (
    [Id] [int] NOT NULL IDENTITY,
    [_id] [nvarchar](max),
    [_item] [nvarchar](max),
    [balance_available] [nvarchar](max),
    [balance_current] [nvarchar](max),
    [institution_type] [nvarchar](max),
    [meta_name] [nvarchar](max),
    [meta_number] [nvarchar](max),
    [subtype] [nvarchar](max),
    [type] [nvarchar](max),
    [lastdateprocessed] [datetime],
    [lastdateadded] [datetime],
    [enabled] [bit] NOT NULL,
    [Monthly] [float],
    [MonthlyIncreaseDecrease] [float],
    [Annual] [float],
    [AnnualIncreaseDecrease] [float],
    [Subs] [int],
    [Card_Id] [int],
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
    [phone] [nvarchar](max),
    [Company_Id] [int],
	[hasSeenIntroEmployee] [bit],
	[hasSeenIntro] [bit],
    CONSTRAINT [PK_dbo.CompanyUser] PRIMARY KEY ([id])
)
CREATE TABLE [dbo].[Service] (
    [Id] [int] NOT NULL IDENTITY,
    [MasterServiceId] [int] NOT NULL,
    [AutoDetected] [bit] NOT NULL,
    [AddedByBankFeed] [bit],
    [DateAdded] [datetime],
    [InactiveWhenDays] [int],
    [CompanyUser_id] [int],
    CONSTRAINT [PK_dbo.Service] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[ServiceUrl] (
    [Id] [int] NOT NULL IDENTITY,
    [Url] [nvarchar](max),
    [Service_Id] [int],
    CONSTRAINT [PK_dbo.ServiceUrl] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[ServiceUsage] (
    [Id] [int] NOT NULL IDENTITY,
    [serviceid] [int],
    [companyuserid] [int],
    [UsageDate] [datetime] NOT NULL,
    [Seconds] [float] NOT NULL,
    CONSTRAINT [PK_dbo.ServiceUsage] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[CompanyUserServices] (
    [Id] [int] NOT NULL IDENTITY,
    [serviceid] [int],
    [companyuserid] [int],
    [DateAdded] [datetime] NOT NULL,
    [AddedManually] [bit] NOT NULL,
    [DetectedAutomatically] [bit] NOT NULL,
    CONSTRAINT [PK_dbo.CompanyUserServices] PRIMARY KEY ([Id])
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
CREATE TABLE [dbo].[ServiceDashboard] (
    [Id] [int] NOT NULL IDENTITY,
    [serviceid] [int],
    [TotalNumberOfUsers] [int],
    [TotalMinutesSpent] [int],
    [TotalSpend] [real],
    [EmployeeRating] [real],
    [ActiveUsers] [int],
    CONSTRAINT [PK_dbo.ServiceDashboard] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[UserDashboard] (
    [Id] [int] NOT NULL IDENTITY,
    [Monthly] [float] NOT NULL,
    [MonthlyIncreaseDecrease] [float] NOT NULL,
    [Annual] [float] NOT NULL,
    [AnnualIncreaseDecrease] [float] NOT NULL,
	[PotentialSavings] [float] NULL,
	[InactiveSeats] [int] NULL,
	[EmployeeEngagement] [nvarchar](max),
    [Subs] [int] NOT NULL,
    CONSTRAINT [PK_dbo.UserDashboard] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[UserSubscription] (
    [Id] [int] NOT NULL IDENTITY,
    [Name] [nvarchar](max),
    [IsActive] [bit] NOT NULL,
    [Period] [int],
    [NextBillingDate] [datetime],
    [PreviousBillingDate] [datetime],
    [DateCreated] [datetime],
    [motherSubscriptionId] [int] NOT NULL,
    [accountid] [int],
    CONSTRAINT [PK_dbo.UserSubscription] PRIMARY KEY ([Id])
)
CREATE TABLE [dbo].[UserSubscriptionPayment] (
    [Id] [int] NOT NULL IDENTITY,
    [TransactionId] [nvarchar](max),
    [Amount] [float] NOT NULL,
    [Date] [datetime] NOT NULL,
    [UserSubscription_Id] [int],
    CONSTRAINT [PK_dbo.UserSubscriptionPayment] PRIMARY KEY ([Id])
)
CREATE INDEX [IX_Card_Id] ON [dbo].[Account]([Card_Id])
CREATE INDEX [IX_Company_Id] ON [dbo].[CompanyUser]([Company_Id])
CREATE INDEX [IX_CompanyUser_id] ON [dbo].[Service]([CompanyUser_id])
CREATE INDEX [IX_Service_Id] ON [dbo].[ServiceUrl]([Service_Id])
CREATE INDEX [IX_serviceid] ON [dbo].[ServiceUsage]([serviceid])
CREATE INDEX [IX_companyuserid] ON [dbo].[ServiceUsage]([companyuserid])
CREATE INDEX [IX_serviceid] ON [dbo].[CompanyUserServices]([serviceid])
CREATE INDEX [IX_companyuserid] ON [dbo].[CompanyUserServices]([companyuserid])
CREATE INDEX [IX_serviceid] ON [dbo].[ServiceDashboard]([serviceid])
CREATE INDEX [IX_accountid] ON [dbo].[UserSubscription]([accountid])
CREATE INDEX [IX_UserSubscription_Id] ON [dbo].[UserSubscriptionPayment]([UserSubscription_Id])
ALTER TABLE [dbo].[Account] ADD CONSTRAINT [FK_dbo.Account_dbo.Card_Card_Id] FOREIGN KEY ([Card_Id]) REFERENCES [dbo].[Card] ([Id])
ALTER TABLE [dbo].[CompanyUser] ADD CONSTRAINT [FK_dbo.CompanyUser_dbo.Company_Company_Id] FOREIGN KEY ([Company_Id]) REFERENCES [dbo].[Company] ([Id])
ALTER TABLE [dbo].[Service] ADD CONSTRAINT [FK_dbo.Service_dbo.CompanyUser_CompanyUser_id] FOREIGN KEY ([CompanyUser_id]) REFERENCES [dbo].[CompanyUser] ([id])
ALTER TABLE [dbo].[ServiceUrl] ADD CONSTRAINT [FK_dbo.ServiceUrl_dbo.Service_Service_Id] FOREIGN KEY ([Service_Id]) REFERENCES [dbo].[Service] ([Id])
ALTER TABLE [dbo].[ServiceUsage] ADD CONSTRAINT [FK_dbo.ServiceUsage_dbo.CompanyUser_companyuserid] FOREIGN KEY ([companyuserid]) REFERENCES [dbo].[CompanyUser] ([id])
ALTER TABLE [dbo].[ServiceUsage] ADD CONSTRAINT [FK_dbo.ServiceUsage_dbo.Service_serviceid] FOREIGN KEY ([serviceid]) REFERENCES [dbo].[Service] ([Id])
ALTER TABLE [dbo].[CompanyUserServices] ADD CONSTRAINT [FK_dbo.CompanyUserServices_dbo.CompanyUser_companyuserid] FOREIGN KEY ([companyuserid]) REFERENCES [dbo].[CompanyUser] ([id])
ALTER TABLE [dbo].[CompanyUserServices] ADD CONSTRAINT [FK_dbo.CompanyUserServices_dbo.Service_serviceid] FOREIGN KEY ([serviceid]) REFERENCES [dbo].[Service] ([Id])
ALTER TABLE [dbo].[ServiceDashboard] ADD CONSTRAINT [FK_dbo.ServiceDashboard_dbo.Service_serviceid] FOREIGN KEY ([serviceid]) REFERENCES [dbo].[Service] ([Id])
ALTER TABLE [dbo].[UserSubscription] ADD CONSTRAINT [FK_dbo.UserSubscription_dbo.Account_accountid] FOREIGN KEY ([accountid]) REFERENCES [dbo].[Account] ([Id])
ALTER TABLE [dbo].[UserSubscriptionPayment] ADD CONSTRAINT [FK_dbo.UserSubscriptionPayment_dbo.UserSubscription_UserSubscription_Id] FOREIGN KEY ([UserSubscription_Id]) REFERENCES [dbo].[UserSubscription] ([Id])