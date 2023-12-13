using JetBrains.Annotations;
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


    private int increaseStorageCost = 500;
    private int storageLevel = 1;
    private int currentStorage = 0;

    private int moneyStored = 0;

    private int wheatStored = 0;
    private int appleStored = 0;
    private int cinnamonStored = 0;
    private int pigMeatStored = 0;

    private int currentTurn = 0;
    public int maxTurn = 0;

    private int workersCreated = 0;
    private int workerPay = 100; 

    private SortedDictionary<int, Worker> workerRegistry = new SortedDictionary<int, Worker>();

    private Dictionary<int, FarmTile> farmTileRegistry = new Dictionary<int, FarmTile>();


    public List<SpecialCard> specialCardsForLevel = new List<SpecialCard>();
    private Dictionary<string, SpecialCard> specialCardRegistry = new Dictionary<string, SpecialCard>();
    private List<SpecialCard> specialCardDeck = new List<SpecialCard>();  
    private List<SpecialCard> specialCardsOnTable = new List<SpecialCard>();
    private int maxSpecialCardsOnTable = 3;
    private int maxContractCardsOnTable = 5; 
    private int maximumHandSize = 10;

    public List<ContractCard> contractCardsForLevel = new List<ContractCard>();
    private Dictionary<string, ContractCard> contractCardRegistry = new Dictionary<string, ContractCard>();
    private List<ContractCard> contractCardDeck = new List<ContractCard>();
    private List<ContractCard> contractCardsOnTable = new List<ContractCard>();

    private List<Card> cardsInHand = new List<Card>();


    private List<EffectLifeTime> activeEffects = new List<EffectLifeTime>();    


    private EffectInterface effectInterface;

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        {
            AddResources(Resource.money, 1000);
        }
    }


    public class EffectInterface
    {
        private GameStateLogic gameStateLogic;


        public EffectInterface(GameStateLogic GameStateLogic)
        {
            gameStateLogic = GameStateLogic;
        }
        public void AddResource(Resource resource, int amount)
        {
            gameStateLogic.AddResources(resource, amount);
        }

        public void IncreaseActions(int amount)
        {
            gameStateLogic.currentActions += amount;
        }

        public void AddWorker()
        {
            gameStateLogic.AddWorker();
        }

        public void BrainStormShuffleCards()
        {
            List<SpecialCard> specialCardsToAdd = new List<SpecialCard>();
            for(int i = 0; i < gameStateLogic.maxSpecialCardsOnTable; i++)
            {
                if (gameStateLogic.specialCardsOnTable[i] != null)
                {
                    specialCardsToAdd.Add(gameStateLogic.specialCardsOnTable[i]);
                }
            }

            gameStateLogic.specialCardDeck.AddRange(specialCardsToAdd);

            gameStateLogic.ShuffleCards<SpecialCard>(gameStateLogic.specialCardDeck);

            for(int i = 0; i < gameStateLogic.maxSpecialCardsOnTable; i++)
            {
                if(gameStateLogic.specialCardDeck.Count  > 0)
                {
                    gameStateLogic.specialCardsOnTable[i] = gameStateLogic.specialCardDeck[0];

                    gameStateLogic.specialCardDeck.RemoveAt(0);
                }
            }
        }

        public void DoubleAnimals()
        {
            foreach (KeyValuePair<int, FarmTile> farmTilePair in gameStateLogic.farmTileRegistry)
            {
                FarmTile farmTile = farmTilePair.Value;

                if (farmTile.isBuilt == false || farmTile.resourceOnTile != Resource.pigMeat)
                {
                    continue;
                }
                if (farmTile.firstTurnBuilt)
                {
                    farmTile.firstTurnBuilt = false;
                    continue;
                }

                farmTile.amountOfAnimals *= 2;
                if (farmTile.amountOfAnimals > farmTile.maxAmountOfAnimals)
                {
                    farmTile.amountOfAnimals = farmTile.maxAmountOfAnimals;
                }
                print(farmTile.amountOfAnimals);
            }
        }

    }

    public void Setup()
    {

        effectInterface = new EffectInterface(this);

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
        foreach(ContractCard contractCardinEditor in contractCardsForLevel)
        {   

            if (!contractCardRegistry.ContainsKey(contractCardinEditor.getCardName()))
            {
                contractCardRegistry.Add(contractCardinEditor.getCardName(), contractCardinEditor);
            }
            contractCardDeck.Add(contractCardinEditor);
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
        FillContractCardsOnTable();

    }


    void FillCardsOnTable()
    {
        ShuffleSpecialCards(specialCardDeck);
     //   print("Special card deck size" + specialCardDeck.Count);
        for(int i = 0; i < maxSpecialCardsOnTable; i++)
        {
            SpecialCard cardToAdd = specialCardDeck[0];
            specialCardsOnTable.Add(cardToAdd);
            specialCardDeck.Remove(cardToAdd);
        }
    }
    

    void FillContractCardsOnTable()
    {
        ShuffleContractCards(contractCardDeck);
    //    print("Special card deck size" + specialCardDeck.Count);
        for(int i = 0; i < maxContractCardsOnTable; i++)
        {
            ContractCard cardToAdd = contractCardDeck[0];
            contractCardsOnTable.Add(cardToAdd);
            contractCardDeck.Remove(cardToAdd);
        }
    }

    private System.Random rng = new System.Random();
    void ShuffleCards<CardType>(List<CardType> cards)
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            CardType value = cards[k];
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
    void ShuffleContractCards(List<ContractCard> cards)
    {
        int n = cards.Count;
        while (n > 1)
        {
            n--;
            int k = rng.Next(n + 1);
            ContractCard value = cards[k];
            cards[k] = cards[n];
            cards[n] = value;
        }
    }

    void StartTurnUpkeep()
    {


        UpdateEffectLifeTimes();
        ProductionPhase();
        WorkPhase();
        AnimalProductionPhase();
        ResetNumbers();
        //MoveCards();
        MoveCards<SpecialCard>(specialCardsOnTable, specialCardDeck);
        MoveCards<ContractCard>(contractCardsOnTable, contractCardDeck);
        //  MoveContractCards();

        PayWorkers();

        currentTurn += 1; 
    }

    private void PayWorkers()
    {
        int amountToPayCoefficient = 100;

        foreach(EffectLifeTime effect in activeEffects)
        {
            amountToPayCoefficient = effect.ModifyWorkerPay(amountToPayCoefficient);
        }

        int amountToPay = amountToPayCoefficient * workerRegistry.Count;
        moneyStored -= amountToPay;
    }

    private void UpdateEffectLifeTimes()
    {
        List<EffectLifeTime> effectsToPreserve = new List<EffectLifeTime>();

        foreach(EffectLifeTime effectLifeTime in activeEffects)
        {
            effectLifeTime.UpdateLifeTime();

            if(effectLifeTime.lifeTimeEnded)
            {
                if(effectLifeTime.typeOfCard == TypeOfCard.special)
                {
                    print("vilken kort typ är det " + effectLifeTime.typeOfCard);

                    specialCardDeck.Add(specialCardRegistry[effectLifeTime.cardIdentifier]);
                }
            }
            else
            {
                effectsToPreserve.Add(effectLifeTime);
            }
        }

        activeEffects = effectsToPreserve; 

        print("hur manga effekter ar aktiva " + activeEffects.Count);

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

    private void MoveCards<CardType>(List<CardType> cardsOnTable, List<CardType> cardsInDeck)
    {
        List<CardType> cardsToRemove = new List<CardType>();
        List<CardType> cardsToMoveLeft = new List<CardType>();

        int maxCardsOnTable = MaxCardsOnTable<CardType>();

        for (int i = 0; i < maxCardsOnTable; i++)
        {
            if (cardsOnTable[i] != null && cardsToRemove.Count < 2)
            {
                cardsToRemove.Add(cardsOnTable[i]);
                cardsOnTable[i] = default(CardType);
            }
            if (cardsOnTable[i] != null && cardsToRemove.Count >= 2)
            {
                cardsToMoveLeft.Add(cardsOnTable[i]);
                cardsOnTable[i] = default(CardType);
            }
        }

        cardsInDeck.AddRange(cardsToRemove);
        for (int i = 0; i < cardsToMoveLeft.Count; i++)
        {
            cardsOnTable[i] = cardsToMoveLeft[i];
        }
        ShuffleCards(cardsInDeck);
        for (int i = 0; i < maxCardsOnTable; i++)
        {
            if (cardsOnTable[i] == null)
            {

                if (cardsInDeck.Count > 0)
                {
                    cardsOnTable[i] = cardsInDeck[0];

                    cardsInDeck.RemoveAt(0);
                }
            }
        }
    }



    private int MaxCardsOnTable<CardType>()
    {
        if(typeof(CardType) == typeof(SpecialCard))
        {
            return maxSpecialCardsOnTable;
        }
        else
        {
            return maxContractCardsOnTable;
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
        else
        {
            string cardName = contractCardsOnTable[action.index].cardName;
            contractCardsOnTable[action.index] = null;
            cardsInHand.Add(contractCardRegistry[cardName]);
        }
    }

    private void PlayCard( PlayCardAction playCardAction)
    {

        currentActions -= 1;

        Card cardPLayed = cardsInHand[playCardAction.index];

        cardsInHand.RemoveAt(playCardAction.index);

        if(cardPLayed is SpecialCard)
        {
            //specialCardDeck.Add((SpecialCard)cardPLayed);
            EffectLifeTime effectLifeTime = cardPLayed.PlayCard(effectInterface);

            if (effectLifeTime != null)
            {
                activeEffects.Add(effectLifeTime);
            }
            else
            {
                specialCardDeck.Add((SpecialCard)cardPLayed);
            }
        }
        else
        {
            contractCardDeck.Add((ContractCard)cardPLayed);

            ContractCard contractCard = (ContractCard)cardPLayed;

            ResourcesAmount resourcesAmount = ResourceAmountFromContract(contractCard);

            foreach(EffectLifeTime effect in activeEffects)
            {
                resourcesAmount = effect.ModifyContract(resourcesAmount,ResourceAmountFromStored());
            }


            if (resourcesAmount.wheatCost != -1)
            {
                wheatStored -= resourcesAmount.wheatCost; 
            }
            if (resourcesAmount.appleCost != -1 )
            {
                appleStored -= resourcesAmount.appleCost;

            }
            if (resourcesAmount.cinnamonCost != -1)
            {
                cinnamonStored -= resourcesAmount.cinnamonCost;

            }
            if (resourcesAmount.pigMeatCost != -1)
            {
                pigMeatStored -= resourcesAmount.pigMeatCost;
            }

            moneyStored += resourcesAmount.moneyGained;

        }
        //




    }

    private void AnimalProductionPhase()
    {
        foreach (KeyValuePair<int, FarmTile> farmTilePair in farmTileRegistry)
        {
            FarmTile farmTile = farmTilePair.Value;

            if (farmTile.isBuilt == false || farmTile.resourceOnTile != Resource.pigMeat)
            {
                continue;
            }
            if (farmTile.firstTurnBuilt)
            {
                farmTile.firstTurnBuilt = false;
                continue;
            }

            farmTile.amountOfAnimals *= 2;
            if (farmTile.amountOfAnimals > farmTile.maxAmountOfAnimals)
            {
                farmTile.amountOfAnimals = farmTile.maxAmountOfAnimals;
            }

            int amountToBeEatenCoefficient = 10;
            foreach (EffectLifeTime effectLifeTime in activeEffects)
            {
                amountToBeEatenCoefficient = effectLifeTime.ModifyAmountToBeEaten(amountToBeEatenCoefficient);
            }

            int amountToBeEaten = (farmTile.amountOfAnimals / 4) * amountToBeEatenCoefficient;

            print("mängden som ska ätas " + amountToBeEaten);

            if (amountToBeEaten < wheatStored)
            {
                wheatStored -= amountToBeEaten;


            }
            else
            {

                int foodDifference = amountToBeEaten - wheatStored;

                int amountToDie = foodDifference / 10;

                farmTile.amountOfAnimals -= amountToDie * 4;

                if (farmTile.amountOfAnimals < 1)
                {
                    farmTile.amountOfAnimals = 1;
                }

                wheatStored = 0;
            }


            print("mangden djur " + farmTile.amountOfAnimals);
           
        }
    }

    void ProductionPhase()
    {
        foreach(KeyValuePair<int,FarmTile> farmTilePair in farmTileRegistry)
        {   
            
            if(farmTilePair.Value.isBuilt == false || farmTilePair.Value.resourceOnTile == Resource.pigMeat)
            {
                continue;
            }

            FarmTile farmTile = farmTilePair.Value;
            if(farmTile.resourceOnTile != Resource.pigMeat)
            {
                int amountProduced = farmTile.productionRate;
                int modifiedProduced = farmTile.productionRate;
                foreach (EffectLifeTime effectLifeTime in activeEffects)
                {
                    modifiedProduced = effectLifeTime.ModifyResourcesGenerated(farmTile.resourceOnTile, farmTile.productionRate);
                }
                if (modifiedProduced > amountProduced)
                {
                    amountProduced = modifiedProduced;
                }

                farmTile.storedResources += amountProduced;

                if (farmTile.storedResources > farmTile.maxStoredResources)
                {
                    farmTile.storedResources = farmTile.maxStoredResources;
                }
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

    public List<Card> getCardsOnTable(TypeOfCard typeOfCard)
    {
        List<Card> listToReturn = new List<Card> ();
        
        if(typeOfCard == TypeOfCard.special)
        {
            foreach(SpecialCard card in  specialCardsOnTable)
            {
                listToReturn.Add(card);
            }
        }
                
        if(typeOfCard == TypeOfCard.contract)
        {
            foreach(ContractCard card in  contractCardsOnTable)
            {
                listToReturn.Add(card);
            }
        }

        return listToReturn;
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
        if(action is AssignWorkersAction)
        {
            return IsValidToAssign((AssignWorkersAction)action, isActionValidMessage);
        }
        if(action is IncreaseStorageAction)
        {
            return IsValidToIncreaseStorage((IncreaseStorageAction)action, isActionValidMessage);
        }

        isActionValidMessage.wasActionValid = true;
        return isActionValidMessage;
    }

    private IsActionValidMessage IsValidToIncreaseStorage(IncreaseStorageAction action, IsActionValidMessage message)
    {
        if(storageLevel == 3)
        {
            message.wasActionValid = false;
            message.errorMessage = "Your storage is already at max level";
            return message;
        }
        if(moneyStored < increaseStorageCost)
        {
            message.wasActionValid = false;
            message.errorMessage = "You have to little money";
            return message;
        }
        int actionCost = 1;
        if (currentActions < actionCost)
        {
            message.wasActionValid = false;
            message.errorMessage = "You do not have enough actions";
            return message;
        }



        message.wasActionValid = true;
        return message;
    }

    private IsActionValidMessage IsValidToAssign(AssignWorkersAction action, IsActionValidMessage message)
    {
        int actionCost = 1;

        foreach(EffectLifeTime effectLifeTime in activeEffects)
        {
            actionCost = effectLifeTime.ModifyActionCost(ActionCostingType.assignWorkers, actionCost);
        }

        if (currentActions < actionCost)
        {
            message.wasActionValid = false;
            message.errorMessage = "You do not have enough actions";
            return message;
        }

        message.wasActionValid = true;
        return message;
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
        if(action.typeOfCard == TypeOfCard.contract)
        {   
            ContractCard contractCard = contractCardRegistry[action.cardIdentifier];

            ResourcesAmount contractResources = ResourceAmountFromContract(contractCard);

            foreach(EffectLifeTime effect in activeEffects)
            {
                contractResources = effect.ModifyContract(contractResources, ResourceAmountFromStored());
            }


            if(contractResources.wheatCost > wheatStored)
            {
                message.wasActionValid = false;
                message.errorMessage = "Not enough resources for contract";
                return message;
            }
            if(contractResources.appleCost > appleStored)
            {
                message.wasActionValid = false;
                message.errorMessage = "Not enough resources for contract";
                return message;

            }
            if (contractResources.cinnamonCost > cinnamonStored)
            {
                message.wasActionValid = false;
                message.errorMessage = "Not enough resources for contract";
                return message;

            }
            if (contractResources.pigMeatCost > pigMeatStored)
            {
                message.wasActionValid = false;
                message.errorMessage = "Not enough resources for contract";
                return message;
            }
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
        if(action is IncreaseStorageAction)
        {
            IncreaseStorage((IncreaseStorageAction)action);
        }
    }

    private void IncreaseStorage(IncreaseStorageAction action)
    {
        storageLevel += 1;

        currentStorage = storageLevel * 120;

        moneyStored -= increaseStorageCost;
        currentActions -= 1;


    }

    private void AssignWorkers(AssignWorkersAction assignAction)
    {
        ClearWorkersFromFarms();

        int actionCost = 1;

        foreach(EffectLifeTime effect in activeEffects)
        {
            actionCost = effect.ModifyActionCost(ActionCostingType.assignWorkers, actionCost);
        }

        currentActions -= actionCost; 

        
        List<int> workerIds = new List<int>();
        foreach(KeyValuePair<int,Worker> workerPair in workerRegistry)
        {
            workerIds.Add(workerPair.Key);
        }


        int index = 0;
        foreach(Tuple<int,WorkType> workAssigned in assignAction.workAssigned )
        {
            Worker workerToAdd = workerRegistry[workerIds[index]];
            FarmTile farmTile = farmTileRegistry[workAssigned.Item1];
            workerToAdd.workType = workAssigned.Item2;

            farmTile.workersOnTile.Add(workerToAdd);
            index += 1;
            if(workAssigned.Item2 == WorkType.slaughtering && farmTile.resourceOnTile == Resource.pigMeat)
            {
                if(farmTile.amountOfAnimals > 1)
                {   
                    int amountSlaughtered = farmTile.amountOfAnimals / 2;
                    farmTile.amountOfAnimals = farmTile.amountOfAnimals / 2;

                    pigMeatStored += amountSlaughtered;
                    if(pigMeatStored > currentStorage / 10)
                    {
                        pigMeatStored = currentStorage / 10;
                    }

                    workerToAdd.workType = WorkType.unassigned;
                }
            }
          //  print("vilken ruta " + workAssigned.Item1 + " vilket arbete " + workAssigned.Item2);
        }

    }

    private ResourcesAmount ResourceAmountFromContract(ContractCard card)
    {
        ResourcesAmount resourcesAmountToReturn = new ResourcesAmount();

        resourcesAmountToReturn.wheatCost = card.wheatNeeded;
        resourcesAmountToReturn.appleCost = card.applesNeeded;
        resourcesAmountToReturn.cinnamonCost = card.cinnamonsNeeded;
        resourcesAmountToReturn.pigMeatCost = card.pigMeatNeeded;
        resourcesAmountToReturn.moneyGained = card.moneyEarned;

        return resourcesAmountToReturn;
    }
    private ResourcesAmount ResourceAmountFromStored()
    {
        ResourcesAmount resourcesAmountToReturn = new ResourcesAmount();

        resourcesAmountToReturn.wheatCost = wheatStored;
        resourcesAmountToReturn.appleCost = appleStored;
        resourcesAmountToReturn.cinnamonCost = cinnamonStored;
        resourcesAmountToReturn.pigMeatCost = pigMeatStored;
        resourcesAmountToReturn.moneyGained = moneyStored;

        return resourcesAmountToReturn;
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
        int actionCost = 1; 

        foreach(EffectLifeTime effect in activeEffects)
        {
            actionCost = effect.ModifyActionCost(ActionCostingType.build, actionCost);
        }


        if(currentActions < actionCost)
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

        int actionCost = 1;

        foreach(EffectLifeTime effect in activeEffects)
        {
            actionCost = effect.ModifyActionCost(ActionCostingType.build, actionCost);
        }

        currentActions -= actionCost;


        farmTileRegistry[buildAction.farmTileIndex].buildingOnTile = true;
        farmTileRegistry[buildAction.farmTileIndex].resourceOnTile = buildAction.resource;
        if(buildAction.resource == Resource.pigMeat)
        {
            farmTileRegistry[buildAction.farmTileIndex].amountOfAnimals = 4;
            farmTileRegistry[buildAction.farmTileIndex].firstTurnBuilt = true ;
        }
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
