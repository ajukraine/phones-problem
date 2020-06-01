using System;
using System.Collections.Generic;
using Dawn;
using Serilog;

namespace Phone.App
{
    class Program
    {
        static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.File("Phone.App.log")
                .CreateLogger();

            try
            {
                Execute(args);
            }
            catch (Exception e)
            {
                Log.Error(e, "Error");
                throw;
            }
        }

        static void Execute(string[] args)
        {
            Guard.Argument(args, nameof(args)).Count(2);

            var inputPath = args[0];
            var outputPath = args[1];

            var jsonIntegration = new JsonIntegration();
            
            // Read problem input
            var phones = jsonIntegration.Read<IEnumerable<APhone>>(inputPath);

            // Solve the problem
            var solution = PriceProblem.Solve(phones);

            // Write problem solution
            jsonIntegration.Write(outputPath, solution);
        }
    }
}
