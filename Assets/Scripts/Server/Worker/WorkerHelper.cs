using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Runtime.InteropServices;

public class WorkerHelper : MonoBehaviour {

    public static WorkerHelper instance { get; protected set; }


    void Awake() {
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    public void checkStart() {
        Dictionary<string, string> args = QTUtils.getCommandLineArgs();
        if(args.ContainsKey("-port")) {
            WorkerServerManager.instance.setupServer(int.Parse(args["-port"]));
        }
    }
}
