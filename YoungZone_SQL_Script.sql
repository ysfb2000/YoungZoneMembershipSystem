USE [master]
GO
/****** Object:  Database [YoungZone]    Script Date: 2025/6/23 1:47:15 ******/
CREATE DATABASE [YoungZone]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'YoungZone', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\YoungZone.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'YoungZone_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\YoungZone_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [YoungZone] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [YoungZone].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [YoungZone] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [YoungZone] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [YoungZone] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [YoungZone] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [YoungZone] SET ARITHABORT OFF 
GO
ALTER DATABASE [YoungZone] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [YoungZone] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [YoungZone] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [YoungZone] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [YoungZone] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [YoungZone] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [YoungZone] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [YoungZone] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [YoungZone] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [YoungZone] SET  DISABLE_BROKER 
GO
ALTER DATABASE [YoungZone] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [YoungZone] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [YoungZone] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [YoungZone] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [YoungZone] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [YoungZone] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [YoungZone] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [YoungZone] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [YoungZone] SET  MULTI_USER 
GO
ALTER DATABASE [YoungZone] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [YoungZone] SET DB_CHAINING OFF 
GO
ALTER DATABASE [YoungZone] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [YoungZone] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [YoungZone] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [YoungZone] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [YoungZone] SET QUERY_STORE = ON
GO
ALTER DATABASE [YoungZone] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [YoungZone]
GO
/****** Object:  Table [dbo].[yz_Administrator]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Administrator](
	[Administrator_ID] [int] IDENTITY(1,1) NOT NULL,
	[Administrator_Name] [nvarchar](50) NULL,
	[Administrator_LoginName] [nvarchar](50) NULL,
	[Administrator_Password] [nvarchar](100) NULL,
	[AdministratorType_ID] [int] NULL,
	[Administrator_LoginCount] [int] NULL,
	[Administrator_State] [smallint] NULL,
	[Administrator_LastLoginTime] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Administrator_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_AdministratorType]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_AdministratorType](
	[AdministratorType_ID] [int] IDENTITY(1,1) NOT NULL,
	[AdministratorType_Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[AdministratorType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_AdminLog]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_AdminLog](
	[AdminLog_ID] [int] IDENTITY(1,1) NOT NULL,
	[AdminType_ID] [int] NULL,
	[Administrator_ID] [int] NULL,
	[Shop_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[AdminLog_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Authority]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Authority](
	[Authority_ID] [int] IDENTITY(1,1) NOT NULL,
	[Authority_Name] [nvarchar](50) NULL,
	[Authority_Control] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[Authority_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Authority_AdministratorType]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Authority_AdministratorType](
	[AdministratorType_ID] [int] NULL,
	[Authority_ID] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Brands]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Brands](
	[Brands_ID] [int] IDENTITY(1,1) NOT NULL,
	[Brands_Name] [nvarchar](100) NULL,
	[Brands_SQL] [nvarchar](500) NULL,
	[Brands_CardNO_Length] [int] NULL,
	[Brands_State] [int] NULL,
	[Brands_Sort] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Brands_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Card]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Card](
	[Card_ID] [int] IDENTITY(1,1) NOT NULL,
	[Card_No] [nvarchar](50) NULL,
	[CardType_ID] [int] NULL,
	[Card_Point] [decimal](18, 2) NULL,
	[Card_State] [smallint] NULL,
	[Member_ID] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Card_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_CardState]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_CardState](
	[CardState_ID] [int] IDENTITY(1,1) NOT NULL,
	[CardState_Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[CardState_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_CardType]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_CardType](
	[CardType_ID] [int] IDENTITY(1,1) NOT NULL,
	[CardType_Name] [nvarchar](50) NULL,
	[CardType_IntValue] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[CardType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Events]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Events](
	[Events_ID] [int] IDENTITY(1,1) NOT NULL,
	[Events_Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Events_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Goods]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Goods](
	[Goods_ID] [int] IDENTITY(1,1) NOT NULL,
	[Goods_Name] [nvarchar](100) NULL,
	[Goods_Value] [decimal](18, 2) NULL,
	[Goods_Type] [int] NULL,
	[Goods_Level] [int] NULL,
	[Goods_State] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Goods_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_GoodsInOut]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_GoodsInOut](
	[GoodsInOut_ID] [int] IDENTITY(1,1) NOT NULL,
	[GoodsInOut_Var] [int] NULL,
	[Shop_ID] [int] NULL,
	[Goods_ID] [int] NULL,
	[Administrator_ID] [int] NULL,
	[Events_ID] [int] NULL,
	[GoodsInOut_Notice] [nvarchar](200) NULL,
PRIMARY KEY CLUSTERED 
(
	[GoodsInOut_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_GoodsType]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_GoodsType](
	[GoodsType_ID] [int] IDENTITY(1,1) NOT NULL,
	[GoodsType_Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[GoodsType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Mac]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Mac](
	[Mac_ID] [nvarchar](50) NOT NULL,
	[Shop_ID] [int] NULL,
	[PrinterName] [nvarchar](100) NULL,
	[PointPrint] [bit] NULL,
	[GoodsPrint] [bit] NULL,
 CONSTRAINT [PK__yz_Mac__136BF8DEF04B0E97] PRIMARY KEY CLUSTERED 
(
	[Mac_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_mac_Authenticate]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_mac_Authenticate](
	[mac_ID] [nvarchar](50) NOT NULL,
	[mac_state] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[mac_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Member]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Member](
	[Member_ID] [int] IDENTITY(1,1) NOT NULL,
	[Member_Name] [nvarchar](50) NULL,
	[Member_Sex] [bit] NULL,
	[Member_Phone] [nvarchar](50) NULL,
	[Member_Address] [nvarchar](200) NULL,
	[Member_Postcode] [nvarchar](20) NULL,
	[Member_Email] [nvarchar](100) NULL,
	[Member_Birthday] [datetime] NULL,
	[Member_Notice] [nvarchar](200) NULL,
	[Member_State] [smallint] NULL,
	[Member_RegDate] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Member_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_OperateLog]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_OperateLog](
	[OperateLog_ID] [int] IDENTITY(1,1) NOT NULL,
	[OperateLog_ChangVar] [decimal](18, 2) NULL,
	[OperateLog_Notice] [nvarchar](200) NULL,
	[Card_No] [nvarchar](50) NULL,
	[Shop_ID] [int] NULL,
	[OperateLog_GoodsAmount] [int] NULL,
	[Administrator_ID] [int] NULL,
	[Goods_ID] [int] NULL,
	[Operation_ID] [int] NULL,
	[OperateLog_Date] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[OperateLog_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Operation]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Operation](
	[Operation_ID] [int] IDENTITY(1,1) NOT NULL,
	[Operation_Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[Operation_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Shop]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Shop](
	[Shop_ID] [int] IDENTITY(1,1) NOT NULL,
	[Shop_Name] [nvarchar](100) NULL,
	[ShopType_ID] [int] NULL,
	[Shop_State] [smallint] NULL,
PRIMARY KEY CLUSTERED 
(
	[Shop_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_ShopType]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_ShopType](
	[ShopType_ID] [int] IDENTITY(1,1) NOT NULL,
	[ShopType_Name] [nvarchar](50) NULL,
PRIMARY KEY CLUSTERED 
(
	[ShopType_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[yz_Store]    Script Date: 2025/6/23 1:47:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[yz_Store](
	[Store_ID] [int] IDENTITY(1,1) NOT NULL,
	[Shop_ID] [int] NULL,
	[Goods_ID] [int] NULL,
	[Store_Amount] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[Store_ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[yz_Administrator] ON 
GO
INSERT [dbo].[yz_Administrator] ([Administrator_ID], [Administrator_Name], [Administrator_LoginName], [Administrator_Password], [AdministratorType_ID], [Administrator_LoginCount], [Administrator_State], [Administrator_LastLoginTime]) VALUES (1, N'张三', N'zhangsan', N'123456', 1, 10, 1, CAST(N'2025-06-23T00:23:09.607' AS DateTime))
GO
INSERT [dbo].[yz_Administrator] ([Administrator_ID], [Administrator_Name], [Administrator_LoginName], [Administrator_Password], [AdministratorType_ID], [Administrator_LoginCount], [Administrator_State], [Administrator_LastLoginTime]) VALUES (2, N'李四', N'lisi', N'654321', 2, 5, 1, CAST(N'2025-06-23T00:23:09.607' AS DateTime))
GO
INSERT [dbo].[yz_Administrator] ([Administrator_ID], [Administrator_Name], [Administrator_LoginName], [Administrator_Password], [AdministratorType_ID], [Administrator_LoginCount], [Administrator_State], [Administrator_LastLoginTime]) VALUES (3, N'JIELIANG XU', N'ysfb2000', N'2347', 1, 7, 1, CAST(N'2025-06-23T01:34:43.633' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[yz_Administrator] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_AdministratorType] ON 
GO
INSERT [dbo].[yz_AdministratorType] ([AdministratorType_ID], [AdministratorType_Name]) VALUES (1, N'超级管理员')
GO
INSERT [dbo].[yz_AdministratorType] ([AdministratorType_ID], [AdministratorType_Name]) VALUES (2, N'普通管理员')
GO
INSERT [dbo].[yz_AdministratorType] ([AdministratorType_ID], [AdministratorType_Name]) VALUES (3, N'Super Administor')
GO
SET IDENTITY_INSERT [dbo].[yz_AdministratorType] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_AdminLog] ON 
GO
INSERT [dbo].[yz_AdminLog] ([AdminLog_ID], [AdminType_ID], [Administrator_ID], [Shop_ID]) VALUES (1, 1, 1, 1)
GO
INSERT [dbo].[yz_AdminLog] ([AdminLog_ID], [AdminType_ID], [Administrator_ID], [Shop_ID]) VALUES (2, 2, 2, 2)
GO
INSERT [dbo].[yz_AdminLog] ([AdminLog_ID], [AdminType_ID], [Administrator_ID], [Shop_ID]) VALUES (3, 1, 3, 1)
GO
INSERT [dbo].[yz_AdminLog] ([AdminLog_ID], [AdminType_ID], [Administrator_ID], [Shop_ID]) VALUES (4, 1, 3, 1)
GO
INSERT [dbo].[yz_AdminLog] ([AdminLog_ID], [AdminType_ID], [Administrator_ID], [Shop_ID]) VALUES (5, 1, 3, 1)
GO
INSERT [dbo].[yz_AdminLog] ([AdminLog_ID], [AdminType_ID], [Administrator_ID], [Shop_ID]) VALUES (6, 1, 3, 1)
GO
INSERT [dbo].[yz_AdminLog] ([AdminLog_ID], [AdminType_ID], [Administrator_ID], [Shop_ID]) VALUES (7, 1, 3, 1)
GO
INSERT [dbo].[yz_AdminLog] ([AdminLog_ID], [AdminType_ID], [Administrator_ID], [Shop_ID]) VALUES (8, 1, 3, 1)
GO
INSERT [dbo].[yz_AdminLog] ([AdminLog_ID], [AdminType_ID], [Administrator_ID], [Shop_ID]) VALUES (9, 8, 3, 1)
GO
INSERT [dbo].[yz_AdminLog] ([AdminLog_ID], [AdminType_ID], [Administrator_ID], [Shop_ID]) VALUES (10, 1, 3, 1)
GO
SET IDENTITY_INSERT [dbo].[yz_AdminLog] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_Authority] ON 
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (1, N'会员管理', N'YZ_MEMBER')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (2, N'积分管理', N'YZ_POINT')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (3, N'会员管理', N'YZ_MEMBER')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (4, N'积分管理', N'YZ_POINT')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (5, N'卡片管理', N'YZ_CARD')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (6, N'门店管理', N'YZ_SHOP')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (7, N'商品管理', N'YZ_GOODS')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (8, N'库存管理', N'YZ_STORE')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (9, N'操作日志', N'YZ_OPERATELOG')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (10, N'管理员管理', N'YZ_ADMIN')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (11, N'权限分配', N'YZ_AUTHORITY')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (12, N'报表查看', N'YZ_REPORT')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (13, N'系统设置', N'YZ_SETTING')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (14, N'数据备份', N'YZ_BACKUP')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (15, N'数据恢复', N'YZ_RESTORE')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (16, N'会员注册', N'YZ_MEMBER_REG')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (17, N'会员注销', N'YZ_MEMBER_CANCEL')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (18, N'会员挂失', N'YZ_MEMBER_LOST')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (19, N'会员解挂', N'YZ_MEMBER_UNLOST')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (20, N'积分兑换', N'YZ_POINT_EXCHANGE')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (21, N'积分调整', N'YZ_POINT_ADJUST')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (22, N'商品入库', N'YZ_GOODS_IN')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (23, N'商品出库', N'YZ_GOODS_OUT')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (24, N'库存盘点', N'YZ_STORE_CHECK')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (25, N'门店调拨', N'YZ_SHOP_TRANSFER')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (26, N'管理员登录', N'YZ_ADMIN_LOGIN')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (27, N'管理员登出', N'YZ_ADMIN_LOGOUT')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (28, N'密码修改', N'YZ_PASSWORD_CHANGE')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (29, N'系统日志', N'YZ_SYSLOG')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (30, N'会员信息修改', N'YZ_MEMBER_EDIT')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (31, N'卡片补办', N'YZ_CARD_REISSUE')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (32, N'卡片激活', N'YZ_CARD_ACTIVE')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (33, N'卡片冻结', N'YZ_CARD_FREEZE')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (34, N'卡片解冻', N'YZ_CARD_UNFREEZE')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (35, N'短信通知', N'YZ_SMS')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (36, N'邮件通知', N'YZ_EMAIL')
GO
INSERT [dbo].[yz_Authority] ([Authority_ID], [Authority_Name], [Authority_Control]) VALUES (37, N'自定义权限', N'YZ_CUSTOM')
GO
SET IDENTITY_INSERT [dbo].[yz_Authority] OFF
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 1)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 2)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 3)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 4)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 5)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 6)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 7)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 8)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 9)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 10)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 11)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 12)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 13)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 14)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 15)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 16)
GO
INSERT [dbo].[yz_Authority_AdministratorType] ([AdministratorType_ID], [Authority_ID]) VALUES (1, 17)
GO
SET IDENTITY_INSERT [dbo].[yz_Brands] ON 
GO
INSERT [dbo].[yz_Brands] ([Brands_ID], [Brands_Name], [Brands_SQL], [Brands_CardNO_Length], [Brands_State], [Brands_Sort]) VALUES (1, N'洋葱', N'SELECT * FROM yz_Member', 8, 1, NULL)
GO
SET IDENTITY_INSERT [dbo].[yz_Brands] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_Card] ON 
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (1, N'100001', 1, CAST(100.00 AS Decimal(18, 2)), 1, 1)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (2, N'100002', 2, CAST(200.00 AS Decimal(18, 2)), 1, 2)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (3, N'00000001', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (4, N'00000002', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (5, N'00000003', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (6, N'00000004', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (7, N'00000005', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (8, N'00000006', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (9, N'00000007', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (10, N'00000008', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (11, N'00000009', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (12, N'00000010', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (13, N'00000011', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (14, N'00000012', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (15, N'00000013', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (16, N'00000014', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (17, N'00000015', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (18, N'00000016', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (19, N'00000017', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (20, N'00000018', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (21, N'00000019', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (22, N'00000020', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (23, N'00000021', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (24, N'00000022', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (25, N'00000023', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (26, N'00000024', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (27, N'00000025', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (28, N'00000026', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (29, N'00000027', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (30, N'00000028', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (31, N'00000029', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (32, N'00000030', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (33, N'00000031', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (34, N'00000032', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (35, N'00000033', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (36, N'00000034', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (37, N'00000035', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (38, N'00000036', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (39, N'00000037', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (40, N'00000038', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (41, N'00000039', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (42, N'00000040', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (43, N'00000041', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (44, N'00000042', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (45, N'00000043', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (46, N'00000044', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (47, N'00000045', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (48, N'00000046', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (49, N'00000047', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (50, N'00000048', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (51, N'00000049', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (52, N'00000050', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (53, N'00000051', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (54, N'00000052', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (55, N'00000053', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (56, N'00000054', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (57, N'00000055', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (58, N'00000056', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (59, N'00000057', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (60, N'00000058', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (61, N'00000059', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (62, N'00000060', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (63, N'00000061', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (64, N'00000062', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (65, N'00000063', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (66, N'00000064', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (67, N'00000065', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (68, N'00000066', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (69, N'00000067', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (70, N'00000068', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (71, N'00000069', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (72, N'00000070', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (73, N'00000071', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (74, N'00000072', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (75, N'00000073', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (76, N'00000074', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (77, N'00000075', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (78, N'00000076', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (79, N'00000077', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (80, N'00000078', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (81, N'00000079', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (82, N'00000080', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (83, N'00000081', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (84, N'00000082', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (85, N'00000083', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (86, N'00000084', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (87, N'00000085', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (88, N'00000086', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (89, N'00000087', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (90, N'00000088', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (91, N'00000089', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (92, N'00000090', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (93, N'00000091', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (94, N'00000092', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (95, N'00000093', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (96, N'00000094', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (97, N'00000095', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (98, N'00000096', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (99, N'00000097', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (100, N'00000098', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (101, N'00000099', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (102, N'00000100', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (103, N'00000101', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (104, N'00000102', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (105, N'00000103', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (106, N'00000104', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (107, N'00000105', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (108, N'00000106', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (109, N'00000107', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (110, N'00000108', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (111, N'00000109', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (112, N'00000110', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (113, N'00000111', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (114, N'00000112', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (115, N'00000113', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (116, N'00000114', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (117, N'00000115', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (118, N'00000116', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (119, N'00000117', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (120, N'00000118', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (121, N'00000119', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (122, N'00000120', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (123, N'00000121', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (124, N'00000122', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (125, N'00000123', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (126, N'00000124', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (127, N'00000125', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (128, N'00000126', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (129, N'00000127', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (130, N'00000128', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (131, N'00000129', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (132, N'00000130', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (133, N'00000131', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (134, N'00000132', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (135, N'00000133', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (136, N'00000134', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (137, N'00000135', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (138, N'00000136', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (139, N'00000137', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (140, N'00000138', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (141, N'00000139', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (142, N'00000140', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (143, N'00000141', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (144, N'00000142', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (145, N'00000143', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (146, N'00000144', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (147, N'00000145', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (148, N'00000146', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (149, N'00000147', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (150, N'00000148', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (151, N'00000149', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (152, N'00000150', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (153, N'00000151', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (154, N'00000152', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (155, N'00000153', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (156, N'00000154', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (157, N'00000155', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (158, N'00000156', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (159, N'00000157', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (160, N'00000158', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (161, N'00000159', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (162, N'00000160', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (163, N'00000161', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (164, N'00000162', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (165, N'00000163', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (166, N'00000164', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (167, N'00000165', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (168, N'00000166', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (169, N'00000167', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (170, N'00000168', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (171, N'00000169', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (172, N'00000170', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (173, N'00000171', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (174, N'00000172', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (175, N'00000173', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (176, N'00000174', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (177, N'00000175', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (178, N'00000176', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (179, N'00000177', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (180, N'00000178', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (181, N'00000179', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (182, N'00000180', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (183, N'00000181', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (184, N'00000182', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (185, N'00000183', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (186, N'00000184', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (187, N'00000185', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (188, N'00000186', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (189, N'00000187', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (190, N'00000188', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (191, N'00000189', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (192, N'00000190', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (193, N'00000191', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (194, N'00000192', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (195, N'00000193', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (196, N'00000194', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (197, N'00000195', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (198, N'00000196', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (199, N'00000197', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (200, N'00000198', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (201, N'00000199', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (202, N'00000200', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (203, N'00000201', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (204, N'00000202', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (205, N'00000203', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (206, N'00000204', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (207, N'00000205', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (208, N'00000206', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (209, N'00000207', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (210, N'00000208', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (211, N'00000209', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (212, N'00000210', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (213, N'00000211', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (214, N'00000212', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (215, N'00000213', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (216, N'00000214', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (217, N'00000215', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (218, N'00000216', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (219, N'00000217', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (220, N'00000218', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (221, N'00000219', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (222, N'00000220', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (223, N'00000221', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (224, N'00000222', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (225, N'00000223', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (226, N'00000224', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (227, N'00000225', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (228, N'00000226', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (229, N'00000227', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (230, N'00000228', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (231, N'00000229', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (232, N'00000230', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (233, N'00000231', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (234, N'00000232', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (235, N'00000233', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (236, N'00000234', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (237, N'00000235', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (238, N'00000236', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (239, N'00000237', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (240, N'00000238', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (241, N'00000239', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (242, N'00000240', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (243, N'00000241', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (244, N'00000242', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (245, N'00000243', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (246, N'00000244', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (247, N'00000245', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (248, N'00000246', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (249, N'00000247', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (250, N'00000248', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (251, N'00000249', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (252, N'00000250', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (253, N'00000251', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (254, N'00000252', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (255, N'00000253', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (256, N'00000254', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (257, N'00000255', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (258, N'00000256', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (259, N'00000257', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (260, N'00000258', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (261, N'00000259', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (262, N'00000260', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (263, N'00000261', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (264, N'00000262', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (265, N'00000263', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (266, N'00000264', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (267, N'00000265', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (268, N'00000266', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (269, N'00000267', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (270, N'00000268', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (271, N'00000269', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (272, N'00000270', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (273, N'00000271', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (274, N'00000272', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (275, N'00000273', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (276, N'00000274', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (277, N'00000275', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (278, N'00000276', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (279, N'00000277', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (280, N'00000278', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (281, N'00000279', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (282, N'00000280', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (283, N'00000281', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (284, N'00000282', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (285, N'00000283', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (286, N'00000284', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (287, N'00000285', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (288, N'00000286', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (289, N'00000287', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (290, N'00000288', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (291, N'00000289', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (292, N'00000290', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (293, N'00000291', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (294, N'00000292', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (295, N'00000293', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (296, N'00000294', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (297, N'00000295', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (298, N'00000296', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (299, N'00000297', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (300, N'00000298', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (301, N'00000299', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
INSERT [dbo].[yz_Card] ([Card_ID], [Card_No], [CardType_ID], [Card_Point], [Card_State], [Member_ID]) VALUES (302, N'00000300', 1, CAST(1.00 AS Decimal(18, 2)), 0, NULL)
GO
SET IDENTITY_INSERT [dbo].[yz_Card] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_CardState] ON 
GO
INSERT [dbo].[yz_CardState] ([CardState_ID], [CardState_Name]) VALUES (1, N'正常')
GO
INSERT [dbo].[yz_CardState] ([CardState_ID], [CardState_Name]) VALUES (2, N'挂失')
GO
INSERT [dbo].[yz_CardState] ([CardState_ID], [CardState_Name]) VALUES (3, N'注销')
GO
SET IDENTITY_INSERT [dbo].[yz_CardState] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_CardType] ON 
GO
INSERT [dbo].[yz_CardType] ([CardType_ID], [CardType_Name], [CardType_IntValue]) VALUES (1, N'普通卡', 1)
GO
INSERT [dbo].[yz_CardType] ([CardType_ID], [CardType_Name], [CardType_IntValue]) VALUES (2, N'金卡', 2)
GO
SET IDENTITY_INSERT [dbo].[yz_CardType] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_Events] ON 
GO
INSERT [dbo].[yz_Events] ([Events_ID], [Events_Name]) VALUES (1, N'入库')
GO
INSERT [dbo].[yz_Events] ([Events_ID], [Events_Name]) VALUES (2, N'出库')
GO
SET IDENTITY_INSERT [dbo].[yz_Events] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_Goods] ON 
GO
INSERT [dbo].[yz_Goods] ([Goods_ID], [Goods_Name], [Goods_Value], [Goods_Type], [Goods_Level], [Goods_State]) VALUES (1, N'苹果', CAST(3.50 AS Decimal(18, 2)), 1, 1, 1)
GO
INSERT [dbo].[yz_Goods] ([Goods_ID], [Goods_Name], [Goods_Value], [Goods_Type], [Goods_Level], [Goods_State]) VALUES (2, N'牙刷', CAST(2.00 AS Decimal(18, 2)), 2, 1, 1)
GO
SET IDENTITY_INSERT [dbo].[yz_Goods] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_GoodsInOut] ON 
GO
INSERT [dbo].[yz_GoodsInOut] ([GoodsInOut_ID], [GoodsInOut_Var], [Shop_ID], [Goods_ID], [Administrator_ID], [Events_ID], [GoodsInOut_Notice]) VALUES (1, 10, 1, 1, 1, 1, N'初始入库')
GO
INSERT [dbo].[yz_GoodsInOut] ([GoodsInOut_ID], [GoodsInOut_Var], [Shop_ID], [Goods_ID], [Administrator_ID], [Events_ID], [GoodsInOut_Notice]) VALUES (2, -2, 2, 2, 2, 2, N'售出2件')
GO
SET IDENTITY_INSERT [dbo].[yz_GoodsInOut] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_GoodsType] ON 
GO
INSERT [dbo].[yz_GoodsType] ([GoodsType_ID], [GoodsType_Name]) VALUES (1, N'食品')
GO
INSERT [dbo].[yz_GoodsType] ([GoodsType_ID], [GoodsType_Name]) VALUES (2, N'日用品')
GO
SET IDENTITY_INSERT [dbo].[yz_GoodsType] OFF
GO
INSERT [dbo].[yz_Mac] ([Mac_ID], [Shop_ID], [PrinterName], [PointPrint], [GoodsPrint]) VALUES (N'C8:8A:9A:52:7F:92', 1, N'HP LaserJet', 1, 1)
GO
INSERT [dbo].[yz_mac_Authenticate] ([mac_ID], [mac_state]) VALUES (N'C8:8A:9A:52:7F:92', 1)
GO
SET IDENTITY_INSERT [dbo].[yz_Member] ON 
GO
INSERT [dbo].[yz_Member] ([Member_ID], [Member_Name], [Member_Sex], [Member_Phone], [Member_Address], [Member_Postcode], [Member_Email], [Member_Birthday], [Member_Notice], [Member_State], [Member_RegDate]) VALUES (1, N'王五', 1, N'13800138000', N'上海市南京路100号', N'200000', N'wangwu@example.com', CAST(N'1990-01-01T00:00:00.000' AS DateTime), N'无', 1, CAST(N'2025-06-23T00:23:09.610' AS DateTime))
GO
INSERT [dbo].[yz_Member] ([Member_ID], [Member_Name], [Member_Sex], [Member_Phone], [Member_Address], [Member_Postcode], [Member_Email], [Member_Birthday], [Member_Notice], [Member_State], [Member_RegDate]) VALUES (2, N'赵六', 0, N'13900139000', N'上海市人民广场1号', N'200001', N'zhaoliu@example.com', CAST(N'1985-05-20T00:00:00.000' AS DateTime), N'无', 1, CAST(N'2025-06-23T00:23:09.610' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[yz_Member] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_OperateLog] ON 
GO
INSERT [dbo].[yz_OperateLog] ([OperateLog_ID], [OperateLog_ChangVar], [OperateLog_Notice], [Card_No], [Shop_ID], [OperateLog_GoodsAmount], [Administrator_ID], [Goods_ID], [Operation_ID], [OperateLog_Date]) VALUES (1, CAST(100.00 AS Decimal(18, 2)), N'注册赠送', N'100001', 1, 0, 1, NULL, 1, CAST(N'2025-06-23T00:23:09.613' AS DateTime))
GO
INSERT [dbo].[yz_OperateLog] ([OperateLog_ID], [OperateLog_ChangVar], [OperateLog_Notice], [Card_No], [Shop_ID], [OperateLog_GoodsAmount], [Administrator_ID], [Goods_ID], [Operation_ID], [OperateLog_Date]) VALUES (2, CAST(-20.00 AS Decimal(18, 2)), N'兑换牙刷', N'100002', 2, 1, 2, 2, 3, CAST(N'2025-06-23T00:23:09.613' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[yz_OperateLog] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_Operation] ON 
GO
INSERT [dbo].[yz_Operation] ([Operation_ID], [Operation_Name]) VALUES (1, N'注册')
GO
INSERT [dbo].[yz_Operation] ([Operation_ID], [Operation_Name]) VALUES (2, N'消费')
GO
INSERT [dbo].[yz_Operation] ([Operation_ID], [Operation_Name]) VALUES (3, N'积分兑换')
GO
SET IDENTITY_INSERT [dbo].[yz_Operation] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_Shop] ON 
GO
INSERT [dbo].[yz_Shop] ([Shop_ID], [Shop_Name], [ShopType_ID], [Shop_State]) VALUES (1, N'南京路店', 1, 1)
GO
INSERT [dbo].[yz_Shop] ([Shop_ID], [Shop_Name], [ShopType_ID], [Shop_State]) VALUES (2, N'人民广场店', 2, 1)
GO
SET IDENTITY_INSERT [dbo].[yz_Shop] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_ShopType] ON 
GO
INSERT [dbo].[yz_ShopType] ([ShopType_ID], [ShopType_Name]) VALUES (1, N'直营店')
GO
INSERT [dbo].[yz_ShopType] ([ShopType_ID], [ShopType_Name]) VALUES (2, N'加盟店')
GO
SET IDENTITY_INSERT [dbo].[yz_ShopType] OFF
GO
SET IDENTITY_INSERT [dbo].[yz_Store] ON 
GO
INSERT [dbo].[yz_Store] ([Store_ID], [Shop_ID], [Goods_ID], [Store_Amount]) VALUES (1, 1, 1, 50)
GO
INSERT [dbo].[yz_Store] ([Store_ID], [Shop_ID], [Goods_ID], [Store_Amount]) VALUES (2, 2, 2, 30)
GO
SET IDENTITY_INSERT [dbo].[yz_Store] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [UQ__yz_Card__45AA6CB11F99084A]    Script Date: 2025/6/23 1:47:16 ******/
ALTER TABLE [dbo].[yz_Card] ADD UNIQUE NONCLUSTERED 
(
	[Card_No] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[yz_Brands] ADD  DEFAULT ((0)) FOR [Brands_Sort]
GO
USE [master]
GO
ALTER DATABASE [YoungZone] SET  READ_WRITE 
GO
