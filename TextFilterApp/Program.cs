using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace TextFilterApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Please the text Files Location");
            var file = Console.ReadLine();

            string[] lines = File.ReadAllLines(file);

            TextFilter textFilter = new TextFilter();

            foreach (string line in lines)
            {
                var filteredText = textFilter.FilterText(line, new char[]{'a','e','i','o','u'}, new char[] {'t'}, 3);
                Console.WriteLine("\t" + filteredText);
            }

            Console.WriteLine("Press any key to exit.");
            System.Console.ReadKey();
        }
    }
}
