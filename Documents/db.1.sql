USE [AbsManagement]
GO
/****** Object:  Table [dbo].[BangQuangCao]    Script Date: 12/27/2023 1:51:39 PM ******/
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
	[NgayBatDau] [datetimeoffset](7) NOT NULL,
 CONSTRAINT [PK_BangQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaoCaoViPham]    Script Date: 12/27/2023 1:51:39 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaoCaoViPham](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdCanBoXuLy] [int] NULL,
	[IdDiemDatQuangCao] [int] NULL,
	[IdBangQuangCao] [int] NULL,
	[HoTen] [nvarchar](200) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[SoDienThoai] [varchar](50) NOT NULL,
	[IdHinhThucBaoCao] [int] NOT NULL,
	[NoiDungXuLy] [nvarchar](max) NULL,
	[NoiDung] [nvarchar](max) NOT NULL,
	[ViTri] [varchar](50) NOT NULL,
	[DanhSachHinhAnh] [nvarchar](100) NOT NULL,
	[IdTinhTrang] [nvarchar](50) NOT NULL,
	[CreateDate] [datetime] NOT NULL,
	[DiaChi] [nvarchar](max) NULL,
	[Phuong] [nvarchar](max) NULL,
	[Quan] [nvarchar](max) NULL,
 CONSTRAINT [PK_BaoCaoViPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CanBo]    Script Date: 12/27/2023 1:51:40 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CanBo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[HoTen] [nvarchar](200) NOT NULL,
	[SoDienThoai] [nchar](20) NOT NULL,
	[NgaySinh] [datetimeoffset](7) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[MatKhau] [varchar](max) NOT NULL,
	[RefreshToken] [varchar](max) NULL,
	[RefreshTokenExpiryTime] [datetimeoffset](7) NULL,
	[NoiCongTac] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_CanBo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiemDatQuangCao]    Script Date: 12/27/2023 1:51:40 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HinhThucBaoCao]    Script Date: 12/27/2023 1:51:40 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[HinhThucQuangCao]    Script Date: 12/27/2023 1:51:40 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiBangQuangCao]    Script Date: 12/27/2023 1:51:41 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LoaiViTri]    Script Date: 12/27/2023 1:51:41 PM ******/
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
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuCapPhepSuaQuangCao]    Script Date: 12/27/2023 1:51:41 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuCapPhepSuaQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdDiemDat] [int] NOT NULL,
	[IdBangQuangCao] [int] NOT NULL,
	[NoiDung] [nvarchar](500) NOT NULL,
	[NgayGui] [datetimeoffset](7) NOT NULL,
	[TinhTrang] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK__PhieuCap__3214EC0720C35F10] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BangQuangCao] ON 

INSERT [dbo].[BangQuangCao] ([Id], [IdDiemDatQuangCao], [IdLoaiBangQuangCao], [KichThuoc], [DanhSachHinhAnh], [NgayHetHan], [IdTinhTrang], [NgayBatDau]) VALUES (2, 1, 1, N'10mx20cm', N'[]', CAST(N'2023-11-11T00:00:00.0000000+07:00' AS DateTimeOffset), N'DaQuyHoach', CAST(N'2023-11-11T00:00:00.0000000+07:00' AS DateTimeOffset))
INSERT [dbo].[BangQuangCao] ([Id], [IdDiemDatQuangCao], [IdLoaiBangQuangCao], [KichThuoc], [DanhSachHinhAnh], [NgayHetHan], [IdTinhTrang], [NgayBatDau]) VALUES (3, 2, 2, N'50cmx20cm', N'[]', CAST(N'2023-11-11T00:00:00.0000000+07:00' AS DateTimeOffset), N'DaQuyHoach', CAST(N'2023-11-11T00:00:00.0000000+07:00' AS DateTimeOffset))
SET IDENTITY_INSERT [dbo].[BangQuangCao] OFF
SET IDENTITY_INSERT [dbo].[BaoCaoViPham] ON 

INSERT [dbo].[BaoCaoViPham] ([Id], [IdCanBoXuLy], [IdDiemDatQuangCao], [IdBangQuangCao], [HoTen], [Email], [SoDienThoai], [IdHinhThucBaoCao], [NoiDungXuLy], [NoiDung], [ViTri], [DanhSachHinhAnh], [IdTinhTrang], [CreateDate], [DiaChi], [Phuong], [Quan]) VALUES (1, NULL, NULL, NULL, N'Thân Văn Đức Tính', N'ductinh280499@gmail.com', N'9281928192     ', 1, NULL, N'<p>Nhập nội dung tại đ&acirc;y</p>', N'[10.762900,106.684563]', N'["vnf-quang-cao-la-gi.jpg"]', N'ChuaXuLy', CAST(N'2023-12-24T21:47:37.217' AS DateTime), NULL, NULL, NULL)
SET IDENTITY_INSERT [dbo].[BaoCaoViPham] OFF
SET IDENTITY_INSERT [dbo].[CanBo] ON 

