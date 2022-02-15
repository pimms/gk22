using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SpotType {
    None,
    Hand,
    Toilet,
    Head,
    Legs,
    Torso,
    ToiletHeadPosition,
    CDPlayer,
    SofaHeadPosition,
}

[RequireComponent(typeof(Collider))]
class PlaceableSpot: MonoBehaviour {
    public SpotType type;

    void OnTriggerEnter(Collider other) {
        PlaceableItem item = other.GetComponent<PlaceableItem>();
        if (item != null) {
            EventHub.instance.Raise(ItemEvent.placedItem(item.type, type));
        }
    }

    void OnTriggerExit(Collider other) {
        PlaceableItem item = other.GetComponent<PlaceableItem>();
        if (item != null) {
            EventHub.instance.Raise(ItemEvent.removedItem(item.type, type));
        }
    }
}