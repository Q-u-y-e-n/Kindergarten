using Microsoft.EntityFrameworkCore;
using kindergarten.Models;

namespace kindergarten.Data
{
    public class KindergartenContext : DbContext
    {
        public KindergartenContext(DbContextOptions<KindergartenContext> options)
            : base(options) { }

        // ====== Tài khoản & nhân sự ======
        public DbSet<TaiKhoan> TaiKhoans { get; set; }
        public DbSet<GiaoVien> GiaoViens { get; set; }
        public DbSet<PhuHuynh> PhuHuynhs { get; set; }

        // ====== Học sinh & lớp học ======
        public DbSet<HocSinh> HocSinhs { get; set; }
        public DbSet<LopHoc> LopHocs { get; set; }
        public DbSet<HOCSINH_PHUHUYNH> HocSinhPhuHuynhs { get; set; }

        // ====== Học tập & điểm danh ======
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<KetQua> KetQuas { get; set; }
        public DbSet<DiemDanh> DiemDanhs { get; set; }

        // ====== Hoạt động ngoại khóa ======
        public DbSet<HoatDong> HoatDongs { get; set; }
        public DbSet<ThamGiaHD> ThamGiaHDs { get; set; }

        // ====== Sức khỏe & dinh dưỡng ======
        public DbSet<HoSoSucKhoe> HoSoSucKhoes { get; set; }
        public DbSet<BuaAn> BuaAns { get; set; }
        public DbSet<ThucDon> ThucDons { get; set; }
        public DbSet<ChiTietThucDon> ChiTietThucDons { get; set; }
        public DbSet<NguyenLieu> NguyenLieus { get; set; }

        // ====== Học phí & thanh toán ======
        public DbSet<HocPhi> HocPhis { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }

        // ====== Cơ sở vật chất & thông báo ======
        public DbSet<TaiSan> TaiSans { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // ====== Khóa chính phức hợp ======
            modelBuilder.Entity<HOCSINH_PHUHUYNH>().HasKey(x => new { x.MaHS, x.MaPH });
            modelBuilder.Entity<ThamGiaHD>().HasKey(x => new { x.MaHD, x.MaHS });
            modelBuilder.Entity<ChiTietThucDon>().HasKey(x => new { x.MaTD, x.MaNL });
            modelBuilder.Entity<TaiKhoan>().ToTable("TAIKHOAN");

            // ====== Quan hệ: Lớp học - Giáo viên chủ nhiệm ======
            modelBuilder.Entity<LopHoc>()
                .HasOne(l => l.GiaoVienChuNhiem)
                .WithMany(g => g.LopHocs)
                .HasForeignKey(l => l.MaGVCN)
                .OnDelete(DeleteBehavior.SetNull); // Khi GV bị xóa, lớp sẽ set MaGVCN = null

            // ====== Quan hệ: Học sinh - Lớp học ======
            modelBuilder.Entity<HocSinh>()
                .HasOne(h => h.LopHoc)
                .WithMany(l => l.HocSinhs)
                .HasForeignKey(h => h.MaLop)
                .OnDelete(DeleteBehavior.Restrict);

            // ====== Quan hệ: Bữa ăn - Thực đơn ======
            modelBuilder.Entity<BuaAn>()
                .HasOne(b => b.ThucDon)
                .WithMany(t => t.BuaAns)
                .HasForeignKey(b => b.MaTD)
                .OnDelete(DeleteBehavior.Restrict);

            // ====== Quan hệ: Hồ sơ sức khỏe - Học sinh ======
            modelBuilder.Entity<HoSoSucKhoe>()
                .HasOne(h => h.HocSinh)
                .WithMany(hs => hs.HoSoSucKhoes)
                .HasForeignKey(h => h.MaHS)
                .OnDelete(DeleteBehavior.SetNull);

            // ====== Quan hệ: Học sinh - ThamGiaHD ======
            modelBuilder.Entity<ThamGiaHD>()
                .HasOne(t => t.HocSinh)
                .WithMany(h => h.ThamGiaHDs)
                .HasForeignKey(t => t.MaHS)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
