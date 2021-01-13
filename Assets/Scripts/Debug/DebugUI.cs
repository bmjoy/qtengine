using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DebugUI : BaseBehaviour {

    public GameObject debugUI;
    public Text objectsText;

    void Awake() {
        DontDestroyOnLoad(gameObject);

        onStart += handleStart;
        onUpdate += handleUpdate;
    }

    public void handleStart() {
        onKeyDown += handleKeyDown;
    }

    public void handleUpdate() {
        updateClientUI();
    }

    public void updateClientUI() {
        if (ClientManager.instance.state != BaseNetworking.componentState.RUNNING) { return; }

        string objects = "";
        foreach (BaseQTObject obj in ClientManager.instance.spawnManager.spawnedObjects.Values) {
            objects += obj.objectID;
        }

        objectsText.text = "Objects: \n" + objects;
    }

    void handleKeyDown(KeyCode key) {
        if(key == KeyCode.F2) {
            debugUI.SetActive(!debugUI.activeSelf);
        }
    }
}
