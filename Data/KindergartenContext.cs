using Microsoft.EntityFrameworkCore;
using kindergarten.Models;

namespace kindergarten.Data  // ⚠️ Đảm bảo trùng với namespace thật trong project (thường là kindergaerten.Data)
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
        public DbSet<GiaoVienChuNhiem> GiaoVienChuNhiems { get; set; }
        public DbSet<HocSinhPhuHuynh> HocSinhPhuHuynhs { get; set; }

        // ====== Học tập & điểm danh ======
        public DbSet<MonHoc> MonHocs { get; set; }
        public DbSet<KetQua> KetQuas { get; set; }
        public DbSet<DiemDanh> DiemDanhs { get; set; }

        // ====== Hoạt động ngoại khóa ======
        public DbSet<HoatDong> HoatDongs { get; set; }
        public DbSet<ThamGiaHD> ThamGiaHDs { get; set; }

        // ====== Sức khỏe & dinh dưỡng ======
        public DbSet<HoSoSucKhoe> HoSoSucKhoes { get; set; }   // ✅ Model này phải có trong thư mục Models
        public DbSet<BuaAn> BuaAns { get; set; }
        public DbSet<ThucDon> ThucDons { get; set; }
        public DbSet<ChiTietThucDon> ChiTietThucDons { get; set; }
        public DbSet<NguyenLieu> NguyenLieus { get; set; }

        // ====== Học phí & thanh toán ======
        public DbSet<HocPhi> HocPhis { get; set; }
        public DbSet<DanhMucPhi> DanhMucPhis { get; set; }
        public DbSet<HoaDon> HoaDons { get; set; }
        public DbSet<ChiTietHoaDon> ChiTietHoaDons { get; set; }
        public DbSet<ThanhToan> ThanhToans { get; set; }

        // ====== Cơ sở vật chất & thông báo ======
        public DbSet<TaiSan> TaiSans { get; set; }
        public DbSet<ThongBao> ThongBaos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // ====== Khóa chính phức hợp ======
            modelBuilder.Entity<HocSinhPhuHuynh>().HasKey(x => new { x.MaHS, x.MaPH });
            modelBuilder.Entity<ThamGiaHD>().HasKey(x => new { x.MaHD, x.MaHS });
            modelBuilder.Entity<ChiTietThucDon>().HasKey(x => new { x.MaTD, x.MaNL });
            modelBuilder.Entity<GiaoVienChuNhiem>().HasKey(x => new { x.MaGV, x.MaLop });
            modelBuilder.Entity<TaiKhoan>().ToTable("TAIKHOAN");

            // ====== Quan hệ ======
            modelBuilder.Entity<HocSinh>()
                .HasOne(h => h.LopHoc)
                .WithMany(l => l.HocSinhs)
                .HasForeignKey(h => h.MaLop)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GiaoVienChuNhiem>()
                .HasOne(g => g.GiaoVien)
                .WithMany(g => g.LopChuNhiem)
                .HasForeignKey(g => g.MaGV)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<GiaoVienChuNhiem>()
                .HasOne(g => g.LopHoc)
                .WithMany(l => l.GiaoVienChuNhiems)
                .HasForeignKey(g => g.MaLop)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<HocPhi>()
                .HasOne(hp => hp.HocSinh)
                .WithMany(h => h.HocPhis)
                .HasForeignKey(hp => hp.MaHS)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<BuaAn>()
                .HasOne(b => b.ThucDon)
                .WithMany(t => t.BuaAns)
                .HasForeignKey(b => b.MaTD)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
