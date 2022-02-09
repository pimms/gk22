using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Objective {
    private Event expectedEvent;
    private String description;
    private String id;
    private bool completed = false;

    private String activationAudio { get { return "Sounds/activate_" + id; } }
    private String completionAudio { get { return "Sounds/comp_" + id; } }

    public Objective(Event expectedEvent, String description, String id) {
        this.expectedEvent = expectedEvent;
        this.description = description;
        this.id = id;
    }

    public String GetDescription() {
        return description;
    }

    public void Activate() {
        Debug.Log("Activating objective: " + description);
        ObjectiveController.instance.StartCoroutine(PlayAudio(activationAudio));
    }

    public void HandleEvent(Event e) {
        if (!completed && expectedEvent.Equals(e)) {
            completed = true;
            Debug.Log("Objective completed: " + description);
            ObjectiveController.instance.StartCoroutine(Finalize());
        }
    }

    private IEnumerator Finalize() {
        EventHub.instance.Raise(new Event(EventType.ObjectiveCompleted));
        yield return PlayAudio(completionAudio, 5);
        ObjectiveController.instance.ObjectiveFinalized(this);
    }

    private IEnumerator PlayAudio(String audio, float minWaitTime = -1) {
        AudioSource player = Player.instance.GetComponent<AudioSource>();
        yield return new WaitWhile(() => player.isPlaying);
        AudioClip clip = Resources.Load<AudioClip>(audio);
        if (clip != null) {
            player.PlayOneShot(clip);

            float waitTime = clip.length;
            if (minWaitTime > clip.length) {
                waitTime = minWaitTime;
            }
            yield return new WaitForSeconds(waitTime);
        }
    }
}