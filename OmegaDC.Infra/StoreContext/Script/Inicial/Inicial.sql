Create SCHEMA sec

CREATE TABLE [sec.User](
	[UserId] UNIQUEIDENTIFIER PRIMARY KEY NOT NULL DEFAULT newid(),
	[Name] [varchar](100) NOT NULL,
	[Password] [varchar](MAX) NOT NULL,
	[Email] [varchar](100) NOT NULL,
	[DtCriacao] [datetime] NOT NULL,
	[Ativo] [bit] DEFAULT 1
) ON [PRIMARY]