using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FarmTile 
{
    
    public bool buildingOnTile = false;
    public bool isBuilt = false;

    public Resource resourceOnTile = Resource.wheat;
     
    public int maxStoredResources = 120;
    public int storedResources = 0;
     
    public int productionRate = 40;

    public List<Worker> workersOnTile = new List<Worker>();
}
