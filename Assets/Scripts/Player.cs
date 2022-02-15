using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour, EventListener
{
    private struct EventSound {
        public ItemType item;
        public SpotType spot;
        public string sound;
        public bool onceOnly;

        public EventSound(ItemType item, SpotType spot, string sound, bool onceOnly = true) {
            this.item = item;
            this.spot = spot;
            this.sound = sound;
            this.onceOnly = onceOnly;
        }
    }

    public static Player instance;

    private AudioSource audioSource;
    private List<EventSound> eventSounds = new List<EventSound>();

    void Start() {
        Player.instance = this;
        EventHub.instance.AddListener(this);

        audioSource = GetComponent<AudioSource>();

        eventSounds.Add(new EventSound(ItemType.Fedora, SpotType.Hand, "pickup_fedora", true));
        eventSounds.Add(new EventSound(ItemType.Cereal, SpotType.Hand, "pickup_cereal", true));
        eventSounds.Add(new EventSound(ItemType.CD, SpotType.Hand, "pickup_cd", true));
        eventSounds.Add(new EventSound(ItemType.ToiletBrush, SpotType.Hand, "pickup_toiletbrush", true));

        eventSounds.Add(new EventSound(ItemType.Beer, SpotType.Head, "destroy_beer", false));
        eventSounds.Add(new EventSound(ItemType.Cheese, SpotType.Head, "destroy_cheese", false));
        eventSounds.Add(new EventSound(ItemType.Bread, SpotType.Head, "destroy_bread", false));
        eventSounds.Add(new EventSound(ItemType.Banana, SpotType.Head, "destroy_banana", false));
        eventSounds.Add(new EventSound(ItemType.Chocolate, SpotType.Head, "destroy_chocolate", false));

        eventSounds.Add(new EventSound(ItemType.CD, SpotType.CDPlayer, "destroy_cd", true));
    }

    public void PlaySound(AudioClip clip) {
        audioSource.PlayOneShot(clip);
    }

    public void HandleEvent(Event e) {
        PlayEventSound(e);
    }

    private void PlayEventSound(Event anyEvent) {
        ItemEvent e = anyEvent as ItemEvent;
        if (e == null) {
            return;
        }

        if (audioSource.isPlaying) {
            return;
        }

        for (int i = 0; i < eventSounds.Count; i++) {
            EventSound sound = eventSounds[i];
            if (e.item == sound.item && e.spot == sound.spot) {
                AudioClip clip = Resources.Load<AudioClip>("Sounds/" + sound.sound);
                if (clip != null) {
                    PlaySound(clip);
                }
                if (sound.onceOnly) {
                    eventSounds.RemoveAt(i);
                }
                break;
            }
        }
    }
}
