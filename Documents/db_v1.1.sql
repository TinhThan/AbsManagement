USE [AbsManagement]
GO
/****** Object:  Table [dbo].[BangQuangCao]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BangQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdDiemDatQuangCao] [int] NOT NULL,
	[IdLoaiBangQuangCao] [int] NOT NULL,
	[KichThuoc] [nvarchar](200) NOT NULL,
	[DanhSachHinhAnh] [varchar](max) NOT NULL,
	[NgayHetHan] [datetimeoffset](7) NOT NULL,
	[IdTinhTrang] [varchar](50) NOT NULL,
	[NgayCapNhat] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_BangQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaoCaoViPham]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaoCaoViPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCanBoXuLy] [int] NULL,
	[HoTen] [nvarchar](200) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[SoDienThoai] [varbinary](50) NOT NULL,
	[IdHinhThucBaoCao] [int] NOT NULL,
	[NoiDungXuLy] [nvarchar](max) NULL,
	[NoiDung] [nvarchar](max) NOT NULL,
	[ViTri] [varchar](50) NOT NULL,
	[DanhSachHinhAnh] [varchar](50) NOT NULL,
	[IdTinhTrang] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_BaoCaoViPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CanBo]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CanBo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TaiKhoan] [varchar](50) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[HoTen] [nvarchar](200) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[MatKhau] [varchar](max) NOT NULL,
 CONSTRAINT [PK_CanBo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietChinhSuaBangQuangCao]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietChinhSuaBangQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdBangQuangCao] [int] NOT NULL,
	[IdPhieuChinhSua] [int] NOT NULL,
	[IdDiemDatQuangCaoMoi] [int] NOT NULL,
	[IdLoaiBangQuangCaoMoi] [int] NOT NULL,
	[DanhSachHinhAnhMoi] [varchar](max) NOT NULL,
	[NgayHetHanMoi] [datetimeoffset](7) NOT NULL,
	[IdTinhTrangMoi] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ChiTietChinhSuaBangQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ChiTietPhieuChinhSuaDiemDatQuangCao]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietPhieuChinhSuaDiemDatQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdPhieuSua] [int] NOT NULL,
	[IdDiemDatQuangCao] [int] NOT NULL,
	[DiaChiMoi] [nvarchar](max) NOT NULL,
	[PhuongMoi] [nvarchar](max) NOT NULL,
	[QuanMoi] [nvarchar](max) NOT NULL,
	[ViTriMoi] [nvarchar](max) NOT NULL,
	[IdLoaiViTriMoi] [int] NOT NULL,
	[IdHinhThucMoi] [int] NOT NULL,
	[DanhSachHinhAnhMoi] [varchar](max) NOT NULL,
	[IdTinhTrangMoi] [varchar](50) NOT NULL,
 CONSTRAINT [PK_ChiTietPhieuChinhSuaDiemDatQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiemDatQuangCao]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DiemDatQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DiaChi] [nvarchar](200) NOT NULL,
	[Phuong] [nvarchar](200) NOT NULL,
	[Quan] [nvarchar](200) NOT NULL,
	[ViTri] [nvarchar](50) NOT NULL,
	[IdLoaiViTri] [int] NOT NULL,
	[IdHinhThucQuangCao] [int] NOT NULL,
	[DanhSachHinhAnh] [varchar](200) NOT NULL,
	[IdTinhTrang] [varchar](50) NOT NULL,
 CONSTRAINT [PK_DiemDatQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HinhThucBaoCao]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HinhThucBaoCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ma] [varchar](50) NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_HinhThucBaoCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HinhThucQuangCao]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HinhThucQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ma] [varchar](50) NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_HinhThucQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiBangQuangCao]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiBangQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ma] [varchar](50) NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_LoaiBangQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiViTri]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LoaiViTri](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ma] [varchar](50) NOT NULL,
	[Ten] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_LoaiViTri] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuChinhSuaBangQuangCao]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuChinhSuaBangQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCanBoTao] [int] NOT NULL,
	[IdCanBoDuyet] [int] NOT NULL,
	[LyDo] [nvarchar](max) NOT NULL,
	[NgayCapNhat] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_PhieuChinhSuaBangQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuChinhSuaDiemDatQuangCao]    Script Date: 11/6/2023 9:15:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuChinhSuaDiemDatQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCanBoTao] [int] NOT NULL,
	[IdCanBoDuyet] [int] NULL,
	[LyDo] [nvarchar](max) NOT NULL,
	[NgayCapNhat] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_PhieuChinhSuaDiemDatQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[HinhThucBaoCao] ON 

INSERT [dbo].[HinhThucBaoCao] ([Id], [Ma], [Ten]) VALUES (1, N'DangKyNoiDung', N'Đăng ký nội dung')
INSERT [dbo].[HinhThucBaoCao] ([Id], [Ma], [Ten]) VALUES (2, N'DongGopYKien', N'Đóng góp ý kiến')
INSERT [dbo].[HinhThucBaoCao] ([Id], [Ma], [Ten]) VALUES (3, N'GiaiDapThacMac', N'Giải đáp thắc mắc')
INSERT [dbo].[HinhThucBaoCao] ([Id], [Ma], [Ten]) VALUES (4, N'ToGiacSaiPham', N'Tố giác sai phạm')
SET IDENTITY_INSERT [dbo].[HinhThucBaoCao] OFF
GO
SET IDENTITY_INSERT [dbo].[HinhThucQuangCao] ON 

INSERT [dbo].[HinhThucQuangCao] ([Id], [Ma], [Ten]) VALUES (1, N'CoDongChinhTri', N'Cổ động chính trị')
INSERT [dbo].[HinhThucQuangCao] ([Id], [Ma], [Ten]) VALUES (2, N'QuangCaoThuongMai', N'Quảng cáo thương mại')
INSERT [dbo].[HinhThucQuangCao] ([Id], [Ma], [Ten]) VALUES (3, N'XaHoiHoa', N'Xã hội hóa')
SET IDENTITY_INSERT [dbo].[HinhThucQuangCao] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiBangQuangCao] ON 

INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (1, N'CongChao', N'Cổng chào')
INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (2, N'DienTuOpTuong', N'Màn hình điện tử ốp tường')
INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (3, N'Hiflex', N'Trụ bảng hiflex')
INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (4, N'HiflexOpTuong', N'Bảng hiflex ốp tường')
INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (5, N'Led', N'Trụ màn hình điện tử LED')
INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (6, N'Pano', N'Trụ/Cụm pano')
INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (7, N'TruHopDen', N'Trụ hộp đèn')
INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (8, N'TrungTamThuongMai', N'Trung tâm thương mại')
INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (9, N'TruTreoBangRonDoc', N'Trụ treo băng rôn dọc')
INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (10, N'TruTreoBangRonNang', N'Trụ treo băng rôn ngang')
SET IDENTITY_INSERT [dbo].[LoaiBangQuangCao] OFF
GO
SET IDENTITY_INSERT [dbo].[LoaiViTri] ON 

INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (1, N'CayXang', N'Cây xăng')
INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (2, N'Cho', N'Chợ')
INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (3, N'DatCong', N'Đất công/Công viên/Hành lang an toàn giao thông')
INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (4, N'DatTuNhan', N'Đất tư nhân/Nhà ở riêng lẻ')
INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (5, N'NhaChoXeBuyt', N'Nhà chờ xe buýt')
INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (6, N'TrungTamThuongMai', N'Trung tâm thương mại')
SET IDENTITY_INSERT [dbo].[LoaiViTri] OFF
GO
ALTER TABLE [dbo].[BangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_BangQuangCao_DiemDatQuangCao] FOREIGN KEY([IdDiemDatQuangCao])
REFERENCES [dbo].[DiemDatQuangCao] ([Id])
GO
ALTER TABLE [dbo].[BangQuangCao] CHECK CONSTRAINT [FK_BangQuangCao_DiemDatQuangCao]
GO
ALTER TABLE [dbo].[BangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_BangQuangCao_LoaiBangQuangCao] FOREIGN KEY([IdLoaiBangQuangCao])
REFERENCES [dbo].[LoaiBangQuangCao] ([Id])
GO
ALTER TABLE [dbo].[BangQuangCao] CHECK CONSTRAINT [FK_BangQuangCao_LoaiBangQuangCao]
GO
ALTER TABLE [dbo].[BaoCaoViPham]  WITH CHECK ADD  CONSTRAINT [FK_BaoCaoViPham_CanBo] FOREIGN KEY([IdCanBoXuLy])
REFERENCES [dbo].[CanBo] ([Id])
GO
ALTER TABLE [dbo].[BaoCaoViPham] CHECK CONSTRAINT [FK_BaoCaoViPham_CanBo]
GO
ALTER TABLE [dbo].[BaoCaoViPham]  WITH CHECK ADD  CONSTRAINT [FK_BaoCaoViPham_HinhThucBaoCao] FOREIGN KEY([IdHinhThucBaoCao])
REFERENCES [dbo].[HinhThucBaoCao] ([Id])
GO
ALTER TABLE [dbo].[BaoCaoViPham] CHECK CONSTRAINT [FK_BaoCaoViPham_HinhThucBaoCao]
GO
ALTER TABLE [dbo].[ChiTietChinhSuaBangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietChinhSuaBangQuangCao_BangQuangCao] FOREIGN KEY([IdBangQuangCao])
REFERENCES [dbo].[BangQuangCao] ([Id])
GO
ALTER TABLE [dbo].[ChiTietChinhSuaBangQuangCao] CHECK CONSTRAINT [FK_ChiTietChinhSuaBangQuangCao_BangQuangCao]
GO
ALTER TABLE [dbo].[ChiTietChinhSuaBangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietChinhSuaBangQuangCao_DiemDatQuangCao] FOREIGN KEY([IdDiemDatQuangCaoMoi])
REFERENCES [dbo].[DiemDatQuangCao] ([Id])
GO
ALTER TABLE [dbo].[ChiTietChinhSuaBangQuangCao] CHECK CONSTRAINT [FK_ChiTietChinhSuaBangQuangCao_DiemDatQuangCao]
GO
ALTER TABLE [dbo].[ChiTietChinhSuaBangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietChinhSuaBangQuangCao_LoaiBangQuangCao] FOREIGN KEY([IdLoaiBangQuangCaoMoi])
REFERENCES [dbo].[LoaiBangQuangCao] ([Id])
GO
ALTER TABLE [dbo].[ChiTietChinhSuaBangQuangCao] CHECK CONSTRAINT [FK_ChiTietChinhSuaBangQuangCao_LoaiBangQuangCao]
GO
ALTER TABLE [dbo].[ChiTietChinhSuaBangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietChinhSuaBangQuangCao_PhieuChinhSuaBangQuangCao] FOREIGN KEY([IdPhieuChinhSua])
REFERENCES [dbo].[PhieuChinhSuaBangQuangCao] ([Id])
GO
ALTER TABLE [dbo].[ChiTietChinhSuaBangQuangCao] CHECK CONSTRAINT [FK_ChiTietChinhSuaBangQuangCao_PhieuChinhSuaBangQuangCao]
GO
ALTER TABLE [dbo].[ChiTietPhieuChinhSuaDiemDatQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuChinhSuaDiemDatQuangCao_DiemDatQuangCao] FOREIGN KEY([IdDiemDatQuangCao])
REFERENCES [dbo].[DiemDatQuangCao] ([Id])
GO
ALTER TABLE [dbo].[ChiTietPhieuChinhSuaDiemDatQuangCao] CHECK CONSTRAINT [FK_ChiTietPhieuChinhSuaDiemDatQuangCao_DiemDatQuangCao]
GO
ALTER TABLE [dbo].[ChiTietPhieuChinhSuaDiemDatQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuChinhSuaDiemDatQuangCao_HinhThucQuangCao] FOREIGN KEY([IdHinhThucMoi])
REFERENCES [dbo].[HinhThucQuangCao] ([Id])
GO
ALTER TABLE [dbo].[ChiTietPhieuChinhSuaDiemDatQuangCao] CHECK CONSTRAINT [FK_ChiTietPhieuChinhSuaDiemDatQuangCao_HinhThucQuangCao]
GO
ALTER TABLE [dbo].[ChiTietPhieuChinhSuaDiemDatQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuChinhSuaDiemDatQuangCao_LoaiViTri] FOREIGN KEY([IdLoaiViTriMoi])
REFERENCES [dbo].[LoaiViTri] ([Id])
GO
ALTER TABLE [dbo].[ChiTietPhieuChinhSuaDiemDatQuangCao] CHECK CONSTRAINT [FK_ChiTietPhieuChinhSuaDiemDatQuangCao_LoaiViTri]
GO
ALTER TABLE [dbo].[ChiTietPhieuChinhSuaDiemDatQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietPhieuChinhSuaDiemDatQuangCao_PhieuChinhSuaDiemDatQuangCao] FOREIGN KEY([IdPhieuSua])
REFERENCES [dbo].[PhieuChinhSuaDiemDatQuangCao] ([Id])
GO
ALTER TABLE [dbo].[ChiTietPhieuChinhSuaDiemDatQuangCao] CHECK CONSTRAINT [FK_ChiTietPhieuChinhSuaDiemDatQuangCao_PhieuChinhSuaDiemDatQuangCao]
GO
ALTER TABLE [dbo].[DiemDatQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_DiemDatQuangCao_HinhThucQuangCao] FOREIGN KEY([IdHinhThucQuangCao])
REFERENCES [dbo].[HinhThucQuangCao] ([Id])
GO
ALTER TABLE [dbo].[DiemDatQuangCao] CHECK CONSTRAINT [FK_DiemDatQuangCao_HinhThucQuangCao]
GO
ALTER TABLE [dbo].[DiemDatQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_DiemDatQuangCao_LoaiViTri] FOREIGN KEY([IdLoaiViTri])
REFERENCES [dbo].[LoaiViTri] ([Id])
GO
ALTER TABLE [dbo].[DiemDatQuangCao] CHECK CONSTRAINT [FK_DiemDatQuangCao_LoaiViTri]
GO
ALTER TABLE [dbo].[PhieuChinhSuaBangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChinhSuaBangQuangCao_CanBoDuyet] FOREIGN KEY([IdCanBoDuyet])
REFERENCES [dbo].[CanBo] ([Id])
GO
ALTER TABLE [dbo].[PhieuChinhSuaBangQuangCao] CHECK CONSTRAINT [FK_PhieuChinhSuaBangQuangCao_CanBoDuyet]
GO
ALTER TABLE [dbo].[PhieuChinhSuaBangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChinhSuaBangQuangCao_CanBoTao] FOREIGN KEY([IdCanBoTao])
REFERENCES [dbo].[CanBo] ([Id])
GO
ALTER TABLE [dbo].[PhieuChinhSuaBangQuangCao] CHECK CONSTRAINT [FK_PhieuChinhSuaBangQuangCao_CanBoTao]
GO
ALTER TABLE [dbo].[PhieuChinhSuaDiemDatQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChinhSuaDiemDatQuangCao_CanBoDuyet] FOREIGN KEY([IdCanBoDuyet])
REFERENCES [dbo].[CanBo] ([Id])
GO
ALTER TABLE [dbo].[PhieuChinhSuaDiemDatQuangCao] CHECK CONSTRAINT [FK_PhieuChinhSuaDiemDatQuangCao_CanBoDuyet]
GO
ALTER TABLE [dbo].[PhieuChinhSuaDiemDatQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_PhieuChinhSuaDiemDatQuangCao_CanBoTao] FOREIGN KEY([IdCanBoTao])
REFERENCES [dbo].[CanBo] ([Id])
GO
ALTER TABLE [dbo].[PhieuChinhSuaDiemDatQuangCao] CHECK CONSTRAINT [FK_PhieuChinhSuaDiemDatQuangCao_CanBoTao]
GO
