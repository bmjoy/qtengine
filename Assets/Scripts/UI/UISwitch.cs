using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UISwitch : MonoBehaviour {

    public List<GameObject> switchObjects;
    public List<GameObject> disableObjects;
    public List<GameObject> enableObjects;

    void Start() {
        if (switchObjects == null) { switchObjects = new List<GameObject>(); }
        if (disableObjects == null) { disableObjects = new List<GameObject>(); }
        if (enableObjects == null) { enableObjects = new List<GameObject>(); }
    }

    public void process() {
        foreach(GameObject obj in switchObjects) {
            obj.SetActive(!obj.activeSelf);
        }

        foreach (GameObject obj in disableObjects) {
            obj.SetActive(false);
        }

        foreach (GameObject obj in enableObjects) {
            obj.SetActive(true);
        }
    }
}
