using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameStateLogic gameStateLogic;
    public List<GameObject> farmTilePositions = new List<GameObject>();
    public List<Tuple<GameObject, bool>> placedMeshes = new List<Tuple<GameObject, bool>>();  

    public CardDeck cardDeck;

    public static UIManager instance;

    public string testString;

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
    }

    void Start()
    {
        gameStateLogic.Setup();

        for(int i = 0; i < 9; i++)
        {
            farmTilePositions.Add(null);
            placedMeshes.Add(new Tuple<GameObject, bool>(null, false));
        }
        FarmMeshPosition[] foundPositions = FindObjectsByType<FarmMeshPosition>(FindObjectsSortMode.None);
        print(foundPositions.Length);
        foreach(FarmMeshPosition position in foundPositions)
        {
            farmTilePositions[position.farmTileIndex] = position.gameObject;
        }
    }

    void PlaceMeshes()
    {
        foreach(KeyValuePair<int, FarmTile> farmTileValue in gameStateLogic.GetFarmTiles())
        {
            if(farmTileValue.Value.buildingOnTile && placedMeshes[farmTileValue.Key].Item1 == null)
            { 
        //      farmTileValue.Value.resourceOnTile lägg ut mesh
        //          farmtileposition för mesh
            }
            if(farmTileValue.Value.isBuilt && placedMeshes[farmTileValue.Key].Item2 == false)
            {

            }
        }
    }
}
