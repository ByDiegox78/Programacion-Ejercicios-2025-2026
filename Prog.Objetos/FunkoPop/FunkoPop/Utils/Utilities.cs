using System.Text.RegularExpressions;
using FunkoPop.Enums;
using FunkoPop.Models;

namespace FunkoPop.Utils;

public static class Utilities {
    public static string ValidarMenu(string msg, string rgx) {
        string input;
        var regex = new Regex(rgx);
        do {
            Console.Write($"{msg} ");
            input = Console.ReadLine()?.Trim() ?? "-1";
        } while (!regex.IsMatch(input));
        Console.WriteLine();
        return input;
    }
    
}