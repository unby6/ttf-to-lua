using System.Diagnostics;
using System.Text;
using Typography.OpenFont;
using static System.Net.Mime.MediaTypeNames;

static class Program
{
    static string Truncate(this string value, int maxLength)
    {
        if (string.IsNullOrEmpty(value))
            return value;
        return value.Length <= maxLength ? value : value.Substring(0, maxLength);
    }

    static string Tabs(short count)
    {
        return new string('\t', count);
    }
    static string? GetChar(Typeface font, ushort glyphId)
    {
        for (int codePoint = 0; codePoint <= 0x10FFFF; codePoint++)
        {
            if (font.GetGlyphIndex(codePoint) == glyphId)
            {
                return char.ConvertFromUtf32(codePoint);
            }
        }

        return null;
    }
    static string? GetGlyphString(Typeface font, ushort glyphId)
    {
        Glyph glyph = font.GetGlyph(glyphId);

        string? glyphChar = GetChar(font, glyphId);

        if (glyphChar == null)
            return null;

        if (glyphChar == "\\")
            glyphChar = "\\\\";
        else if (glyphChar == "\"")
            glyphChar = "\\\"";

        string finStr = $"[\"{glyphChar}\"]={{";

        finStr += $"{font.GetAdvanceWidthFromGlyphIndex(glyphId)},";
        finStr += $"{font.GetLeftSideBearing(glyphId)},";
        finStr += $"{glyph.Bounds.XMin},";
        finStr += $"{glyph.Bounds.XMax},";
        finStr += $"{glyph.Bounds.YMin},";
        finStr += $"{glyph.Bounds.YMax}";
        finStr += $"}}";
        return finStr;
    }
    static void Main(string[] args)
    {
        Console.WriteLine("Loading...");

        if (args.Length == 0)
        {
            Console.WriteLine("No File passed");
            Console.ReadKey();
            Environment.Exit(0);
            return;
        }
        else
            Console.WriteLine(args[0]);

        Console.WriteLine("Loading .ttf...");

        FileStream file = File.OpenRead(args[0]);

        OpenFontReader reader = new();
        Typeface font = reader.Read(file);
        file.Close();

        Console.WriteLine("Compiling font data...");

        string finalOutput =
              $"local FontFace = {{\n"
            + $"{Tabs(1)}FontConfig = {{\n"
            + $"{Tabs(2)}Ascender = {font.Ascender},\n"
            + $"{Tabs(2)}Descender = {font.Descender},\n"
            + $"{Tabs(2)}LineGap = {font.LineGap},\n"
            + $"{Tabs(2)}UnitsPerEm = {font.UnitsPerEm}\n"
            + $"{Tabs(1)}}},\n"
            + $"{Tabs(1)}GlyphConfig = {{\n";

        for (ushort i=0; i < font.GlyphCount; i++)
        {
            string? thestr = GetGlyphString(font, i);

            if (thestr == null)
                continue;

            finalOutput += thestr + (i < font.GlyphCount - 1 ? ",\n" : "");
        }

        finalOutput += $"\n}}\n}}\n\nreturn FontFace";

        Console.WriteLine("Writing File...");

        FileStream outputFile = File.Create(Truncate(args[0], args[0].Length-4) + ".txt");
        using StreamWriter writer = new(outputFile, Encoding.UTF8);

        writer.Write(finalOutput);

        Console.WriteLine("Done!");
        Console.ReadKey();
    }
}