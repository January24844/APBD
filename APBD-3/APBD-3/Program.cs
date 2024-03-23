using System.Collections.Generic;

namespace APBD_3
{
  internal class Program
  {
    public static void Main(string[] args)
    {
      var ship = new ContainerShip(30, 100, 5000); 
      
      var container1 = new LiquidContainer(2000, 300, 400, 500, "KON-L-1", true);
      var container2 = new RefrigeratedContainer(1500, 250, 350, 450, "KON-R-1", "Fish", 5);
      
      ship.LoadContainers(new List<Container> { container1, container2 });
      
      ship.PrintShipInfo();
      ship.PrintContainerInfo(container1);
      
      var destinationShip = new ContainerShip(25, 50, 2000);
      ship.MoveContainer(container1, destinationShip);

      // po przeniesieniu
      ship.PrintShipInfo();
      destinationShip.PrintShipInfo();
      }
    }
}