namespace _01._School_Competition
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StartUp
    {
        public static void Main()
        {
            //Task 1 done here
            var scores = new Dictionary<string, int>();
            var categories = new Dictionary<string, SortedSet<string>>();

            while (true)
            {
                var line = Console.ReadLine();

                if (line == "END")
                {
                    break;
                }

                var parts = line.Split(' ');
                var name = parts[0];
                var category = parts[1];
                var score = int.Parse(parts[2]);

                if (!scores.ContainsKey(name))
                {
                    scores[name] = 0;
                }

                if (!categories.ContainsKey(name))
                {
                    categories[name] = new SortedSet<string>();
                }

                scores[name] += score;
                categories[name].Add(category);
            }

            var orderedStudents = scores
                .OrderByDescending(kvp => kvp.Value)
                .ThenBy(kvp => kvp.Key);

            foreach (var studentKvp in orderedStudents)
            {
                var name = studentKvp.Key;
                var score = studentKvp.Value;
                var studentCategories = categories[name];

                var categoriesText = $"[{string.Join(", ", studentCategories)}]";

                Console.WriteLine($"{name}: {score} {categoriesText}");
            }
        }
    }
}