using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;

using UnityEngine;

public class GameStateLogic : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private int maxActions = 0;
    private int currentActions = 0;

    private int actionLevel = 0;
    private int storageLevel = 0;
    private int currentStorage = 0;

    private int moneyStored = 0;

    private int wheatStored = 0;
    private int appleStored = 0;
    private int cottonStored = 0;
    private int pigMeatStored = 0;

    private int workersCreated = 0;

    private SortedDictionary<int, Worker> workerRegistry = new SortedDictionary<int, Worker>();

    private Dictionary<int, FarmTile> farmTileRegistry = new Dictionary<int, FarmTile>();

    [SerializeField] private List<SpecialCard> specialCardDeck = new List<SpecialCard>();  
    private List<SpecialCard> specialCardsOnTable = new List<SpecialCard>();

    [SerializeField] private List<ContractCard> contractCardDeck = new List<ContractCard>();
    private List<SpecialCard> contractCardsOnTable = new List<SpecialCard>();

    private List<Card> cardsOnHand = new List<Card>();

    // Update is called once per frame
    void Update()
    {
        
    }
    void Setup()
    {
        for (int i = 0; i < 9; i++)
        {
            farmTileRegistry.Add(i, new FarmTile());
        }




        AddWorker();
        //farmTileRegistry[0].workersOnTile.Add(workerRegistry[0]);
        AddResources(Resource.Money,1500);
        FillCardsOnTable();
    }

    void FillCardsOnTable()
    {
    }

    private System.Random rng = new System.Random();
    void ShuffleCards(List<Card> cards)
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            Card value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    void StartTurnUpkeep()
    {
        ProductionPhase();
        WorkPhase();
        ResetNumbers();
        MoveCards();

    }
    void ResetNumbers()
    {
        currentActions = maxActions;
    }

    void MoveCards()
    {
        bool hej = true;
        hej = false;
    }

    void PlaySpecialCard( SpecialCard specialCard)
    {
        cardsOnHand.Remove(specialCard);
        specialCardDeck.Add(specialCard);
    }

    void ProductionPhase()
    {
        foreach(KeyValuePair<int,FarmTile> farmTilePair in farmTileRegistry)
        {

            farmTilePair.Value.storedResources += farmTilePair.Value.productionRate;

            if (farmTilePair.Value.storedResources > farmTilePair.Value.maxStoredResources)
            {
                farmTilePair.Value.storedResources = farmTilePair.Value.maxStoredResources;
            }
        }
    }
    void WorkPhase()
    {
        foreach(KeyValuePair<int,FarmTile> farmTilePair in farmTileRegistry)
        {
            foreach(Worker workerOnTile in farmTilePair.Value.workersOnTile)
            {
                if (workerOnTile.workType == WorkType.harvesting)
                {
                    if (farmTilePair.Value.storedResources < workerOnTile.workrate)
                    {
                        AddResources(farmTilePair.Value.resourceOnTile, farmTilePair.Value.storedResources);
                        farmTilePair.Value.storedResources = 0;
                    }
                    else
                    {
                        AddResources(farmTilePair.Value.resourceOnTile, workerOnTile.workrate);
                        farmTilePair.Value.storedResources = farmTilePair.Value.storedResources - workerOnTile.workrate;
                    }
                }
            }
        }
    }

    public List<SpecialCard> getSpecialCards()
    {
        List<SpecialCard> listToSend = new List<SpecialCard>();

        listToSend.Add(new SpecialCard(0, "Labour Rush", "Hire 3 workers for 3 rounds"));
        listToSend.Add(new SpecialCard(1, "Wheat Season", "Double the Wheat production for 2 rounds"));
        listToSend.Add(new SpecialCard(2, "Wheat Alchemy", "Use Wheat to cover the difference of another resource to fulfill the next contract"));


        return listToSend;
    }

    void AddWorker()
    {
        Worker newWorker = new Worker();
        newWorker.workedId = workersCreated;
        workerRegistry.Add(workersCreated, newWorker);

        workersCreated += 1;
    }
    void AddResources(Resource resource, int amount)
    {

    }
    // void AddMoney(int32 amount);
    public bool IsActionValid(Action action)
    {
        return true;
    }

    public void DoAction(Action action)
    {

    }
    int GetStoredResourceAmount(Resource resourceType)
    {
        if (resourceType == Resource.wheat)
        {
            return wheatStored;
        }

        if (resourceType == Resource.apple)
        {
            return appleStored;
        }
        if (resourceType == Resource.cotton)
        {
            return cottonStored;
        }
        if (resourceType == Resource.pigMeat)
        {
            return pigMeatStored;
        }
        return -1;
    }
}
