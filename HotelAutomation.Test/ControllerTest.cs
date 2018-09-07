using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace HotelAutomation.Test
{
    [TestClass]
    public class ControllerTest
    {
        private static Controller _controller;
        private static Hotel _hotel;

        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {
            _controller = new Controller();

            _hotel = _controller.Initialize(2, 1, 2);
        }

        [TestMethod]
        public void TestHotelInitialize()
        {
            Assert.AreEqual(2, _hotel.Floors.Count);
            Assert.AreEqual(2, _hotel.Floors.SelectMany(x=>x.Corridors).Count(y => y.CorriderType == CorridorType.Main));
            Assert.AreEqual(4, _hotel.Floors.SelectMany(x => x.Corridors).Count(y => y.CorriderType == CorridorType.Sub));
        }

        [TestMethod]
        public void TestEquipmentStateWhenMovementDetected()
        {
            var floorNumber = 1;
            var corridorNumber = 2;

            _controller.ReceiveSensorInput(new SensorInformation(floorNumber, corridorNumber, true));

            var floor = _hotel.GetFloor(floorNumber);
            var lightState =
                floor.GetCorridor(corridorNumber, CorridorType.Sub)
                    .Equipments.Any(x => x.EquipmentType == EquipmentType.Light && x.EquipmentState == EquipmentState.On);

            var acState =
                floor.Corridors.Where(x => x.CorriderType == CorridorType.Sub)
                    .SelectMany(y => y.Equipments)
                    .Any(x => x.EquipmentType == EquipmentType.Ac && x.EquipmentState == EquipmentState.Off);

            Assert.IsTrue(lightState);
            Assert.IsTrue(acState);
        }

        [TestMethod]
        public void TestEquipmentStateWhenMovementNotDetected()
        {
            var floorNumber = 1;
            var corridorNumber = 2;

            _controller.ReceiveSensorInput(new SensorInformation(floorNumber, corridorNumber, false));

            var floor = _hotel.GetFloor(floorNumber);
            var lightState =
                floor.GetCorridor(corridorNumber, CorridorType.Sub)
                    .Equipments.Any(x => x.EquipmentType == EquipmentType.Light && x.EquipmentState == EquipmentState.On);

            var acState =
                floor.Corridors.Where(x => x.CorriderType == CorridorType.Sub)
                    .SelectMany(y => y.Equipments)
                    .Any(x => x.EquipmentType == EquipmentType.Ac && x.EquipmentState == EquipmentState.Off);

            Assert.IsFalse(lightState);
            Assert.IsFalse(acState);
        }
    }
}
