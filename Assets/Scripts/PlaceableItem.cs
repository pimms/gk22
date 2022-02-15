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
}

class PlaceableItem: MonoBehaviour {
    public ItemType type;
    public SpotType destroyWhenPlacedIn;


    void OnTriggerEnter(Collider other) {
        PlaceableSpot spot = other.GetComponent<PlaceableSpot>();
        if (spot != null && spot.type != SpotType.None && spot.type == destroyWhenPlacedIn) {
            Destroy(gameObject);
        }
    }
}