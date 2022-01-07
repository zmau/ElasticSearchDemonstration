using System.IO;
using System.Collections.Generic;

namespace DataPreparer
{
    class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length < 1)
                return;

            string inputFileName = args[0]+".ndjson";
            int counter = 1;
            string indexName = "prop";
            string lineTemplate = "{{ \"index\" : {{ \"_index\": \"{0}\", \"_id\" : \"{1}\" }} }}";
            string lineToInsert;
            List<string> output = new List<string>();
            IEnumerable<string> lines = File.ReadLines(inputFileName);
            foreach (string line in lines)
            {
                lineToInsert = string.Format(lineTemplate, indexName, counter++);
                output.Add(lineToInsert);
                output.Add(line);
            }
            string outputFileName = args[0] + " prepared.ndjson";
            File.WriteAllLines(outputFileName, output.ToArray());
        }


    }
}
