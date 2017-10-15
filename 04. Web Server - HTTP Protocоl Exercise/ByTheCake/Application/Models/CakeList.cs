using System;
using System.Collections.Generic;
using System.IO;

namespace WebServer.Application.Models
{
    public static class CakeList
	{
		private static List<Cake> cakes = new List<Cake>();

		private const string dbPath =
				@"C:\Users\Bogdan Alov\Documents\Visual Studio 2017\Projects\Asynchronous-Programming-Exercise-Concurrent Users\WebServer\Application\Data\database.csv"
			;

		private static bool isInitialized = false;
		
		public static void Add(Cake cake)
		{
			cakes.Add(cake);
			SaveToDb(cake);
		}

		private static void SaveToDb(Cake cake)
		{
			File.AppendAllText(dbPath, $"{cake.Name},{cake.Price}{Environment.NewLine}");
		}

		public static string AllCakes()
		{
			if (!isInitialized)
			{
				AddCakesFromDb();
				isInitialized = true;
			}

			var result = "<pre style=\"font-size:larger\">";

			foreach (var cake in cakes)
			{
				result += $"Name: {cake.Name}, Price: {cake.Price:F2}\n";
			}

			result += "</pre>";
			return result;
		}

		public static string Search(string criteria)
		{
			if (!isInitialized)
			{
				AddCakesFromDb();
				isInitialized = true;
			}

			var result = "<pre style=\"font-size:larger\">";

			foreach (var cake in cakes)
			{
				if (cake.Name.ToLower().Contains(criteria))
				{
					result += $"Name: {cake.Name}, Price: {cake.Price:F2}\n";
				}
			}

			result += "</pre>";

			return result;
		}

		private static void AddCakesFromDb()
		{
			var database = File.ReadAllLines(dbPath);

			foreach (var cake in database)
			{
				if (!string.IsNullOrEmpty(cake))
				{
					var arr = cake.Split(',');
					var name = arr[0];
					var price = decimal.Parse(arr[1]);
					var realCake = new Cake(name, price);
					cakes.Add(realCake);
				}
			}
		}
	}
}