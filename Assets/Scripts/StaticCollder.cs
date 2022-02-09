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
                MeshCollider meshCollider = childObject.gameObject.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = mesh;

                if (childObject.name.Contains("MOVABLE")) {
                    childObject.gameObject.AddComponent<BoxCollider>();
                    childObject.gameObject.AddComponent<Rigidbody>();
                    childObject.gameObject.AddComponent<OVRGrabbable>();
                }
            }
        }
    }
}