using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace ExtractRustItems
{
    class Program
    {
        public const string OutputFileName = "itemsExperimental.txt";
        public const string InvalidArguments = "    Missing path to JSON files!";
        public const string InvalidCommand = "Invalid command!";
        public const string NothingFound = @"    No valid json files were found at {0}";
        public const string SuccessMessage = @"    {0} items saved to {1}";

        static string[] Filenames;
        static string[] ItemLines;
        static string FullFilePath = "";

        static readonly string JsonFilePath = @"Bundles\items";
        static readonly string PreviousItemsFilename = "RustItems.json";
        static readonly string ChangesFilename = "Changes.txt";

        static int NewItems = 0;
        static int ValidJsonFiles;

        static Dictionary<string, RustItem> CurrentItems;
        static Dictionary<string, RustItem> PreviousRustItems;


        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine(InvalidArguments);
                return;
            }

            GetPath(args[0]);
            GetFileNames();
            if (Filenames.Length == 0)
            {
                Console.WriteLine(NothingFound);
                return;
            }

            ProcessFiles();
            SaveNewItems();
            Array.Sort(ItemLines);
            CreateOutput();
        }

        private static void GetPath(string serverFilePath)
        {
            if (String.IsNullOrEmpty(serverFilePath))
            {
                FullFilePath = Environment.CurrentDirectory;
            }
            else
            {
                FullFilePath = serverFilePath.Trim();
                if (!FullFilePath.EndsWith(@"\"))
                {
                    FullFilePath += @"\";
                }

                FullFilePath += JsonFilePath;
            }
        }

        private static void GetFileNames()
        {
            Filenames = Directory.GetFiles(FullFilePath, "*.json");
        }

        private static void ProcessFiles()
        {
            if (File.Exists(ChangesFilename))
            {
                File.Delete(ChangesFilename);
            }

            CurrentItems = new Dictionary<string, RustItem>();
            PreviousRustItems = new Dictionary<string, RustItem>();
            ItemLines = new string[Filenames.Length];
            int FilesExamined = 0;
            ValidJsonFiles = 0;

            if (File.Exists(PreviousItemsFilename))
            {
                string contents = File.ReadAllText(PreviousItemsFilename);
                PreviousRustItems = JsonConvert.DeserializeObject<Dictionary<string, RustItem>>(contents);
            }

            foreach (var fileName in Filenames)
            {
                string contents = File.ReadAllText(fileName);
                RustItem NewRustItem = JsonConvert.DeserializeObject<RustItem>(contents);
                CurrentItems.Add(NewRustItem.shortname, NewRustItem);
                if (!PreviousRustItems.ContainsKey(NewRustItem.shortname))
                {
                    ++NewItems;
                    string content = String.Format($"{NewRustItem.shortname} was added");
                    File.AppendAllText(ChangesFilename, content + Environment.NewLine);
                }


                if (!String.IsNullOrEmpty(NewRustItem.shortname))
                {
                    if (String.IsNullOrEmpty(NewRustItem.Name))
                    {
                        NewRustItem.Name = TitleCase(NewRustItem.shortname.Replace('.', ' '));
                    }

                    string NewItemLine = $"{NewRustItem.Category} - {NewRustItem.Name}|{NewRustItem.shortname}";
                    ItemLines[ValidJsonFiles] = NewItemLine;
                    ++ValidJsonFiles;
                }
                ++FilesExamined;
            }
        }

        private static void SaveNewItems()
        {
            string json = JsonConvert.SerializeObject(CurrentItems, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(PreviousItemsFilename, json);
        }

        private static void CreateOutput()
        {
            Console.WriteLine($"ExtractRustItems.exe created by CatMeat, v0.3 (c) 2020");
            Console.WriteLine($"    Processed {ValidJsonFiles} item files.");
            if (!File.Exists(ChangesFilename))
            {
                File.AppendAllText(ChangesFilename, "No changes found on last run." + Environment.NewLine);
            }

            if (ValidJsonFiles > 0)
            {
                if (File.Exists(OutputFileName))
                {
                    File.Copy(OutputFileName, OutputFileName + ".last", true);
                }

                File.WriteAllLines(OutputFileName, ItemLines);
                Console.WriteLine(SuccessMessage, ItemLines.Count().ToString(), OutputFileName);
            }
            else
            {
                Console.WriteLine(NothingFound, FullFilePath);
            }

            if (NewItems > 0)
            {
                Console.WriteLine($"    {NewItems} items added since last run! See Changes.txt");
            }
            else
            {
                Console.WriteLine("    No new items were found since last run.");
            }
        }

        private static string TitleCase(string variableCase)
        {
            TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;
            return textInfo.ToTitleCase(variableCase.ToLower());
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
