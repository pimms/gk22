using Oculus;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Grabbable: OVRGrabbable {
    public void KeyboardGrabBegin() {
        SendGrabBeginNotification();
    }

    public void KeyboardGrabEnd() {
        SendGrabEndNotification();
    }

	override public void GrabBegin(OVRGrabber hand, Collider grabPoint) {
        base.GrabBegin(hand, grabPoint);
        SendGrabBeginNotification();
    }

	override public void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity) {
        base.GrabEnd(linearVelocity, angularVelocity);
    }

    private void SendGrabBeginNotification() {
        PlaceableItem item = GetComponent<PlaceableItem>();
        if (item != null) {
            EventHub.instance.Raise(ItemEvent.placedItem(item.type, SpotType.Hand));
        }
    }

    private void SendGrabEndNotification() {
        PlaceableItem item = GetComponent<PlaceableItem>();
        if (item != null) {
            EventHub.instance.Raise(ItemEvent.placedItem(item.type, SpotType.None));
        }
    }
}