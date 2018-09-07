namespace HotelAutomation
{
    public class Equipment
    {
        public Equipment(int equipmentNumber, EquipmentType type, EquipmentState state)
        {
            EquipmentNumber = equipmentNumber;
            EquipmentType = type;
            EquipmentState = state;
        }

        public int EquipmentNumber { get; private set; }

        public EquipmentType EquipmentType { get; private set; }

        public EquipmentState EquipmentState { get; set; }
    }
}
