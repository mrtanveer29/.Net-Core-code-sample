using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using PorterStemmerAlgorithm;
using NPOI.OpenXmlFormats.Dml;
/// <summary>
/// Tool to remove unwanted words such as 'the' or 'a'.
/// </summary>
static class TextFormaterTool
{
    /// <summary>
    /// Words we want to remove.
    /// </summary>
    static Dictionary<string, bool> _stops = new Dictionary<string, bool>
    {
        { "a", true },
        { "about", true },
        { "above", true },
        { "across", true },
        { "after", true },
        { "afterwards", true },
        { "again", true },
        { "against", true },
        { "all", true },
        { "almost", true },
        { "alone", true },
        { "along", true },
        { "already", true },
        { "also", true },
        { "although", true },
        { "always", true },
        { "am", true },
        { "among", true },
        { "amongst", true },
        { "amount", true },
        { "an", true },
        { "and", true },
        { "another", true },
        { "any", true },
        { "anyhow", true },
        { "anyone", true },
        { "anything", true },
        { "anyway", true },
        { "anywhere", true },
        { "are", true },
        { "around", true },
        { "as", true },
        { "at", true },
        { "back", true },
        { "be", true },
        { "became", true },
        { "because", true },
        { "become", true },
        { "becomes", true },
        { "becoming", true },
        { "been", true },
        { "before", true },
        { "beforehand", true },
        { "behind", true },
        { "being", true },
        { "below", true },
        { "beside", true },
        { "besides", true },
        { "between", true },
        { "beyond", true },
        { "bill", true },
        { "both", true },
        { "bottom", true },
        { "but", true },
        { "by", true },
        { "c", true },
        { "c++", true },
        { "call", true },
        { "can", true },
        { "cannot", true },
        { "cant", true },
        { "co", true },
        { "computer", true },
        { "con", true },
        { "could", true },
        { "couldnt", true },
        { "cry", true },
        { "de", true },
        { "describe", true },
        { "detail", true },
        { "do", true },
        { "done", true },
        { "down", true },
        { "due", true },
        { "during", true },
        { "each", true },
        { "eg", true },
        { "eight", true },
        { "either", true },
        { "eleven", true },
        { "else", true },
        { "elsewhere", true },
        { "empty", true },
        { "enough", true },
        { "etc", true },
        { "even", true },
        { "ever", true },
        { "every", true },
        { "everyone", true },
        { "everything", true },
        { "everywhere", true },
        { "except", true },
        { "few", true },
        { "fifteen", true },
        { "fify", true },
        { "fill", true },
        { "find", true },
        { "fire", true },
        { "first", true },
        { "five", true },
        { "for", true },
        { "former", true },
        { "formerly", true },
        { "forty", true },
        { "found", true },
        { "four", true },
        { "from", true },
        { "front", true },
        { "full", true },
        { "further", true },
        //{ "get", true },
        { "give", true },
        { "go", true },
        { "had", true },
        { "has", true },
        { "have", true },
        { "he", true },
        { "hence", true },
        { "her", true },
        { "here", true },
        { "hereafter", true },
        { "hereby", true },
        { "herein", true },
        { "hereupon", true },
        { "hers", true },
        { "herself", true },
        { "him", true },
        { "himself", true },
        { "his", true },
        { "how", true },
        { "however", true },
        { "hundred", true },
        { "i", true },
        { "ie", true },
        { "if", true },
        { "in", true },
        { "inc", true },
        { "indeed", true },
        { "interest", true },
        { "into", true },
        { "is", true },
        { "it", true },
        { "its", true },
        { "itself", true },
        { "java", true },
        { "keep", true },
        { "last", true },
        { "latter", true },
        { "latterly", true },
        { "least", true },
        { "lot", true },
        { "lots", true },
        { "less", true },
        { "ltd", true },
        { "made", true },
        { "many", true },
        { "may", true },
        { "me", true },
        { "meanwhile", true },
        { "might", true },
        { "mill", true },
        { "mine", true },
        { "more", true },
        { "moreover", true },
        { "most", true },
        { "mostly", true },
        { "move", true },
        { "much", true },
        { "must", true },
        { "my", true },
        { "myself", true },
        { "name", true },
        { "namely", true },
        { "neither", true },
        { "never", true },
        { "nevertheless", true },
        { "next", true },
        { "nine", true },
        { "no", true },
        { "nobody", true },
        { "none", true },
        { "nor", true },
        { "not", true },
        { "nothing", true },
        { "now", true },
        { "nowhere", true },
        { "of", true },
        { "off", true },
        { "often", true },
        { "on", true },
        { "once", true },
        { "one", true },
        { "only", true },
        { "onto", true },
        { "or", true },
        { "other", true },
        { "others", true },
        { "otherwise", true },
        { "our", true },
        { "ours", true },
        { "ourselves", true },
        { "out", true },
        { "over", true },
        { "own", true },
        { "part", true },
        { "per", true },
        { "perhaps", true },
        { "please", true },
        { "put", true },
        { "rather", true },
        { "re", true },
        { "same", true },
        { "see", true },
        { "seem", true },
        { "seemed", true },
        { "seeming", true },
        { "seems", true },
        { "serious", true },
        { "several", true },
        { "she", true },
        { "should", true },
        { "show", true },
        { "side", true },
        { "since", true },
        { "sincere", true },
        { "six", true },
        { "sixty", true },
        { "so", true },
        { "some", true },
        { "somehow", true },
        { "someone", true },
        { "something", true },
        { "sometime", true },
        { "sometimes", true },
        { "somewhere", true },
        { "still", true },
        { "such", true },
        { "system", true },
        { "take", true },
        { "ten", true },
        { "than", true },
        { "that", true },
        { "the", true },
        { "their", true },
        { "them", true },
        { "themselves", true },
        { "then", true },
        { "thence", true },
        { "there", true },
        { "thereafter", true },
        { "thereby", true },
        { "therefore", true },
        { "therein", true },
        { "thereupon", true },
        { "these", true },
        { "they", true },
        { "thick", true },
        { "thin", true },
        { "third", true },
        { "this", true },
        { "those", true },
        { "though", true },
        { "three", true },
        { "through", true },
        { "throughout", true },
        { "thru", true },
        { "thus", true },
        { "to", true },
        { "together", true },
        { "too", true },
        { "top", true },
        { "toward", true },
        { "towards", true },
        { "twelve", true },
        { "twenty", true },
        { "two", true },
        { "un", true },
        { "under", true },
        { "until", true },
        { "up", true },
        { "upon", true },
        { "us", true },
        { "very", true },
        { "via", true },
        { "was", true },
        { "wasnt", true },
        { "wasnot", true },
        { "we", true },
        { "well", true },
        { "were", true },
        { "what", true },
        { "whatever", true },
        { "when", true },
        { "whence", true },
        { "whenever", true },
        { "where", true },
        { "whereafter", true },
        { "whereas", true },
        { "whereby", true },
        { "wherein", true },
        { "whereupon", true },
        { "wherever", true },
        { "whether", true },
        { "which", true },
        { "while", true },
        { "whither", true },
        { "who", true },
        { "whoever", true },
        { "whole", true },
        { "whom", true },
        { "whose", true },
        { "why", true },
        { "will", true },
        { "willing", true },
        { "with", true },
        { "within", true },
        { "without", true },
        { "would", true },
        { "yet", true },
        { "you", true },
        { "your", true },
        { "yours", true },
        { "yourself", true },
        { "yourselves", true },

    };

