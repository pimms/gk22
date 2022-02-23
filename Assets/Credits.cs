using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Credits : MonoBehaviour, EventListener {
    void Start() {
        EventHub.instance.AddListener(this);
    }

    public void HandleEvent(Event e) {
        ObjectiveEvent objectiveEvent = e as ObjectiveEvent;
        if (objectiveEvent == null) {
            return;
        }

        if (objectiveEvent.type == EventType.ObjectiveCompleted && objectiveEvent.objectiveId == "gym") {
            for (int i=0; i<transform.childCount; i++) {
                GameObject child = transform.GetChild(i).gameObject;
                child.SetActive(true);
            }
        }
    }
}
