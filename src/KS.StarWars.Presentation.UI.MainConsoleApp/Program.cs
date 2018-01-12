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
            var message = string.Empty;
            if (!IsValidEntry(command, out message))
            {
                ShowInvalidEntryMessage(message);
            }
            else if (!spaceTrip.IsValid())
            {
                ShowErros(spaceTrip.ValidationResult);
            }
            else
            {
                PrintBB8HelpRequestMessage();
                var resupplyStops = spaceTripAppService.GetAllResupplyStopsForSpaceTrip(spaceTrip);
                ShowResult(resupplyStops);
            }
        }

        // Helper methods

        private static void PrintBB8HelpRequestMessage()
        {
            Console.WriteLine("Requesting BB-8 help to plan resupply stops...");
        }

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
            Console.Write("Please type the Space Trip distance: ");
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

        private static void ShowInvalidEntryMessage(string message)
        {
            Console.WriteLine();
            Console.WriteLine(message);
        }

        private static bool IsValidEntry(string command, out string message)
        {
            message = string.Empty;
            try
            {
                decimal.Parse(command);
            }
            catch (OverflowException)
            {
                message = "Distance value is too big";
                return false;
            }
            catch (FormatException)
            {
                message = "Distance must be a valid decimal number";
                return false;
            }

            return true;
        }

        private static void ShowErros(ValidationResult validationResult)
        {
            Console.WriteLine();
            foreach (var error in validationResult.Errors)
            {
                Console.WriteLine(error.Message);
            }
        }

        private static void ShowResult(Dictionary<string, string> resupplyStops)
        {
            Console.WriteLine();
            foreach (var stop in resupplyStops)
            {
                Console.WriteLine($"{stop.Key}: {stop.Value}");
            }
        }
    }
}
