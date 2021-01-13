using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AppSettings : MonoBehaviour {

    public static AppSettings instance { get; protected set; }
    public string menuScene;
    public string serverIP;

    void Awake() {
        DontDestroyOnLoad(this);
        instance = this;
    }

}
