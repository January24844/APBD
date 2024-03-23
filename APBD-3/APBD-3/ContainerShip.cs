using System;
using System.Collections.Generic;
using System.Linq;

namespace APBD_3
{
    public class ContainerShip
    {
        public List<Container> Containers { get; private set; }
        public int MaxSpeed { get; private set; } // węzły
        public int MaxContainerCount { get; private set; }
        public double MaxTotalWeight { get; private set; } // w tonach

        public ContainerShip(int maxSpeed, int maxContainerCount, double maxTotalWeight)
        {
            Containers = new List<Container>();
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxTotalWeight = maxTotalWeight;
        }

        public void AddContainer(Container container)
        {
            if (Containers.Count < MaxContainerCount && GetCurrentTotalWeight() + container.Weight <= MaxTotalWeight)
            {
                Containers.Add(container);
            }
            else
            {
                throw new Exception("Cannot add container: maximum limit exceeded.");
            }
        }

        public void RemoveContainer(Container container)
        {
            Containers.Remove(container);
        }

        public void LoadContainers(List<Container> containers)
        {
            foreach (var container in containers)
            {
                AddContainer(container);
            }
        }

        public void ReplaceContainer(string containerNumber, Container newContainer)
        {
            var containerToRemove = Containers.FirstOrDefault(c => c.SerialNumber == containerNumber);
            if (containerToRemove != null)
            {
                Containers.Remove(containerToRemove);
                AddContainer(newContainer);
            }
            else
            {
                throw new Exception("Container not found on the ship.");
            }
        }

        public void MoveContainer(Container container, ContainerShip destinationShip)
        {
            if (destinationShip.Containers.Count < destinationShip.MaxContainerCount &&
                destinationShip.GetCurrentTotalWeight() + container.Weight <= destinationShip.MaxTotalWeight)
            {
                RemoveContainer(container);
                destinationShip.AddContainer(container);
            }
            else
            {
                throw new Exception("Cannot move container: destination ship's capacity exceeded.");
            }
        }

        public void PrintContainerInfo(Container container)
        {
            Console.WriteLine($"Container Info:\nSerial Number: {container.SerialNumber}\nLoad Capacity: {container.LoadCapacity} kg\nHeight: {container.Height} cm\nWeight: {container.Weight} kg\nDepth: {container.Depth} cm");
        }

        public void PrintShipInfo()
        {
            Console.WriteLine($"Ship Info:\nMax Speed: {MaxSpeed} knots\nMax Containers: {MaxContainerCount}\nMax Total Weight: {MaxTotalWeight} tons\nNumber of Containers on Ship: {Containers.Count}");
        }

        private double GetCurrentTotalWeight()
        {
            return Containers.Sum(container => container.Weight);
        }
    }
}
