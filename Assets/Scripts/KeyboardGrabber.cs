using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardGrabber : MonoBehaviour
{
    private enum State {
        Idle,
        Grabbing,
    }

    private GameObject grabbedObject;
    private float relativeDistance;
    private Quaternion initialRotation;
    private State state;

    void Start()
    {
        
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.E)) {
            if (state == State.Idle) {
                state = State.Grabbing;
                grabbedObject = GetTargetedObject();
                if (grabbedObject != null) {
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = true;
                    relativeDistance = Vector3.Distance(transform.position, grabbedObject.transform.position);
                    initialRotation = Camera.main.transform.rotation;
                }
            }
        } else {
            if (state == State.Grabbing) {
                state = State.Idle;
                if (grabbedObject != null) {
                    grabbedObject.GetComponent<Rigidbody>().isKinematic = false;
                    grabbedObject = null;
                }
            }
        }

        if (state == State.Grabbing && grabbedObject != null) {
            Vector3 newPosition = Camera.main.transform.rotation * new Vector3(0, 0, relativeDistance) + Camera.main.transform.position;
            grabbedObject.transform.position = newPosition;
        }
    }

    GameObject GetTargetedObject() {
        RaycastHit hit;

        Transform camera = Camera.main.transform;
        if (Physics.Raycast(camera.position, camera.forward, out hit)) {
            GameObject gameObject = hit.collider.gameObject;
            if (gameObject.GetComponent<OVRGrabbable>() != null) {
                return gameObject;
            } else {
                Debug.Log("Object not grabbable: " + gameObject.name);
            }
        }
        return null;
    }
}
