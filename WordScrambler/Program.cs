using System;
using System.Collections.Generic;
using System.Linq;
using NHunspell;

namespace WordScrambler
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var words = WordBuilder.GetWords();
            var lengthLimitedWords = words.Where(w => w.Length <= 6);
            Console.WriteLine(lengthLimitedWords.Count());

            var letters = lengthLimitedWords.SelectMany(w => w.Select(c => c));
            var groupedLetters = letters.GroupBy(c => c);
            var lettersOrderedByOccurences = groupedLetters.OrderBy(x => x.Count()).Reverse();
            foreach (var lettersOrderedByOccurence in lettersOrderedByOccurences)
            {
                Console.WriteLine($"{lettersOrderedByOccurence.Key} and {lettersOrderedByOccurence.Count()}");
            }

            var lettersToCheck = new List<char> { 'e', 'r', 'a', 't', 'o', 'i', 's', 'l', 'n', 'c', 'u', 'h', 'd', 'm', 'y', 'w', 'f', 'p', 'g', 'b', 'k' };
            using (var dict = new Hunspell())
            {
/*                dict.Analyze();*/
            }
            
        }
    }
}