using System;

class Program
{
    static string text =@"";
    static string path = "", name = "";
    
    static void Main()
    {
        try
        {
            AskForFile(ref text, ref path, ref name);
        }
        catch(Exeption ex)
        {
            Console.Error.WriteLine("Error: " + ex);
            Console.ReadKey();
        }
    }

    static void AskForFile(ref string text, ref string path, ref string name)
    {
        Console.WriteLine("Enter the path to the text file:\nExample: \"C:\\Documents\\Super Secret\\file.md\"");
        string filePath = Console.ReadLine();
        if (Path.GetExtension(filePath) == ".md")
        {
            if (File.Exists(filePath))
            {
                text = File.ReadAllText(filePath);
                path = Path.GetDirectoryName(filePath) + "\\";
                name = Path.GetFileNameWithoutExtension(filePath);
            }
            else
            {
                Console.WriteLine("File does not exist. Please try again.");
                AskForFile(ref text, ref path, ref name);
            }
        }
        else
        {
            Console.WriteLine("The extention must be .md. Please try again.");
            AskForFile(ref text, ref path, ref name);
        }
    }
}