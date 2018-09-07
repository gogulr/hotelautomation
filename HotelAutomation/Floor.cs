using System.Collections.Generic;
using System.Linq;

namespace HotelAutomation
{
    public class Floor
    {
        private readonly Dictionary<CorridorType, int> _corridorNumber = new Dictionary<CorridorType, int>();

        public Floor(int floorNumber)
        {
            FloorNumber = floorNumber;
            Corridors = new List<Corridor>();
        }

        public int FloorNumber { get; private set; }

        public List<Corridor> Corridors { get; }

        public Corridor AddCorridor(CorridorType corridorType)
        {
            var corridorNumber = 1;
            if (_corridorNumber.ContainsKey(corridorType))
            {
                corridorNumber = ++_corridorNumber[corridorType];
            }
            else
            {
                _corridorNumber.Add(corridorType, corridorNumber);
            }
            var corridor = new Corridor(corridorNumber, corridorType);
            Corridors.Add(corridor);
            return corridor;
        }

        public Corridor GetCorridor(int corridorNumber, CorridorType corridorType)
        {
            return Corridors.FirstOrDefault(x => x.CorridorNumber == corridorNumber && x.CorriderType == corridorType);
        }
    }
}
