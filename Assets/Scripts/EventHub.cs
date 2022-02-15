using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface EventListener {
    void HandleEvent(Event e);
}

class EventHub {
    public static EventHub instance = new EventHub();

    private List<EventListener> listeners = new List<EventListener>();

    public void AddListener(EventListener listener) {
        listeners.Add(listener);
    }

    public void RemoveListener(EventListener listener) {
        listeners.Remove(listener);
    }

    public void Raise(Event e) {
        for (int i = 0; i < listeners.Count; i++) {
            listeners[i].HandleEvent(e);
        }
    }
}