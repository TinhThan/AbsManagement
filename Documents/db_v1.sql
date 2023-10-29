USE [AbsManagement]
GO
/****** Object:  Table [dbo].[BangQuangCao]    Script Date: 10/29/2023 12:32:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BangQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DiaChi] [nvarchar](200) NOT NULL,
	[Phuong] [nvarchar](200) NOT NULL,
	[Quan] [nvarchar](200) NOT NULL,
	[ViTri] [varchar](200) NOT NULL,
	[MaLoaiViTri] [varchar](50) NOT NULL,
	[MaHinhThucQuangCao] [varchar](50) NOT NULL,
	[MaLoaiBangQuangCao] [varchar](50) NOT NULL,
	[DanhSachHinhAnh] [varchar](200) NOT NULL,
	[KichThuoc] [varchar](50) NOT NULL,
	[NgayHetHan] [datetimeoffset](7) NOT NULL,
	[TrangThai] [int] NOT NULL,
	[NgayCapNhat] [datetimeoffset](7) NULL,
	[NhanVienCapNhat] [varchar](50) NULL,
 CONSTRAINT [PK_BangQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaoCaoViPham]    Script Date: 10/29/2023 12:32:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaoCaoViPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](200) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[SoDienThoai] [varbinary](50) NOT NULL,
	[MaHinhThucBaoCao] [varchar](50) NOT NULL,
	[NoiDung] [nvarchar](max) NOT NULL,
	[ViTri] [varchar](50) NOT NULL,
	[DanhSachHinhAnh] [varchar](50) NOT NULL,
 CONSTRAINT [PK_BaoCaoViPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HinhThucBaoCao]    Script Date: 10/29/2023 12:32:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HinhThucBaoCao](
	[Ma] [varchar](50) NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_HinhThucBaoCao] PRIMARY KEY CLUSTERED 
(
	[Ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HinhThucQuangCao]    Script Date: 10/29/2023 12:32:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HinhThucQuangCao](
	[Ma] [varchar](50) NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_HinhThuc] PRIMARY KEY CLUSTERED 
(
	[Ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiBangQuangCao]    Script Date: 10/29/2023 12:32:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiBangQuangCao](
	[Ma] [varchar](50) NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_LoaiBangQuangCao] PRIMARY KEY CLUSTERED 
(
	[Ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiViTri]    Script Date: 10/29/2023 12:32:38 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiViTri](
	[Ma] [varchar](50) NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_LoaiViTri] PRIMARY KEY CLUSTERED 
(
	[Ma] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BangQuangCao] ON 

INSERT [dbo].[BangQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [MaLoaiViTri], [MaHinhThucQuangCao], [MaLoaiBangQuangCao], [DanhSachHinhAnh], [KichThuoc], [NgayHetHan], [TrangThai], [NgayCapNhat], [NhanVienCapNhat]) VALUES (1, N'300 đường 3/2', N'10', N'Quận 10', N'["10","10"]', N'CayXang', N'CoDongChinhTri', N'DienTuOpTuong', N'[]', N'10cm x 20cm', CAST(N'2023-10-29T04:04:50.7160000+00:00' AS DateTimeOffset), 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[BangQuangCao] OFF
GO
INSERT [dbo].[HinhThucBaoCao] ([Ma], [Ten]) VALUES (N'DangKyNoiDung', N'Đăng ký nội dung')
INSERT [dbo].[HinhThucBaoCao] ([Ma], [Ten]) VALUES (N'DongGopYKien', N'Đóng góp ý kiến')
INSERT [dbo].[HinhThucBaoCao] ([Ma], [Ten]) VALUES (N'GiaiDapThacMac', N'Giải đáp thắc mắc')
INSERT [dbo].[HinhThucBaoCao] ([Ma], [Ten]) VALUES (N'ToGiacSaiPham', N'Tố giác sai phạm')
GO
INSERT [dbo].[HinhThucQuangCao] ([Ma], [Ten]) VALUES (N'CoDongChinhTri', N'Cổ động chính trị')
INSERT [dbo].[HinhThucQuangCao] ([Ma], [Ten]) VALUES (N'QuangCaoThuongMai', N'Quảng cáo thương mại')
INSERT [dbo].[HinhThucQuangCao] ([Ma], [Ten]) VALUES (N'XaHoiHoa', N'Xã hội hóa')
GO
INSERT [dbo].[LoaiBangQuangCao] ([Ma], [Ten]) VALUES (N'CongChao', N'Cổng chào')
INSERT [dbo].[LoaiBangQuangCao] ([Ma], [Ten]) VALUES (N'DienTuOpTuong', N'Màn hình điện tử ốp tường')
INSERT [dbo].[LoaiBangQuangCao] ([Ma], [Ten]) VALUES (N'Hiflex', N'Trụ bảng hiflex')
INSERT [dbo].[LoaiBangQuangCao] ([Ma], [Ten]) VALUES (N'HiflexOpTuong', N'Bảng hiflex ốp tường')
INSERT [dbo].[LoaiBangQuangCao] ([Ma], [Ten]) VALUES (N'Led', N'Trụ màn hình điện tử LED')
INSERT [dbo].[LoaiBangQuangCao] ([Ma], [Ten]) VALUES (N'Pano', N'Trụ/Cụm pano')
INSERT [dbo].[LoaiBangQuangCao] ([Ma], [Ten]) VALUES (N'TruHopDen', N'Trụ hộp đèn')
INSERT [dbo].[LoaiBangQuangCao] ([Ma], [Ten]) VALUES (N'TrungTamThuongMai', N'Trung tâm thương mại')
INSERT [dbo].[LoaiBangQuangCao] ([Ma], [Ten]) VALUES (N'TruTreoBangRonDoc', N'Trụ treo băng rôn dọc')
INSERT [dbo].[LoaiBangQuangCao] ([Ma], [Ten]) VALUES (N'TruTreoBangRonNang', N'Trụ treo băng rôn ngang')
GO
INSERT [dbo].[LoaiViTri] ([Ma], [Ten]) VALUES (N'CayXang', N'Cây xăng')
INSERT [dbo].[LoaiViTri] ([Ma], [Ten]) VALUES (N'Cho', N'Chợ')
INSERT [dbo].[LoaiViTri] ([Ma], [Ten]) VALUES (N'DatCong', N'Đất công/Công viên/Hành lang an toàn giao thông')
INSERT [dbo].[LoaiViTri] ([Ma], [Ten]) VALUES (N'DatTuNhan', N'Đất tư nhân/Nhà ở riêng lẻ')
INSERT [dbo].[LoaiViTri] ([Ma], [Ten]) VALUES (N'NhaChoXeBuyt', N'Nhà chờ xe buýt')
INSERT [dbo].[LoaiViTri] ([Ma], [Ten]) VALUES (N'TrungTamThuongMai', N'Trung tâm thương mại')
GO
ALTER TABLE [dbo].[BangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_BangQuangCao_HinhThucQuangCao] FOREIGN KEY([MaHinhThucQuangCao])
REFERENCES [dbo].[HinhThucQuangCao] ([Ma])
GO
ALTER TABLE [dbo].[BangQuangCao] CHECK CONSTRAINT [FK_BangQuangCao_HinhThucQuangCao]
GO
ALTER TABLE [dbo].[BangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_BangQuangCao_LoaiBangQuangCao] FOREIGN KEY([MaLoaiBangQuangCao])
REFERENCES [dbo].[LoaiBangQuangCao] ([Ma])
GO
ALTER TABLE [dbo].[BangQuangCao] CHECK CONSTRAINT [FK_BangQuangCao_LoaiBangQuangCao]
GO
ALTER TABLE [dbo].[BangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_BangQuangCao_LoaiViTri] FOREIGN KEY([MaLoaiViTri])
REFERENCES [dbo].[LoaiViTri] ([Ma])
GO
ALTER TABLE [dbo].[BangQuangCao] CHECK CONSTRAINT [FK_BangQuangCao_LoaiViTri]
GO
ALTER TABLE [dbo].[BaoCaoViPham]  WITH CHECK ADD  CONSTRAINT [FK_BaoCaoViPham_HinhThucBaoCao] FOREIGN KEY([MaHinhThucBaoCao])
REFERENCES [dbo].[HinhThucBaoCao] ([Ma])
GO
ALTER TABLE [dbo].[BaoCaoViPham] CHECK CONSTRAINT [FK_BaoCaoViPham_HinhThucBaoCao]
GO
