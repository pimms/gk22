using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ItemType {
    Toothbrush,
    Fedora,
    Butt,
    Jeans,
    Shirt,
    Head,
    Beer,
    Cheese,
    Bread,
    Cereal,
    Milk,
    CD,
    ToiletBrush,
    Banana,
    Chocolate,
    PS5Controller,
    Cake,
    Cup,
    BirthdayCard,
}

class PlaceableItem: MonoBehaviour, EventListener {
    public ItemType type;
    public SpotType destroyWhenPlacedIn;
    public string requiredForObjective;
    
    private bool canDestroy = true;

    void Start() {
        if (string.IsNullOrEmpty(requiredForObjective)) {
            canDestroy = true;
        } else {
            canDestroy = false;
            EventHub.instance.AddListener(this);
        }
    }

    void OnTriggerEnter(Collider other) {
        PlaceableSpot spot = other.GetComponent<PlaceableSpot>();
        if (spot != null && spot.type != SpotType.None && spot.type == destroyWhenPlacedIn && canDestroy) {
            Destroy(gameObject);
        }
    }

    public void HandleEvent(Event e) {
        ObjectiveEvent objectiveEvent = e as ObjectiveEvent;
        if (objectiveEvent != null && objectiveEvent.type == EventType.ObjectiveChanged && objectiveEvent.objectiveId == requiredForObjective) {
            canDestroy = true;
        }
    }
}