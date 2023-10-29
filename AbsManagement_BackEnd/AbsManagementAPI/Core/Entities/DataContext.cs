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
        public DbSet<HinhThucBaoCaoEntity> HinhThucBaoCaos { get; set; }
        public DbSet<HinhThucQuangCaoEntity> HinhThucQuangCaos { get; set; }
        public DbSet<LoaiBangQuangCaoEntity> LoaiBangQuangCaos { get; set; }
        public DbSet<LoaiViTriEntity> LoaiViTris { get; set; }

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

                entity.HasOne(e => e.LoaiViTri)
                .WithMany(e => e.BangQuangCaos)
                .HasForeignKey(e => e.MaLoaiViTri)
                .HasConstraintName("FK_BangQuangCao_LoaiViTri");


                entity.HasOne(e => e.HinhThucQuangCao)
                .WithMany(e => e.BangQuangCaos)
                .HasForeignKey(e => e.MaHinhThucQuangCao)
                .HasConstraintName("FK_BangQuangCao_HinhThucQuangCao");


                entity.HasOne(e => e.LoaiBangQuangCao)
                .WithMany(e => e.BangQuangCaos)
                .HasForeignKey(e => e.MaLoaiBangQuangCao)
                .HasConstraintName("FK_BangQuangCao_LoaiBangQuangCao");
            });

            modelBuilder.Entity<BaoCaoViPhamEntity>(entity =>
            {
                entity.ToTable("BaoCaoViPham");

                entity.HasKey(e => e.Id);

                entity.HasOne(e => e.HinhThucBaoCao)
                .WithMany(e => e.BaoCaoViPhams)
                .HasForeignKey(e => e.MaHinhThucBaoCao)
                .HasConstraintName("FK_BaoCaoViPham_HinhThucBaoCao");
            });

            modelBuilder.Entity<HinhThucBaoCaoEntity>(entity =>
            {
                entity.ToTable("HinhThucBaoCao");

                entity.HasKey(e => e.Ma);
            });

            modelBuilder.Entity<HinhThucQuangCaoEntity>(entity =>
            {
                entity.ToTable("HinhThucQuangCao");

                entity.HasKey(e => e.Ma);
            });

            modelBuilder.Entity<LoaiBangQuangCaoEntity>(entity =>
            {
                entity.ToTable("LoaiBangQuangCao");

                entity.HasKey(e => e.Ma);
            });

            modelBuilder.Entity<LoaiViTriEntity>(entity =>
            {
                entity.ToTable("LoaiViTri");

                entity.HasKey(e => e.Ma);
            });
        }
    }
}
