using System;
using System.Linq;

namespace HotelAutomation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var input = System.IO.File.ReadAllLines("input.txt");
            var line1 = Array.ConvertAll(input[0].Split(' '), int.Parse);
            var noOfInputs = int.Parse(input[1]);

            var noOfFloors = line1[0];
            var noOfMainCorridor = line1[1];
            var noOfSubCorridor = line1[2];

            var controller = new Controller();
            var hotel = controller.Initialize(noOfFloors, noOfMainCorridor, noOfSubCorridor);

            PrintCurrentState(hotel);

            var lineNumber = 2;
            while (noOfInputs-- > 0)
            {
                var sensorInput = Array.ConvertAll(input[lineNumber++].Split(' '), int.Parse);
                var floorNumber = sensorInput[0];
                var corridorNumber = sensorInput[1];
                var isMovementDetected = sensorInput[2] == 1;

                controller.ReceiveSensorInput(new SensorInformation(floorNumber, corridorNumber, isMovementDetected));

                PrintCurrentState(hotel);
            }

            Console.ReadKey();
        }

        private static void PrintCurrentState(Hotel hotel)
        {
            foreach (var floor in hotel.Floors)
            {
                Console.WriteLine($"Floor {floor.FloorNumber}");
                foreach (var corridor in floor.Corridors.OrderBy(x => x.CorriderType))
                {
                    Console.Write($"{corridor.CorriderType} corridor {corridor.CorridorNumber} ");
                    foreach (var equipment in corridor.Equipments.OrderBy(x => x.EquipmentType))
                    {
                        Console.Write($"{equipment.EquipmentType} {equipment.EquipmentNumber} : {equipment.EquipmentState} ");
                    }
                    Console.WriteLine();
                }
            }
            Console.WriteLine();
        }
    }
}
