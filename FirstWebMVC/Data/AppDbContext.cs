using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using FirstWebMVC.Models.DonHang;

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<FirstWebMVC.Models.DonHang.DonHang> DonHang { get; set; } = default!;

public DbSet<FirstWebMVC.Models.DonHang.SanPham> SanPham { get; set; } = default!;
    }
