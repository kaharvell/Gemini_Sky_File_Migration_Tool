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

        Console.Write("\n\nEnter the destination path: ");
        string destinationPath = Console.ReadLine();

        Console.Write("\n\nEnter the filter: ");
        string filter = Console.ReadLine();

        Console.Write($"\n\nMigrate {filter} files from {sourcePath} to {destinationPath}?\n\n\tY/N: ");
        string input = Console.ReadLine();
        
        Console.WriteLine();

        if (input.ToLower() != "y")
        {
            Console.Clear();
            goto Start;
        }
        else
        {
            new FileMigration(sourcePath, destinationPath, filter);

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
