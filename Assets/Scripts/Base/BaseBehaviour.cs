using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseBehaviour : MonoBehaviour {

    public Action<KeyCode> onKeyDown;
    public Action<KeyCode> onKeyUp;

    public Action onStart;
    public Action onUpdate;
    public Action onApplicationQuit;

    void Start() {
        onStart += debugStart;
        onUpdate += debugUpdate;
        onKeyDown += debugKey;
        onKeyUp += debugKey;
        onApplicationQuit += debugQuit;

        onStart();
    }

    void Update() {
        foreach (KeyCode key in QTUtils.GetEnumList<KeyCode>()) {
            if (Input.GetKeyDown(key)) {
                onKeyDown(key);
            } else if (Input.GetKeyUp(key)) {
                onKeyUp(key);
            }
        }

        onUpdate();
    }

    void OnApplicationQuit() {
        OnApplicationQuit();
    }

    public void debugStart() {

    }

    public void debugUpdate() {

    }

    public void debugQuit() {

    }

    public void debugKey(KeyCode key) {

    }
}
