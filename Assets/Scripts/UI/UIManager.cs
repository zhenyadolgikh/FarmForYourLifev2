using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameStateLogic gameStateLogic;
    public List<GameObject> farmTilePositions = new List<GameObject>();
    public List<BuildingOnTile> placedMeshes = new List<BuildingOnTile>();  

    private List<List<WorkerPosition>> workerPositions = new List<List<WorkerPosition>>();

    public CardDeck cardDeck;

    public static UIManager instance;

    public string testString;

    public GameObject builtPigMesh;
    public GameObject workerMesh;

    public HudState hudState;

    private Stack<AddedUIElement> addedUIElements = new Stack<AddedUIElement>();

    private SortedDictionary<int, GameObject> workerToBePlacedRegistry = new SortedDictionary<int, GameObject>();

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
                print(gameObject.name);
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
    public bool IsActionValid(Action action)
    {
        return gameStateLogic.IsActionValid(action);    
    }
        
    public void EndTurn()
    {
        EndTurnAction endTurnAction = new EndTurnAction();

        DoAction(endTurnAction);
    }

    public void Refresh()
    {
        
        cardDeck.ResetCards();
        cardDeck.CreateDisplayCards();
        PlaceMeshes();
        PlaceWorkers();

    }

    void Start()
    {
        gameStateLogic.Setup();

        for(int i = 0; i < 9; i++)
        {
            farmTilePositions.Add(null);
            placedMeshes.Add(new BuildingOnTile());
        }
        FarmMeshPosition[] foundPositions = FindObjectsByType<FarmMeshPosition>(FindObjectsSortMode.None);
        print(foundPositions.Length);
        foreach(FarmMeshPosition position in foundPositions)
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


        

    }

    void PlaceMeshes()
    {   
        
        foreach(KeyValuePair<int, FarmTile> farmTileValue in gameStateLogic.GetFarmTiles())
        {
            if(farmTileValue.Value.buildingOnTile && placedMeshes[farmTileValue.Key].meshToPlace == null)
            { 

                if(farmTileValue.Value.resourceOnTile == Resource.pigMeat)
                {
                    print("kommer den till grisarna");

                    placedMeshes[farmTileValue.Key].meshToPlace = Instantiate<GameObject>(builtPigMesh);

                    placedMeshes[farmTileValue.Key].meshToPlace.transform.position = farmTilePositions[farmTileValue.Key].transform.position;
                }

        //      farmTileValue.Value.resourceOnTile lägg ut mesh
        //          farmtileposition för mesh
            }
            if(farmTileValue.Value.isBuilt && placedMeshes[farmTileValue.Key].isBuilt == false)
            {

            }
        }
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
                workerToBePlacedRegistry[pair.Key].transform.position = new UnityEngine.Vector3(0,0+padding,0);
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
