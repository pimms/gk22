using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCollder: MonoBehaviour
{
    // Use this for initialization
    void Start () {
        foreach (Transform childObject in transform) {
            if (childObject.name.Contains("NOPHYSICS")) {
                continue;
            }

            Mesh mesh = childObject.gameObject.GetComponent<MeshFilter>().mesh;
            if (mesh != null) {
                if (childObject.name.Contains("MOVABLE")) {
                    childObject.gameObject.AddComponent<BoxCollider>();
                    childObject.gameObject.AddComponent<Rigidbody>();
                    childObject.gameObject.AddComponent<OVRGrabbable>();
                } else {
                    MeshCollider meshCollider = childObject.gameObject.AddComponent<MeshCollider>();
                    meshCollider.sharedMesh = mesh;

                    Rigidbody rigidbody = childObject.gameObject.AddComponent<Rigidbody>();
                    rigidbody.isKinematic = true;
                    rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
                }
            }
        }
    }
}
