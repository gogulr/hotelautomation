namespace HotelAutomation
{
    public class SensorInformation
    {
        public SensorInformation(int floorNumber, int corridorNumber, bool isMovementDetected)
        {
            FloorNumber = floorNumber;
            CorridorNumber = corridorNumber;
            IsMovementDetected = isMovementDetected;
        }

        public int FloorNumber { get; private set; }

        public int CorridorNumber { get; private set; }

        public bool IsMovementDetected { get; private set; }
    }
}
