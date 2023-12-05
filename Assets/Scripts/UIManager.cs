using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{

    public GameStateLogic gameStateLogic;

    public CardDeck cardDeck;


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
        print("setup har h�nt" + gameStateLogic.GetStoredResourceAmount(Resource.money));
    }
}
