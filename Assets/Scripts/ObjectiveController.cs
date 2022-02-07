using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ObjectiveController: MonoBehaviour, EventListener {
    public static ObjectiveController instance;

    public Objective currentObjective {
        get {
            if (currentObjectiveIndex < objectives.Count) {
                return objectives[currentObjectiveIndex];
            } else {
                return null;
            }
        }
    }

    private List<Objective> objectives = new List<Objective>();
    private int currentObjectiveIndex = 0;

    private ObjectiveController() {
        objectives.Add(new Objective(RoomEvent.leftRoom(RoomType.Bedroom), "Re opp sengen"));
        objectives.Add(new Objective(RoomEvent.leftRoom(RoomType.Bathroom), "Puss tennene"));
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

    public void ObjectiveCompleted(Objective objective) {
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
        if (currentObjectiveIndex >= objectives.Count) {
            return;
        }

        currentObjective.HandleEvent(e);
    }
}