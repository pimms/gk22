using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poop: MonoBehaviour, EventListener {
    void Start() {
        EventHub.instance.AddListener(this);
    }

    public void HandleEvent(Event e) {
        ObjectiveEvent objectiveEvent = e as ObjectiveEvent;
        if (objectiveEvent == null || objectiveEvent.objectiveId == null) {
            return;
        }

        if (objectiveEvent.objectiveId == "toilet") {
            for (int i=0; i<transform.childCount; i++) {
                GameObject child = transform.GetChild(i).gameObject;
                child.SetActive(true);
            }
        }
    }
}
