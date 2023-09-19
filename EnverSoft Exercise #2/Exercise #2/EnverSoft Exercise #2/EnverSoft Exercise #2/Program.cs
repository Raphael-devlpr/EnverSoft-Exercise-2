using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EnverSoft_Exercise__2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Read the CSV file
            List<string[]> csvData = File.ReadAllLines("data.csv") //path tothe csv will be specified
                .Select(line => line.Split(','))
                .ToList();

            // to Process the data
            Dictionary<string, int> nameFrequency = new Dictionary<string, int>();
            List<string> addresses = new List<string>();

            foreach (string[] data in csvData)
            {
                // Counts the frequency of first and last names
                string firstName = data[0];
                string lastName = data[1];
                string fullName = $"{firstName} {lastName}"; //all names displayed

                if (nameFrequency.ContainsKey(fullName))  //conditonal (checks name) uses 'Contains()'
                    nameFrequency[fullName]++;
                else
                    nameFrequency[fullName] = 1;


                string address = data[2];  // Collecting and storing addresses
                addresses.Add(address);
            }

            // Write the first text file
            string firstFileContent = nameFrequency
                .OrderByDescending(kv => kv.Value)
                .ThenBy(kv => kv.Key)
                .Select(kv => $"{kv.Key}: {kv.Value}")
                .Aggregate((s1, s2) => s1 + Environment.NewLine + s2);

            File.WriteAllText("name_frequency.txt", firstFileContent);

            //  second text file
            string secondFileContent = addresses
                .OrderBy(a => ExtractStreetName(a))
                .Aggregate((s1, s2) => s1 + Environment.NewLine + s2);

            File.WriteAllText("sorted_addresses.txt", secondFileContent);
        }

        static string ExtractStreetName(string address)
        {
            string[] parts = address.Split(' ');
            string streetName = string.Join(" ", parts.Skip(1));

            // to remove leading/trailing punctuation marks
            streetName = new string(streetName
                .Where(c => !char.IsPunctuation(c))
                .ToArray());

            return streetName;
        }
    }

}

