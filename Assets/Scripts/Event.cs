#nullable enable

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EventType {
    PlayerEnteredRoom,
    PlayerLeftRoom,
    ObjectiveCompleted,
    ObjectiveChanged,
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
