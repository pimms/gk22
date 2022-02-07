using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

class HUDController: MonoBehaviour, EventListener {

    public TMP_Text objectiveLabel;
    public Image checkbox;

    void Start() {
        EventHub.instance.AddListener(this);
        objectiveLabel.text = "";
        checkbox.enabled = false;
    }

    void Destroy() {
        EventHub.instance.RemoveListener(this);
    }

    public void HandleEvent(Event e) {
        if (e.type == EventType.ObjectiveCompleted) {
            Debug.Log("----------- COMPLETED ----------- ");
            OnObjectiveCompleted();
        } else if (e.type == EventType.ObjectiveChanged) {
            Debug.Log("----------- CHANGED ----------- ");
            OnObjectiveChanged();
        }
    }

    private void OnObjectiveCompleted() {
        checkbox.enabled = true;
        checkbox.transform.localScale = new Vector3(1, 1, 1);
        StartCoroutine(AnimateCheckboxIn());
    }

    private IEnumerator AnimateCheckboxIn() {
        checkbox.enabled = true;

        Color color = Color.green;
        color.a = 0;
        float time = 0;
        while (time < 1) {
            time += Time.deltaTime;
            color.a = Mathf.Clamp(time, 0, 1);
            checkbox.color = color;
            yield return null;
        }
    }

    private void OnObjectiveChanged() {
        checkbox.enabled = false;
        Objective current = ObjectiveController.instance.currentObjective;
        if (current == null) {
            objectiveLabel.text = "";
        } else {
            objectiveLabel.text = current.GetDescription();
        }
    }
}