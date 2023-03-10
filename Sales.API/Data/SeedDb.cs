using System;
using Microsoft.EntityFrameworkCore;
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
           // await CheckCountriesAsync();
            await CheckCategoriesAsync();
            await countries();
        }

        private async Task countries()
        {
            List<json.Country> countries = json.FactoryCountries.GetCountries();
            List<json.State> states = json.FactoryCountries.GetStates();
            List<json.City> cities = json.FactoryCountries.GetCities();
            List<Country> listCounties = new List<Country>();

            if (!_context.Countries.Any())
            {
                foreach (json.Country country in countries)
                {
                    List<State> states1 = new List<State>();
                    foreach (json.State state in states)
                    {

                        if (country.id == state.id_country)
                        {
                            List<City> cities1 = new List<City>();
                            foreach (json.City city in cities)
                            {
                                if (state.id == city.id_state)
                                {

                                    cities1.Add(new City { Name = city.name, StateId = city.id_state });
                                   
                                }

                            }

                            if (cities1.Count() > 0)
                            {
                                Console.WriteLine("State " + state.name + " cities :" + cities1.Count());
                                states1.Add(new State { Name = state.name, CountryId = state.id_country, Cities = cities1 });
                            }

                        }

                    }

                    Console.WriteLine("country " + country.name + " states :" + states1.Count());

                    if (states1.Count() > 0)
                    {
                        try
                        {
                            _context.Countries.Add(new Country { Name = country.name, States = states1 });
                            await _context.SaveChangesAsync();
                        }
                        catch (DbUpdateException ex)
                        {
                            Console.WriteLine("Error " + ex.InnerException!.Message);
                        }
                          
                        
                       
                    }

                  

                }

            }
           
        }

        private async Task CheckCountriesAsync()
        {
            if (!_context.Countries.Any())
            {
                _context.Countries.Add(new Country
                {
                    Name = "Colombia",
                    States = new List<State>()
                    {
                        new State (){
                            Name="Antioquia",
                            Cities=new List<City>()
                            {
                            new City () {Name="Medellín"},
                            new City () {Name="Itagüí"},
                            new City () {Name="Envigado"},
                            new City () {Name="Bello"},
                            new City () {Name="Rionegro"}


                            }
                        },
                        new State() {
                            Name="Bogota",
                            Cities = new List<City>(){
                                new City () {Name="Usaquen"},
                                new City () {Name="Champinero"},
                                new City () {Name="Santa fe"},
                                new City () {Name="Useme"},
                                new City () {Name="Bosa"}
                            }
                        }


                    }
                });

                _context.Countries.Add(new Country
                {
                    Name = "Estados Unidos",
                    States = new List<State>()
                    {
                        new State
                        {
                            Name="Florida",
                            Cities = new List<City>()
                            {
                                new City () {Name="Orlando"},
                                new City () {Name="Miami"},
                                new City () {Name="Tampa"},
                                new City () {Name="Fort Lauderdale"},
                                new City () {Name="Key West"}
                            }
                        },
                    new State()
                    {
                        Name = "Texas",
                        Cities = new List<City>()
                        {
                        new City () {Name="Houston"},
                        new City () {Name="San Antonio"},
                        new City () {Name="Dallas"},
                        new City () {Name="Austin"},
                        new City () {Name="El Paso"}
                        }
                    }



                }
                });

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

