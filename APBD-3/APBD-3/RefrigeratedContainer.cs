using System;

namespace APBD_3
{
    public class RefrigeratedContainer : Container
    {
        public string ProductType { get; private set; }
        public double Temperature { get; private set; }
        private int currentCargoWeight;

        public RefrigeratedContainer(int loadCapacity, int height, int weight, int depth, string serialNumber, string productType, double temperature)
            : base(loadCapacity, height, weight, depth, serialNumber)
        {
            ProductType = productType;
            Temperature = temperature;
            currentCargoWeight = 0;
        }

        public override void Load(int cargoWeight)
        {
            if (cargoWeight > LoadCapacity)
            {
                throw new OverfillException("Overfill");
            }
            if (Temperature < GetRequiredTemperature(ProductType))
            {
                throw new Exception($"Temperature too low, required temperature: {GetRequiredTemperature(ProductType)}");
            }

            currentCargoWeight += cargoWeight;
        }

        public override void Unload()
        {
            Console.WriteLine($"Unloading refrigerated container: {SerialNumber}");
            currentCargoWeight = 0; 
        }
        private double GetRequiredTemperature(string productType)
        {
            switch (productType)
            {
                case "Bananas":
                    return 13.3;
                case "Chocolate":
                    return 18;
                case "Fish":
                    return 2;
                case "Meat":
                    return -15;
                case "Ice cream":
                    return -18;
                case "Frozen pizza":
                    return -30;
                case "Cheese":
                    return 7.2;
                case "Sausages":
                    return 5;
                case "Butter":
                    return 20.5;
                case "Eggs":
                    return 19;
                default:
                    return 0;
            }
        }
    }
    
}