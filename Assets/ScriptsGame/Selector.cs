using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selector : MonoBehaviour {

    public GameObject gura;
    public Animator guraAnimator;
    public AudioSource guraAudio;

    public GameObject amelia;
    public Animator ameliaAnimator;
    public AudioSource ameliaAudio;

    public Text characterName;
    public Text characterDescription;
    public Slider timer;

    [HideInInspector]
    public float elapsedTimer;

    void Start() {
        selectGura();
    }

    void Update() {
        elapsedTimer += Time.deltaTime;
        timer.value = elapsedTimer / 30;
    }

    public void selectGura() {
        deactiveAll();
        gura.SetActive(true);
        characterName.text = "Gawr Gura";
        characterDescription.text = "Cute Predator";

        guraAnimator.Play("gura_select", 0);
        Invoke("selectAudioPlayGura", 0.3f);
    }

    public void selectAmelia() {
        deactiveAll();
        amelia.SetActive(true);
        characterName.text = "Amelia Watson";
        characterDescription.text = "Toxic Detective";

        ameliaAnimator.Play("amelia_select", 0);
        Invoke("selectAudioPlayAmelia", 0.3f);
    }

    public void selectAudioPlayGura() {
        guraAudio.Play();
    }

    public void selectAudioPlayAmelia() {
        ameliaAudio.Play();
    }

    public void deactiveAll() {
        gura.SetActive(false);
        amelia.SetActive(false);
    }
}
