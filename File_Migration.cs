using System;
using System.Collections.Generic;
using System.IO;

namespace GeminiSkyFileMigrationTool
{
    class FileMigration
    {
        static List<string> moved = new List<string>();
        static List<string> unableToMoveWithFilter = new List<string>();

        static int unableToMove = 0;

        public static string SourcePath { get; set; }
        public static string DestinationPath { get; set; }
        public static string Filter { get; set; }

        public FileMigration(string sourcePath, string destinationPath, string filter)
        {
            SourcePath = sourcePath;
            DestinationPath = destinationPath;
            Filter = filter;
        }

        public static void ScanDirectory(string directory)
        {
            string[] fileExists = Directory.GetFiles(directory);
            foreach (string file in fileExists)
                ScanFile(file);

            string[] subdirectoryExists = Directory.GetDirectories(directory);
            foreach (string subdirectory in subdirectoryExists)
                ScanDirectory(subdirectory);
        }

        public static void ScanFile(string path)
        {
            string destination = DestinationPath + path.Substring(path.LastIndexOf(@"\"));

            if (path.EndsWith(Filter))
            {
                if (!Directory.Exists(DestinationPath))
                {
                    Directory.CreateDirectory(DestinationPath);
                }
                if (!File.Exists(destination))
                {
                    try
                    {
                        File.Move(path, destination, true);
                        moved.Add(path);
                    }
                    catch
                    {
                        unableToMoveWithFilter.Add(path);
                    }
                }
            }
            else
            {
                unableToMove++;
            }
        }

        public static void ScanComplete()
        {
            if (moved.Count > 0)
            {
                Console.WriteLine($"Moved {moved.Count} files from {SourcePath} to {DestinationPath}.\n\n");
            }
            if (unableToMove > 0)
            {
                Console.WriteLine($"Unable to move {unableToMove + unableToMoveWithFilter.Count} files from {SourcePath} to {DestinationPath}.\n\n");
            }
            if (unableToMoveWithFilter.Count > 0)
            {
                Console.WriteLine($"Unable to move {unableToMoveWithFilter.Count} files with {Filter} filter: ");

                foreach (string s in unableToMoveWithFilter)
                {
                    Console.WriteLine($"\r{s}");
                }
            }
            Console.WriteLine("\n\n\rScan complete...");
        }
    }
}
