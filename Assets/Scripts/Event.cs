#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType {
    PlayerEnteredRoom,
    PlayerLeftRoom,
    ObjectiveCompleted,
    ObjectiveChanged,

    ItemPlacedInSpot,
    ItemRemovedFromSpot,
}

public enum RoomType {
    Bedroom,
    LivingRoom,
    Office,
    Closet,
    Bathroom
}

public class Event {
    public EventType type;

    public Event(EventType type) {
        this.type = type;
    }
}

public class ObjectiveEvent: Event {
    public string objectiveId;

    public static ObjectiveEvent changed(string objectiveId) {
        return new ObjectiveEvent(EventType.ObjectiveChanged, objectiveId);
    }

    public static ObjectiveEvent completed(string objectiveId) {
        return new ObjectiveEvent(EventType.ObjectiveCompleted, objectiveId);
    }

    private ObjectiveEvent(EventType type, string objectiveId) : base(type) {
        this.objectiveId = objectiveId;
    }
}

public class RoomEvent: Event {
    public RoomType room;

    public static RoomEvent enteredRoom(RoomType room) {
        return new RoomEvent(EventType.PlayerEnteredRoom, room);
    }

    public static RoomEvent leftRoom(RoomType room) {
        return new RoomEvent(EventType.PlayerLeftRoom, room);
    }

    private RoomEvent(EventType type, RoomType room) : base(type) {
        this.room = room;
    }

    public override bool Equals(object? obj) {
        if (obj == null) {
            return false;
        }

        #pragma warning disable CS8600
        RoomEvent other = obj as RoomEvent;
        if (other == null) {
            return false;
        }

        return this.type == other.type && this.room == other.room; 
    }

    public override int GetHashCode() {
        return type.GetHashCode() ^ room.GetHashCode();
    }
}

public class ItemEvent: Event {
    public ItemType item;
    public SpotType spot;

    public static ItemEvent placedItem(ItemType item, SpotType spot) {
        return new ItemEvent(EventType.ItemPlacedInSpot, item, spot);
    }

    public static ItemEvent removedItem(ItemType item, SpotType spot) {
        return new ItemEvent(EventType.ItemRemovedFromSpot, item, spot);
    }

    private ItemEvent(EventType type, ItemType item, SpotType spot) : base(type) {
        this.item = item;
        this.spot = spot;
    }

    public override bool Equals(object? obj) {
        if (obj == null) {
            return false;
        }

        ItemEvent other = obj as ItemEvent;
        if (other == null) {
            return false;
        }

        return this.type == other.type && this.item == other.item && this.spot == other.spot; 
    }

    public override int GetHashCode() {
        return type.GetHashCode() ^ item.GetHashCode() ^ spot.GetHashCode();
    }
}
