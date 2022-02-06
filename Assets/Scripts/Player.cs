using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class Player : MonoBehaviour
{
    public static Player instance;

    void Start() {
        Player.instance = this;
    }

    public void PlaySound(AudioClip clip) {
        GetComponent<AudioSource>().PlayOneShot(clip);
    }
}
