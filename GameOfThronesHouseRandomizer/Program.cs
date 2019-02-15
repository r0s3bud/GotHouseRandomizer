using GameOfThronesHouseRandomizer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfThronesHouseRandomizer
{
    class Program
    {
        public static List<Player> PlayerList { get; set; } = new List<Player>();

        public static List<RandomizerResult> Results { get; set; } = new List<RandomizerResult>();

        static void Main()
        {
            PopulatePlayerList();
            Console.WriteLine();
            Randomize();           
            for (var i = 0; i < PlayerList.Count; i++) {
                var MaxValue = GetMaxResult();
                var highestScoredResult = Results.Where(x => x.Scores.Max() == MaxValue).First();
                Results.Remove(Results.Where(x => x.Scores.Max() == MaxValue).First());
                var houseIndex = highestScoredResult.Scores.ToList().IndexOf(highestScoredResult.Scores.Max());
                PlayerList.ElementAt(highestScoredResult.PlayerID).House = (Houses)houseIndex + 1;
                Results.ForEach(x => {
                    x.Scores[houseIndex] = 0;
                });
            }

            PlayerList.ForEach(x =>
            {
                Console.WriteLine();
                Console.WriteLine(Colorize(x.House) + " : " + x.PlayerName);
            });
            Console.ResetColor();
            Console.ReadLine();
        }

        public static void PopulatePlayerList()
        {            
            Console.WriteLine("Enter the name of the first player");
            PlayerList.Add(new Player() { PlayerName = Console.ReadLine() });
            Console.WriteLine("Enter the name of the second player");
            PlayerList.Add(new Player() { PlayerName = Console.ReadLine() });
            Console.WriteLine("Enter the name of the third player");
            PlayerList.Add(new Player() { PlayerName = Console.ReadLine() });
            Console.Write("Enter the name of the fourth player, or press enter if there are no more players");
            var input = Console.ReadLine();
            if (input == string.Empty)
            {
                return;
            }

            PlayerList.Add(new Player() { PlayerName = input });
            Console.WriteLine("Enter the name of the fifth player, or press enter if there are no more players");
            input = Console.ReadLine();
            if (input == string.Empty)
            {
                return;
            }

            PlayerList.Add(new Player() { PlayerName = input });
            Console.WriteLine("Enter the name of the sixth player, or press enter if there are no more players");
            input = Console.ReadLine();
            if (input == string.Empty)
            {
                return;
            }

            PlayerList.Add(new Player() { PlayerName = input });
        }

        public static void Randomize()
        {
            Random rnd = new Random();

            int index = 0;
            foreach (var item in PlayerList)
            {
                Results.Add(new RandomizerResult() { PlayerID = index });
                for (int j = 0; j < 10000; j++)
                {
                    int dice = rnd.Next(1, 7);
                    Results[index].Scores[dice-1]++;
                }
                
                index++;
            }        
        }

        public static int GetMaxResult()
        {
            var resultsAggregated = Results.Select(x => x.Scores);
            var maxValues = new List<int>();
            foreach (int[] result in resultsAggregated)
            {
                maxValues.Add(result.Max());
            }

            return maxValues.Max();
        }

        public static Houses Colorize(Houses name)
        {
            Console.ResetColor();

            switch(name)
            {
                case Houses.Stark:
                    Console.ForegroundColor = ConsoleColor.White;
                    break;
                case Houses.Baratheon:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;                
                case Houses.Greyjoy:
                    Console.ForegroundColor = ConsoleColor.DarkBlue;
                    break;
                case Houses.Lannister:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case Houses.Tyrell:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
                case Houses.Martell:
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    break;
            }

            return name;
        }
    }
}
