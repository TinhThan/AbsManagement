using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AbsManagementAPI.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CanBo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgaySinh = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MatKhau = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RefreshToken = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RefreshTokenExpiryTime = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    NoiCongTac = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CanBo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HinhThucBaoCao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhThucBaoCao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HinhThucQuangCao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HinhThucQuangCao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiBangQuangCao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiBangQuangCao", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoaiViTri",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ma = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Ten = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoaiViTri", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PhieuChinhSuaBangQuangCao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCanBoTao = table.Column<int>(type: "int", nullable: false),
                    IdCanBoDuyet = table.Column<int>(type: "int", nullable: true),
                    LyDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuChinhSuaBangQuangCao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhieuChinhSuaBangQuangCao_CanBoDuyet",
                        column: x => x.IdCanBoDuyet,
                        principalTable: "CanBo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhieuChinhSuaBangQuangCao_CanBoTao",
                        column: x => x.IdCanBoTao,
                        principalTable: "CanBo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "PhieuChinhSuaDiemDatQuangCao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCanBoTao = table.Column<int>(type: "int", nullable: false),
                    IdCanBoDuyet = table.Column<int>(type: "int", nullable: true),
                    LyDo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PhieuChinhSuaDiemDatQuangCao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PhieuChinhSuaDiemDatQuangCao_CanBo",
                        column: x => x.IdCanBoTao,
                        principalTable: "CanBo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_PhieuChinhSuaDiemDatQuangCao_CanBoDuyet",
                        column: x => x.IdCanBoDuyet,
                        principalTable: "CanBo",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "DiemDatQuangCao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViTri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdLoaiViTri = table.Column<int>(type: "int", nullable: false),
                    IdHinhThucQuangCao = table.Column<int>(type: "int", nullable: false),
                    DanhSachHinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTinhTrang = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DiemDatQuangCao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DiemDatQuangCao_HinhThucQuangCao",
                        column: x => x.IdHinhThucQuangCao,
                        principalTable: "HinhThucQuangCao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_DiemDatQuangCao_LoaiViTri",
                        column: x => x.IdLoaiViTri,
                        principalTable: "LoaiViTri",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BangQuangCao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdDiemDatQuangCao = table.Column<int>(type: "int", nullable: false),
                    IdLoaiBangQuangCao = table.Column<int>(type: "int", nullable: false),
                    KichThuoc = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DanhSachHinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayHetHan = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IdTinhTrang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayCapNhat = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BangQuangCao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BangQuangCao_DiemDatQuangCao",
                        column: x => x.IdDiemDatQuangCao,
                        principalTable: "DiemDatQuangCao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BangQuangCao_LoaiBangQuangCao",
                        column: x => x.IdLoaiBangQuangCao,
                        principalTable: "LoaiBangQuangCao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietPhieuChinhSuaDiemDatQuangCao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdPhieuSua = table.Column<int>(type: "int", nullable: false),
                    IdDiemDatQuangCao = table.Column<int>(type: "int", nullable: false),
                    DiaChiMoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhuongMoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    QuanMoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ViTriMoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdLoaiVitriMoi = table.Column<int>(type: "int", nullable: false),
                    IdHinhThucMoi = table.Column<int>(type: "int", nullable: false),
                    DanhSachHinhAnhMoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTinhTrang = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietPhieuChinhSuaDiemDatQuangCao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuChinhSuaDiemDatQuangCao_DiemDatQuangCao",
                        column: x => x.IdDiemDatQuangCao,
                        principalTable: "DiemDatQuangCao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuChinhSuaDiemDatQuangCao_HinhThucQuangCao",
                        column: x => x.IdHinhThucMoi,
                        principalTable: "HinhThucQuangCao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuChinhSuaDiemDatQuangCao_LoaiViTri",
                        column: x => x.IdLoaiVitriMoi,
                        principalTable: "LoaiViTri",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietPhieuChinhSuaDiemDatQuangCao_PhieuChinhSuaDiemDatQuangCao",
                        column: x => x.IdPhieuSua,
                        principalTable: "PhieuChinhSuaDiemDatQuangCao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "BaoCaoViPham",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdCanBoXuLy = table.Column<int>(type: "int", nullable: true),
                    IdBangQuangCao = table.Column<int>(type: "int", nullable: true),
                    IdDiemDatQuangCao = table.Column<int>(type: "int", nullable: true),
                    HoTen = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SoDienThoai = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdHinhThucBaoCao = table.Column<int>(type: "int", nullable: false),
                    NoiDung = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NoiDungXuLy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ViTri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DanhSachHinhAnh = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IdTinhTrang = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DiaChi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phuong = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quan = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaoCaoViPham", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BaoCaoViPham_BangQuangCao",
                        column: x => x.IdBangQuangCao,
                        principalTable: "BangQuangCao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaoCaoViPham_CanBo",
                        column: x => x.IdCanBoXuLy,
                        principalTable: "CanBo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaoCaoViPham_DiemDatQuangCao",
                        column: x => x.IdDiemDatQuangCao,
                        principalTable: "DiemDatQuangCao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_BaoCaoViPham_HinhThucBaoCao",
                        column: x => x.IdHinhThucBaoCao,
                        principalTable: "HinhThucBaoCao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ChiTietChinhSuaBangQuangCao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdBangQuangCao = table.Column<int>(type: "int", nullable: false),
                    IdPhieuChinhSua = table.Column<int>(type: "int", nullable: false),
                    IdDiemDatQuangCaoMoi = table.Column<int>(type: "int", nullable: false),
                    IdLoaiBangQuangCaoMoi = table.Column<int>(type: "int", nullable: false),
                    DanhSachHinhAnhMoi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NgayHetHanMoi = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    IdTinhTrang = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChiTietChinhSuaBangQuangCao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChiTietChinhSuaBangQuangCao_BangQuangCao",
                        column: x => x.IdBangQuangCao,
                        principalTable: "BangQuangCao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietChinhSuaBangQuangCao_DiemDatQuangCao",
                        column: x => x.IdDiemDatQuangCaoMoi,
                        principalTable: "DiemDatQuangCao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietChinhSuaBangQuangCao_LoaiBangQuangCao",
                        column: x => x.IdLoaiBangQuangCaoMoi,
                        principalTable: "LoaiBangQuangCao",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ChiTietChinhSuaBangQuangCao_PhieuChinhSuaBangQuangCao",
                        column: x => x.IdPhieuChinhSua,
                        principalTable: "PhieuChinhSuaBangQuangCao",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BangQuangCao_IdDiemDatQuangCao",
                table: "BangQuangCao",
                column: "IdDiemDatQuangCao");

            migrationBuilder.CreateIndex(
                name: "IX_BangQuangCao_IdLoaiBangQuangCao",
                table: "BangQuangCao",
                column: "IdLoaiBangQuangCao");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoViPham_IdBangQuangCao",
                table: "BaoCaoViPham",
                column: "IdBangQuangCao");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoViPham_IdCanBoXuLy",
                table: "BaoCaoViPham",
                column: "IdCanBoXuLy");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoViPham_IdDiemDatQuangCao",
                table: "BaoCaoViPham",
                column: "IdDiemDatQuangCao");

            migrationBuilder.CreateIndex(
                name: "IX_BaoCaoViPham_IdHinhThucBaoCao",
                table: "BaoCaoViPham",
                column: "IdHinhThucBaoCao");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChinhSuaBangQuangCao_IdBangQuangCao",
                table: "ChiTietChinhSuaBangQuangCao",
                column: "IdBangQuangCao");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChinhSuaBangQuangCao_IdDiemDatQuangCaoMoi",
                table: "ChiTietChinhSuaBangQuangCao",
                column: "IdDiemDatQuangCaoMoi");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChinhSuaBangQuangCao_IdLoaiBangQuangCaoMoi",
                table: "ChiTietChinhSuaBangQuangCao",
                column: "IdLoaiBangQuangCaoMoi");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietChinhSuaBangQuangCao_IdPhieuChinhSua",
                table: "ChiTietChinhSuaBangQuangCao",
                column: "IdPhieuChinhSua");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuChinhSuaDiemDatQuangCao_IdDiemDatQuangCao",
                table: "ChiTietPhieuChinhSuaDiemDatQuangCao",
                column: "IdDiemDatQuangCao");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuChinhSuaDiemDatQuangCao_IdHinhThucMoi",
                table: "ChiTietPhieuChinhSuaDiemDatQuangCao",
                column: "IdHinhThucMoi");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuChinhSuaDiemDatQuangCao_IdLoaiVitriMoi",
                table: "ChiTietPhieuChinhSuaDiemDatQuangCao",
                column: "IdLoaiVitriMoi");

            migrationBuilder.CreateIndex(
                name: "IX_ChiTietPhieuChinhSuaDiemDatQuangCao_IdPhieuSua",
                table: "ChiTietPhieuChinhSuaDiemDatQuangCao",
                column: "IdPhieuSua");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDatQuangCao_IdHinhThucQuangCao",
                table: "DiemDatQuangCao",
                column: "IdHinhThucQuangCao");

            migrationBuilder.CreateIndex(
                name: "IX_DiemDatQuangCao_IdLoaiViTri",
                table: "DiemDatQuangCao",
                column: "IdLoaiViTri");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuChinhSuaBangQuangCao_IdCanBoDuyet",
                table: "PhieuChinhSuaBangQuangCao",
                column: "IdCanBoDuyet");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuChinhSuaBangQuangCao_IdCanBoTao",
                table: "PhieuChinhSuaBangQuangCao",
                column: "IdCanBoTao");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuChinhSuaDiemDatQuangCao_IdCanBoDuyet",
                table: "PhieuChinhSuaDiemDatQuangCao",
                column: "IdCanBoDuyet");

            migrationBuilder.CreateIndex(
                name: "IX_PhieuChinhSuaDiemDatQuangCao_IdCanBoTao",
                table: "PhieuChinhSuaDiemDatQuangCao",
                column: "IdCanBoTao");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaoCaoViPham");

            migrationBuilder.DropTable(
                name: "ChiTietChinhSuaBangQuangCao");

            migrationBuilder.DropTable(
                name: "ChiTietPhieuChinhSuaDiemDatQuangCao");

            migrationBuilder.DropTable(
                name: "HinhThucBaoCao");

            migrationBuilder.DropTable(
                name: "BangQuangCao");

            migrationBuilder.DropTable(
                name: "PhieuChinhSuaBangQuangCao");

            migrationBuilder.DropTable(
                name: "PhieuChinhSuaDiemDatQuangCao");

            migrationBuilder.DropTable(
                name: "DiemDatQuangCao");

            migrationBuilder.DropTable(
                name: "LoaiBangQuangCao");

            migrationBuilder.DropTable(
                name: "CanBo");

            migrationBuilder.DropTable(
                name: "HinhThucQuangCao");

            migrationBuilder.DropTable(
                name: "LoaiViTri");
        }
    }
}