INSERT [dbo].[CanBo] ([Id], [Email], [HoTen], [SoDienThoai], [NgaySinh], [Role], [MatKhau], [RefreshToken], [RefreshTokenExpiryTime], [NoiCongTac]) VALUES (1, N'ductinh@gmail.com', N'Đức Tính', N'093203921111        ', CAST(N'2023-04-19T17:00:00.0000000+00:00' AS DateTimeOffset), N'CanBoSo', N'x+tceDNc0SCdiaVEdvSmII/z/Wmwyk2p10MdaZFTEtE=', N'33KMC/0wKR168n+yLZTjwET4RJ36Hy1uvPeKoc8CTDUgGJzqvxPbJ4uCR6xr9bH4vQ6khI+M83YxZcZixgo8CQ==', CAST(N'2023-12-22T12:12:20.2857778+00:00' AS DateTimeOffset), N'[]')
INSERT [dbo].[CanBo] ([Id], [Email], [HoTen], [SoDienThoai], [NgaySinh], [Role], [MatKhau], [RefreshToken], [RefreshTokenExpiryTime], [NoiCongTac]) VALUES (2, N'22424018@student.hcmus.edu.vn', N'Thân Văn Đức Tính', N'9281928192          ', CAST(N'2023-11-01T15:14:08.6900000+00:00' AS DateTimeOffset), N'CanBoSo', N'x+tceDNc0SCdiaVEdvSmII/z/Wmwyk2p10MdaZFTEtE=', N'sRF7MnvkACIwvCKaNBPHWhvO8jlLKSBkcUK/snCYIS4zKmL1CVot36wm5kukTJlBSv9UEVhkdl4oME8ZAghpWA==', CAST(N'2023-12-05T15:29:35.4831640+00:00' AS DateTimeOffset), N'[]')
INSERT [dbo].[CanBo] ([Id], [Email], [HoTen], [SoDienThoai], [NgaySinh], [Role], [MatKhau], [RefreshToken], [RefreshTokenExpiryTime], [NoiCongTac]) VALUES (3, N'ductinh280499@gmail.com', N'Đức Tính', N'98219281            ', CAST(N'2023-11-09T15:46:57.9930000+00:00' AS DateTimeOffset), N'CanBoQuan', N'x+tceDNc0SCdiaVEdvSmII/z/Wmwyk2p10MdaZFTEtE=', N'hlH61HxN5GpZ821gNMDoqHzsg/w+4dLtgYGuPL3DtLUWz1vU3P1gJ0hJi+atu5fmwQe3U4j1sdUghG3lah8jeg==', CAST(N'2023-12-22T12:12:09.5323977+00:00' AS DateTimeOffset), N'[]')
INSERT [dbo].[CanBo] ([Id], [Email], [HoTen], [SoDienThoai], [NgaySinh], [Role], [MatKhau], [RefreshToken], [RefreshTokenExpiryTime], [NoiCongTac]) VALUES (4, N'phuonglinh10032000@gmail.com', N'Đức tính', N'121212121           ', CAST(N'2023-11-15T15:49:17.5990000+00:00' AS DateTimeOffset), N'CanBoSo', N'x+tceDNc0SCdiaVEdvSmII/z/Wmwyk2p10MdaZFTEtE=', NULL, NULL, N'[]')
SET IDENTITY_INSERT [dbo].[CanBo] OFF
SET IDENTITY_INSERT [dbo].[DiemDatQuangCao] ON 

INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1, N'235 Đ. Nguyễn Văn Cừ, Phường Nguyễn Cư Trinh, Quận 1, Thành phố Hồ Chí Minh, Việt Nam', N'Phường Nguyễn Cư Trinh', N'Quận 1', N'[106.682462, 10.764324]', 1, 1, N'[]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (2, N'227 Đ. Nguyễn Văn Cừ, Phường 4, Quận 5, Thành phố Hồ Chí Minh, Việt Nam', N'Phường 4', N'Quận 5', N'[106.682475, 10.762828]', 2, 2, N'[]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1004, N'235 Đ. Nguyễn Văn Cừ, Phường 4, Quận 5, Thành phố Hồ Chí Minh, Việt Nam', N'Phường 4', N'Quận 5', N'[106.681473, 10.763485]', 1, 2, N'[]', N'DaQuyHoach')
SET IDENTITY_INSERT [dbo].[DiemDatQuangCao] OFF
SET IDENTITY_INSERT [dbo].[HinhThucBaoCao] ON 

INSERT [dbo].[HinhThucBaoCao] ([Id], [Ma], [Ten]) VALUES (1, N'DangKyNoiDung', N'Đăng ký nội dung')
INSERT [dbo].[HinhThucBaoCao] ([Id], [Ma], [Ten]) VALUES (2, N'DongGopYKien', N'Đóng góp ý kiến')
INSERT [dbo].[HinhThucBaoCao] ([Id], [Ma], [Ten]) VALUES (3, N'GiaiDapThacMac', N'Giải đáp thắc mắc')
INSERT [dbo].[HinhThucBaoCao] ([Id], [Ma], [Ten]) VALUES (4, N'ToGiacSaiPham', N'Tố giác sai phạm')
SET IDENTITY_INSERT [dbo].[HinhThucBaoCao] OFF
SET IDENTITY_INSERT [dbo].[HinhThucQuangCao] ON 

