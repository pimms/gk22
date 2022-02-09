using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XZLocker: MonoBehaviour {
    public Transform lockTo;

    void Update() {
        Vector3 position = transform.position;
        position.x = lockTo.position.x;
        position.z = lockTo.position.z;
        transform.position = position;
    }
}
