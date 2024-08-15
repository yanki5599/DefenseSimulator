using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DefenseSimulator.Models;

namespace DefenseSimulator.Data
{
    public class DefenseSimulatorContext : DbContext
    {
        public DefenseSimulatorContext (DbContextOptions<DefenseSimulatorContext> options)
            : base(options)
        {
        }

        public DbSet<DefenseSimulator.Models.Arsenal> Arsenal { get; set; } = default!;
        public DbSet<DefenseSimulator.Models.AttackWeapon> AttackWeapon { get; set; } = default!;
        public DbSet<DefenseSimulator.Models.DefenseWeapon> DefenseWeapon { get; set; } = default!;
        public DbSet<DefenseSimulator.Models.OriginThreat> OriginThreat { get; set; } = default!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // 
            modelBuilder.Entity<Arsenal>().HasIndex(a => a.DefenseWeaponId).IsUnique();
            modelBuilder.Entity<OriginThreat>().HasIndex(a => a.Name).IsUnique();
        }
        public DbSet<DefenseSimulator.Models.Threat> Threat { get; set; } = default!;
        public DbSet<DefenseSimulator.Models.Response> Response { get; set; } = default!;
    }
}
