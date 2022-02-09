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
            new Objective(ItemEvent.placedItem(ItemType.Fedora, SpotType.Head), "Kle på deg noe saklig", null),
            new Objective(ItemEvent.placedItem(ItemType.Head, SpotType.ToiletHeadPosition), "Gå på do", null),
            new Objective(RoomEvent.leftRoom(RoomType.Bedroom), "Re opp sengen", "reoppsengen"),
            new Objective(ItemEvent.placedItem(ItemType.Toothbrush, SpotType.Toilet), "Puss tennene", "tannborste"),
        };
    }

    void Start() {
        ObjectiveController.instance = this;
        EventHub.instance.AddListener(this);
        StartCoroutine(ActivateCurrentObjective(3));
    }

    void Destroy() {
        ObjectiveController.instance = null;
        EventHub.instance.RemoveListener(this);
    }

    private IEnumerator ActivateCurrentObjective(float delay) {
        if (currentObjectiveIndex < objectives.Length) {
            yield return new WaitForSeconds(delay);
            transitioningBetweenObjectives = false;
            objectives[currentObjectiveIndex].Activate();
            EventHub.instance.Raise(new Event(EventType.ObjectiveChanged));
        }
    }

    public void ObjectiveFinalized(Objective objective) {
        if (objective != currentObjective) {
            Debug.LogError("Objective completed is not the current objective");
            return;
        }

        currentObjectiveIndex++;
        transitioningBetweenObjectives = true;
        EventHub.instance.Raise(new Event(EventType.ObjectiveChanged));
        StartCoroutine(ActivateCurrentObjective(3));
    }

    public void HandleEvent(Event e) {
        if (currentObjectiveIndex >= objectives.Length) {
            return;
        }

        objectives[currentObjectiveIndex].HandleEvent(e);
    }
}