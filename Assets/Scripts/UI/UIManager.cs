using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameStateLogic gameStateLogic;
    public List<GameObject> farmTilePositions = new List<GameObject>();
    public List<BuildingOnTile> placedMeshes = new List<BuildingOnTile>();

    public List<TextMeshProUGUI> farmTileTexts = new List<TextMeshProUGUI>();

    private List<List<WorkerPosition>> workerPositions = new List<List<WorkerPosition>>();

    public CardDeck cardDeck;

    public static UIManager instance;

    public string testString;

    public GameObject builtPigMesh;
    public GameObject builtWeatMesh;
    public GameObject builtAppleMesh;
    public GameObject builtCinnamonMesh;

    public GameObject workerMesh;

    public GameObject farmTextPrefab;

    public HudState hudState;

    private Stack<AddedUIElement> addedUIElements = new Stack<AddedUIElement>();

    private SortedDictionary<int, GameObject> workerToBePlacedRegistry = new SortedDictionary<int, GameObject>();


    public TextMeshProUGUI wheatText;
    public TextMeshProUGUI appleText;
    public TextMeshProUGUI cinnamonText;
    public TextMeshProUGUI pigText;
    public TextMeshProUGUI moneyText;
    public TextMeshProUGUI actionText;
    public TextMeshProUGUI turnText;
    public TextMeshProUGUI amountOfWorkersText;

    public FarmMeshStages wheatStages;
    public FarmMeshStages appleStages;
    public FarmMeshStages cinnamonStages;
    public FarmMeshStages pigStages;

    public ErrorMessage errorMessage;


    private ContractLayout contractLayout;

    public void AddUIElement(AddedUIElement uiElement)
    {
        hudState = uiElement.hudStateNeeded;

        addedUIElements.Push(uiElement);
    }

    public void PopUIElement()
    {
        if (addedUIElements.Count > 0)
        {

            foreach (GameObject gameObject in addedUIElements.Peek().uiElementToInactivate)
            {
                print("element poppades " + gameObject.name);
                gameObject.SetActive(false);
            }
            //addedUIElements.Peek().uiElementToInactivate.SetActive(false);
            addedUIElements.Pop();
            if (addedUIElements.Count > 0)
            {
                hudState = addedUIElements.Peek().hudStateNeeded;
            }
            else
            {
                hudState = HudState.standard;
            }
        }
    }


    public void SendErrorMessage(string message)
    {
        errorMessage.SetErrorMessage(message);
    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) 
        {
            PopUIElement();
        }
    }

    void Awake()
    {
        if (instance != null)
            GameObject.Destroy(instance);
        else
            instance = this;

        DontDestroyOnLoad(this);
    }

    public void TestString()
    {
        print(testString);
    }

    public void DoAction(Action action)
    {
        gameStateLogic.DoAction(action);
        Refresh();
    }
    public IsActionValidMessage IsActionValid(Action action)
    {
        return gameStateLogic.IsActionValid(action);    
    }
        
    public void EndTurn()
    {
        EndTurnAction endTurnAction = new EndTurnAction();

        DoAction(endTurnAction);
    }

    private void Refresh()
    {
        
        cardDeck.Refresh();
        PlaceMeshes();
        PlaceWorkers();
        UpdateResourceText();
        ResourceTextOnTile();
        RefreshContractCards();

    }

