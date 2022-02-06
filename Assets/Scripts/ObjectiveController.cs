using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class ObjectiveController: MonoBehaviour, EventListener {
    public static ObjectiveController instance;

    private List<Objective> objectives = new List<Objective>();
    private int currentObjectiveIndex = 0;
    private Objective currentObjective {
        get {
            return objectives[currentObjectiveIndex];
        }
    }

    void Start() {
        ObjectiveController.instance = this;
        EventHub.instance.AddListener(this);
    }

    void Destroy() {
        ObjectiveController.instance = null;
        EventHub.instance.RemoveListener(this);
    }

    private ObjectiveController() {
        objectives.Add(new Objective(RoomEvent.leftRoom(RoomType.Bedroom), "Re opp sengen"));
        objectives.Add(new Objective(RoomEvent.leftRoom(RoomType.Bathroom), "Puss tennene"));
        objectives[0].Activate();
    }

    public void ObjectiveCompleted(Objective objective) {
        if (objective == currentObjective) {
            currentObjectiveIndex++;
        }
    }

    public void HandleEvent(Event e) {
        if (currentObjectiveIndex >= objectives.Count) {
            return;
        }

        currentObjective.HandleEvent(e);
    }
}