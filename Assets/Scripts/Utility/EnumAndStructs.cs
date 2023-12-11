using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Resource
{
    wheat, apple, cinnamon, pigMeat, money, unassignedResource
};
public enum WorkType
{
    harvesting, building, slaughtering, unassigned, wrongWorkType
};
public enum HudState
{
    standard, buildPopup, assignWorkers
};
public enum TypeOfCard
{
    special, contract
};
public enum ActionCostingType
{
    build,assignWorkers
}

public struct ResourcesAmount
{
    public int wheatCost; 
    public int appleCost;
    public int cinnamonCost;
    public int pigMeatCost;
    public int moneyGained;
}

public class EnumAndStructs 
{

}
