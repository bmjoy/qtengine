using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MasterServerWorker {

    public Process process;
    public RoomInfo room;

    public Action onWorkerReady;
    
    public MasterServerWorker(RoomInfo _room) {
        room = _room;

        onWorkerReady += debugReadyWorker;
    }

    public void start() {
        process = new Process();
        process.StartInfo.WorkingDirectory = "C:\\Users\\LamkasDev\\Desktop\\UnityProjects\\qtEngine-builds";
        process.StartInfo.FileName = "qtEngine.exe";
        process.StartInfo.Arguments = "-logFile C:\\Users\\LamkasDev\\Downloads\\qtEngine-logs\\worker_" + room.port + ".log -roomID " + room.id + " -port " + room.port;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        process.Start();
    }

    public void stop() {
        process.Kill();
    }

    public void debugReadyWorker() {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Worker(" + room.id + ") is ready...");
    }
}
