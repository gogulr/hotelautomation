using System.Collections.Generic;

namespace HotelAutomation
{
    public class Corridor
    {
        private readonly Dictionary<EquipmentType, int> _equipmentNumber = new Dictionary<EquipmentType, int>();

        public Corridor(int corridorNumber, CorridorType type)
        {
            CorridorNumber = corridorNumber;
            CorriderType = type;
            Equipments = new List<Equipment>();
        }

        public int CorridorNumber { get; private set; }

        public CorridorType CorriderType { get; private set; }

        public List<Equipment> Equipments { get; }

        public Equipment AddEquipment(EquipmentType equipmentType, EquipmentState equipmentState)
        {
            var equipmentNumber = 1;
            if (_equipmentNumber.ContainsKey(equipmentType))
            {
                equipmentNumber = ++_equipmentNumber[equipmentType];
            }
            else
            {
                _equipmentNumber.Add(equipmentType, equipmentNumber);
            }
            var equipment = new Equipment(equipmentNumber, equipmentType, equipmentState);
            Equipments.Add(equipment);
            return equipment;
        }
    }
}
