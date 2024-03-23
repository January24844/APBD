using System;

namespace APBD_3

{
    public abstract class Container
    {
        public int LoadCapacity { get; protected set; }
        public int Height { get; protected set; }
        public int Weight { get; protected set; }
        public int Depth { get; protected set; }
        public string SerialNumber { get; protected set; }

        public Container(int loadCapacity, int height, int weight, int depth, string serialNumber)
        {
            LoadCapacity = loadCapacity;
            Height = height;
            Weight = weight;
            Depth = depth;
            SerialNumber = serialNumber;
        }
        public abstract void Load(int cargoWeight);

        public abstract void Unload();
    }
}