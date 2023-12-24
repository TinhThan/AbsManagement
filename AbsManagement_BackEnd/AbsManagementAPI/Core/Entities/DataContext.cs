using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AbsManagementAPI.Core.Entities
{
    public class DataContext : DbContext
    {
        protected readonly IConfiguration _configuration;

        public DataContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<BangQuangCaoEntity> BangQuangCaos { get; set; }
        public DbSet<BaoCaoViPhamEntity> BaoCaoViPhams { get; set; }
        public DbSet<CanBoEntity> CanBos { get; set; }
        public DbSet<ChiTietChinhSuaBangQuangCaoEntity> ChiTietChinhSuaBangQuangCaos { get; set; }
        public DbSet<ChiTietPhieuChinhSuaDiemDatQuangCaoEntity> ChiTietPhieuChinhSuaDiemDatQuangs { get; set; }
        public DbSet<DiemDatQuangCaoEntity> DiemDatQuangCaos { get; set; }
        public DbSet<HinhThucBaoCaoEntity> HinhThucBaoCaos { get; set; }
        public DbSet<HinhThucQuangCaoEntity> HinhThucQuangCaos { get; set; }
        public DbSet<LoaiBangQuangCaoEntity> LoaiBangQuangCaos { get; set; }
        public DbSet<LoaiViTriEntity> LoaiViTris { get; set; }
        public DbSet<PhieuChinhSuaBangQuangCaoEntity> PhieuChinhSuaBangQuangCaos { get; set; }
        public DbSet<PhieuChinhSuaDiemDatQuangCaoEntity> PhieuChinhSuaDiemDatQuangCaos { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("AbsManagement");
            optionsBuilder.UseSqlServer(connectionString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BangQuangCaoEntity>(entity =>
            {
                entity.ToTable("BangQuangCao");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.DiemDatQuangCao)
                .WithMany(e => e.BangQuangCaos)
                .HasForeignKey(e => e.IdDiemDatQuangCao)
                .HasConstraintName("FK_BangQuangCao_DiemDatQuangCao");


                entity.HasOne(e => e.LoaiBangQuangCao)
                .WithMany(e => e.BangQuangCaos)
                .HasForeignKey(e => e.IdLoaiBangQuangCao)
                .HasConstraintName("FK_BangQuangCao_LoaiBangQuangCao");
            });

            modelBuilder.Entity<BaoCaoViPhamEntity>(entity =>
            {
                entity.ToTable("BaoCaoViPham");

                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.HinhThucBaoCao)
                .WithMany(e => e.BaoCaoViPhams)
                .HasForeignKey(e => e.IdHinhThucBaoCao)
                .HasConstraintName("FK_BaoCaoViPham_HinhThucBaoCao");


                entity.HasOne(e => e.CanBoXuLy)
                .WithMany(e => e.BaoCaoViPhams)
                .HasForeignKey(e => e.IdCanBoXuLy)
                .HasConstraintName("FK_BaoCaoViPham_CanBo");

                entity.HasOne(e => e.BangQuangCao)
                .WithMany(e => e.BaoCaoViPhams)
                .HasForeignKey(e => e.IdBangQuangCao)
                .HasConstraintName("FK_BaoCaoViPham_BangQuangCao");

                entity.HasOne(e => e.DiemDatQuangCao)
                .WithMany(e => e.BaoCaoViPhams)
                .HasForeignKey(e => e.IdDiemDatQuangCao)
                .HasConstraintName("FK_BaoCaoViPham_DiemDatQuangCao");
            });

            modelBuilder.Entity<CanBoEntity>(entity =>
            {
                entity.ToTable("CanBo");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<ChiTietChinhSuaBangQuangCaoEntity>(entity =>
            {
                entity.ToTable("ChiTietChinhSuaBangQuangCao");

                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.BangQuangCao)
                .WithMany(e => e.ChiTietChinhSuaBangQuangCaos)
                .HasForeignKey(e => e.IdBangQuangCao)
                .HasConstraintName("FK_ChiTietChinhSuaBangQuangCao_BangQuangCao");


                entity.HasOne(e => e.DiemDatQuangCaoMoi)
                .WithMany(e => e.ChiTietChinhSuaBangQuangCao_CapNhatMois)
                .HasForeignKey(e => e.IdDiemDatQuangCaoMoi)
                .HasConstraintName("FK_ChiTietChinhSuaBangQuangCao_DiemDatQuangCao");

                entity.HasOne(e => e.LoaiBangQuangCaoMoi)
                .WithMany(e => e.ChiTietChinhSuaBangQuangCaos)
                .HasForeignKey(e => e.IdLoaiBangQuangCaoMoi)
                .HasConstraintName("FK_ChiTietChinhSuaBangQuangCao_LoaiBangQuangCao");

                entity.HasOne(e => e.PhieuChinhSua)
                .WithMany(e => e.ChiTietChinhSuaBangQuangCaos)
                .HasForeignKey(e => e.IdPhieuChinhSua)
                .HasConstraintName("FK_ChiTietChinhSuaBangQuangCao_PhieuChinhSuaBangQuangCao");
            });

            modelBuilder.Entity<ChiTietPhieuChinhSuaDiemDatQuangCaoEntity>(entity =>
            {
                entity.ToTable("ChiTietPhieuChinhSuaDiemDatQuangCao");

                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.DiemDatQuangCao)
                    .WithMany(e => e.ChiTietPhieuChinhSuaDiemDatQuangCao_CapNhats)
                    .HasForeignKey(e => e.IdDiemDatQuangCao)
                    .HasConstraintName("FK_ChiTietPhieuChinhSuaDiemDatQuangCao_DiemDatQuangCao");


                entity.HasOne(e => e.HinhThucQuangCaoMoi)
                .WithMany(e => e.ChiTietPhieuChinhSuaDiemDatQuangCaos)
                .HasForeignKey(e => e.IdHinhThucMoi)
                .HasConstraintName("FK_ChiTietPhieuChinhSuaDiemDatQuangCao_HinhThucQuangCao");

                entity.HasOne(e => e.LoaiViTriMoi)
                .WithMany(e => e.ChiTietPhieuChinhSuaDiemDatQuangCaos)
                .HasForeignKey(e => e.IdLoaiVitriMoi)
                .HasConstraintName("FK_ChiTietPhieuChinhSuaDiemDatQuangCao_LoaiViTri");

                entity.HasOne(e => e.PhieuChinhSua)
                .WithMany(e => e.ChiTietPhieuChinhSuaDiemDatQuangCaos)
                .HasForeignKey(e => e.IdPhieuSua)
                .HasConstraintName("FK_ChiTietPhieuChinhSuaDiemDatQuangCao_PhieuChinhSuaDiemDatQuangCao");
            });

            modelBuilder.Entity<DiemDatQuangCaoEntity>(entity =>
            {
                entity.ToTable("DiemDatQuangCao");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.HinhThucQuangCao)
                .WithMany(e => e.DiemDatQuangCaos)
                .HasForeignKey(e => e.IdHinhThucQuangCao)
                .HasConstraintName("FK_DiemDatQuangCao_HinhThucQuangCao");

                entity.HasOne(e => e.LoaiViTri)
                .WithMany(e => e.DiemDatQuangCaos)
                .HasForeignKey(e => e.IdLoaiViTri)
                .HasConstraintName("FK_DiemDatQuangCao_LoaiViTri");
            });

            modelBuilder.Entity<HinhThucBaoCaoEntity>(entity =>
            {
                entity.ToTable("HinhThucBaoCao");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<HinhThucQuangCaoEntity>(entity =>
            {
                entity.ToTable("HinhThucQuangCao");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<LoaiBangQuangCaoEntity>(entity =>
            {
                entity.ToTable("LoaiBangQuangCao");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<LoaiViTriEntity>(entity =>
            {
                entity.ToTable("LoaiViTri");

                entity.HasKey(e => e.Id);
            });

            modelBuilder.Entity<PhieuChinhSuaBangQuangCaoEntity>(entity =>
            {
                entity.ToTable("PhieuChinhSuaBangQuangCao");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.CanBoTao)
                .WithMany(e => e.PhieuChinhSuaBangQuangCao_Taos)
                .HasForeignKey(e => e.IdCanBoTao)
                .HasConstraintName("FK_PhieuChinhSuaBangQuangCao_CanBoTao");

                entity.HasOne(e => e.CanBoDuyet)
                .WithMany(e => e.PhieuChinhSuaBangQuangCao_Duyets)
                .HasForeignKey(e => e.IdCanBoDuyet)
                .HasConstraintName("FK_PhieuChinhSuaBangQuangCao_CanBoDuyet");
            });

            modelBuilder.Entity<PhieuChinhSuaDiemDatQuangCaoEntity>(entity =>
            {
                entity.ToTable("PhieuChinhSuaDiemDatQuangCao");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.CanBoTao)
                .WithMany(e => e.PhieuChinhSuaDiemDatQuangCao_Taos)
                .HasForeignKey(e => e.IdCanBoTao)
                .HasConstraintName("FK_PhieuChinhSuaDiemDatQuangCao_CanBo");

                entity.HasOne(e => e.CanBoDuyet)
                .WithMany(e => e.PhieuChinhSuaDiemDatQuangCao_Duyets)
                .HasForeignKey(e => e.IdCanBoDuyet)
                .HasConstraintName("FK_PhieuChinhSuaDiemDatQuangCao_CanBoDuyet");
            });
        }
    }
}
