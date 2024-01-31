using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UIElements;

public class ActiveEffectHover : MonoBehaviour, IPointerEnterHandler
{

    private UIManager manager;

    [SerializeField] private GameObject hoverTextPrefab;

    private List<EffectHover> effectsPlaced = new List<EffectHover>(); 

    private bool effectsShowing = false;

    private void Start()
    {
        manager = UIManager.instance;
    }

    private void OnMouseEnter()
    {
        
    }

    private void OnMouseExit()
    {
        
    }

    public void CreateActiveEffects()
    {
        

        List<EffectLifeTime> activeEffects = UIManager.instance.gameStateLogic.GetActiveEffects();

        foreach (EffectLifeTime effect in activeEffects)
        {
            EffectLifeTime activeEffect = effect;

            if (ContainsInEffectHover(effect.cardIdentifier)== false)
            {
                GameObject createdCardHover = Instantiate<GameObject>(hoverTextPrefab);

                createdCardHover.transform.SetParent(gameObject.transform, false);

                EffectHover effectHover = createdCardHover.GetComponent<EffectHover>();

                print(activeEffect.cardIdentifier + " nu är den i hover");

                effectHover.InitializeObject(activeEffect.cardIdentifier, activeEffect.turnAmount);

                effectsPlaced.Add(effectHover);
            }
        }
        foreach (EffectHover effect in effectsPlaced)
        {
            string cardName = effect.cardName;

            if (ContainsInEffectList(activeEffects,cardName)== false)
            {
                GameObject actorToRemove = effectsPlaced[GetEffectHover(cardName)].gameObject;

                effectsPlaced.RemoveAt(GetEffectHover(cardName));

                Destroy(actorToRemove);
            }
        }
    }

    private bool ContainsInEffectHover(string cardName)
    {

        foreach(EffectHover effect in effectsPlaced)
        {
            if(effect.cardName.Equals(cardName))
            {
                return true; 
            }
        }

        return false;
    }
    private bool ContainsInEffectList(List<EffectLifeTime> activeEffects,string cardName)
    {

        foreach(EffectLifeTime effect in activeEffects)
        {
            if(effect.cardIdentifier.Equals(cardName))
            {
                return true; 
            }
        }

        return false;
    }
    
    private int GetEffectHover(string cardName)
    {
        for(int i = 0; i < effectsPlaced.Count; i++)
        {
            if (effectsPlaced[i].cardName.Equals(cardName))
            {
                return i;
            }
        }

        return -1;
    }

    public void OnClick()
    {

        if (manager == null) { manager = UIManager.instance; }

        EffectHover[] children = gameObject.GetComponentsInChildren<EffectHover>(true);

        foreach (EffectHover child in children)
        {
            if(effectsShowing )
            {
                child.gameObject.SetActive(false);
                effectsShowing = false;
            }
            else
            {
                child.gameObject.SetActive(true);
                effectsShowing = true;
            }
        }

        //print(children.Length + "hej hej hej");
    }
    public void OnPointerEnter(PointerEventData eventData)
    {

    }
}
