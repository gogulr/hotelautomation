using System.Collections.Generic;
using System.Linq;

namespace HotelAutomation
{
    public class Hotel
    {
        private int _floorNumber = 1;

        public Hotel()
        {
            Floors = new List<Floor>();
        }

        public List<Floor> Floors { get; }

        public Floor AddFloor()
        {
            var floor = new Floor(_floorNumber++);
            Floors.Add(floor);
            return floor;
        }

        public Floor GetFloor(int floorNumber)
        {
            return Floors.FirstOrDefault(x => x.FloorNumber == floorNumber);
        }

        public int GetTotalPowerConsumption(int floorNumber)
        {
            var totalPowerConsumption = 0;
            var floor = GetFloor(floorNumber);
            if (floor != null)
            {
                var equipments = floor.Corridors.SelectMany(x => x.Equipments).ToList();

                totalPowerConsumption = equipments.Count(equipment => equipment.EquipmentType == EquipmentType.Light
                                                                      && equipment.EquipmentState == EquipmentState.On)
                                        *Constants.LightPowerConsumption
                                        + equipments.Count(equipment => equipment.EquipmentType == EquipmentType.Ac
                                                                        && equipment.EquipmentState == EquipmentState.On)
                                        *Constants.AcPowerConsumption;
            }
            return totalPowerConsumption;
        }
    }
}
