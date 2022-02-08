using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ObjectiveController: MonoBehaviour, EventListener {
    public static ObjectiveController instance;

    public Objective currentObjective {
        get {
            if (currentObjectiveIndex < objectives.Length) {
                return objectives[currentObjectiveIndex];
            } else {
                return null;
            }
        }
    }

    private Objective[] objectives;
    private int currentObjectiveIndex = 0;

    private ObjectiveController() {
        objectives = new Objective [] {
            new Objective(RoomEvent.leftRoom(RoomType.Bedroom), "Re opp sengen", "reoppsengen"),
            new Objective(ItemEvent.placedItem(ItemType.Toothrush, SpotType.Toilet), "Puss tennene", "tannborste"),
        };
    }

    void Start() {
        ObjectiveController.instance = this;
        EventHub.instance.AddListener(this);
        StartCoroutine(NotifyFirstObjective());
    }

    void Destroy() {
        ObjectiveController.instance = null;
        EventHub.instance.RemoveListener(this);
    }

    private IEnumerator NotifyFirstObjective() {
        yield return new WaitForSeconds(3);
        currentObjective.Activate();
        EventHub.instance.Raise(new Event(EventType.ObjectiveChanged));
    }

    public void ObjectiveFinalized(Objective objective) {
        if (objective != currentObjective) {
            Debug.LogError("Objective completed is not the current objective");
            return;
        }

        currentObjectiveIndex++;
        if (currentObjective != null) {
            currentObjective.Activate();
        }

        EventHub.instance.Raise(new Event(EventType.ObjectiveChanged));
    }

    public void HandleEvent(Event e) {
        if (currentObjectiveIndex >= objectives.Length) {
            return;
        }

        currentObjective.HandleEvent(e);
    }
}