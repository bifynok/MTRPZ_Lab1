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
            CheckParag(ref text);
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
            CheckBold(ref text, i);
            CheckPref(ref text, ref i);
            CheckMono(ref text, i);
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

    static void CheckBold(ref string text, int i)
    {
        if (text[i] == '*')
        {
            if (i < text.Length - 2 && text[i + 1] == '*')
            {
                if (char.IsLetterOrDigit(text[i + 2]))
                {
                    text = text.Remove(i, 2);
                    text = text.Insert(i, "<b>");
                    CloseBold(ref text, i);
                }
                else if (char.IsPunctuation(text[i + 2]) || text[i + 2] == '`')
                {
                    text = text.Remove(i, 2);
                    text = text.Insert(i, "<b>");
                    CloseBold(ref text, i);
                }
            }
        }
    }

    static void CloseBold(ref string text, int i)
    {
        for (int j = i + 1; j < text.Length; j++)
        {
            if (text[j] == '*')
            {
                if (j > 1 && text[j - 1] == '*')
                {
                    if (char.IsLetterOrDigit(text[j - 2]))
                    {
                        text = text.Remove(j - 1, 2);
                        text = text.Insert(j - 1, "</b>");
                        break;
                    }
                    else if (char.IsPunctuation(text[j - 2]) || text[j - 2] == '`' || text[j - 2] == '>')
                    {
                        text = text.Remove(j - 1, 2);
                        text = text.Insert(j - 1, "</b>");
                        break;
                    }
                }
            }
            else if (j == text.Length - 1)
            {
                throw new Exception("Unclosed Punctuation");
            }
        }
    }

    static void CheckMono(ref string text, int i)
    {
        if (text[i] == '`')
        {
            if (i < text.Length - 1)
            {
                if (char.IsLetterOrDigit(text[i + 1]))
                {
                    text = text.Remove(i, 1);
                    text = text.Insert(i, "<tt>");
                    CloseMono(ref text, i);
                }
                else if (char.IsPunctuation(text[i + 1]) || text[i + 1] == '`')
                {
                    text = text.Remove(i, 1);
                    text = text.Insert(i, "<tt>");
                    CloseMono(ref text, i);
                }
            }
        }
    }

    static void CloseMono(ref string text, int i)
    {
        for (int j = i + 1; j < text.Length; j++)
        {
            if (text[j] == '`')
            {
                if (j > 0)
                {
                    if (char.IsLetterOrDigit(text[j - 1]))
                    {
                        text = text.Remove(j, 1);
                        text = text.Insert(j, "</tt>");
                        break;
                    }
                    else if (char.IsPunctuation(text[j - 1]) || text[j - 1] == '`' || text[j - 1] == '>')
                    {
                        text = text.Remove(j, 1);
                        text = text.Insert(j, "</tt>");
                        break;
                    }
                }
            }
            else if (j == text.Length - 1)
            {
                throw new Exception("Unclosed Punctuation");
            }
        }
    }

    static void CheckPref(ref string text, ref int i)
    {
        if (text[i] == '`')
        {
            if (i < text.Length - 3 && text[i + 1] == '`' && text[i+2]== '`')
            {
                text = text.Remove(i, 3);
                text = text.Insert(i, "<pre>");
                ClosePref(ref text, ref i);
            }
        }
    }

    static void ClosePref(ref string text, ref int i)
    {
        for (int j = i + 1; j < text.Length; j++)
        {
            if (text[j] == '`')
            {
                if (j > 2 && text[j - 1] == '`' && text[j - 2] == '`')
                {
                    text = text.Remove(j - 2, 3);
                    text = text.Insert(j - 2, "</pre>");
                    i = j;
                    break;
                }
            }
            else if (j == text.Length - 1)
            {
                throw new Exception("Unclosed Punctuation");
            }
        }
    }

    static void CheckParag(ref string text)
    {
        text = "<p>" + text;
        CloseParag(ref text, 0);
    }
        
    static void CloseParag(ref string text, int i)
    {
        for (int j = i; j < text.Length; j++)
        {
            if (text[j] == '\n')
            {
                if (j < text.Length - 3 && text[j + 2] == '\n')
                {
                    text = text.Remove(j - 1, 4);
                    text = text.Insert(j - 1, "</p><p>");
                    CloseParag(ref text, j);
                    break;
                }
                else if (j == text.Length - 3 && text[j + 2] == '\n')
                {
                    text = text.Remove(j - 1, 4);
                    text = text + "</p>";
                    break;
                }
                else if (j == text.Length - 1)
                {
                    text = text.Remove(j - 1, 2);
                    text = text + "</p>";
                    break;
                }
            }
            else if (j == text.Length - 1)
            {
                text = text + "</p>";
                break;
            }
        }
    }



}
