using KS.StarWars.Application.Interfaces;
using KS.StarWars.Domain.Entities;
using KS.StarWars.Domain.ValueObjects;
using System;
using System.Collections.Generic;

namespace KS.StarWars.Presentation.UI.MainConsoleApp
{
    class Program
    {
        private static readonly ISpaceTripAppService spaceTripAppService;

        static void Main(string[] args)
        {
            PlanTripStops();
        }

        private static void PlanTripStops()
        {
            PrintPresentation();

            while (true)
            {
                PrintHelpMessages();
                var command = RequestCommand();
                while (string.IsNullOrEmpty(command) || string.IsNullOrWhiteSpace(command))
                {
                    command = RequestCommand();
                }
                if (IsExitProgramRequested(command))
                    break;

                var spaceTrip = new SpaceTrip(command);

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

        private static void ShowResult(Dictionary<string, int> resuplyStops)
        {
            foreach (var stop in resuplyStops)
            {
                Console.WriteLine($"{stop.Key}: {stop.Value}");
            }
        }
    }
}
