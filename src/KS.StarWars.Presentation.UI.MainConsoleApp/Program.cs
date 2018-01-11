using KS.StarWars.Application.Interfaces;
using KS.StarWars.CrossCutting.IoC;
using KS.StarWars.Domain.Entities;
using KS.StarWars.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace KS.StarWars.Presentation.UI.MainConsoleApp
{
    /// <summary>
    /// Main Program that executes the Console and prompts the user interaction messages.</br>
    ///
    /// I decided to leave the messages and interaction methods in this class so I do not take too long</br>
    /// to finish the challenge and because all the methods are related to this kind of UI</br>
    /// and would not be used by a different kind of UI.
    /// </summary>
    static class Program
    {
        private static readonly ISpaceTripAppService spaceTripAppService;

        static Program()
        {
            StarWarsDependencyInjenction.RegisterServices();
            spaceTripAppService = StarWarsDependencyInjenction.container.GetInstance<ISpaceTripAppService>();
        }

        static void Main()
        {
            Start();
        }

        private static void Start()
        {
            PrintPresentation();

            Execute();
        }

        private static void Execute()
        {
            while (true)
            {
                PrintHelpMessages();
                var command = RequestCommand();
                command = RequestValidCommand(command);

                if (IsExitProgramRequested(command))
                    break;

                var spaceTrip = new SpaceTrip(command);

                ProcessSpaceTrip(command, spaceTrip);
            }
        }

        private static void ProcessSpaceTrip(string command, SpaceTrip spaceTrip)
        {
            if (!IsDistanceNumeric(command))
            {
                ShowInvalidEntryMessage();
            }
            else if (!spaceTrip.IsValid())
            {
                ShowErros(spaceTrip.ValidationResult);
            }
            else
            {
                var resuplyStops = spaceTripAppService.GetAllResuplyStopsForSpaceTrip(spaceTrip);
                ShowResult(resuplyStops);
            }
        }

        // Helper methods

        private static string RequestValidCommand(string command)
        {
            while (string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command))
            {
                command = RequestCommand();
            }

            return command;
        }

        private static bool IsExitProgramRequested(string command)
        {
            return command.ToLower() == "exit";
        }

        private static string RequestCommand()
        {
            Console.Write("Please type the space trip distance: ");
            var command = Console.ReadLine();
            return command;
        }

        private static void PrintHelpMessages()
        {
            Console.WriteLine();
            Console.WriteLine("Type 'exit' and press ENTER to close R2-D2 travel log.");
        }

        private static void PrintPresentation()
        {
            Console.WriteLine("############# Welcome to R2-D2 travel log #############");
        }

        private static void ShowInvalidEntryMessage()
        {
            Console.WriteLine("Distance must be numeric");
        }

        private static bool IsDistanceNumeric(string command)
        {
            return decimal.TryParse(command, out decimal value);
        }

        private static void ShowErros(ValidationResult validationResult)
        {
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine(error.Message);
            }
        }

        private static void ShowResult(Dictionary<string, string> resuplyStops)
        {
            foreach (var stop in resuplyStops)
            {
                Console.WriteLine($"{stop.Key}: {stop.Value}");
            }
        }
    }
}
