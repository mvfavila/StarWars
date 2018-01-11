using System;

namespace KS.StarWars.Presentation.UI.MainConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            PlanTripStops();
        }

        private static void PlanTripStops()
        {
            Console.WriteLine("############# Welcome to R2-D2 travel log #############");

            while (true)
            {
                Console.WriteLine(
                        $@"Please type this trip distance.");
                Console.WriteLine("Type 'exit' and press ENTER to close R2-D2 travel log.");

                // TODO: check for valid input and valid SpaceTrip
            }
        }
    }
}