    //programming language stop words
    static Dictionary<string, bool> _progStop = new Dictionary<string, bool> {
    {"abstract", true},
        {"base", true },
        {"bool", true },
        {"break", true },
        {"byte", true },
        {"case", true },
        {"Catch", true},
        {"char", true },
        {"checked", true },
        {"class", true },
        {"const", true },
        {"continue", true },
        {"decimal", true },
        {"default", true },
        {"delegate", true },
        {"double", true },
        {"enum", true },
        {"event", true },
        {"explicit", true },
        {"extern", true },
        {"false", true },
        {"finally", true },
        {"fixed", true},
        {"float", true},
        {"foreach", true},
        {"goto", true},
        {"implicit", true},
        {"in", true},
        {"int", true},
        {"interface", true},
        {"internal", true},
        {"is", true},
        {"lock", true},
        {"long", true},
        {"namespace", true},
        {"new", true},
        {"null", true},
        {"object", true},
        {"operator", true},
        {"out", true},
        {"override", true},
        {"params", true},
        {"private", true},
        {"protected", true},
        {"public", true},
        {"readonly", true},
        {"ref", true},
        {"return", true},
        {"sbyte", true},
        {"sealed", true},
        {"short", true},
        {"sizeof", true},
        {"stackalloc", true},
        {"static", true},
        {"string", true},
        {"struct", true},
        {"switch", true},
        {"this", true},
        {"throw", true},
        {"true", true},
        {"try", true},
        {"typeof", true},
        {"uint", true},
        {"ulong", true},
        {"unchecked", true},
        {"unsafe", true},
        {"ushort", true},
        {"using", true},
        {"virtual", true},
        {"void", true},
        {"volatile", true},
        {"while", true},
        {"add", true},
        {"alias", true},
        {"ascending", true},
        {"async", true},
        {"await", true},
        {"by", true},
        {"descending", true},
        {"dynamic", true},
        {"equals", true},
        {"from", true},
       // {"get", true},
        {"global", true},
        {"group", true},
        {"into", true},
        {"join", true},
        {"let", true},
        {"nameof", true},
        {"on", true},
        {"orderby", true},
        {"partial", true},
        {"method", true},
        {"remove", true},
        {"select", true},
        {"set", true},
        {"unmanaged", true},
        {"value", true},
        {"var", true},
        {"generic", true},
        {"yield", true},
        {"constraint", true},
        {"type", true},
        {"filter", true},
        {"condition", true}
    };
    /// <summary>
    /// Chars that separate words.
    /// </summary>
    static char[] _delimiters = new char[]
    {
        ' ',
        ',',
        ';',
        '.'
    };
    public static string ExtractWordFromPascalCase(string pascal)
    {

        StringBuilder sb = new StringBuilder();

        //input all words in string builder first. for example: getUserlist= getUserlist get user list
        var words = pascal.Split(_delimiters,
            StringSplitOptions.RemoveEmptyEntries).Distinct().OrderBy(x => x);

        foreach (string currentWord in words)
        {
            sb.Append(currentWord).Append(' ');
            var word=SplitCamelCase(currentWord);
            if (currentWord != word)
            {
                sb.Append(word).Append(' ');
            }
            
        }
        //check camel Case word

        //foreach (char @char in pascal)
        //{

        //    if (Char.IsUpper(@char))
        //    {
        //        sb.Append(" ");
        //    }
        //    sb.Append(@char);
        //}

        //if (pascal != null && pascal.Length > 0 && Char.IsUpper(pascal[0]))
        //{
        //    sb.Remove(0, 1);
        //}

        String data = sb.ToString();
        var result = string.Join(" ", data.Split(' ').Distinct(StringComparer.CurrentCultureIgnoreCase));
        return result;
    }
    public static string SplitCamelCase(string input)
    {
        return System.Text.RegularExpressions.Regex.Replace(input, "([A-Z])", " $1", System.Text.RegularExpressions.RegexOptions.Compiled).Trim();
    }
    /// <summary>
    /// Remove stopwords from string.
    /// </summary>
    /// 
    //data pre-processing full function

