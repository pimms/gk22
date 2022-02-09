using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyListeners : MonoBehaviour {
    public Transform follow;

    private Vector3 relativePosition;

    void Start() {
        relativePosition = transform.position - follow.position;
    }

    void Update() {
        
    }
}
