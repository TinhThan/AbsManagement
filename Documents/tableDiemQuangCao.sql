USE [AbsManagement]
GO
/****** Object:  Table [dbo].[DiemDatQuangCao]    Script Date: 1/5/2024 4:33:46 PM ******/
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