    public static string DataPreprocess(string data)
    {
        //Remove all symbol from summaryAndDesc.
        string pattern = @"(\d+|\@|\.|\,|\&|\'|\(|\)|<|>|#|{|}|\[|\]|%|\||\\|\/)";
        //string pattern = @"[^0-9a-zA-Z\._]";
        string filtered1 = Regex.Replace(data, pattern, string.Empty, RegexOptions.IgnoreCase);
        string pattern2 = @"[^a-zA-Z0-9]+";
        string filtered = Regex.Replace(filtered1, pattern2, " ", RegexOptions.IgnoreCase);

        //Extract all Camel Case word from summaryAndDesc.
        string nopascal = ExtractWordFromPascalCase(filtered);


        //Remove all stop word from summaryAndDesc.
        string stopWords = RemoveStopwords(nopascal);

        //Stemming all word.
        //PorterStemmer stem = new PorterStemmer();
        //string stemming = stem.stemTerm(stopWords);
        Porter2 stem = new Porter2();
        string stemming = stem.stem(stopWords);

        //Distinct & orderby all keywords.
        var keywords = string.Join(" ", stemming.Split(' ').Distinct(StringComparer.CurrentCultureIgnoreCase));

        return keywords;
    }
    public static string RemoveStopwords(string input)
    {
        // 1
        // Split parameter into words
        var words = input.Split(_delimiters,
            StringSplitOptions.RemoveEmptyEntries);
        // 2
        // Allocate new dictionary to store found words
        var found = new Dictionary<string, bool>();
        // 3
        // Store results in this StringBuilder
        StringBuilder builder = new StringBuilder();
        // 4
        // Loop through all words
        foreach (string currentWord in words)
        {
            //check word length first
            if (currentWord.Length>1)
            {
                // 5
                // Convert to lowercase
                string lowerWord = currentWord.ToLower();
                // 6
                // If this is a usable word, add it
                if (!_stops.ContainsKey(lowerWord) &&
                    !found.ContainsKey(lowerWord))
                {
                    if (!_progStop.ContainsKey(lowerWord) &&
                    !found.ContainsKey(lowerWord))
                    {
                        builder.Append(currentWord).Append(' ');
                        found.Add(lowerWord, true);
                    }

                }
            }
            
        }
        // 7
        // Return string with words removed
        return builder.ToString().Trim();
    }

    public static string DistinctWord(string input)
    {
        // 1
        // Split parameter into words
        var newWord = input.Split(_delimiters,
            StringSplitOptions.RemoveEmptyEntries).Distinct().OrderBy(x=>x);
        // Store results in this StringBuilder
        StringBuilder builder = new StringBuilder();
        builder.Append(newWord);
        //foreach (string s in newWord)
        //{
        //    builder.Append(s).Append(' ');
        //}
        // Return string with words removed
        return builder.ToString().Trim();
    }
}