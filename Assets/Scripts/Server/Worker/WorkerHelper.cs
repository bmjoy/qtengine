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
        if(args.ContainsKey("-port") && args.ContainsKey("-roomID")) {
            WorkerServerManager.instance.room = new RoomInfo();
            WorkerServerManager.instance.room.id = args["-roomID"];

            WorkerServerManager.instance.setupServer(int.Parse(args["-port"]));
        }
    }
}
