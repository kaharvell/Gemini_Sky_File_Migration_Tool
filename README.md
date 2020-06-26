# Gemini_Sky_File_Migration_Tool
A console app that will move files based on their extension to a user input destination.

user inputs three parameters; source path, destination path, and filter.

Source path is the folder the console app will scan, enter the directory here by copying the path from file explorer or typing it in manually.
  I.E. C:\Users\user\Desktop\SVGBundle is the input of the source path, all files and subdirectories inside of "SVGBundle" located on user's desktop
  will be searched for files ending with filter.

Destination path is the folder the files will be moved to. If this path does not exist, the console app will create it before moving files.
  I.E. C:\Users\user\Desktop\SVG is the input of destination path, but all files will move to the folder "SVG" located on user's desktop.
  
Filter is search parameter, in this context it will be the file extension.
  I.E. If we input ".svg", the console app will scan all files and subfolders for files that end with ".svg", then move them to the destination path.
  
  

If the file is protected, or requires additional permissions, or if a path does not exist as a source path, or is not reachable as a destination path,
  the application will exit without migrating.
  
Future implementation will include GUI, additional filters prior to program execution, and ability to extract directly from zip files with filter parameters.
