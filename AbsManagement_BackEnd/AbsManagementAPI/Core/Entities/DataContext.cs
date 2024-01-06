using AbsManagementAPI.Entities;
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
        public DbSet<DiemDatQuangCaoEntity> DiemDatQuangCaos { get; set; }
        public DbSet<HinhThucBaoCaoEntity> HinhThucBaoCaos { get; set; }
        public DbSet<HinhThucQuangCaoEntity> HinhThucQuangCaos { get; set; }
        public DbSet<LoaiBangQuangCaoEntity> LoaiBangQuangCaos { get; set; }
        public DbSet<LoaiViTriEntity> LoaiViTris { get; set; }
        public DbSet<PhieuCapPhepSuaQuangCaoEntity> PhieuCapPhepSuaQuangCaos { get; set; }

        public DbSet<PhieuCapPhepQuangCaoEntity> PhieuCapPhepQuangCaos { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = _configuration.GetConnectionString("AbsManagement");
            optionsBuilder.UseSqlServer(connectionString);

            //var connectionString = _configuration.GetConnectionString("Sakila");
            //optionsBuilder.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString));
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

            modelBuilder.Entity<PhieuCapPhepQuangCaoEntity>(entity =>
            {
                entity.ToTable("PhieuCapPhepQuangCao");
                entity.HasKey(e => e.Id);

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
            modelBuilder.Entity<PhieuCapPhepSuaQuangCaoEntity>(entity =>
            {
                entity.ToTable("PhieuCapPhepSuaQuangCao");
                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.DiemDatQuangCao)
                .WithMany(e => e.PhieuCapPhepSuaQuangCaos)
                .HasForeignKey(e => e.IdDiemDat)
                .HasConstraintName("Fk_PhieuCapPhepSuaQuangCao_DiemDatQuangCao");


                entity.HasOne(e => e.BangQuangCao)
                .WithMany(e => e.PhieuCapPhepSuaQuangCaos)
                .HasForeignKey(e => e.IdBangQuangCao)
                .HasConstraintName("Fk_PhieuCapPhepSuaQuangCao_BangQuangCao");
            });
        }
    }
}
