using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class Objective {
    public float activationDelay;

    private Event expectedEvent;
    private String description;
    private String id;
    private bool completed = false;

    private string preactivationAudio { get { return "Sounds/preactivate_" + id; } }
    private String activationAudio { get { return "Sounds/activate_" + id; } }
    private String completionAudio { get { return "Sounds/comp_" + id; } }

    public Objective(Event expectedEvent, String description, String id, float activationDelay = 3) {
        this.expectedEvent = expectedEvent;
        this.description = description;
        this.activationDelay = activationDelay;
        
        if (id != null) {
            this.id = id;
        } else {
            this.id = "";
        }
    }

    public String GetDescription() {
        return description;
    }

    public IEnumerator Activate() {
        yield return PlayAudio(preactivationAudio);
        EventHub.instance.Raise(ObjectiveEvent.changed(id));
        yield return PlayAudio(activationAudio);
    }

    public void HandleEvent(Event e) {
        if (!completed && expectedEvent.Equals(e)) {
            completed = true;
            ObjectiveController.instance.StartCoroutine(Finalize());
        }
    }

    private IEnumerator Finalize() {
        EventHub.instance.Raise(ObjectiveEvent.completed(id));
        yield return PlayAudio(completionAudio, 3);
        ObjectiveController.instance.ObjectiveFinalized(this);
    }

    private IEnumerator PlayAudio(String audio, float minWaitTime = 0) {
        AudioSource player = Player.instance.GetComponent<AudioSource>();
        yield return new WaitWhile(() => player.isPlaying);
        AudioClip clip = Resources.Load<AudioClip>(audio);
        
        float waitTime = minWaitTime;

        if (clip != null) {
            player.PlayOneShot(clip);
            if (clip.length > waitTime) {
                waitTime = clip.length;
            }
        }

        if (waitTime > 0) {
            yield return new WaitForSeconds(waitTime);
        }
    }
}