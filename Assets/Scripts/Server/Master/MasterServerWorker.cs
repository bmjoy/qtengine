using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MasterServerWorker {

    public Process process;
    public RoomInfo room;
    
    public MasterServerWorker(RoomInfo _room) {
        room = _room;
    }

    public void start() {
        process = new Process();
        process.StartInfo.FileName = "C:\\Users\\LamkasDev\\Downloads\\qtEngine-builds\\qtEngine.exe";
        process.StartInfo.Arguments = "-port " + room.port;
        process.StartInfo.WindowStyle = ProcessWindowStyle.Normal;
        process.Start();
    }

    public void stop() {
        process.Kill();
    }
}
