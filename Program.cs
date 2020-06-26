using System;
using System.IO;
using GeminiSkyFileMigrationTool;

class Program
{
    public static void Main(string[] args)
    {
        Start:
        Console.WriteLine("File Migration Tool\n\n");

        Console.Write("Enter the source path: ");
        string sourcePath = Console.ReadLine();
        Console.WriteLine(sourcePath);

        Console.Write("\n\nEnter the destination path: ");
        string destinationPath = Console.ReadLine();
        Console.WriteLine(destinationPath);

        Console.Write("\n\nEnter the filter: ");
        string filter = Console.ReadLine();
        Console.WriteLine(filter);

        Console.Write($"\n\nMigrate {filter} files from {sourcePath} to {destinationPath}? Y/N: ");
        string input = Console.ReadLine();

        if (input.ToLower() != "y")
        {
            goto Start;
        }
        else
        {
            new FileMigration(sourcePath, destinationPath, filter);

            Console.WriteLine($"\n{FileMigration.SourcePath}, {FileMigration.DestinationPath}, {FileMigration.Filter}\n\n");

            string[] path = { FileMigration.SourcePath };

            foreach (string file in path)
            {
                if (File.Exists(file))
                {
                    FileMigration.ScanFile(file);
                }
                else if (Directory.Exists(file))
                {
                    FileMigration.ScanDirectory(file);
                }
                else
                {
                    Console.WriteLine("{0} is not a valid file or directory.", path);
                }
            }
        }

        FileMigration.ScanComplete();
    }
}