//   private void HUDVFX()
//   {
//       int currentMoney = gameStateLogic.GetStoredResourceAmount(Resource.money);
//       if(currentMoney != 0)
//       {
//
//       }
//   }

    private void UpdateResourceText()
    {
        moneyText.SetText(gameStateLogic.GetStoredResourceAmount(Resource.money).ToString());
        wheatText.SetText(gameStateLogic.GetStoredResourceAmount(Resource.wheat) + "/" + gameStateLogic.GetMaxStorage());
        appleText.SetText(gameStateLogic.GetStoredResourceAmount(Resource.apple) + "/" + gameStateLogic.GetMaxStorage());
        cinnamonText.SetText(gameStateLogic.GetStoredResourceAmount(Resource.cinnamon) + "/" + gameStateLogic.GetMaxStorage());
        pigText.SetText(gameStateLogic.GetStoredResourceAmount(Resource.pigMeat) + "/" + (gameStateLogic.GetMaxStorage()/10));


        //print("vilken turn är det " + gameStateLogic.GetCurrentTurn());

        string turnTextString = gameStateLogic.GetCurrentTurn() + "/" + gameStateLogic.GetMaxTurn();

        turnText.SetText(turnTextString);

       // print(turnTextString);

        actionText.SetText(gameStateLogic.GetCurrentActions() + "/" + gameStateLogic.GetMaxActions());


        amountOfWorkersText.SetText("Workers " + gameStateLogic.GetWorkerRegistry().Count);
    }


  private void ResourceTextOnTile()
   {
       foreach (KeyValuePair<int, FarmTile> farmTileRef in gameStateLogic.GetFarmTiles())
       {
           int farmTileIndex = farmTileRef.Key;

           GameObject farmMeshPosition = farmTilePositions[farmTileIndex];

           if (farmMeshPosition != null)
           {
               FarmTile farmTile = farmTileRef.Value;

               if (farmTile.buildingOnTile || farmTile.isBuilt)
               {
                   TextMeshProUGUI farmTileText = farmMeshPosition.GetComponentInChildren<TextMeshProUGUI>(true);

                    farmTileText.gameObject.transform.parent.gameObject.SetActive(true);

                   if (farmTileText != null && farmTile.resourceOnTile != Resource.pigMeat)
                   {
                       farmTileText.SetText(farmTileRef.Value.storedResources + " / " + farmTileRef.Value.maxStoredResources);
                   }
                   if (farmTileText != null && farmTile.resourceOnTile == Resource.pigMeat)
                   {
                       farmTileText.SetText(farmTileRef.Value.amountOfAnimals + " / " + farmTileRef.Value.maxAmountOfAnimals);
                   }
               }
               else
                {
                    TextMeshProUGUI farmTileText = farmMeshPosition.GetComponentInChildren<TextMeshProUGUI>(true);
                    farmTileText.gameObject.transform.parent.gameObject.SetActive(false);

                }

            }
       }
   }



    void Start()
    {
        gameStateLogic.Setup();

        contractLayout = FindAnyObjectByType<ContractLayout>();

        for(int i = 0; i < 9; i++)
        {
            farmTilePositions.Add(null);
            placedMeshes.Add(new BuildingOnTile());
        }
        FarmMeshPosition[] foundPositions = FindObjectsByType<FarmMeshPosition>(FindObjectsSortMode.None);
        foreach (FarmMeshPosition position in foundPositions)
        {
            farmTilePositions[position.farmTileIndex] = position.gameObject;
        }


        for(int i = 0; i < 9; i++)
        {
            workerPositions.Add(new List<WorkerPosition>());
            for(int z = 0; z < 3; z++)
            {
                workerPositions[i].Add(null);
            }
        }
        WorkerPosition[] foundWorkerPositions = FindObjectsByType<WorkerPosition>(FindObjectsSortMode.None);

        print("hur många worker positions " + foundWorkerPositions.Length);

        foreach(WorkerPosition workerPosition in foundWorkerPositions)
        {
            workerPositions[workerPosition.farmTileIndex][workerPosition.positionOrder] = workerPosition;
        }


        Refresh();
        

    }

    void PlaceMeshes()
    {   
        
        foreach(KeyValuePair<int, FarmTile> farmTileValue in gameStateLogic.GetFarmTiles())
        {
            if(farmTileValue.Value.buildingOnTile && placedMeshes[farmTileValue.Key].meshToPlace == null && !farmTileValue.Value.isBuilt)
            {
                GameObject meshToPlace = GetMeshToPlace(farmTileValue.Value.resourceOnTile);

                placedMeshes[farmTileValue.Key].meshToPlace = Instantiate<GameObject>(meshToPlace);

                placedMeshes[farmTileValue.Key].meshToPlace.transform.position = farmTilePositions[farmTileValue.Key].transform.position;
                

        //      farmTileValue.Value.resourceOnTile lägg ut mesh
        //          farmtileposition för mesh
            }
            FarmTile farmTile = farmTileValue.Value;
            int index = farmTileValue.Key;
            if (farmTile.isBuilt)
            {
                if(farmTile.resourceOnTile != Resource.pigMeat)
                {
                    if (farmTile.storedResources == placedMeshes[index].previousAmount)
                    {
                        continue;
                    }
                    else
                    {
                        Destroy(placedMeshes[index].meshToPlace);
                        placedMeshes[index].previousAmount = farmTile.storedResources;
                        GameObject meshToPlace = Instantiate<GameObject>(GetMeshToPlaceBuilt(farmTile.resourceOnTile, farmTile.storedResources));
                        placedMeshes[index].meshToPlace = meshToPlace;
                        placedMeshes[index].meshToPlace.transform.position = farmTilePositions[index].transform.position;
                    }
                }
                else
                {
                    if (farmTile.amountOfAnimals == placedMeshes[index].previousAmount)
                    {
                        continue;
                    }
                    else
                    {
                        Destroy(placedMeshes[index].meshToPlace);
                        placedMeshes[index].previousAmount = farmTile.amountOfAnimals;
                        GameObject meshToPlace = Instantiate<GameObject>(GetMeshToPlaceBuilt(farmTile.resourceOnTile, farmTile.amountOfAnimals));
                        placedMeshes[index].meshToPlace = meshToPlace;
                        placedMeshes[index].meshToPlace.transform.position = farmTilePositions[index].transform.position;
                    }
                }


                
            }
        }
    }

    private GameObject GetMeshToPlace(Resource resource)
    {
        return builtWeatMesh;
    //   if(resource == Resource.wheat)
    //   {
    //       return builtWeatMesh;
    //   }
    //   if(resource == Resource.apple)
    //   {
    //       return builtAppleMesh;
    //   }
    //   if(resource == Resource.cinnamon)
    //   {
    //       return builtCinnamonMesh;
    //   }
    //   if(resource == Resource.pigMeat)
    //   {
    //       return builtPigMesh;
    //   }
    //
    //   return builtWeatMesh;
    }
    private GameObject GetMeshToPlaceBuilt(Resource resource,int amount)
    {
        if(resource == Resource.wheat )
        {
            if(amount == 0)
            {
                return wheatStages.stage0;
            }
            if(amount == 40)
            {
                return wheatStages.stage1;
            }
            if(amount == 80)
            {
                return wheatStages.stage2;
            }
            if(amount == 120)
            {
                return wheatStages.stage3;
            }
        }
        if(resource == Resource.apple)
        {
            if (amount == 0)
            {
                return appleStages.stage0;
            }
            if (amount == 40)
            {
                return appleStages.stage1;
            }
            if (amount == 80)
            {
                return appleStages.stage2;
            }
            if (amount == 120)
            {
                return appleStages.stage3;
            }
        }
        if(resource == Resource.cinnamon)
        {
            if (amount == 0)
            {
                return cinnamonStages.stage0;
            }
            if (amount == 40)
            {
                return cinnamonStages.stage1;
            }
            if (amount == 80)
            {
                return cinnamonStages.stage2;
            }
            if (amount == 120)
            {
                return cinnamonStages.stage3;
            }
        }
        if(resource == Resource.pigMeat)
        {

            if(amount <= 4)
            {
                return pigStages.stage0;
            }
            if( 4 < amount && amount < 12)
            {
                return pigStages.stage1;
            }
            if( 12 <= amount && amount < 16)
            {
                return pigStages.stage2;
            }
            if( 16 <= amount )
            {
                return pigStages.stage3;
            }
        }

        return builtWeatMesh;
    }


    void PlaceWorkers()
    {
        ResetWorkerPositions();
        SortedDictionary<int, Worker> registredWorkers = gameStateLogic.GetWorkerRegistry();

        foreach(KeyValuePair<int,Worker> pair in registredWorkers)
        {
            Worker registredWorker = pair.Value;

            if (workerToBePlacedRegistry.ContainsKey(registredWorker.workedId) == false)
            {
                GameObject spawnedWorker = Instantiate<GameObject>(workerMesh);
                //	workerToBePlacedRegistry.Add()
                workerToBePlacedRegistry.Add(registredWorker.workedId, spawnedWorker);
            }
        }
        foreach(KeyValuePair<int, GameObject> pair in workerToBePlacedRegistry)
        {
            int workerId = pair.Key;

            if (registredWorkers.ContainsKey(workerId) == false)
            {
                GameObject actorToRemove = workerToBePlacedRegistry[workerId];

                workerToBePlacedRegistry.Remove(workerId);

                Destroy(actorToRemove);
            }
        }


        int indexFarmTile = 0;
        List<Worker> workersOnTiles = new List<Worker>();
        foreach(KeyValuePair<int,FarmTile> pair in gameStateLogic.GetFarmTiles())
        {

            FarmTile farmTile = pair.Value;
            foreach(Worker worker in farmTile.workersOnTile)
            {
                PlaceWorker(indexFarmTile, worker.workedId);
                workersOnTiles.Add(worker);
            }
            indexFarmTile += 1;
        }
        int padding = 0;

        foreach(KeyValuePair<int, Worker> pair in registredWorkers)
        {
            if (ContainsForTArrayWorker(workersOnTiles, pair.Value) == false)
            {
                workerToBePlacedRegistry[pair.Key].transform.position = new UnityEngine.Vector3(0,300+padding,0);
                padding += 30;
            }
        }

    }

    void PlaceWorker(int farmTileIndex, int workerId)
    {
        List<WorkerPosition> arrayToLoop = workerPositions[farmTileIndex];

        for (int i = 0; i < 3; i++)
        {
            if (arrayToLoop[i].workerPlacedHere == false)
            {
                arrayToLoop[i].workerPlacedHere = true;
                GameObject workerToPlace = workerToBePlacedRegistry[workerId];
                workerToPlace.transform.position = arrayToLoop[i].transform.position;
                return;
            }
        }
    }

    private void RefreshContractCards()
    {
        contractLayout.OrderCards();
    }

    void ResetWorkerPositions()
    {
        foreach(List<WorkerPosition> listOfWorkerPositions in workerPositions)
        {
            foreach(WorkerPosition workerPosition in listOfWorkerPositions)
            {
                workerPosition.workerPlacedHere = false;
            }
        }
    }

    bool ContainsForTArrayWorker(List<Worker> arrayToExamine, Worker workerToFind)
    {
        foreach(Worker  worker in arrayToExamine)
        {
            if (worker.workedId == workerToFind.workedId)
            {
                return true;
            }
        }

        return false;
    }


}
