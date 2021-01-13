using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ServerSettings : MonoBehaviour {

    public static ServerSettings instance { get; protected set; }

    public float syncRate = 0.05f;
    public int heartbeatTimeout = 5000;
    public int heartbeatRate = 1000;
    public string serverScene;

    void Awake() {
        DontDestroyOnLoad(this);
        instance = this;
    }

}
