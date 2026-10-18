using LINQ.DataSources;
using LINQ.Models;
using System.Collections.Generic;
using System.Linq;


namespace LinQ2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //Q1
            var result1 = Source.ProductList
                .OrderBy(p => p.Category)
                .ThenByDescending(p => p.UnitPrice);

            foreach (var p in result1)
            {
                Console.WriteLine($"{p.ProductName} - {p.Category} - {p.UnitPrice}");
            }
            Console.WriteLine("====================================");
            //Q2
            var result2 = Source.ProductList
                .Where(p => p.Category == "Beverages")
                .OrderByDescending(p => p.UnitsInStock);

            foreach (var p in result2)
                Console.WriteLine($"{p.ProductName} - {p.UnitsInStock}");

            Console.WriteLine("=====================================");

            //Q3
            var result3 =
                from c in Source.CustomerList
                from o in c.Orders
                where o.OrderDate.Year >= 1997
                select new { c.CustomerID, o.OrderDate };

            foreach (var o in result3)
                Console.WriteLine($"{o.CustomerID} - {o.OrderDate:yyyy-MM-dd}");

            Console.WriteLine("====================================");

            //Q4
            var result4 = Source.ProductList
                .Select((p, index) => new
                {
                    Position = index + 1,
                    p.ProductName
                });

            foreach (var p in result4)
                Console.WriteLine($"{p.Position} - {p.ProductName}");

            Console.WriteLine("====================================");

            //Q5
            string[] Arr =
            {
                "aPPLE", "AbAcUs", "bRaNcH",
                "BlUeBeRrY", "ClOvEr", "cHeRry"
            };

            var result5 = Arr
                .OrderBy(s => s.Length)
                .ThenBy(s => s, StringComparer.OrdinalIgnoreCase);

            foreach (var word in result5)
                Console.WriteLine(word);

            Console.WriteLine("====================================");

            //Q6
            var result6 = Arr
                .Where(s => s.Length > 1 && s[1] == 'i')
                .Reverse();

            if (result6.Any())
            {
                foreach (var word in result6)
                    Console.WriteLine(word);
            }
            else
            {
                Console.WriteLine("No matching words");
            }

            Console.WriteLine("====================================");

            //Q7
            var result7 = Source.ProductList
                .OrderByDescending(p => p.UnitPrice)
                .Take(3);

            foreach (var p in result7)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

            Console.WriteLine("====================================");

            //Q8
            var result8 = Source.ProductList
                .Skip(5)
                .Take(5);

            foreach (var p in result8)
                Console.WriteLine($"{p.ProductID} - {p.ProductName}");

            Console.WriteLine("====================================");

            //Q9
            var result9 = Source.ProductList
                .OrderBy(p => p.UnitPrice)
                .TakeWhile(p => p.UnitPrice < 25);

            foreach (var p in result9)
                Console.WriteLine($"{p.ProductName} - {p.UnitPrice}");

            Console.WriteLine("====================================");

            //Q10
            bool result10 = Source.ProductList
                .Where(p => p.Category == "Seafood")
                .All(p => p.UnitsInStock > 0);

            Console.WriteLine($"All Seafood products are in stock: {result10}");

            Console.WriteLine("====================================");

            //Q11
            int[] ids = { 3, 9, 13, 18 };

            bool result11 = ids.Contains(9);

            Console.WriteLine($"Contains 9: {result11}");

            Console.WriteLine("====================================");

            //Q12
            var result12 = Source.ProductList
                .GroupBy(p => p.Category);

            foreach (var group in result12)
                Console.WriteLine($"{group.Key} - Count: {group.Count()}");

            Console.WriteLine("====================================");

            //Q13
            var result13 = Source.ProductList
                .GroupBy(p => p.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    ProductNames = g.Select(p => p.ProductName)
                });

            foreach (var group in result13)
            {
                Console.WriteLine(group.Category);

                foreach (var name in group.ProductNames)
                    Console.WriteLine($"  {name}");
            }

            Console.WriteLine("====================================");

            //Q14
            var result14 = Source.ProductList
                .GroupBy(p => p.Category)
                .Where(g => g.Count() > 3)
                .Select(g => new
                {
                    Category = g.Key,
                    Count = g.Count()
                });

            foreach (var group in result14)
                Console.WriteLine($"{group.Category} - {group.Count}");

            Console.WriteLine("====================================");

            //Q15
            var result15 =
                from c in Source.CustomerList
                group c by c.Country into countryGroup
                select new
                {
                    Country = countryGroup.Key,
                    Count = countryGroup.Count(),
                    TotalOrderValue = countryGroup
                        .SelectMany(c => c.Orders)
                        .Sum(o => o.Total)
                };

            foreach (var group in result15)
            {
                Console.WriteLine(
                    $"{group.Country} - Customers: {group.Count} - Total Orders: {group.TotalOrderValue}");
            }

            Console.WriteLine("====================================");

            //Q16
            var result16 = Source.ProductList
                .Sum(p => p.UnitsInStock);

            Console.WriteLine($"Total units in stock: {result16}");

            Console.WriteLine("====================================");

            //Q17
            var cheapest = Source.ProductList.Min(p => p.UnitPrice);
            var mostExpensive = Source.ProductList.Max(p => p.UnitPrice);

            Console.WriteLine($"Cheapest price: {cheapest}");
            Console.WriteLine($"Most expensive price: {mostExpensive}");

            Console.WriteLine("====================================");

            //Q18
            var result18 = Source.ProductList
                .Select(p => p.Category)
                .Distinct();

            foreach (var category in result18)
                Console.WriteLine(category);

            Console.WriteLine("====================================");

            //Q19
            int[] setA = { 1, 3, 5, 7, 9, 11, 13 };
            int[] setB = { 3, 6, 9, 12, 15, 13 };

            var result19 = setA.Except(setB);

            Console.WriteLine(string.Join(", ", result19));

            Console.WriteLine("====================================");

            //Q20
            string[] list1 = { "Germany", "France", "UK", "Spain" };
            string[] list2 = { "france", "SPAIN", "Italy" };

            var result20 = list1.Except(
                list2,
                StringComparer.OrdinalIgnoreCase);

            foreach (var country in result20)
                Console.WriteLine(country);

            Console.WriteLine("====================================");

            //Q21
            var productDictionary = Source.ProductList
                .ToDictionary(p => p.ProductID);

            if (productDictionary.TryGetValue(18, out var product))
                Console.WriteLine($"{product.ProductID} - {product.ProductName} - {product.UnitPrice}");

            Console.WriteLine("====================================");

            //Q22
            var result22 = Source.ProductList
                .First(p => p.UnitPrice > 50);

            Console.WriteLine($"{result22.ProductName} - {result22.UnitPrice}");

            Console.WriteLine("====================================");

            //Q23
            var result23 = Source.ProductList
                .FirstOrDefault(p => p.UnitPrice > 500);

            if (result23 != null)
                Console.WriteLine($"{result23.ProductName} - {result23.UnitPrice}");
            else
                Console.WriteLine("No product found");

            Console.WriteLine("====================================");

            //Q24
            var result24 = Enumerable.Range(1, 10)
                .Select(n => $"7 x {n} = {7 * n}");

            foreach (var item in result24)
                Console.WriteLine(item);

            Console.WriteLine("====================================");

            //Q25
            var result25 = Enumerable.Range(1, 30)
                .Where(n => n % 2 == 0);

            Console.WriteLine(string.Join(", ", result25));

            Console.WriteLine("====================================");

            //Q26
            var result26 = Source.ProductList
                .Take(3)
                .Select(p => p.ProductName)
                .Concat(
                    Source.CustomerList
                        .Take(3)
                        .Select(c => c.CustomerName));

            foreach (var name in result26)
                Console.WriteLine(name);

            Console.WriteLine("====================================");

            //Q27
            var result27 = Source.ProductList
                .Zip(
                    Source.CustomerList,
                    (p, c) => $"{p.ProductName} sold to {c.CustomerName}");

            foreach (var item in result27)
                Console.WriteLine(item);

            
        }
    }
}
