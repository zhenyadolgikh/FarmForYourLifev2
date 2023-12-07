using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEditor;
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
    private int cinnamonStored = 0;
    private int pigMeatStored = 0;

    private int currentTurn = 0;
    public int maxTurn = 0;

    private int workersCreated = 0;

    private SortedDictionary<int, Worker> workerRegistry = new SortedDictionary<int, Worker>();

    private Dictionary<int, FarmTile> farmTileRegistry = new Dictionary<int, FarmTile>();


    public List<SpecialCard> specialCardsForLevel = new List<SpecialCard>();
    private Dictionary<string, SpecialCard> specialCardRegistry = new Dictionary<string, SpecialCard>();
    private List<SpecialCard> specialCardDeck = new List<SpecialCard>();  
    private List<SpecialCard> specialCardsOnTable = new List<SpecialCard>();
    private int maxSpecialCardsOnTable = 3;
    private int maximumHandSize = 10;

    private List<ContractCard> contractCardDeck = new List<ContractCard>();
    private List<ContractCard> contractCardsOnTable = new List<ContractCard>();

    private List<Card> cardsInHand = new List<Card>();


    private List<EffectLifeTime> activeEffects = new List<EffectLifeTime>();    

    // Update is called once per frame

    public void Setup()
    {
        for (int i = 0; i < 9; i++)
        {
            farmTileRegistry.Add(i, new FarmTile());
        }

        foreach(SpecialCard specialCardinEditor in specialCardsForLevel)
        {   

            SpecialCard specialCard = (SpecialCard)specialCardinEditor;
            if (!specialCardRegistry.ContainsKey(specialCard.cardName))
            {
                specialCardRegistry.Add(specialCard.getCardName(), specialCard);
            }

            specialCardDeck.Add(specialCard);
        }


        currentStorage = 120;
        maxActions = 3;
        currentActions = maxActions;
        currentTurn = 1;
        maxTurn = 12;

        AddWorker();
        AddWorker();
        AddWorker();
        //farmTileRegistry[0].workersOnTile.Add(workerRegistry[0]);
        AddResources(Resource.money,1500);
        FillCardsOnTable();

    }


    void FillCardsOnTable()
    {
        ShuffleSpecialCards(specialCardDeck);
        print("Special card deck size" + specialCardDeck.Count);
        for(int i = 0; i < maxSpecialCardsOnTable; i++)
        {
            SpecialCard cardToAdd = specialCardDeck[0];
            specialCardsOnTable.Add(cardToAdd);
            specialCardDeck.Remove(cardToAdd);
        }
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
    void ShuffleSpecialCards(List<SpecialCard> cards)
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            SpecialCard value = cards[k];
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

        currentTurn += 1; 
    }
    void ResetNumbers()
    {
        currentActions = maxActions;

        if(wheatStored > currentStorage)
        {
            wheatStored = currentStorage;
        }
        if(appleStored > currentStorage)
        {
            appleStored = currentStorage;
        }
        if(cinnamonStored > currentStorage)
        {
            cinnamonStored = currentStorage;
        }
        if(pigMeatStored > currentStorage/10)
        {
            pigMeatStored = currentStorage/10;
        }
    }

    void MoveCards()
    {
        List<SpecialCard> cardsToRemove = new List<SpecialCard>();
        List<SpecialCard> cardsToMoveLeft = new List<SpecialCard>();
        for(int i = 0; i < maxSpecialCardsOnTable; i++)
        {
            if (specialCardsOnTable[i] != null && cardsToRemove.Count < 2)
            {
                cardsToRemove.Add(specialCardsOnTable[i]);
                specialCardsOnTable[i] = null;
            }
            if (specialCardsOnTable[i] != null && cardsToRemove.Count >= 2)
            {
                cardsToMoveLeft.Add(specialCardsOnTable[i]);
                specialCardsOnTable[i] = null;
            }
        }
        specialCardDeck.AddRange(cardsToRemove);

        for (int i = 0; i < cardsToMoveLeft.Count; i++)
        {
            specialCardsOnTable[i] = cardsToMoveLeft[i];
        }
        ShuffleSpecialCards(specialCardDeck);
        for(int i = 0; i < maxSpecialCardsOnTable; i++) 
        {
            if (specialCardsOnTable[i] == null)
            {

                if(specialCardDeck.Count > 0)
                {
                    specialCardsOnTable[i] = specialCardDeck[0];

                    specialCardDeck.RemoveAt(0);
                }
            }
        }
    }

    void AddCardToHand(AddCardToHandAction action)
    {

        currentActions -= 1;
        if(action.cardType == TypeOfCard.special)
        {
            string cardName = specialCardsOnTable[action.index].cardName;
            specialCardsOnTable[action.index] = null;
            cardsInHand.Add(specialCardRegistry[cardName]);
        }
    }

    private void PlayCard( PlayCardAction playCardAction)
    {

        currentActions -= 1;

        Card cardPLayed = cardsInHand[playCardAction.index];

        cardsInHand.RemoveAt(playCardAction.index);

        if(cardPLayed is SpecialCard)
        {
            specialCardDeck.Add((SpecialCard)cardPLayed);
        }
        else
        {
            contractCardDeck.Add((ContractCard)cardPLayed);
        }

        if(cardPLayed is MoneyPrinter)
        {
            AddResources(Resource.money, 300);

            print("det money printades");
        }

        EffectLifeTime effectLifeTime = cardPLayed.PlayCard();

        if(effectLifeTime != null)
        {
            activeEffects.Add(effectLifeTime);
        }

    }

    void ProductionPhase()
    {
        foreach(KeyValuePair<int,FarmTile> farmTilePair in farmTileRegistry)
        {
            if(farmTilePair.Value.isBuilt == false)
            {
                continue;
            }
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

                if(workerOnTile.workType == WorkType.building && farmTilePair.Value.buildingOnTile && farmTilePair.Value.isBuilt == false)
                {
                    farmTilePair.Value.isBuilt = true;

                    print("worker har byggt en byggnad");
                }
                
            }
        }
    }

    public List<SpecialCard> getSpecialCardsOnTable()
    {
        //List<SpecialCard> listToSend = new List<SpecialCard>();
        //
        //
        //ShuffleSpecialCards(listToSend);

        return specialCardsOnTable;
    }

    public List<Card> GetCardsInHand()
    {
        return cardsInHand;
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
        if(resource == Resource.money)
        {
            moneyStored += amount;
        }
        if(resource == Resource.wheat)
        {
            wheatStored += amount;
        }
        if(resource == Resource.apple)
        {
            appleStored += amount;
        }
        if(resource == Resource.cinnamon)
        {
            cinnamonStored += amount;
        }
    }
    // void AddMoney(int32 amount);
    public IsActionValidMessage IsActionValid(Action action)
    {
        IsActionValidMessage isActionValidMessage = new IsActionValidMessage();



        if (action is BuildAction)
        {
            return IsValidToBuild((BuildAction)action, isActionValidMessage);
        }

        if( action is AddCardToHandAction)
        {
            return IsValidToAdd((AddCardToHandAction)action, isActionValidMessage);
        }
        if( action is PlayCardAction)
        {
            return IsValidToPlay((PlayCardAction)action, isActionValidMessage);

        }


        isActionValidMessage.wasActionValid = true;
        return isActionValidMessage;
    }

    private IsActionValidMessage IsValidToAdd(AddCardToHandAction action, IsActionValidMessage message)
    {
        if(currentActions == 0)
        {
            message.wasActionValid = false;
            message.errorMessage = "You do not have enough actions";
            return message;
        }

        message.wasActionValid = true;
        return message;
    }
    private IsActionValidMessage IsValidToPlay(PlayCardAction action, IsActionValidMessage message)
    {
        if (currentActions == 0)
        {
            message.wasActionValid = false;
            message.errorMessage = "You do not have enough actions";
            return message;
        }

        message.wasActionValid = true;
        return message; 
    }



    public void DoAction(Action action)
    {
        if (IsActionValid(action).wasActionValid == false)
        {
            print("skickade en invalid action");

            IsActionValidMessage isActionValidMessage = IsActionValid(action);

            if(isActionValidMessage.errorMessage != null)
            {
                print(isActionValidMessage.errorMessage);
            }
            
            return;
        }
        if (action is EndTurnAction)
        {
            StartTurnUpkeep();
        }
        if(action is BuildAction)
        {
            BuildAction buildAction = (BuildAction)action;

            BuildOnFarmtile(buildAction);

            AddResources(Resource.money, GetBuildingCost(buildAction.resource) * -1);

            currentActions -= 1;

            //print("Built!" + buildAction.resource);
        }
        if(action is AssignWorkersAction)
        {
            AssignWorkers((AssignWorkersAction)action);
        }
        if(action is AddCardToHandAction)
        {
            AddCardToHand((AddCardToHandAction)action);
        }
        if(action is PlayCardAction)
        {
            PlayCard((PlayCardAction)action);
        }
    }

    private void AssignWorkers(AssignWorkersAction assignAction)
    {
        ClearWorkersFromFarms();

        List<int> workerIds = new List<int>();
        foreach(KeyValuePair<int,Worker> workerPair in workerRegistry)
        {
            workerIds.Add(workerPair.Key);
        }


        int index = 0;
        foreach(Tuple<int,WorkType> workAssigned in assignAction.workAssigned )
        {
            Worker workerToAdd = workerRegistry[workerIds[index]];
            workerToAdd.workType = workAssigned.Item2;

            farmTileRegistry[workAssigned.Item1].workersOnTile.Add(workerToAdd);
            index += 1;

          //  print("vilken ruta " + workAssigned.Item1 + " vilket arbete " + workAssigned.Item2);
        }

    }

    private void ClearWorkersFromFarms()
    {
        foreach(KeyValuePair<int, FarmTile> farmTileValue  in farmTileRegistry)
        {
            farmTileValue.Value.workersOnTile.Clear();
        }
    }

    private IsActionValidMessage IsValidToBuild(BuildAction buildAction, IsActionValidMessage isActionValidMessage)
    {
        if (farmTileRegistry[buildAction.farmTileIndex].buildingOnTile)
        {
            isActionValidMessage.wasActionValid = false;
            isActionValidMessage.errorMessage = "Already a building on the tile";
            return isActionValidMessage;
        }
        if(GetBuildingCost(buildAction.resource) > moneyStored)
        {
            isActionValidMessage.wasActionValid = false;
            isActionValidMessage.errorMessage = "You do not have enough money";
            return isActionValidMessage;
        }
        if(currentActions == 0)
        {
            isActionValidMessage.wasActionValid = false;
            isActionValidMessage.errorMessage = "You do not have enough actions";
            return isActionValidMessage;
        }
        isActionValidMessage.wasActionValid = true;

        return isActionValidMessage;
    }

    private void BuildOnFarmtile(BuildAction buildAction)
    {

        print(buildAction.farmTileIndex);

        farmTileRegistry[buildAction.farmTileIndex].buildingOnTile = true;
        farmTileRegistry[buildAction.farmTileIndex].resourceOnTile = buildAction.resource;
    }
    public int GetStoredResourceAmount(Resource resourceType)
    {
        if (resourceType == Resource.wheat)
        {
            return wheatStored;
        }

        if (resourceType == Resource.apple)
        {
            return appleStored;
        }
        if (resourceType == Resource.cinnamon)
        {
            return cinnamonStored;
        }
        if (resourceType == Resource.pigMeat)
        {
            return pigMeatStored;
        }

        if (resourceType == Resource.money)
        {
            return moneyStored;
            
        }
        return -1;
    }
    public int GetBuildingCost(Resource resource) 
    {
        if (resource == Resource.wheat)
        {
            return 500;
        }

        if (resource == Resource.apple)
        {
            return 600;
        }
        if (resource == Resource.cinnamon)
        {
            return 700;
        }
        if (resource == Resource.pigMeat)
        {
            return 600;
        }

        return -1; 
    }

    public SortedDictionary<int,Worker> GetWorkerRegistry()
    {
        return workerRegistry;
    }

    public int GetMaxStorage()
    {
        return currentStorage;
    }

    public int GetMaxActions()
    {
        return maxActions;
    }

    public int GetCurrentActions()
    {
        return currentActions;
    }

    public int GetCurrentTurn()
    {
        return currentTurn;
    }

    public int GetMaxTurn()
    {
        return maxTurn;
    }

    public Dictionary<int, FarmTile> GetFarmTiles()
    {
        return farmTileRegistry;
    }
}
