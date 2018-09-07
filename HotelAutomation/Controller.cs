using System.Collections.Generic;
using System.Linq;

namespace HotelAutomation
{
    public class Controller
    {
        private static Hotel _hotel;
        private static int _optimalPowerConsumption;

        public Hotel Initialize(int noOfFloors, int noOfMainCorridor, int noOfSubCorridor)
        {
            _hotel = new Hotel();
            while (noOfFloors-- > 0)
            {
                var floor = _hotel.AddFloor();
                
                var x = noOfMainCorridor;
                var y = noOfSubCorridor;
                while (x-- > 0)
                {
                    var corridor = floor.AddCorridor(CorridorType.Main);
                    corridor.AddEquipment(EquipmentType.Light, EquipmentState.On);
                    corridor.AddEquipment(EquipmentType.Ac, EquipmentState.On);
                }

                while (y-- > 0)
                {
                    var corridor = floor.AddCorridor(CorridorType.Sub);
                    corridor.AddEquipment(EquipmentType.Light, EquipmentState.Off);
                    corridor.AddEquipment(EquipmentType.Ac, EquipmentState.On);
                }
            }

            _optimalPowerConsumption = GetOptimalPowerConsumption(noOfMainCorridor, noOfSubCorridor);

            return _hotel;
        }

        public void ReceiveSensorInput(SensorInformation sensorInformation)
        {
            var floor = _hotel.GetFloor(sensorInformation.FloorNumber);

            var corridor = floor?.GetCorridor(sensorInformation.CorridorNumber, CorridorType.Sub);

            if (corridor != null)
            {
                var equipments = corridor.Equipments;

                var lights = equipments.Where(equipment => equipment.EquipmentType == EquipmentType.Light);

                ChangeEquipmentsState(lights, sensorInformation.IsMovementDetected ? EquipmentState.On : EquipmentState.Off);
            }

            OptimizePowerConsumption(sensorInformation.FloorNumber);
        }

        private static void OptimizePowerConsumption(int floorNumber)
        {
            var totalPowerConsumption = _hotel.GetTotalPowerConsumption(floorNumber);

            var floor = _hotel.GetFloor(floorNumber);
            if (floor != null) 
            {
                var equipments = floor.Corridors.Where(corridor => corridor.CorriderType == CorridorType.Sub).SelectMany(x => x.Equipments);

                if (totalPowerConsumption > _optimalPowerConsumption)
                {
                    var noOfAcsShouldBeSwitchedOff = (totalPowerConsumption - _optimalPowerConsumption) / Constants.AcPowerConsumption;
                    var acList = equipments.Where(equipment => equipment.EquipmentType == EquipmentType.Ac && equipment.EquipmentState == EquipmentState.On).ToList();
                    if (noOfAcsShouldBeSwitchedOff < acList.Count)
                    {
                        if (noOfAcsShouldBeSwitchedOff == 0) noOfAcsShouldBeSwitchedOff = 1;
                        acList = acList.Take(noOfAcsShouldBeSwitchedOff).ToList();
                    }
                    ChangeEquipmentsState(acList, EquipmentState.Off);
                }
                else
                {
                    var noOfAcsCanBeSwitchedOn = (_optimalPowerConsumption - totalPowerConsumption) / Constants.AcPowerConsumption;
                    var acList = equipments.Where(equipment => equipment.EquipmentType == EquipmentType.Ac && equipment.EquipmentState == EquipmentState.Off).ToList();
                    if (noOfAcsCanBeSwitchedOn < acList.Count)
                    {
                        acList = acList.Take(noOfAcsCanBeSwitchedOn).ToList();
                    }
                    ChangeEquipmentsState(acList, EquipmentState.On);
                }
            }
        }

        private static void ChangeEquipmentsState(IEnumerable<Equipment> equipments, EquipmentState equipmentState)
        {
            foreach (var light in equipments)
            {
                light.EquipmentState = equipmentState;
            }
        }

        private static int GetOptimalPowerConsumption(int noOfMainCorridor, int noOfSubCorridor)
        {
            return (noOfMainCorridor * 15) + (noOfSubCorridor * 10);
        }
    }
}
