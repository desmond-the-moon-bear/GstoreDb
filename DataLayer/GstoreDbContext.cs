using System;
using Microsoft.EntityFrameworkCore;

using BusinessLayer;

namespace DataLayer
{
    public class GstoreDbContext : DbContext
    {
		public GstoreDbContext() : base()
		{

		}

		public GstoreDbContext(DbContextOptions contextOptions) : base(contextOptions)
		{

		}

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			if (!optionsBuilder.IsConfigured)
			{
				optionsBuilder.UseSqlServer("Server=DESKTOP-GK4F9OM\\SQLEXPRESS;Database=GstoreDb;Trusted_Connection=True;");
			}

			base.OnConfiguring(optionsBuilder);
		}

		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			/* modelBuilder.Entity<User>()
				.HasMany(user => user.Friends)
				.WithMany(user => user.Friends)
				.UsingEntity("UserUser"); */
			
			base.OnModelCreating(modelBuilder);
		}

		public DbSet<User> Users { get; set; }

		public DbSet<Game> Games { get; set; }

		public DbSet<Genre> Genres { get; set; }
	}
}
