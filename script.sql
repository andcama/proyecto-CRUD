USE [PolizasCRUD]
GO
/****** Object:  Table [dbo].[Clientes]    Script Date: 4/4/2025 10:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Clientes](
	[CedulaAsegurado] [nvarchar](20) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
	[PrimerApellido] [nvarchar](50) NOT NULL,
	[SegundoApellido] [nvarchar](50) NOT NULL,
	[TipoPersona] [nvarchar](20) NOT NULL,
	[FechaNacimiento] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_Clientes] PRIMARY KEY CLUSTERED 
(
	[CedulaAsegurado] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Coberturas]    Script Date: 4/4/2025 10:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coberturas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](100) NOT NULL,
	[Descripcion] [nvarchar](255) NOT NULL,
 CONSTRAINT [PK_Coberturas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EstadosPoliza]    Script Date: 4/4/2025 10:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EstadosPoliza](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_EstadosPoliza] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PolizaCoberturas]    Script Date: 4/4/2025 10:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PolizaCoberturas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NumeroPoliza] [nvarchar](50) NOT NULL,
	[CoberturaId] [int] NOT NULL,
 CONSTRAINT [PK_PolizaCoberturas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Polizas]    Script Date: 4/4/2025 10:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Polizas](
	[NumeroPoliza] [nvarchar](50) NOT NULL,
	[TipoPolizaId] [int] NOT NULL,
	[CedulaAsegurado] [nvarchar](20) NOT NULL,
	[MontoAsegurado] [decimal](18, 2) NOT NULL,
	[FechaVencimiento] [datetime2](7) NOT NULL,
	[FechaEmision] [datetime2](7) NOT NULL,
	[EstadoPolizaId] [int] NOT NULL,
	[Prima] [decimal](18, 2) NOT NULL,
	[Periodo] [datetime2](7) NOT NULL,
	[FechaInclusion] [datetime2](7) NOT NULL,
	[Aseguradora] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_Polizas] PRIMARY KEY CLUSTERED 
(
	[NumeroPoliza] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposPoliza]    Script Date: 4/4/2025 10:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposPoliza](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_TiposPoliza] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 4/4/2025 10:26:12 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[PasswordHash] [varbinary](max) NOT NULL,
	[PasswordSalt] [varbinary](max) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[RefreshToken] [nvarchar](max) NULL,
	[RefreshTokenExpiryTime] [datetime2](7) NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[PolizaCoberturas]  WITH CHECK ADD  CONSTRAINT [FK_PolizaCoberturas_Coberturas_CoberturaId] FOREIGN KEY([CoberturaId])
REFERENCES [dbo].[Coberturas] ([Id])
GO
ALTER TABLE [dbo].[PolizaCoberturas] CHECK CONSTRAINT [FK_PolizaCoberturas_Coberturas_CoberturaId]
GO
ALTER TABLE [dbo].[PolizaCoberturas]  WITH CHECK ADD  CONSTRAINT [FK_PolizaCoberturas_Polizas_NumeroPoliza] FOREIGN KEY([NumeroPoliza])
REFERENCES [dbo].[Polizas] ([NumeroPoliza])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[PolizaCoberturas] CHECK CONSTRAINT [FK_PolizaCoberturas_Polizas_NumeroPoliza]
GO
ALTER TABLE [dbo].[Polizas]  WITH CHECK ADD  CONSTRAINT [FK_Polizas_Clientes_CedulaAsegurado] FOREIGN KEY([CedulaAsegurado])
REFERENCES [dbo].[Clientes] ([CedulaAsegurado])
GO
ALTER TABLE [dbo].[Polizas] CHECK CONSTRAINT [FK_Polizas_Clientes_CedulaAsegurado]
GO
ALTER TABLE [dbo].[Polizas]  WITH CHECK ADD  CONSTRAINT [FK_Polizas_EstadosPoliza_EstadoPolizaId] FOREIGN KEY([EstadoPolizaId])
REFERENCES [dbo].[EstadosPoliza] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Polizas] CHECK CONSTRAINT [FK_Polizas_EstadosPoliza_EstadoPolizaId]
GO
ALTER TABLE [dbo].[Polizas]  WITH CHECK ADD  CONSTRAINT [FK_Polizas_TiposPoliza_TipoPolizaId] FOREIGN KEY([TipoPolizaId])
REFERENCES [dbo].[TiposPoliza] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Polizas] CHECK CONSTRAINT [FK_Polizas_TiposPoliza_TipoPolizaId]
GO
