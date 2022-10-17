using Microsoft.EntityFrameworkCore;

namespace _102190334_NguyenMinhQuang.Models;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
    public DbSet<HoaDon> HoaDons { get; set; }
    public DbSet<LoaiHoaDon> LoaiHoaDons { get; set; }
}