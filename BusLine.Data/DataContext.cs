using BusLine.Data.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusLine.Data
{
    public class DataContext : IdentityDbContext<User, AppRole, string>
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ScheduleUser>().HasKey(su => su.Id);
            builder.Entity<ScheduleUser>().HasOne(su => su.schedule).WithMany(su=> su.scheduleUsers).HasForeignKey(su=>su.ScheduleId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<ScheduleUser>().HasOne(su => su.user).WithMany(su => su.ScheduleUsers).HasForeignKey(su=>su.UserId).OnDelete(DeleteBehavior.SetNull);

            builder.Entity<Travel>().HasKey( su => su.Id );
            builder.Entity<Travel>().HasOne(su => su.Schedule).WithMany(su => su.travels).HasForeignKey(su => su.ScheduleId).OnDelete(DeleteBehavior.Cascade);
            builder.Entity<Travel>().HasOne(su => su.Bus).WithMany(su => su.travels).HasForeignKey(su => su.BusId).OnDelete(DeleteBehavior.SetNull);

       

        }

        public DbSet<Bus> buses { get; set; }
        public DbSet<Models.BusLine> busLines { get; set; }
        public DbSet<Malfunction> malfunctions { get; set; }
        public DbSet<Schedule> schedules { get; set; }
        public DbSet<ScheduleUser> scheduleUsers { get; set; }
        public DbSet<Ticket> tickets { get; set; }
        public DbSet<Travel> travels { get; set; }

    }
}
