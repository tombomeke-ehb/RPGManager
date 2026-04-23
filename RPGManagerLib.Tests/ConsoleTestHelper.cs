namespace RPGManagerLib.Tests;

internal static class ConsoleTestHelper
{
    public static string CaptureOutput(Action action, string? input = null)
    {
        var originalOut = Console.Out;
        var originalIn = Console.In;
        using var writer = new StringWriter();

        try
        {
            if (input is not null)
            {
                Console.SetIn(new StringReader(input));
            }

            Console.SetOut(writer);
            action();
            return writer.ToString();
        }
        finally
        {
            Console.SetOut(originalOut);
            Console.SetIn(originalIn);
        }
    }
}
