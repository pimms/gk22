using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Objective {
    private Event expectedEvent;
    private String description;
    private bool completed = false;

    public Objective(Event expectedEvent, String description) {
        this.expectedEvent = expectedEvent;
        this.description = description;
    }

    public String GetDescription() {
        return description;
    }

    public void Activate() {
        Debug.Log("Activating objective: " + description);
    }

    public void HandleEvent(Event e) {
        if (!completed && expectedEvent.Equals(e)) {
            completed = true;
            Debug.Log("Objective completed: " + description);
            ObjectiveController.instance.StartCoroutine(Finalize());
        }
    }

    private IEnumerator Finalize() {
        AudioSource player = Player.instance.GetComponent<AudioSource>();
        yield return new WaitWhile(() => player.isPlaying);
        AudioClip audio = Resources.Load<AudioClip>("Sounds/comp_reoppsengen");
        if (audio != null) {
            player.PlayOneShot(audio);
            yield return new WaitForSeconds(audio.length);
        }

        ObjectiveController.instance.ObjectiveCompleted(this);
    }
}