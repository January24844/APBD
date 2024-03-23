using System;

namespace APBD_3
{

    public class LiquidContainer : Container, IHazardNotifier
    {
        private bool isDangerousCargo;
        private int currentCargoWeight;

        public LiquidContainer(int loadCapacity, int height, int weight, int depth, string serialNumber, bool isDangerousCargo)
            : base(loadCapacity, height, weight, depth, serialNumber)
        {
            this.isDangerousCargo = isDangerousCargo;
            currentCargoWeight = 0;
        }

        public override void Load(int cargoWeight)
        {
            if (isDangerousCargo && cargoWeight > LoadCapacity * 0.5)
            {
                throw new OverfillException("overfill!! cargo can be filled up to 50% capacity.");
            }
            else if (!isDangerousCargo && cargoWeight > LoadCapacity * 0.9)
            {
                throw new OverfillException("overfill!! cargo can be filled up to 90% capacity.");
            }
            currentCargoWeight += cargoWeight;
        }

        public override void Unload()
        {
            Console.WriteLine($"unloading liquid container: {SerialNumber}");
            currentCargoWeight = 0;
        }

        public void NotifyDangerousSituation(string containerNumber)
        {
            Console.WriteLine($"Danger!!! {containerNumber}");
        }
    }
}