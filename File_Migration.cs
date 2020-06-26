using System;
using System.IO;

namespace GeminiSkyFileMigrationTool
{
    class FileMigration
    {
        static int moved = 0;
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
                    File.Move(path, destination, true);
                    moved++;
                }
            }
            else
            {
                unableToMove++;
            }
        }

        public static void ScanComplete()
        {
            if (moved > 0)
            {
                Console.WriteLine($"Moved {moved} files from {SourcePath} to {DestinationPath}.");
            }
            if (unableToMove > 0)
            {
                Console.WriteLine($"Unable to move {unableToMove} files from {SourcePath} to {DestinationPath}.");
            }
            Console.WriteLine("\n\n\rScan complete...");
        }
    }
}