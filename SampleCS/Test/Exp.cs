using System;
using Serilog;

class Program
{
    StreamWriter writer = new StreamWriter("abc.txt");
    static void Main()
    {
        // Configure Serilog
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();

        try
        {
            // Your application code
            
            Log.Information(bankAccountNumber);

            int result = Divide(10, 0);

            Log.Information("Application ended");
            writer.WriteLine(bankAccountNumber);
        }
        catch (Exception ex)
        {
            Log.Error(ex, "An error occurred");
        }
        finally
        {
            // Close and flush the log
            Log.CloseAndFlush();
        }
    }

    static int Divide(int x, int y)
    {
        return x / y;
    }
}
