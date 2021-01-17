using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AppManager : BaseBehaviour {

    public static AppManager instance { get; protected set; }
    public NetworkStorage networkStorage;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        instance = this;

        onUpdate += handleUpdate;
        SteamVR.Initialize();
    }

    public void handleUpdate() {
        
    }

    public void closeApp() {
        #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
        #else
              Application.Quit();
        #endif
    }
}
