using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomListener : MonoBehaviour
{
    new public BoxCollider collider;
    public RoomType roomType;

    private bool isPlayerInRoom = false;

    void Start()
    {
        if (collider == null) {
            collider = GetComponent<BoxCollider>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Get the current game objects name
        string name = gameObject.name;

        bool isInRoomNow = (collider.bounds.Contains(Player.instance.transform.position));

        if (isInRoomNow != isPlayerInRoom) {
            isPlayerInRoom = isInRoomNow;
            if (isInRoomNow) {
                EventHub.instance.Raise(RoomEvent.enteredRoom(roomType));
            } else {
                EventHub.instance.Raise(RoomEvent.leftRoom(roomType));
            }
        }
    }
}
