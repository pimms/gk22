using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticCollder: MonoBehaviour
{
    // Use this for initialization
    void Start () {
        foreach (Transform childObject in transform) {
            if (childObject.name.Contains("NOPHYSICS") || childObject.name.Contains("MOVABLE")) {
                continue;
            }

            Mesh mesh = childObject.gameObject.GetComponent<MeshFilter>().mesh;
            if (mesh != null) {
                MeshCollider meshCollider = childObject.gameObject.AddComponent<MeshCollider>();
                meshCollider.sharedMesh = mesh;
            }
        }
    }
}
