using Spectre.Console;

namespace OOP_ICT.Fifth.Messages;

public class MessageGenerator
{
    public static void DisplayMessage(string color, string message)
    {
        AnsiConsole.MarkupLine($"[{color}]{message}[/]");
    }

    public static void DisplayGameName(string message)
    {
        AnsiConsole.Write(new FigletText($"{message}"));
    }
    
    public static int PromptForNumberInt(string color, string message)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<int>( $"{message}")
                .PromptStyle($"{color}")
        );
    }

    public static double PromptForNumberDouble(string color, string message)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<double>( $"{message}")
                .PromptStyle($"{color}")
        );
    }

    public static string PromprForString(string color, string message)
    {
        return AnsiConsole.Prompt(
            new TextPrompt<string>($"{message}")
                .PromptStyle($"{color}")
        );
    }
}