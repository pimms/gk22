using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CDPlayer: MonoBehaviour, EventListener {

    private AudioSource audioSource;

    void Start() {
        audioSource = GetComponent<AudioSource>();
        EventHub.instance.AddListener(this);
    }

    public void HandleEvent(Event e) {
        ItemEvent itemEvent = e as ItemEvent;
        if (itemEvent == null) return;

        if (itemEvent.item == ItemType.CD && itemEvent.spot == SpotType.CDPlayer) {
            AudioClip clip = Resources.Load<AudioClip>("Sounds/ikkesann");
            audioSource.PlayOneShot(clip);
        }
    }
}
