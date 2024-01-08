using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{

    [SerializeField] private bool isTutorial;
    [SerializeField] private bool acceptActions;


    [SerializeField] private AssignWorkersButton assignWorkersButton;

    private List<Action> tutorialAcceptedActions = new List<Action>();

    private TutorialObject tutorialObjectToPop;

    

    public GameStateLogic gameStateLogic;
    public List<GameObject> farmTilePositions = new List<GameObject>();
    public List<BuildingOnTile> placedMeshes = new List<BuildingOnTile>();

    public List<TextMeshProUGUI> farmTileTexts = new List<TextMeshProUGUI>();

    private List<List<WorkerPosition>> workerPositions = new List<List<WorkerPosition>>();

    private List<Transform> idleWorkerTransforms = new List<Transform>();

    public CardDeck cardDeck;
    public EndPanel endPanel;
    public UICostManager costManager;
    public ClonedWorker clonedWorker;

    public static UIManager instance;

    public string testString;

    public GameObject builtPigMesh;
    public GameObject builtWeatMesh;
    public GameObject builtAppleMesh;
    public GameObject builtCinnamonMesh;

    public GameObject wheatResource;
    public GameObject appleResource;
    public GameObject cinnamonResource;
    public GameObject pigResource;
    public GameObject moneyResource;

    public GameObject workers;
    public GameObject workersAssignment;

    public GameObject buildWheatFarm;
    public GameObject buildAppleFarm;
    public GameObject buildCinnamonFarm;
    public GameObject buildPigFarm;

    public ParticleSystem slaughterParticle;

    public GameObject workerMesh;

    public GameObject farmTextPrefab;

    public HudState hudState;

    public AddedUIElement currentUIelement { get; private set; }

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

    public GameObject pausePanel;


    [SerializeField] private ContractLayout contractLayout;
    [SerializeField] private ContractLayout specialCardLayout;
    [SerializeField] private CardHandLayout handLayout;

    //ghetto af
    private bool mouseClickHandled = false;
    private bool mouseClickOccured = false;
    private bool mouseClickedHandledSecondTime = false;


    private bool isWorkerDragged = false;
    private bool isWorkerDraggedSecondFrame = false;
    private int workerBeingDraggedId = -1;

    private bool farmTileMouseUp = false;
    private bool farmTileMouseUpSecondFrame = false;
    private int farmTileMouseUpIndex = -1;

    private Dictionary<string, ParticleSystem> playingVFXeffects = new Dictionary<string, ParticleSystem>();


    void Start()
    {
        gameStateLogic.Setup();

        if (isTutorial)
        {
            BuildAction buildAction = new BuildAction();
            buildAction.farmTileIndex = 3;
            buildAction.resource = Resource.wheat;

            tutorialAcceptedActions.Add(buildAction);

            AssignWorkersAction assignAction = new AssignWorkersAction();

            Tuple<int, WorkType> tuple = new Tuple<int, WorkType>(3, WorkType.building);

            assignAction.workAssigned.Add(tuple);

            tutorialAcceptedActions.Add(assignAction);


            AddCardToHandAction addCardToHandAction = new AddCardToHandAction();
            addCardToHandAction.index = 2;
            addCardToHandAction.cardType = TypeOfCard.special;

            tutorialAcceptedActions.Add(addCardToHandAction);

            EndTurnAction endTurnAction = new EndTurnAction();
            tutorialAcceptedActions.Add(endTurnAction);


            AssignWorkersAction assignActionHarvest = new AssignWorkersAction();

            Tuple<int, WorkType> tupleHarvest = new Tuple<int, WorkType>(3, WorkType.harvesting);

            assignActionHarvest.workAssigned.Add(tupleHarvest);

            tutorialAcceptedActions.Add(assignActionHarvest);

            IncreaseStorageAction increaseStorageAction = new IncreaseStorageAction();

            tutorialAcceptedActions.Add(increaseStorageAction);

            PlayCardAction playCard = new PlayCardAction();
            playCard.index = 0;
            playCard.typeOfCard = TypeOfCard.special;
            playCard.cardIdentifier = "Money printer";
            tutorialAcceptedActions.Add(playCard);

            MoveCardsAction moveCards = new MoveCardsAction();
            moveCards.typeOfdeck = TypeOfCard.special;
            moveCards.moneyCost = 200;

            tutorialAcceptedActions.Add(moveCards);

            tutorialAcceptedActions.Add(endTurnAction);


            BuildAction buildPig = new BuildAction();
            buildPig.farmTileIndex = 0;
            buildPig.resource = Resource.pigMeat;

            tutorialAcceptedActions.Add(buildPig);

            AssignWorkersAction assignPigWorkBuild = new AssignWorkersAction();

            Tuple<int, WorkType> tupleBuildPig = new Tuple<int, WorkType>(0, WorkType.building);

            assignPigWorkBuild.workAssigned.Add(tupleBuildPig);


            tutorialAcceptedActions.Add(assignPigWorkBuild);


            tutorialAcceptedActions.Add(endTurnAction);


            AssignWorkersAction assignPigWorkSlaughter = new AssignWorkersAction();

            Tuple<int, WorkType> tupleSlaughter = new Tuple<int, WorkType>(0, WorkType.slaughtering);

            assignPigWorkSlaughter.workAssigned.Add(tupleSlaughter);

            tutorialAcceptedActions.Add(assignPigWorkSlaughter);

            //
            gameStateLogic.CardSetupTutorial();
        }



        contractLayout = FindAnyObjectByType<ContractLayout>();

        for (int i = 0; i < 9; i++)
        {
            farmTilePositions.Add(null);
            placedMeshes.Add(new BuildingOnTile());
        }
        FarmMeshPosition[] foundPositions = FindObjectsByType<FarmMeshPosition>(FindObjectsSortMode.None);
        foreach (FarmMeshPosition position in foundPositions)
        {
            farmTilePositions[position.farmTileIndex] = position.gameObject;
        }


        for (int i = 0; i < 9; i++)
        {
            workerPositions.Add(new List<WorkerPosition>());
            for (int z = 0; z < 3; z++)
            {
                workerPositions[i].Add(null);
            }
        }
        WorkerPosition[] foundWorkerPositions = FindObjectsByType<WorkerPosition>(FindObjectsSortMode.None);

        print("hur m�nga worker positions " + foundWorkerPositions.Length);

        foreach (WorkerPosition workerPosition in foundWorkerPositions)
        {
            workerPositions[workerPosition.farmTileIndex][workerPosition.positionOrder] = workerPosition;
        }


        IdleWorkerPosition[] foundIdlePositions = FindObjectsByType<IdleWorkerPosition>(FindObjectsSortMode.None);

        for (int i = 0; i < 9; i++)
        {
            idleWorkerTransforms.Add(null);
        }

        foreach (IdleWorkerPosition idlePosition in foundIdlePositions)
        {
            idleWorkerTransforms[idlePosition.index] = idlePosition.gameObject.transform;
        }


        Refresh();


    }
        
    private WorkType GetWorkTypeFromFarm(int farmindex)
    {
        FarmTile farmTile = gameStateLogic.GetFarmTiles()[farmindex];

        if(!farmTile.isBuilt || !farmTile.buildingOnTile)
        {
            return WorkType.building;
        }
        if(farmTile.resourceOnTile == Resource.pigMeat)
        {
            return WorkType.slaughtering;
        }
        else
        {
            return WorkType.harvesting;
        }

    }
    private void AssignWorkerFromDrag(int farmTileIndex)
    {
        AssignIndividualWorker action = new AssignIndividualWorker();

        action.workerId = workerBeingDraggedId;
        action.workType = GetWorkTypeFromFarm(farmTileIndex);
        action.farmTileIndex = farmTileIndex;

        IsActionValidMessage message = IsActionValid(action);

        if(message != null )
        {
            if(message.wasActionValid)
            {
                DoAction(action);
                ResetDragValues();
            }
            else
            {
                SendErrorMessage(message.errorMessage);
            }
        }

    }

    private void ResetDragValues()
    {
        isWorkerDragged = false;
        isWorkerDraggedSecondFrame = false;
        workerBeingDraggedId = -1;

        farmTileMouseUp = false;
        farmTileMouseUpSecondFrame = false;
        farmTileMouseUpIndex = -1;
    }

    public void SetFarmTileMouseUp(int farmTileIndex, bool value)
    {
        farmTileMouseUpIndex = farmTileIndex;

        farmTileMouseUp = value;
        farmTileMouseUpSecondFrame = value;
    }

    private void Update()
    {

        if(Input.GetMouseButtonUp((int)MouseButton.Left))
        {

            RayCastFarmTile();
        }

        print("är knappen uppe " + farmTileMouseUp);
        print("är workern dragged " + isWorkerDragged);

        if(farmTileMouseUp && isWorkerDragged)
        {
            print("kommer den hit");
            AssignWorkerFromDrag(farmTileMouseUpIndex);
        }

        if(farmTileMouseUp)
        {
            if(farmTileMouseUpSecondFrame)
            {
                farmTileMouseUpSecondFrame = false;
            }
            else
            {
                farmTileMouseUp = false;
            }
        }

        if (isWorkerDragged)
        {
            if (isWorkerDraggedSecondFrame)
            {
                isWorkerDraggedSecondFrame = false;
            }
            else
            {
                isWorkerDragged = false;
            }
        }


        if (mouseClickOccured && !mouseClickHandled)
        {
            if (!mouseClickedHandledSecondTime)
            {
                PopUIElement();

            }
            else
            {
                mouseClickedHandledSecondTime = false;
            }

        }
        else
        {
            //print("fungerade typ kanske " + mouseClickHandled + "occurades det " + mouseClickOccured);
        }

        mouseClickOccured = false;
        mouseClickHandled = false;
        if (Input.GetMouseButtonUp(((int)MouseButton.Left)))
        {
            mouseClickOccured = true;
        }
        if (Input.GetMouseButtonDown(((int)MouseButton.Right)))
        {
            PopUIElement();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(true);
        }


        //print(hudState);

    }

    void Awake()
    {
        if (instance != null)
            GameObject.Destroy(instance);
        else
            instance = this;

        DontDestroyOnLoad(this);

    }

    public void Refresh()
    {

        cardDeck.Refresh();
        PlaceMeshes();
        PlaceWorkers();
        UpdateResourceText();
        ResourceTextOnTile();
        RefreshContractCards();
        costManager.CanAfford();
        handLayout.OrderCards();
        CardEffectVFX();
        endPanel.GameEnd();

    }


    public void MouseClickHandled()
    {
        mouseClickHandled = true;
        mouseClickedHandledSecondTime = true;
    }


    public void AddUIElement(AddedUIElement uiElement)
    {

        ResetUIElement();

        hudState = uiElement.hudStateNeeded;
        currentUIelement = uiElement;
    }


    public void ResetUIElement()
    {
        if (currentUIelement != null)
        {
            bool elementRemoved = false;
            while (!elementRemoved)
            {
                elementRemoved = currentUIelement.RemoveElement();
            }
        }
        hudState = HudState.standard;

        currentUIelement = null;

    }

    public void SetHudState(HudState newState)
    {
        hudState = newState;
    }

    public void PopUIElement()
    {
        if (currentUIelement != null)
        {
            bool elementRemoved = false;

            elementRemoved = currentUIelement.RemoveElement();
            if(elementRemoved)
            {
                currentUIelement = null;
                hudState = HudState.standard;
            }

        }
        else
        {
            hudState = HudState.standard;
        }

    }

    public void ContinuePlay()
    {
        pausePanel.SetActive(false);
    }

    public void SendErrorMessage(string message)
    {
        errorMessage.SetErrorMessage(message);
    }
   

    public void TestString()
    {
        print(testString);
    }
    
    public void SetWorkerBeingDragged(bool value)
    {
        isWorkerDragged = value;
        isWorkerDraggedSecondFrame = value;
    }


    public void RayCastFarmTile()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit = new RaycastHit();
        LayerMask mask = LayerMask.GetMask("FarmTile");

        if(Physics.Raycast(ray, out hit, mask))
        {
            print(hit.collider.gameObject.GetComponent<FarmTileUI>().farmTileIndex);

            farmTileMouseUpIndex = hit.collider.gameObject.GetComponent<FarmTileUI>().farmTileIndex;
            farmTileMouseUp = true;
            farmTileMouseUpSecondFrame = true;
        }

    }


    public void DoAction(Action action)
    {

        if (!acceptActions)
        {
            return ;
        }

        if (isTutorial)
        {
            if(action is AssignWorkersAction)
            {
                int hej = 4;
            }

            if (tutorialAcceptedActions.Count > 0 &&!action.Equals(tutorialAcceptedActions[0]))
            {
                return ;
            }
            else
            {   
                if(action is MoveCardsAction)
                {
                    gameStateLogic.CheatActionsTutorial();
                }

                tutorialAcceptedActions.RemoveAt(0);

                tutorialObjectToPop.RemoveObject();
            }
        }

        gameStateLogic.DoAction(action);
        Refresh();

    }
    public IsActionValidMessage IsActionValid(Action action)
    {   

        if(action is PlayCardAction)
        {

            int hej = 5;
        }
        if (!acceptActions)
        {
            return null;
        }

        if (isTutorial)
        {   
            if (tutorialAcceptedActions.Count > 0 && !action.Equals(tutorialAcceptedActions[0]))
            {
                return null;
            }
            else
            {
            }
        }

        return gameStateLogic.IsActionValid(action);    
    }
        
    public void EndTurn()
    {
        EndTurnAction endTurnAction = new EndTurnAction();

        DoAction(endTurnAction);
    }



    //Callback ist�llet f�r refresh strukturen
    public void OrderContractCards()
    {
        contractLayout.OrderCards();

    }
    public void OrderSpecialCards()
    {
        specialCardLayout.OrderCards();
    }

    private void UpdateResourceText()
    {
        moneyText.SetText(gameStateLogic.GetStoredResourceAmount(Resource.money).ToString());
        wheatText.SetText(gameStateLogic.GetStoredResourceAmount(Resource.wheat) + "/" + gameStateLogic.GetMaxStorage());
        appleText.SetText(gameStateLogic.GetStoredResourceAmount(Resource.apple) + "/" + gameStateLogic.GetMaxStorage());
        cinnamonText.SetText(gameStateLogic.GetStoredResourceAmount(Resource.cinnamon) + "/" + gameStateLogic.GetMaxStorage());
        pigText.SetText(gameStateLogic.GetStoredResourceAmount(Resource.pigMeat) + "/" + (gameStateLogic.GetMaxStorage()/10));


        //print("vilken turn �r det " + gameStateLogic.GetCurrentTurn());

        string turnTextString = gameStateLogic.GetCurrentTurn() + "/" + gameStateLogic.GetMaxTurn();

        turnText.SetText(turnTextString);

        actionText.SetText(gameStateLogic.GetCurrentActions() + "/" + gameStateLogic.GetMaxActions());

        amountOfWorkersText.SetText("Workers: " + gameStateLogic.GetWorkerRegistry().Count);
    }

    public void ResourceVFX(Resource resource)
    {   
        if(isTutorial)
        {
            return;
        }
        switch (resource)
        {
            case Resource.wheat:
                Animator wheatAnim = wheatResource.GetComponent<Animator>();
                wheatAnim.SetTrigger("reSize");
                break;

            case Resource.apple:
                Animator appleAnim = appleResource.GetComponent<Animator>();
                appleAnim.SetTrigger("reSize");
                break;

            case Resource.cinnamon:
                Animator cinnamonAnim = cinnamonResource.GetComponent<Animator>();
                cinnamonAnim.SetTrigger("reSize");
                break;

            case Resource.pigMeat:
                Animator pigAnim = pigResource.GetComponent<Animator>();
                pigAnim.SetTrigger("reSize");
                break;

            case Resource.money:
                Animator moneyAnim = moneyResource.GetComponent<Animator>();
                moneyAnim.SetTrigger("reSize");
                break;

            default:
                
                break;
        }

    }

    public void MoneyFromContract()
    {
        moneyResource.GetComponentInChildren<ParticleSystem>().Play();
    }

    public void CardEffectVFX()
    {
        ParticleSystem particleSystem;

        foreach(EffectLifeTime effect in gameStateLogic.GetActiveEffects())
        {
            if(effect is AnimalFastingLifeTime)
            {
                //VFX
            }
            if (effect is WheatAlchemyLifeTime)
            {
                particleSystem = wheatResource.GetComponentInChildren<ParticleSystem>();
                particleSystem.loop = true;
                particleSystem.Play();

                if(!playingVFXeffects.ContainsKey(effect.cardIdentifier))
                {
                    playingVFXeffects.Add(effect.cardIdentifier, particleSystem);
                }

            }
            if (effect is WheatSeasonLifeTme)
            {
                //VFX
            }
            if (effect is UnionCrackDownLifeTime)
            {
                particleSystem = moneyResource.GetComponentInChildren<ParticleSystem>();
                particleSystem.loop = true;
                particleSystem.Play();

                if (!playingVFXeffects.ContainsKey(effect.cardIdentifier))
                {
                    playingVFXeffects.Add(effect.cardIdentifier, particleSystem);
                }
            }
            if (effect is LabourRushLifeTime)
            {
                particleSystem = workers.GetComponentInChildren<ParticleSystem>();
                particleSystem.loop = true;
                particleSystem.Play();

                if (!playingVFXeffects.ContainsKey(effect.cardIdentifier))
                {
                    playingVFXeffects.Add(effect.cardIdentifier, particleSystem);
                }

            }
            //Over Working
            if (effect is ActionCostLifeTime)
            {
                particleSystem = workersAssignment.GetComponentInChildren<ParticleSystem>();
                particleSystem.loop = true;
                particleSystem.Play();

                if (!playingVFXeffects.ContainsKey(effect.cardIdentifier))
                {
                    playingVFXeffects.Add(effect.cardIdentifier, particleSystem);
                }
            }
            //Regulation Cheating
            if (effect is ActionCostLifeTime)
            {
                particleSystem = workersAssignment.GetComponentInChildren<ParticleSystem>();
                particleSystem.loop = true;
                particleSystem.Play();

                if (!playingVFXeffects.ContainsKey(effect.cardIdentifier))
                {
                    playingVFXeffects.Add(effect.cardIdentifier, particleSystem);
                }
            }
            if (effect is ActionCostLifeTime)
            {
                particleSystem = workersAssignment.GetComponentInChildren<ParticleSystem>();
                particleSystem.loop = true;
                particleSystem.Play();

                if (!playingVFXeffects.ContainsKey(effect.cardIdentifier))
                {
                    playingVFXeffects.Add(effect.cardIdentifier, particleSystem);
                }
            }
            if (effect is ActionCostLifeTime)
            {
                particleSystem = workersAssignment.GetComponentInChildren<ParticleSystem>();
                particleSystem.loop = true;
                particleSystem.Play();

                if (!playingVFXeffects.ContainsKey(effect.cardIdentifier))
                {
                    playingVFXeffects.Add(effect.cardIdentifier, particleSystem);
                }
            }
        }

        foreach(KeyValuePair<string, ParticleSystem> valuePair in playingVFXeffects)
        {
            bool contains = false;

            foreach(EffectLifeTime effectLifeTime in gameStateLogic.GetActiveEffects())
            {
                if(effectLifeTime.cardIdentifier.Equals(valuePair.Key))
                {
                    contains = true;
                }
            }

            if(contains == false)
            {
                playingVFXeffects[valuePair.Key].Stop();
            }
        }

        
    }

    public void SlaughterEffect(int farmTileIndex)
    {
        ParticleSystem particle = Instantiate(slaughterParticle);
        particle.transform.position = farmTilePositions[farmTileIndex].transform.position;
        particle.Play(true);
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
                if (!farmTile.isBuilt)
                {
                    TextMeshProUGUI farmTileText = farmMeshPosition.GetComponentInChildren<TextMeshProUGUI>(true);
                    farmTileText.gameObject.transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    TextMeshProUGUI farmTileText = farmMeshPosition.GetComponentInChildren<TextMeshProUGUI>(true);
                    farmTileText.gameObject.transform.parent.gameObject.SetActive(true);
                }

                if (farmTile.buildingOnTile || farmTile.isBuilt)
               {
                   TextMeshProUGUI farmTileText = farmMeshPosition.GetComponentInChildren<TextMeshProUGUI>(true);

                    //farmTileText.gameObject.transform.parent.gameObject.SetActive(true);

                   if (farmTileText != null && farmTile.resourceOnTile != Resource.pigMeat)
                   {
                       farmTileText.SetText(farmTileRef.Value.storedResources + " / " + farmTileRef.Value.maxStoredResources);
                   }
                   if (farmTileText != null && farmTile.resourceOnTile == Resource.pigMeat)
                   {
                       farmTileText.SetText(farmTileRef.Value.amountOfAnimals + " / " + farmTileRef.Value.maxAmountOfAnimals);
                   }
               }

            }
       }
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
                

        //      farmTileValue.Value.resourceOnTile l�gg ut mesh
        //          farmtileposition f�r mesh
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
        if(resource == Resource.wheat)
        {
            return builtWeatMesh;
        }
        if(resource == Resource.apple)
        {
            return builtAppleMesh;
        }
        if(resource == Resource.cinnamon)
        {
            return builtCinnamonMesh;
        }
        if(resource == Resource.pigMeat)
        {
            return builtPigMesh;
        }

        return builtWeatMesh;
        
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
                spawnedWorker.GetComponent<ClonedWorker>().workerId = registredWorker.workedId;
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
                PlaceWorker(indexFarmTile, worker.workedId, worker.workType);
                workersOnTiles.Add(worker);
            }
            indexFarmTile += 1;
        }
        int placedWorkers = 0;
        foreach(KeyValuePair<int, Worker> pair in registredWorkers)
        {
            if (ContainsForTArrayWorker(workersOnTiles, pair.Value) == false)
            {   

                workerToBePlacedRegistry[pair.Key].transform.position = idleWorkerTransforms[placedWorkers].position;


                placedWorkers += 1;
            }
        }

    }
    
    public void PlaceWorkerDuringAssign(Tuple<int, WorkType> workAssigned, int count)
    {   
        if(count == 0)
        {
            return;
        }
        else
        {
            PlaceWorker(workAssigned.Item1, count - 1, workAssigned.Item2);
         //   workerToBePlacedRegistry[count - 1].transform.position = farmTilePositions[workAssigned.Item1].transform.position;
        }

    }

    IEnumerator PopElementLeftClick()
    {
        yield return new WaitForEndOfFrame();
        print("�r det handled " + mouseClickHandled);
        if(Input.GetMouseButtonDown(((int)MouseButton.Left)))
        {
            if(mouseClickHandled)
            {
            }
            else
            {
                PopUIElement();
            }
        }

    }

    public void PlaceAllWorkersIdle()
    {
        ResetWorkerPositions();
        int placedWorkers = 0;
        foreach (KeyValuePair<int, Worker> pair in gameStateLogic.GetWorkerRegistry())
        {

            workerToBePlacedRegistry[pair.Key].transform.position = idleWorkerTransforms[placedWorkers].position;
            workerToBePlacedRegistry[pair.Key].GetComponent<ClonedWorker>().WorkerAnimation(WorkType.unassigned);

            placedWorkers += 1;

        }
    }

    void PlaceWorker(int farmTileIndex, int workerId, WorkType workType)
    {
        List<WorkerPosition> arrayToLoop = workerPositions[farmTileIndex];

        for (int i = 0; i < 3; i++)
        {
            if (arrayToLoop[i].workerPlacedHere == false)
            {
                arrayToLoop[i].workerPlacedHere = true;
                GameObject workerToPlace = workerToBePlacedRegistry[workerId];
                workerToPlace.transform.position = arrayToLoop[i].transform.position;
                workerToPlace.GetComponent<ClonedWorker>().WorkerAnimation(workType);
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



    public void SetAcceptActions(bool value)
    {
        acceptActions = value;
    }
    public void SetTutorialObjectToPop(TutorialObject value)
    {
        tutorialObjectToPop = value;
    }


    public void SetWorkerBeingDraggedId(int value)
    {
        workerBeingDraggedId = value;
    }

    public bool GetIsTutorial()
    {
        return isTutorial;
    }
}
