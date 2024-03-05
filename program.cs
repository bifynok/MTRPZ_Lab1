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
            ChangeText();
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

    static void ChangeText()
    {
        for (int i = 0; i < text.Length; i++)
        {
            CheckItalic(ref text, i);
        }
    }

    static void CheckItalic(ref string text, int i)
    {
        if (text[i] == '_')
        {
            if (i == 0 || (i > 0 && !char.IsLetterOrDigit(text[i - 1])))
            {
                if (i < text.Length - 1)
                {
                    if (char.IsLetterOrDigit(text[i + 1]))
                    {
                        text = text.Remove(i, 1);
                        text = text.Insert(i, "<i>");
                        CloseItalic(ref text, i);
                    }
                    else if (char.IsPunctuation(text[i + 1]) || text[i + 1] == '`')
                    {
                        text = text.Remove(i, 1);
                        text = text.Insert(i, "<i>");
                        CloseItalic(ref text, i);
                    }
                }
            }
        }
    }

    static void CloseItalic(ref string text, int i)
    {
        for (int j = i + 1; j < text.Length; j++)
        {
            if (text[j] == '_')
            {
                if (j == text.Length-1 || (j < text.Length-1 && !char.IsLetterOrDigit(text[j+1])))
                {
                    if (j > 0)
                    {
                        if (char.IsLetterOrDigit(text[j - 1]))
                        {
                            text = text.Remove(j, 1);
                            text = text.Insert(j, "</i>");
                            break;
                        }
                        else if (char.IsPunctuation(text[j - 1]) || text[j - 1] == '`' || text[j - 1] == '>')
                        {
                            text = text.Remove(j, 1);
                            text = text.Insert(j, "</i>");
                            break;
                        }
                    }
                }
            }
            else if (j == text.Length - 1)
            {
                throw new Exception("Unclosed Punctuation");
            }
        }
    }



}
