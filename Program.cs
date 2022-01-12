using Newtonsoft.Json;
using System;
using System.IO;
using System.Linq;

namespace ExtractRustIems
{
    class Program
    {
        public const string OutputFileName = "itemsExperimental.txt";
        public const string InvalidArguments = "    Invalid number of arguments!";
        public const string InvalidCommand = "Invalid command!";
        public const string HelpMessage = "    Usage: ItemsExperimental -extract <path to item json files>";
        public const string NothingFound = @"    No valid json files were found at {0}";
        public const string SuccessMessage = @"    {0} items saved to {1}";

        static string[] Filenames;
        static string[] ItemLines;
        static string JsonFilePath = "";
        static int ValidJsonFiles;

        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(InvalidArguments);
                return;
            }

            var command = args[0];

            switch (command)
            {
                case "extract":
                case "/e":
                case "/extract":
                case "-e":
                case "-extract":
                case "--extract":
                    ExtractMain(args);
                    break;

                case "?":
                case "/h":
                case "/help":
                case "-h":
                case "-help":
                case "--help":
                    DisplayHelp();
                    break;
                default:
                    Console.WriteLine(InvalidCommand);
                    break;
            }
        }

        private static void ExtractMain(string[] args)
        {
            GetPath(args);
            GetFileNames();
            if (Filenames.Length == 0)
            {
                Console.WriteLine(NothingFound);
                return;
            }

            ProcessFiles();
            CreateOutput();
        }

        private static void GetPath(string[] args)
        {
            if (args[1] == null)
            {
                JsonFilePath = Environment.CurrentDirectory;
            }
            else
            {
                JsonFilePath = args[1].Trim();
            }
        }

        private static void DisplayHelp()
        {
            JsonFilePath = "";
            Console.WriteLine(HelpMessage);
        }

        private static void GetFileNames()
        {
            string[] Filenames = Directory.GetFiles(JsonFilePath, "*.json");
        }

        private static void ProcessFiles()
        {
            ItemLines = new string[Filenames.Length];

            int FilesExamined = 0;
            ValidJsonFiles = 0;
            foreach (var fileName in Filenames)
            {
                string contents = File.ReadAllText(fileName);
                RustItem NewRustItem = JsonConvert.DeserializeObject<RustItem>(contents);
                if (NewRustItem.shortname != null)
                {
                    string NewItemLine = $"{NewRustItem.Category} - {NewRustItem.Name}|{NewRustItem.shortname}";
                    ItemLines[ValidJsonFiles] = NewItemLine;
                    ValidJsonFiles += 1;
                }
                FilesExamined += 1;
            }

            Console.WriteLine(@"Processed {0} of {1} item files", ValidJsonFiles, FilesExamined);
        }

        private static void CreateOutput()
        {
            Array.Sort(ItemLines);
            if (ValidJsonFiles > 0)
            {
                File.WriteAllLines(OutputFileName, ItemLines);
                Console.WriteLine(SuccessMessage, ItemLines.Count().ToString(), OutputFileName);
            }
            else
            {
                Console.WriteLine(NothingFound, JsonFilePath);
            }
        }
    }
    public class Condition
    {
        public bool enabled { get; set; }
        public double max { get; set; }
        public bool repairable { get; set; }
    }

    public class RustItem
    {
        public int itemid { get; set; }
        public string shortname { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public int maxDraggable { get; set; }
        public string ItemType { get; set; }
        public string AmountType { get; set; }
        public int stackable { get; set; }
        public bool quickDespawn { get; set; }
        public string rarity { get; set; }
        public Condition condition { get; set; }
        public int Parent { get; set; }
        public bool isWearable { get; set; }
        public bool isHoldable { get; set; }
        public bool isUsable { get; set; }
        public bool HasSkins { get; set; }
    }

}
