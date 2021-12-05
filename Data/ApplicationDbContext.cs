using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using PIS.Models;

namespace PIS.Data
{
	public class ApplicationDbContext : IdentityDbContext
	{
		public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options )
			: base( options )
		{
		}
		public DbSet<PIS.Models.Address> Address { get; set; }
		public DbSet<PIS.Models.Currency> Currency { get; set; }
		public DbSet<PIS.Models.Material> Material { get; set; }
		public DbSet<PIS.Models.Order> Order { get; set; }
		public DbSet<PIS.Models.OrderItem> OrderItem { get; set; }
		public DbSet<PIS.Models.Price> Price { get; set; }
	}
}
