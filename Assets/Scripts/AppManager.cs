using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AppManager : BaseBehaviour {

    public static AppManager instance { get; protected set; }
    public NetworkStorage networkStorage;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void closeApp() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
              Application.Quit();
        #endif
    }
}
