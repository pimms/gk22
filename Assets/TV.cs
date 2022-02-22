using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(AudioSource))]
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
        int imageCount = 21;

        yield return new WaitForSeconds(3f);

        float[] delays = new float[] {
            0f, // 0: Not included
            4f, // 1: Sony
            4f, // 2: Japan Studios
            4f, // 3: FROM
            4f, // 4: Havok
            8f, // 5: "On the first day"
            4.5f, // 6: "Man was granted..."
            4f, // 7: "On the second day"
            3f, // 8: "Upon earth was placed..."
            5f, // 9: "A soul devouring demon"
            7f, // 10: Old one with undead
            3f, // 11: "Demon's Souls"

            3f, // 12: Fight 1
            3f, // 13: Fight 2
            3f, // 14: Explosion
            3f, // 15: Skeleton
            3f, // 16: Fight 1
            3f, // 17: Fight 2
            3f, // 18: Fight 3
            3f, // 19: Dragon God
            3f, // 20: Dragon God 2
            3f, // 21: Complete
        };

        for (int imageIndex = 1; imageIndex <= imageCount; imageIndex++) {
            if (imageIndex == 5) {
                GetComponent<AudioSource>().Play();
            }

            Debug.Log("displaying image " + imageIndex + " of " + imageCount);
            string imageName = "DS/dsintro" + imageIndex;
            Sprite image = Resources.Load<Sprite>(imageName);
            canvas.sprite = image;
            canvas.color = Color.white;
            yield return new WaitForSeconds(delays[imageIndex]);
        }

        EventHub.instance.Raise(new Event(EventType.DSIntroCompleted));
        yield return null;
    }
}
