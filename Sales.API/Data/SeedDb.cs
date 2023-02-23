using System;
using Sales.Shared.Entities;

namespace Sales.API.Data
{
	public class SeedDb
	{
		private readonly DataContext _context;
		public SeedDb(DataContext context)
		{
			_context = context;

		}

		public async Task SeedAsync()
		{
			await _context.Database.EnsureCreatedAsync();
			await CheckCountriesAsync();
			await CheckCategoriesAsync();
		}

		private async Task CheckCountriesAsync()
		{
			if (!_context.Countries.Any())
			{
				_context.Countries.Add(new Country {Name ="Colombia" });
                _context.Countries.Add(new Country { Name = "Perú" });
                _context.Countries.Add(new Country { Name = "México" });
				await _context.SaveChangesAsync();
            }

		
		}

        private async Task CheckCategoriesAsync()
        {
            if (!_context.Categories.Any())
            {
                _context.Categories.Add(new Category { Name = "Televisores" });
                _context.Categories.Add(new Category { Name = "Celulares" });
                _context.Categories.Add(new Category { Name = "Computadores" });
                _context.Categories.Add(new Category { Name = "Muebles" });
                await _context.SaveChangesAsync();
            }


        }

    }
}

