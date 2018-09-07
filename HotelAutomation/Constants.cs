namespace HotelAutomation
{
    public static class Constants
    {
        public static int LightPowerConsumption = 5;

        public static int AcPowerConsumption = 10;
    }

    public enum CorridorType
    {
        Main = 0,
        Sub = 1
    }

    public enum EquipmentState
    {
        Off = 0,
        On = 1
    }

    public enum EquipmentType
    {
        Light = 0,
        Ac = 1
    }
}
