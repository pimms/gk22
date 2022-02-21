using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ObjectiveController: MonoBehaviour, EventListener {
    public static ObjectiveController instance;

    public Objective currentObjective {
        get {
            if (transitioningBetweenObjectives || currentObjectiveIndex >= objectives.Length) {
                return null;
            }
            return objectives[currentObjectiveIndex];
        }
    }

    private Objective[] objectives;
    private int currentObjectiveIndex = 0;
    private bool transitioningBetweenObjectives = false;

    private ObjectiveController() {
        objectives = new Objective [] {
            new Objective(ItemEvent.placedItem(ItemType.Fedora, SpotType.Head), "Kle på deg", "get_dressed", 10f),
            new Objective(RoomEvent.leftRoom(RoomType.Bedroom), "Re opp sengen", "make_bed", 0f),
            new Objective(ItemEvent.placedItem(ItemType.Toothbrush, SpotType.Toilet), "Puss tennene", "brush_teeth", 0f),
            new Objective(ItemEvent.placedItem(ItemType.Beer, SpotType.Head), "Drikk en kopp kaffe", "coffee"),
            new Objective(ItemEvent.placedItem(ItemType.PS5Controller, SpotType.Hand), "Gå på jobb", "job"),
            new Objective(ItemEvent.placedItem(ItemType.Cake, SpotType.Head), "Spis en sunn lunch", "lunch"),
            new Objective(ItemEvent.placedItem(ItemType.Head, SpotType.ToiletHeadPosition), "Gå og bæsj", "toilet"),
            new Objective(RoomEvent.leftRoom(RoomType.Bathroom), "Børst toalettet", "toilet_cleanup", 0f),
            new Objective(ItemEvent.placedItem(ItemType.Butt, SpotType.Sofa), "Dra på trening", "gym", 0f),
        };
    }

    void Start() {
        ObjectiveController.instance = this;
        EventHub.instance.AddListener(this);
        StartCoroutine(ActivateCurrentObjective());
    }

    void Destroy() {
        ObjectiveController.instance = null;
        EventHub.instance.RemoveListener(this);
    }

    private IEnumerator ActivateCurrentObjective() {
        if (currentObjectiveIndex < objectives.Length) {
            Objective objective = objectives[currentObjectiveIndex];
            yield return new WaitForSeconds(objective.activationDelay);
            transitioningBetweenObjectives = false;
            StartCoroutine(objectives[currentObjectiveIndex].Activate());
        }
    }

    public void ObjectiveFinalized(Objective objective) {
        if (objective != currentObjective) {
            Debug.LogError("Objective completed is not the current objective");
            return;
        }

        currentObjectiveIndex++;
        transitioningBetweenObjectives = true;
        EventHub.instance.Raise(ObjectiveEvent.changed(null));
        StartCoroutine(ActivateCurrentObjective());
    }

    public void HandleEvent(Event e) {
        if (currentObjectiveIndex >= objectives.Length) {
            return;
        }

        objectives[currentObjectiveIndex].HandleEvent(e);
    }
}