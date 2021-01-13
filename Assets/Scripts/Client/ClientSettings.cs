using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientSettings : MonoBehaviour {

    public static ClientSettings instance { get; protected set; }

    public string clientScene;

    void Awake() {
        DontDestroyOnLoad(this);
        instance = this;
    }

}
