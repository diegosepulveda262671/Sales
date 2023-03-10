using System;
using Newtonsoft.Json;
using Sales.API.json;

namespace Sales.API.json
{
	public class FactoryCountries
	{
		private static string _filePath = "/Users/diegosepulveda/proyectos/itm/Sales/Sales.API/json/";
		public FactoryCountries()
		{
		}

		private static string GetStringDataFromFile(string fileName)
		{
			string stringDataFromFile;
			using (var reader = new StreamReader(_filePath + fileName))
			{
				stringDataFromFile = reader.ReadToEnd();
			}

			return stringDataFromFile;
		}

		public static string GetCountriesJsonFromFile()
		{
			return GetStringDataFromFile("countries.json");
		}



		public static List<Country> GetCountries()
		{
			

			List<Country> countries = JsonConvert.DeserializeObject<List<Country>>(GetStringDataFromFile("countries.json"));
			if (countries is null)
			{
				countries = new List<Country>();
			}

				
			return countries;
		}

        public static List<State> GetStates()
        {


            List<State> states = JsonConvert.DeserializeObject<List<State>>(GetStringDataFromFile("states.json"));
            if (states is null)
            {
                states = new List<State>();
            }


            return states;
        }

        public static List<City> GetCities()
        {


            List<City> cities = JsonConvert.DeserializeObject<List<City>>(GetStringDataFromFile("cities.json"));
            if (cities is null)
            {
                cities = new List<City>();
            }


            return cities;
        }

    }

	
}

