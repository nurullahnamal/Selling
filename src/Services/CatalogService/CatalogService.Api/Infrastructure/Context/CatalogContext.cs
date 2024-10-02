﻿using CatalogService.Api.Core.Domain;
using CatalogService.Api.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace CatalogService.Api.Infrastructure.Context
{
	public class CatalogContext:DbContext
	{

		public const string DEFAULT_SCHEMA = "catalog";

        public CatalogContext(DbContextOptions<CatalogContext> options):base(options) { }

        public DbSet<CatalogItem>CatalogItems { get; set; }
        public DbSet<CatalogBrand>CatalogBrands{ get; set; }
        public DbSet<CatalogType>CatalogTypes{ get; set; }


		protected override void OnModelCreating(ModelBuilder Builder)
		{
			Builder.ApplyConfiguration(new CatalogBrandEntityTypeConfiguration());
			Builder.ApplyConfiguration(new CatalogItemEntityTypeConfiguration());
			Builder.ApplyConfiguration(new CatalogTypeEntityTypeConfiguration());
		}
	}
}
