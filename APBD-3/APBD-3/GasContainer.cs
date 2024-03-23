using System;

namespace APBD_3
{

    public class GasContainer : Container, IHazardNotifier
    {
        public double Pressure { get; private set; }
        private double currentCargoWeight;

        public GasContainer(int loadCapacity, int height, int weight, int depth, string serialNumber, double pressure)
            : base(loadCapacity, height, weight, depth, serialNumber)
        {
            Pressure = pressure;
            currentCargoWeight = 0;
        }

        public override void Load(int cargoWeight)
        {
            if (cargoWeight > LoadCapacity)
            {
                throw new OverfillException("Overfill");
            }
            currentCargoWeight += cargoWeight;
        }

        public override void Unload()
        {
            double remainingCargo = LoadCapacity * 0.05;
            Console.WriteLine($"Remaining cargo in gas container {SerialNumber}: {remainingCargo} kg");
            currentCargoWeight *= 0.95; //odejmujemy 5%
        }

        public void NotifyDangerousSituation(string containerNumber)
        {
            Console.WriteLine(
                $"Danger!!! {containerNumber}.");
        }
    }
}