INSERT [dbo].[HinhThucQuangCao] ([Id], [Ma], [Ten]) VALUES (1, N'CoDongChinhTri', N'Cổ động chính trị')
INSERT [dbo].[HinhThucQuangCao] ([Id], [Ma], [Ten]) VALUES (2, N'QuangCaoThuongMai', N'Quảng cáo thương mại')
INSERT [dbo].[HinhThucQuangCao] ([Id], [Ma], [Ten]) VALUES (3, N'XaHoiHoa', N'Xã hội hóa')
SET IDENTITY_INSERT [dbo].[HinhThucQuangCao] OFF
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
SET IDENTITY_INSERT [dbo].[LoaiViTri] ON 

INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (1, N'CayXang', N'Cây xăng')
INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (2, N'Cho', N'Chợ')
INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (3, N'DatCong', N'Đất công/Công viên/Hành lang an toàn giao thông')
INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (4, N'DatTuNhan', N'Đất tư nhân/Nhà ở riêng lẻ')
INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (5, N'NhaChoXeBuyt', N'Nhà chờ xe buýt')
INSERT [dbo].[LoaiViTri] ([Id], [Ma], [Ten]) VALUES (6, N'TrungTamThuongMai', N'Trung tâm thương mại')
SET IDENTITY_INSERT [dbo].[LoaiViTri] OFF
SET IDENTITY_INSERT [dbo].[PhieuCapPhepSuaQuangCao] ON 

INSERT [dbo].[PhieuCapPhepSuaQuangCao] ([Id], [IdDiemDat], [IdBangQuangCao], [NoiDung], [NgayGui], [TinhTrang]) VALUES (3, 1, 2, N'thay đổi', CAST(N'2023-12-27T06:46:42.0055184+00:00' AS DateTimeOffset), N'Mới tạo phiếu')
SET IDENTITY_INSERT [dbo].[PhieuCapPhepSuaQuangCao] OFF
ALTER TABLE [dbo].[BangQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_BangQuangCao_DiemDatQuangCao] FOREIGN KEY([IdDiemDatQuangCao])
REFERENCES [dbo].[DiemDatQuangCao] ([Id])
GO
ALTER TABLE [dbo].[BangQuangCao] CHECK CONSTRAINT [FK_BangQuangCao_DiemDatQuangCao]
GO
ALTER TABLE [dbo].[BaoCaoViPham]  WITH CHECK ADD  CONSTRAINT [FK_BaoCaoViPham_BangQuangCao] FOREIGN KEY([IdBangQuangCao])
REFERENCES [dbo].[BangQuangCao] ([Id])
GO
ALTER TABLE [dbo].[BaoCaoViPham] CHECK CONSTRAINT [FK_BaoCaoViPham_BangQuangCao]
GO
ALTER TABLE [dbo].[BaoCaoViPham]  WITH CHECK ADD  CONSTRAINT [FK_BaoCaoViPham_CanBo] FOREIGN KEY([IdCanBoXuLy])
REFERENCES [dbo].[CanBo] ([Id])
GO
ALTER TABLE [dbo].[BaoCaoViPham] CHECK CONSTRAINT [FK_BaoCaoViPham_CanBo]
GO
ALTER TABLE [dbo].[BaoCaoViPham]  WITH CHECK ADD  CONSTRAINT [FK_BaoCaoViPham_DiemDatQuangCao] FOREIGN KEY([IdDiemDatQuangCao])
REFERENCES [dbo].[DiemDatQuangCao] ([Id])
GO
ALTER TABLE [dbo].[BaoCaoViPham] CHECK CONSTRAINT [FK_BaoCaoViPham_DiemDatQuangCao]
GO
ALTER TABLE [dbo].[BaoCaoViPham]  WITH CHECK ADD  CONSTRAINT [FK_BaoCaoViPham_HinhThucBaoCao] FOREIGN KEY([IdHinhThucBaoCao])
REFERENCES [dbo].[HinhThucBaoCao] ([Id])
GO
ALTER TABLE [dbo].[BaoCaoViPham] CHECK CONSTRAINT [FK_BaoCaoViPham_HinhThucBaoCao]
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
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao]  WITH CHECK ADD  CONSTRAINT [Fk_PhieuCapPhepSuaQuangCao_BangQuangCao] FOREIGN KEY([IdBangQuangCao])
REFERENCES [dbo].[BangQuangCao] ([Id])
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao] CHECK CONSTRAINT [Fk_PhieuCapPhepSuaQuangCao_BangQuangCao]
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao]  WITH CHECK ADD  CONSTRAINT [Fk_PhieuCapPhepSuaQuangCao_DiemDatQuangCao] FOREIGN KEY([IdDiemDat])
REFERENCES [dbo].[DiemDatQuangCao] ([Id])
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao] CHECK CONSTRAINT [Fk_PhieuCapPhepSuaQuangCao_DiemDatQuangCao]
GO
