using System.Collections.Generic;

public class FarmTile 
{
    
    public bool buildingOnTile = false;
    public bool isBuilt = false;

    public Resource resourceOnTile = Resource.wheat;
     
    public int maxStoredResources = 120;
    public int storedResources = 0;
     
    public int productionRate = 40;

    public int maxAmountOfAnimals = 24;
    public int amountOfAnimals;
    public bool firstTurnBuilt = false;

    public List<Worker> workersOnTile = new List<Worker>();
}
