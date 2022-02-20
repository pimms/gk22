using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TV: MonoBehaviour, EventListener {
    public Image canvas;
    private bool hasStarted = false;

    void Start() {
        EventHub.instance.AddListener(this);
    }

    public void HandleEvent(Event e) {
        if (hasStarted) return;
        ItemEvent itemEvent = e as ItemEvent;
        if (itemEvent == null) return;

        if (itemEvent.item == ItemType.PS5Controller && itemEvent.spot == SpotType.Hand) {
            hasStarted = true;
            StartCoroutine(PlayIntro());
        }
    }

    private IEnumerator PlayIntro() {
        int imageCount = 20;

        yield return new WaitForSeconds(3f);

        for (int imageIndex = 1; imageIndex <= imageCount; imageIndex++) {
            Debug.Log("displaying image " + imageIndex + " of " + imageCount);
            string imageName = "DS/dsintro" + imageIndex;
            Sprite image = Resources.Load<Sprite>(imageName);
            canvas.sprite = image;
            canvas.color = Color.white;
            yield return new WaitForSeconds(5f);
        }

        yield return null;
    }
}
