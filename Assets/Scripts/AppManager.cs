using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class AppManager : BaseBehaviour {

    public static AppManager instance { get; protected set; }
    public NetworkStorage networkStorage;

    public Camera mainCamera;
    public GameObject mainMenu;
    public GameObject loadingMenu;
    public GameObject masterServerMenu;
    public GameObject loginMenu;
    public GameObject registerMenu;

    void Awake() {
        DontDestroyOnLoad(gameObject);
        DontDestroyOnLoad(mainMenu);
        DontDestroyOnLoad(loadingMenu);
        DontDestroyOnLoad(masterServerMenu);
        DontDestroyOnLoad(loginMenu);
        DontDestroyOnLoad(registerMenu);
        instance = this;

        onUpdate += handleUpdate;
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

    public void disableAllMenus() {
        mainMenu.SetActive(false);
        loadingMenu.SetActive(false);
        masterServerMenu.SetActive(false);
        loginMenu.SetActive(false);
        registerMenu.SetActive(false);
    }
}
