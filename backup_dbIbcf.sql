USE [master]
GO
/****** Object:  Database [db_ICBF]    Script Date: 26/06/2024 12:09:11 a. m. ******/
CREATE DATABASE [db_ICBF]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'db_ICBF_Data', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\db_ICBF.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'db_ICBF_Log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\db_ICBF.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [db_ICBF] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [db_ICBF].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [db_ICBF] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [db_ICBF] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [db_ICBF] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [db_ICBF] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [db_ICBF] SET ARITHABORT OFF 
GO
ALTER DATABASE [db_ICBF] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [db_ICBF] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [db_ICBF] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [db_ICBF] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [db_ICBF] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [db_ICBF] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [db_ICBF] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [db_ICBF] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [db_ICBF] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [db_ICBF] SET  DISABLE_BROKER 
GO
ALTER DATABASE [db_ICBF] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [db_ICBF] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [db_ICBF] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [db_ICBF] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [db_ICBF] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [db_ICBF] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [db_ICBF] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [db_ICBF] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [db_ICBF] SET  MULTI_USER 
GO
ALTER DATABASE [db_ICBF] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [db_ICBF] SET DB_CHAINING OFF 
GO
ALTER DATABASE [db_ICBF] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [db_ICBF] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [db_ICBF] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [db_ICBF] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [db_ICBF] SET QUERY_STORE = ON
GO
ALTER DATABASE [db_ICBF] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [db_ICBF]
GO
/****** Object:  Table [dbo].[Asistencias]    Script Date: 26/06/2024 12:09:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Asistencias](
	[idAsistencia] [int] IDENTITY(1,1) NOT NULL,
	[fecha] [date] NOT NULL,
	[estadoNino] [nchar](20) NOT NULL,
	[idNino] [int] NOT NULL,
 CONSTRAINT [PK_Asistencias] PRIMARY KEY CLUSTERED 
(
	[idAsistencia] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[AvancesAcademicos]    Script Date: 26/06/2024 12:09:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[AvancesAcademicos](
	[idAvanceAcademico] [int] IDENTITY(1,1) NOT NULL,
	[nivel] [nchar](20) NOT NULL,
	[notas] [nchar](10) NOT NULL,
	[descripcion] [nchar](150) NOT NULL,
	[fechaEntrega] [date] NOT NULL,
	[idNino] [int] NOT NULL,
 CONSTRAINT [PK_AvancesAcademicos] PRIMARY KEY CLUSTERED 
(
	[idAvanceAcademico] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DatosBasicos]    Script Date: 26/06/2024 12:09:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DatosBasicos](
	[idDatosBasicos] [int] IDENTITY(1,1) NOT NULL,
	[identificacion] [varchar](10) NOT NULL,
	[nombres] [nchar](100) NOT NULL,
	[fechaNacimiento] [date] NOT NULL,
	[celular] [nchar](10) NOT NULL,
	[direccion] [nchar](80) NOT NULL,
	[idTipoDocumento] [int] NOT NULL,
 CONSTRAINT [PK_DatosBasicos] PRIMARY KEY CLUSTERED 
(
	[idDatosBasicos] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EPS]    Script Date: 26/06/2024 12:09:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EPS](
	[idEps] [int] IDENTITY(1,1) NOT NULL,
	[NIT] [nchar](15) NOT NULL,
	[nombre] [nchar](50) NOT NULL,
	[direccion] [nchar](80) NOT NULL,
	[telefono] [nchar](10) NULL,
 CONSTRAINT [PK_EPS] PRIMARY KEY CLUSTERED 
(
	[idEps] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Jardines]    Script Date: 26/06/2024 12:09:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Jardines](
	[idJardin] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nchar](50) NOT NULL,
	[direccion] [nchar](80) NOT NULL,
	[estado] [varchar](15) NOT NULL,
 CONSTRAINT [PK_Jardin] PRIMARY KEY CLUSTERED 
(
	[idJardin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Ninos]    Script Date: 26/06/2024 12:09:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Ninos](
	[idNino] [int] IDENTITY(1,1) NOT NULL,
	[tipoSangre] [nchar](10) NOT NULL,
	[ciudadNacimiento] [nchar](50) NOT NULL,
	[peso] [int] NULL,
	[estatura] [float] NULL,
	[idJardin] [int] NOT NULL,
	[idAcudiente] [int] NOT NULL,
	[idMadreComunitaria] [int] NOT NULL,
	[idDatosBasicos] [int] NOT NULL,
	[idEps] [int] NOT NULL,
 CONSTRAINT [PK_Ninos] PRIMARY KEY CLUSTERED 
(
	[idNino] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Roles]    Script Date: 26/06/2024 12:09:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Roles](
	[idRol] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nchar](70) NOT NULL,
 CONSTRAINT [PK_Roles] PRIMARY KEY CLUSTERED 
(
	[idRol] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TipoDocumento]    Script Date: 26/06/2024 12:09:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TipoDocumento](
	[idTipoDoc] [int] IDENTITY(1,1) NOT NULL,
	[tipo] [nchar](10) NOT NULL,
 CONSTRAINT [PK_TipoDocumento] PRIMARY KEY CLUSTERED 
(
	[idTipoDoc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Usuarios]    Script Date: 26/06/2024 12:09:11 a. m. ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Usuarios](
	[idUsuario] [int] IDENTITY(1,1) NOT NULL,
	[clave] [nchar](50) NULL,
	[nombreUsuario] [nchar](50) NULL,
	[correo] [nchar](50) NULL,
	[idDatosBasicos] [int] NOT NULL,
	[idRol] [int] NOT NULL,
 CONSTRAINT [PK_Usuarios] PRIMARY KEY CLUSTERED 
(
	[idUsuario] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Asistencias] ON 

INSERT [dbo].[Asistencias] ([idAsistencia], [fecha], [estadoNino], [idNino]) VALUES (1, CAST(N'2024-06-25' AS Date), N'Sano                ', 2)
INSERT [dbo].[Asistencias] ([idAsistencia], [fecha], [estadoNino], [idNino]) VALUES (4, CAST(N'2024-06-25' AS Date), N'Sano                ', 3)
INSERT [dbo].[Asistencias] ([idAsistencia], [fecha], [estadoNino], [idNino]) VALUES (5, CAST(N'2024-06-25' AS Date), N'Decaido             ', 1)
SET IDENTITY_INSERT [dbo].[Asistencias] OFF
GO
SET IDENTITY_INSERT [dbo].[AvancesAcademicos] ON 

INSERT [dbo].[AvancesAcademicos] ([idAvanceAcademico], [nivel], [notas], [descripcion], [fechaEntrega], [idNino]) VALUES (1, N'Prejardín           ', N'B         ', N'Debe esforzarse mas                                                                                                                                   ', CAST(N'2024-06-25' AS Date), 1)
INSERT [dbo].[AvancesAcademicos] ([idAvanceAcademico], [nivel], [notas], [descripcion], [fechaEntrega], [idNino]) VALUES (2, N'Natal               ', N'A         ', N'Muy bien                                                                                                                                              ', CAST(N'2024-06-05' AS Date), 3)
INSERT [dbo].[AvancesAcademicos] ([idAvanceAcademico], [nivel], [notas], [descripcion], [fechaEntrega], [idNino]) VALUES (3, N'Jardín              ', N'S         ', N'Excelente                                                                                                                                             ', CAST(N'2024-06-24' AS Date), 2)
SET IDENTITY_INSERT [dbo].[AvancesAcademicos] OFF
GO
SET IDENTITY_INSERT [dbo].[DatosBasicos] ON 

INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (1, N'1234567890', N'Ana María Pérez                                                                                     ', CAST(N'1980-05-12' AS Date), N'3001234567', N'Carrera 15 #45-67                                                               ', 1)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (2, N'2345678901', N'Carlos Andrés Gómez                                                                                 ', CAST(N'1975-08-23' AS Date), N'3102345678', N'Calle 10 #20-30                                                                 ', 1)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (3, N'3456789012', N'María Fernanda Rodríguez                                                                            ', CAST(N'1985-11-15' AS Date), N'3203456789', N'Avenida Siempre Viva #123                                                       ', 1)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (4, N'0123456789', N'Alejandro González                                                                                  ', CAST(N'1981-01-30' AS Date), N'3000123456', N'Calle 17 #68D-13                                                                ', 1)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (5, N'9012345678', N'Sandra Milena Morales                                                                               ', CAST(N'1983-06-10' AS Date), N'3209012345', N'Avenida El Dorado #68-90                                                        ', 1)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (6, N'8901234567', N'Miguel Ángel Torres                                                                                 ', CAST(N'1988-04-22' AS Date), N'3108901234', N'Carrera 3 #54-67                                                                ', 1)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (7, N'6789012345', N'Juan Carlos Ramírez                                                                                 ', CAST(N'1978-09-12' AS Date), N'3206789012', N'Carrera 7 #34-56                                                                ', 1)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (8, N'7890123456', N'Laura Patricia Herrera                                                                              ', CAST(N'1992-02-18' AS Date), N'3007890123', N'Calle 8 #21-45                                                                  ', 1)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (9, N'1000001333', N'Juan Esteban Ramírez                                                                                ', CAST(N'2020-03-10' AS Date), N'3101234567', N'Carrera 15 #45-67                                                               ', 3)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (10, N'1000002234', N'Valentina Gómez                                                                                     ', CAST(N'2020-01-05' AS Date), N'3201234567', N'Calle 10 #20-30                                                                 ', 3)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (11, N'1000003566', N'Samuel Rodríguez                                                                                    ', CAST(N'2022-11-20' AS Date), N'3103456789', N'Avenida Siempre Viva #123                                                       ', 3)
INSERT [dbo].[DatosBasicos] ([idDatosBasicos], [identificacion], [nombres], [fechaNacimiento], [celular], [direccion], [idTipoDocumento]) VALUES (12, N'1000004787', N'Sofía Martínez                                                                                      ', CAST(N'2021-08-12' AS Date), N'3104567890', N'Calle 45 #12-23                                                                 ', 3)
SET IDENTITY_INSERT [dbo].[DatosBasicos] OFF
GO
SET IDENTITY_INSERT [dbo].[EPS] ON 

INSERT [dbo].[EPS] ([idEps], [NIT], [nombre], [direccion], [telefono]) VALUES (1, N'89090000-1     ', N'Salud Total                                       ', N'Carrera 13 #32-76                                                               ', N'6011160012')
INSERT [dbo].[EPS] ([idEps], [NIT], [nombre], [direccion], [telefono]) VALUES (2, N'90000234-6     ', N'Nueva EPS                                         ', N'Avenida El Dorado #68-94                                                        ', N'6012530767')
INSERT [dbo].[EPS] ([idEps], [NIT], [nombre], [direccion], [telefono]) VALUES (3, N'86001699-1     ', N'Sanitas                                           ', N'Carrera 7 #40-49                                                                ', N'6017237529')
INSERT [dbo].[EPS] ([idEps], [NIT], [nombre], [direccion], [telefono]) VALUES (4, N'89090345-7     ', N'Sura                                              ', N'Carrera 43A #14-109                                                             ', N'6013444826')
INSERT [dbo].[EPS] ([idEps], [NIT], [nombre], [direccion], [telefono]) VALUES (5, N'86002162-1     ', N'Compensar                                         ', N'Avenida 68 #49A-47                                                              ', N'6011244431')
INSERT [dbo].[EPS] ([idEps], [NIT], [nombre], [direccion], [telefono]) VALUES (6, N'89090115-8     ', N'Coomeva                                           ', N'Calle 5 #38-71                                                                  ', N'6011288676')
INSERT [dbo].[EPS] ([idEps], [NIT], [nombre], [direccion], [telefono]) VALUES (7, N'86000210-7     ', N'Famisanar                                         ', N'Calle 17 #68D-13                                                                ', N'6015643078')
INSERT [dbo].[EPS] ([idEps], [NIT], [nombre], [direccion], [telefono]) VALUES (8, N'86001697-1     ', N'SaludCoop                                         ', N'Carrera 13 #26-45                                                               ', N'6012565942')
SET IDENTITY_INSERT [dbo].[EPS] OFF
GO
SET IDENTITY_INSERT [dbo].[Jardines] ON 

INSERT [dbo].[Jardines] ([idJardin], [nombre], [direccion], [estado]) VALUES (1, N'Jardín Infantil Pequeños Exploradores             ', N'Calle 123 #45-67                                                                ', N'En trámite')
INSERT [dbo].[Jardines] ([idJardin], [nombre], [direccion], [estado]) VALUES (2, N'Jardín Infantil Sonrisas Brillantes               ', N'Carrera 15 #87-34                                                               ', N'Aprobado')
INSERT [dbo].[Jardines] ([idJardin], [nombre], [direccion], [estado]) VALUES (3, N'Jardín Infantil Mundo Mágico                      ', N'Avenida Siempre Viva #123                                                       ', N'En trámite')
INSERT [dbo].[Jardines] ([idJardin], [nombre], [direccion], [estado]) VALUES (4, N'Jardín Infantil Dulces Sueños                     ', N'Calle 45 #12-23                                                                 ', N'Negado')
INSERT [dbo].[Jardines] ([idJardin], [nombre], [direccion], [estado]) VALUES (5, N'Jardín Infantil Estrellitas del Futuro            ', N'Carrera 9 #56-78                                                                ', N'En trámite')
INSERT [dbo].[Jardines] ([idJardin], [nombre], [direccion], [estado]) VALUES (6, N'Jardín Infantil Pequeños Gigantes                 ', N'Calle 10 #20-30                                                                 ', N'Aprobado')
INSERT [dbo].[Jardines] ([idJardin], [nombre], [direccion], [estado]) VALUES (7, N'Jardín Infantil Arcoíris de Alegría               ', N'Avenida El Dorado #68-90                                                        ', N'En trámite')
INSERT [dbo].[Jardines] ([idJardin], [nombre], [direccion], [estado]) VALUES (8, N'Jardín Infantil Mundo de Colores                  ', N'Carrera 7 #34-56                                                                ', N'En trámite')
INSERT [dbo].[Jardines] ([idJardin], [nombre], [direccion], [estado]) VALUES (9, N'Jardín Infantil Amigos del Saber                  ', N'Calle 8 #21-45                                                                  ', N'En trámite')
INSERT [dbo].[Jardines] ([idJardin], [nombre], [direccion], [estado]) VALUES (10, N'Jardín Infantil Risas y Juegos                    ', N'Carrera 3 #54-67                                                                ', N'Negado')
SET IDENTITY_INSERT [dbo].[Jardines] OFF
GO
SET IDENTITY_INSERT [dbo].[Ninos] ON 

INSERT [dbo].[Ninos] ([idNino], [tipoSangre], [ciudadNacimiento], [peso], [estatura], [idJardin], [idAcudiente], [idMadreComunitaria], [idDatosBasicos], [idEps]) VALUES (1, N'O+        ', N'Bogotá                                            ', NULL, NULL, 1, 2, 1, 9, 1)
INSERT [dbo].[Ninos] ([idNino], [tipoSangre], [ciudadNacimiento], [peso], [estatura], [idJardin], [idAcudiente], [idMadreComunitaria], [idDatosBasicos], [idEps]) VALUES (2, N'A+        ', N'Medellín                                          ', NULL, NULL, 2, 4, 3, 10, 2)
INSERT [dbo].[Ninos] ([idNino], [tipoSangre], [ciudadNacimiento], [peso], [estatura], [idJardin], [idAcudiente], [idMadreComunitaria], [idDatosBasicos], [idEps]) VALUES (3, N'AB+       ', N'Cali                                              ', NULL, NULL, 3, 6, 5, 11, 3)
INSERT [dbo].[Ninos] ([idNino], [tipoSangre], [ciudadNacimiento], [peso], [estatura], [idJardin], [idAcudiente], [idMadreComunitaria], [idDatosBasicos], [idEps]) VALUES (4, N'A-        ', N'Barranquilla                                      ', NULL, NULL, 9, 7, 1, 12, 4)
SET IDENTITY_INSERT [dbo].[Ninos] OFF
GO
SET IDENTITY_INSERT [dbo].[Roles] ON 

INSERT [dbo].[Roles] ([idRol], [nombre]) VALUES (1, N'Administrador                                                         ')
INSERT [dbo].[Roles] ([idRol], [nombre]) VALUES (2, N'Madre Comunitaria                                                     ')
INSERT [dbo].[Roles] ([idRol], [nombre]) VALUES (3, N'Acudiente                                                             ')
SET IDENTITY_INSERT [dbo].[Roles] OFF
GO
SET IDENTITY_INSERT [dbo].[TipoDocumento] ON 

INSERT [dbo].[TipoDocumento] ([idTipoDoc], [tipo]) VALUES (1, N'CC        ')
INSERT [dbo].[TipoDocumento] ([idTipoDoc], [tipo]) VALUES (2, N'CE        ')
INSERT [dbo].[TipoDocumento] ([idTipoDoc], [tipo]) VALUES (3, N'NIUP      ')
INSERT [dbo].[TipoDocumento] ([idTipoDoc], [tipo]) VALUES (4, N'Pasaporte ')
INSERT [dbo].[TipoDocumento] ([idTipoDoc], [tipo]) VALUES (5, N'PEP       ')
SET IDENTITY_INSERT [dbo].[TipoDocumento] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuarios] ON 

INSERT [dbo].[Usuarios] ([idUsuario], [clave], [nombreUsuario], [correo], [idDatosBasicos], [idRol]) VALUES (1, NULL, NULL, NULL, 1, 2)
INSERT [dbo].[Usuarios] ([idUsuario], [clave], [nombreUsuario], [correo], [idDatosBasicos], [idRol]) VALUES (2, NULL, NULL, NULL, 2, 3)
INSERT [dbo].[Usuarios] ([idUsuario], [clave], [nombreUsuario], [correo], [idDatosBasicos], [idRol]) VALUES (3, NULL, NULL, NULL, 3, 2)
INSERT [dbo].[Usuarios] ([idUsuario], [clave], [nombreUsuario], [correo], [idDatosBasicos], [idRol]) VALUES (4, NULL, NULL, NULL, 4, 3)
INSERT [dbo].[Usuarios] ([idUsuario], [clave], [nombreUsuario], [correo], [idDatosBasicos], [idRol]) VALUES (5, NULL, NULL, NULL, 5, 2)
INSERT [dbo].[Usuarios] ([idUsuario], [clave], [nombreUsuario], [correo], [idDatosBasicos], [idRol]) VALUES (6, NULL, NULL, NULL, 6, 3)
INSERT [dbo].[Usuarios] ([idUsuario], [clave], [nombreUsuario], [correo], [idDatosBasicos], [idRol]) VALUES (7, NULL, NULL, NULL, 7, 3)
INSERT [dbo].[Usuarios] ([idUsuario], [clave], [nombreUsuario], [correo], [idDatosBasicos], [idRol]) VALUES (8, NULL, NULL, NULL, 8, 3)
SET IDENTITY_INSERT [dbo].[Usuarios] OFF
GO
/****** Object:  Index [Unique_Key_Table_1]    Script Date: 26/06/2024 12:09:11 a. m. ******/
ALTER TABLE [dbo].[Jardines] ADD  CONSTRAINT [Unique_Key_Table_1] UNIQUE NONCLUSTERED 
(
	[idJardin] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [Unique_Key_Table_N]    Script Date: 26/06/2024 12:09:11 a. m. ******/
ALTER TABLE [dbo].[Ninos] ADD  CONSTRAINT [Unique_Key_Table_N] UNIQUE NONCLUSTERED 
(
	[idNino] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Asistencias]  WITH CHECK ADD  CONSTRAINT [FK_Asistencias_Ninos] FOREIGN KEY([idNino])
REFERENCES [dbo].[Ninos] ([idNino])
GO
ALTER TABLE [dbo].[Asistencias] CHECK CONSTRAINT [FK_Asistencias_Ninos]
GO
ALTER TABLE [dbo].[AvancesAcademicos]  WITH CHECK ADD  CONSTRAINT [FK_AvancesAcademicos_Ninos] FOREIGN KEY([idNino])
REFERENCES [dbo].[Ninos] ([idNino])
GO
ALTER TABLE [dbo].[AvancesAcademicos] CHECK CONSTRAINT [FK_AvancesAcademicos_Ninos]
GO
ALTER TABLE [dbo].[DatosBasicos]  WITH CHECK ADD  CONSTRAINT [FK_DatosBasicos_TipoDocumento] FOREIGN KEY([idTipoDocumento])
REFERENCES [dbo].[TipoDocumento] ([idTipoDoc])
GO
ALTER TABLE [dbo].[DatosBasicos] CHECK CONSTRAINT [FK_DatosBasicos_TipoDocumento]
GO
ALTER TABLE [dbo].[Ninos]  WITH CHECK ADD  CONSTRAINT [FK_Ninos_DatosBasicos] FOREIGN KEY([idDatosBasicos])
REFERENCES [dbo].[DatosBasicos] ([idDatosBasicos])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Ninos] CHECK CONSTRAINT [FK_Ninos_DatosBasicos]
GO
ALTER TABLE [dbo].[Ninos]  WITH CHECK ADD  CONSTRAINT [FK_Ninos_EPS] FOREIGN KEY([idEps])
REFERENCES [dbo].[EPS] ([idEps])
GO
ALTER TABLE [dbo].[Ninos] CHECK CONSTRAINT [FK_Ninos_EPS]
GO
ALTER TABLE [dbo].[Ninos]  WITH CHECK ADD  CONSTRAINT [FK_Ninos_Jardines] FOREIGN KEY([idJardin])
REFERENCES [dbo].[Jardines] ([idJardin])
GO
ALTER TABLE [dbo].[Ninos] CHECK CONSTRAINT [FK_Ninos_Jardines]
GO
ALTER TABLE [dbo].[Ninos]  WITH CHECK ADD  CONSTRAINT [FK_Ninos_Usuarios] FOREIGN KEY([idAcudiente])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Ninos] CHECK CONSTRAINT [FK_Ninos_Usuarios]
GO
ALTER TABLE [dbo].[Ninos]  WITH CHECK ADD  CONSTRAINT [FK_Ninos_Usuarios2] FOREIGN KEY([idAcudiente])
REFERENCES [dbo].[Usuarios] ([idUsuario])
GO
ALTER TABLE [dbo].[Ninos] CHECK CONSTRAINT [FK_Ninos_Usuarios2]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_DatosBasicos] FOREIGN KEY([idDatosBasicos])
REFERENCES [dbo].[DatosBasicos] ([idDatosBasicos])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_DatosBasicos]
GO
ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_Usuarios_Roles] FOREIGN KEY([idRol])
REFERENCES [dbo].[Roles] ([idRol])
GO
ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_Usuarios_Roles]
GO
USE [master]
GO
ALTER DATABASE [db_ICBF] SET  READ_WRITE 
GO
