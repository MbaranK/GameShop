using GameShop.Models;
using GameShopModels;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameShopData.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options) : base(options)
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Studio> Studios { get; set; }  

        public DbSet<Game> Games { get; set; }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<OrderDetails> OrderDetails { get; set; }
        public DbSet<OrderHeader> OrderHeaders { get; set; }
    }
}
