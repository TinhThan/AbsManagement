USE [AbsManagement]
GO
/****** Object:  Table [dbo].[BangQuangCao]    Script Date: 1/6/2024 2:43:52 PM ******/
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
	[TenCongTy] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL,
	[SoDienThoai] [nvarchar](200) NULL,
	[DiaChiCongTy] [nvarchar](200) NULL,
 CONSTRAINT [PK_BangQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[BaoCaoViPham]    Script Date: 1/6/2024 2:43:52 PM ******/
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
	[CreateDate] [datetime] NULL,
	[DiaChi] [nvarchar](max) NULL,
	[Phuong] [nvarchar](max) NULL,
	[Quan] [nvarchar](max) NULL,
	[ApproveDate] [datetime] NULL,
 CONSTRAINT [PK_BaoCaoViPham] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CanBo]    Script Date: 1/6/2024 2:43:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CanBo](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Email] [varchar](200) NOT NULL,
	[HoTen] [nvarchar](200) NOT NULL,
	[SoDienThoai] [varchar](50) NOT NULL,
	[NgaySinh] [datetimeoffset](7) NOT NULL,
	[Role] [varchar](50) NOT NULL,
	[MatKhau] [varchar](max) NOT NULL,
	[RefreshToken] [varchar](max) NULL,
	[RefreshTokenExpiryTime] [datetimeoffset](7) NULL,
	[NoiCongTac] [nvarchar](50) NOT NULL,
	[NgayCapNhat] [datetime] NULL,
	[EmailVerified] [int] NULL,
	[PasswordResetOTP] [nvarchar](max) NULL,
	[PasswordResetOTPExpiration] [datetime] NULL,
 CONSTRAINT [PK_CanBo] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[DiemDatQuangCao]    Script Date: 1/6/2024 2:43:52 PM ******/
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
/****** Object:  Table [dbo].[HinhThucBaoCao]    Script Date: 1/6/2024 2:43:52 PM ******/
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
/****** Object:  Table [dbo].[HinhThucQuangCao]    Script Date: 1/6/2024 2:43:52 PM ******/
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
/****** Object:  Table [dbo].[LoaiBangQuangCao]    Script Date: 1/6/2024 2:43:52 PM ******/
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
/****** Object:  Table [dbo].[LoaiViTri]    Script Date: 1/6/2024 2:43:52 PM ******/
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
/****** Object:  Table [dbo].[PhieuCapPhepQuangCao]    Script Date: 1/6/2024 2:43:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuCapPhepQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdTinhTrang] [varchar](50) NOT NULL,
	[IdBangQuangCao] [int] NULL,
	[NgayGui] [datetimeoffset](7) NULL,
	[IdCanBoDuyet] [int] NULL,
	[IdCanBoGui] [int] NOT NULL,
 CONSTRAINT [PK_PhieuCapPhepQuangCao] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PhieuCapPhepSuaQuangCao]    Script Date: 1/6/2024 2:43:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PhieuCapPhepSuaQuangCao](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdDiemDat] [int] NULL,
	[IdBangQuangCao] [int] NULL,
	[NoiDung] [nvarchar](500) NOT NULL,
	[NgayGui] [datetimeoffset](7) NOT NULL,
	[TinhTrang] [nvarchar](50) NOT NULL,
	[IdCanBoGui] [int] NOT NULL,
	[IdCanBoDuyet] [int] NULL,
	[NgayDuyet] [datetime] NULL,
 CONSTRAINT [PK__PhieuCap__3214EC0720C35F10] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[BangQuangCao] ON 

INSERT [dbo].[BangQuangCao] ([Id], [IdDiemDatQuangCao], [IdLoaiBangQuangCao], [KichThuoc], [DanhSachHinhAnh], [NgayHetHan], [IdTinhTrang], [NgayBatDau], [TenCongTy], [Email], [SoDienThoai], [DiaChiCongTy]) VALUES (1, 1034, 3, N'12x12', N'["quang-cao-ngoai-troi-la-gi.jpg"]', CAST(N'2024-01-17T07:09:57.1230000+00:00' AS DateTimeOffset), N'ChoDuyet', CAST(N'2024-01-06T07:12:23.9173181+00:00' AS DateTimeOffset), N'KHTN', N'ductinh280499@gmail.com', N'9281928192     ', N'Quận 7')
INSERT [dbo].[BangQuangCao] ([Id], [IdDiemDatQuangCao], [IdLoaiBangQuangCao], [KichThuoc], [DanhSachHinhAnh], [NgayHetHan], [IdTinhTrang], [NgayBatDau], [TenCongTy], [Email], [SoDienThoai], [DiaChiCongTy]) VALUES (2, 1028, 3, N'12x12', N'["quang-cao-ngoai-troi-la-gi.jpg","loai-hinh-quang-cao-Billboard-600x400.jpg"]', CAST(N'2024-01-09T07:12:37.7670000+00:00' AS DateTimeOffset), N'DaQuyHoach', CAST(N'2024-01-06T07:13:10.4838958+00:00' AS DateTimeOffset), N'KHTN', N'ductinh280499@gmail.com', N'9281928192     ', N'Quận 10')
SET IDENTITY_INSERT [dbo].[BangQuangCao] OFF
GO
SET IDENTITY_INSERT [dbo].[CanBo] ON 

INSERT [dbo].[CanBo] ([Id], [Email], [HoTen], [SoDienThoai], [NgaySinh], [Role], [MatKhau], [RefreshToken], [RefreshTokenExpiryTime], [NoiCongTac], [NgayCapNhat], [EmailVerified], [PasswordResetOTP], [PasswordResetOTPExpiration]) VALUES (1, N'ductinh@gmail.com', N'Đức Tính', N'093203921111', CAST(N'2023-04-19T17:00:00.0000000+00:00' AS DateTimeOffset), N'CanBoSo', N'$2b$10$of/rHjZE5K/gMFTDG/0XY.XFzHRKuF3l62n3ANEDMhhQ2v0kTDc9K', N'T78GfFWWlZ6HU+VutkKEgkP4+tNWoeSpiPE+McHr3DNKHNSc4bsCU7zrX8Ntzv3hpeVOPlJq5S7B+wh3GNH8xQ==', CAST(N'2024-01-09T13:46:28.8840120+00:00' AS DateTimeOffset), N'[]', NULL, NULL, NULL, NULL)
INSERT [dbo].[CanBo] ([Id], [Email], [HoTen], [SoDienThoai], [NgaySinh], [Role], [MatKhau], [RefreshToken], [RefreshTokenExpiryTime], [NoiCongTac], [NgayCapNhat], [EmailVerified], [PasswordResetOTP], [PasswordResetOTPExpiration]) VALUES (2, N'22424018@student.hcmus.edu.vn', N'Thân Văn Đức Tính', N'9281928192', CAST(N'2023-11-01T15:14:08.6900000+00:00' AS DateTimeOffset), N'CanBoSo', N'$2b$10$of/rHjZE5K/gMFTDG/0XY.XFzHRKuF3l62n3ANEDMhhQ2v0kTDc9K', N'sRF7MnvkACIwvCKaNBPHWhvO8jlLKSBkcUK/snCYIS4zKmL1CVot36wm5kukTJlBSv9UEVhkdl4oME8ZAghpWA==', CAST(N'2023-12-05T15:29:35.4831640+00:00' AS DateTimeOffset), N'[]', NULL, NULL, NULL, NULL)
INSERT [dbo].[CanBo] ([Id], [Email], [HoTen], [SoDienThoai], [NgaySinh], [Role], [MatKhau], [RefreshToken], [RefreshTokenExpiryTime], [NoiCongTac], [NgayCapNhat], [EmailVerified], [PasswordResetOTP], [PasswordResetOTPExpiration]) VALUES (3, N'ductinh280499@gmail.com', N'Đức Tính', N'12121212121', CAST(N'2023-11-09T15:46:57.9930000+00:00' AS DateTimeOffset), N'CanBoQuan', N'$2b$10$of/rHjZE5K/gMFTDG/0XY.XFzHRKuF3l62n3ANEDMhhQ2v0kTDc9K', N'HGiroRa1u50kul3Dq+RlLkt4uIizXd1lKKXqQWcuJQc/69E7CF9eKuTnU1gXcbZIIH8CUnNWuzAGloon0MMLEQ==', CAST(N'2024-01-11T07:14:55.5478773+00:00' AS DateTimeOffset), N'[]', NULL, NULL, NULL, NULL)
INSERT [dbo].[CanBo] ([Id], [Email], [HoTen], [SoDienThoai], [NgaySinh], [Role], [MatKhau], [RefreshToken], [RefreshTokenExpiryTime], [NoiCongTac], [NgayCapNhat], [EmailVerified], [PasswordResetOTP], [PasswordResetOTPExpiration]) VALUES (4, N'phuonglinh10032000@gmail.com', N'Đức tính', N'121212121', CAST(N'2023-11-15T15:49:17.5990000+00:00' AS DateTimeOffset), N'CanBoSo', N'$2b$10$of/rHjZE5K/gMFTDG/0XY.XFzHRKuF3l62n3ANEDMhhQ2v0kTDc9K', NULL, NULL, N'[]', NULL, NULL, NULL, NULL)
INSERT [dbo].[CanBo] ([Id], [Email], [HoTen], [SoDienThoai], [NgaySinh], [Role], [MatKhau], [RefreshToken], [RefreshTokenExpiryTime], [NoiCongTac], [NgayCapNhat], [EmailVerified], [PasswordResetOTP], [PasswordResetOTPExpiration]) VALUES (5, N'admin@gmail.com', N'Thân Văn Đức Tính Admin', N'09231212121', CAST(N'1999-04-28T13:46:54.8480000+00:00' AS DateTimeOffset), N'CanBoSo', N'$2b$10$of/rHjZE5K/gMFTDG/0XY.XFzHRKuF3l62n3ANEDMhhQ2v0kTDc9K', N'/GtECAHaQeYOIBbbekkg4fj/0WFKPXx7FhPz1ZbNMgbOwxqMNqlAnLwqjUV89ts5lGcMgrgKxqbPLVfWstiq/A==', CAST(N'2024-01-11T03:20:29.1575855+00:00' AS DateTimeOffset), N'[]', CAST(N'2024-01-04T13:47:13.407' AS DateTime), 1, N'0', CAST(N'2024-01-04T13:47:13.320' AS DateTime))
SET IDENTITY_INSERT [dbo].[CanBo] OFF
GO
SET IDENTITY_INSERT [dbo].[DiemDatQuangCao] ON 

INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1027, N'Photocopy Tan Cuu Long, 217 Nguyễn Văn Cừ St., Dist. 5, Ho Chi Minh City, 748400, Vietnam', N'72711', N'72700', N'[106.681216,10.762520]', 1, 1, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'ChoDuyet')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1028, N'Texas Chicken - Nguyễn Văn Cừ, 217B Nguyễn Văn Cừ, Quận 5, Ho Chi Minh City, 711600, Vietnam', N'71014', N'71000', N'[106.684083,10.762015]', 5, 2, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1029, N'Đại Học Sư Phạm TPHCM, 280 An Duong Vuong St., District 5, Ho Chi Minh City, 748400, Vietnam', N'72711', N'72700', N'[106.682158,10.761175]', 4, 2, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1030, N'The Coffee Bean & Tea Leaf, Now Zone Shopping Mall, 235 Nguyễn Văn Cừ St., District 1, Ho Chi Minh City, 748400, Vietnam', N'72711', N'72700', N'[106.682027,10.763213]', 3, 2, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'ChoDuyet')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1031, N'Chùa Long An, 106 Nguyễn Văn Cừ, Quận 1, Ho Chi Minh City, 711600, Vietnam', N'71014', N'71000', N'[106.685180,10.758631]', 4, 3, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1032, N'Pool @ Orchids Saigon Hotel, Ho Chi Minh City, 722400, Vietnam', N'72407', N'72400', N'[106.695769,10.780958]', 2, 3, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1033, N'Urban Station Nguyen Trai, Ho Chi Minh City, 748200, Vietnam', N'72710', N'72700', N'[106.683179,10.759500]', 3, 2, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1034, N'Vitamin Smile, 96 Đường Đề Thám, Ho Chi Minh City, 712100, Vietnam', N'71013', N'71000', N'[106.695423,10.764446]', 4, 1, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1035, N'Đại Học Sư Phạm TPHCM, 280 An Duong Vuong St., District 5, Ho Chi Minh City, 748400, Vietnam', N'72711', N'72700', N'[106.681194,10.762003]', 3, 2, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1036, N'The Faceshop, 94 Ng Trai Q. 5, Ho Chi Minh City, 748200, Vietnam', N'72709', N'72700', N'[106.679631,10.757721]', 5, 3, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1037, N'Hủ Tíu Nam Vang Tường Phát, Trần Phú, P4, Q5, Ho Chi Minh City, 748300, Vietnam', N'72711', N'72700', N'[106.675683,10.759744]', 4, 3, N'["bien-quang-cao-dep-01.jpg","demo_billboard_step-2_logoembosse_180509_dayview.jpg"]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1038, N'Viettel Telecom, 186 Lê Hồng Phong, Q5, Ho Chi Minh City, 748300, Vietnam', N'72711', N'72700', N'[106.677729,10.758962]', 3, 2, N'["bien-tam-lon.jpg","dich-vu-billboard.jpg"]', N'DaQuyHoach')
INSERT [dbo].[DiemDatQuangCao] ([Id], [DiaChi], [Phuong], [Quan], [ViTri], [IdLoaiViTri], [IdHinhThucQuangCao], [DanhSachHinhAnh], [IdTinhTrang]) VALUES (1039, N'Công Huy Photolab, 450 Trần Hưng Đạo St., Ho Chi Minh City, 750200, Vietnam', N'72709', N'72700', N'[106.683202,10.756319]', 3, 2, N'["dich-vu-billboard.jpg","quang-cao-ngoai-troi-la-gi.jpg"]', N'DaQuyHoach')
SET IDENTITY_INSERT [dbo].[DiemDatQuangCao] OFF
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
INSERT [dbo].[LoaiBangQuangCao] ([Id], [Ma], [Ten]) VALUES (11, N'BangQuangCaoMoi', N'Bảng quảng cáo loại mới')
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
SET IDENTITY_INSERT [dbo].[PhieuCapPhepSuaQuangCao] ON 

INSERT [dbo].[PhieuCapPhepSuaQuangCao] ([Id], [IdDiemDat], [IdBangQuangCao], [NoiDung], [NgayGui], [TinhTrang], [IdCanBoGui], [IdCanBoDuyet], [NgayDuyet]) VALUES (3, NULL, 1, N'{"IdDiemDatQuangCao":1034,"IdLoaiBangQuangCao":3,"KichThuoc":"12x12","DanhSachHinhAnh":["quang-cao-ngoai-troi-la-gi.jpg"],"NgayHetHan":"2024-01-17T07:09:57.123+00:00","NgayBatDau":"2024-01-06T07:12:23.917+00:00","IdTinhTrang":"DaQuyHoach","TenCongTy":"KHTN","Email":"ductinh280499@gmail.com","SoDienThoai":"9281928192     ","DiaChiCongTy":"Quận 7"}', CAST(N'2024-01-06T07:24:27.6581685+00:00' AS DateTimeOffset), N'ChoDuyet', 3, NULL, NULL)
INSERT [dbo].[PhieuCapPhepSuaQuangCao] ([Id], [IdDiemDat], [IdBangQuangCao], [NoiDung], [NgayGui], [TinhTrang], [IdCanBoGui], [IdCanBoDuyet], [NgayDuyet]) VALUES (4, 1027, NULL, N'{"DiaChi":"Photocopy Tan Cuu Long, 217 Nguyễn Văn Cừ St., Dist. 5, Ho Chi Minh City, 748400, Vietnam","Phuong":"72711","Quan":"72700","DanhSachViTri":[106.681216,10.76252],"IdLoaiViTri":1,"IdHinhThucQuangCao":1,"DanhSachHinhAnh":["bien-quang-cao-dep-01.jpg"]}', CAST(N'2024-01-06T07:25:59.6856081+00:00' AS DateTimeOffset), N'ChoDuyet', 3, NULL, NULL)
SET IDENTITY_INSERT [dbo].[PhieuCapPhepSuaQuangCao] OFF
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
ALTER TABLE [dbo].[PhieuCapPhepQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_PhieuCapPhepQuangCao_BangQuangCao] FOREIGN KEY([IdBangQuangCao])
REFERENCES [dbo].[BangQuangCao] ([Id])
GO
ALTER TABLE [dbo].[PhieuCapPhepQuangCao] CHECK CONSTRAINT [FK_PhieuCapPhepQuangCao_BangQuangCao]
GO
ALTER TABLE [dbo].[PhieuCapPhepQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_PhieuCapPhepQuangCao_CanBoDuyet] FOREIGN KEY([IdCanBoDuyet])
REFERENCES [dbo].[CanBo] ([Id])
GO
ALTER TABLE [dbo].[PhieuCapPhepQuangCao] CHECK CONSTRAINT [FK_PhieuCapPhepQuangCao_CanBoDuyet]
GO
ALTER TABLE [dbo].[PhieuCapPhepQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_PhieuCapPhepQuangCao_CanBoGui] FOREIGN KEY([IdCanBoGui])
REFERENCES [dbo].[CanBo] ([Id])
GO
ALTER TABLE [dbo].[PhieuCapPhepQuangCao] CHECK CONSTRAINT [FK_PhieuCapPhepQuangCao_CanBoGui]
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao]  WITH CHECK ADD  CONSTRAINT [Fk_PhieuCapPhepSuaQuangCao_BangQuangCao] FOREIGN KEY([IdBangQuangCao])
REFERENCES [dbo].[BangQuangCao] ([Id])
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao] CHECK CONSTRAINT [Fk_PhieuCapPhepSuaQuangCao_BangQuangCao]
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_PhieuCapPhepSuaQuangCao_CanBo] FOREIGN KEY([IdCanBoDuyet])
REFERENCES [dbo].[CanBo] ([Id])
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao] CHECK CONSTRAINT [FK_PhieuCapPhepSuaQuangCao_CanBo]
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao]  WITH CHECK ADD  CONSTRAINT [FK_PhieuCapPhepSuaQuangCao_CanBoGui] FOREIGN KEY([IdCanBoGui])
REFERENCES [dbo].[CanBo] ([Id])
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao] CHECK CONSTRAINT [FK_PhieuCapPhepSuaQuangCao_CanBoGui]
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao]  WITH CHECK ADD  CONSTRAINT [Fk_PhieuCapPhepSuaQuangCao_DiemDatQuangCao] FOREIGN KEY([IdDiemDat])
REFERENCES [dbo].[DiemDatQuangCao] ([Id])
GO
ALTER TABLE [dbo].[PhieuCapPhepSuaQuangCao] CHECK CONSTRAINT [Fk_PhieuCapPhepSuaQuangCao_DiemDatQuangCao]
GO
