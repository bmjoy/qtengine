using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Diagnostics;

public class MasterServerWorkersManager {
    
    public Dictionary<string, MasterServerWorker> workers;
    public int lastWorkerPort = 8113;

    public MasterServerWorkersManager() {
        workers = new Dictionary<string, MasterServerWorker>();
    }

    public MasterServerWorker spawnWorker(string id) {
        RoomInfo room = new RoomInfo();
        room.id = id;
        room.port = lastWorkerPort;

        MasterServerWorker worker = new MasterServerWorker(room);
        worker.start();
        workers.Add(worker.room.id, worker);

        lastWorkerPort++;

        return worker;
    }

    public Dictionary<string, RoomInfo> getRooms() {
        Dictionary<string, RoomInfo> rooms = new Dictionary<string, RoomInfo>();
        foreach(MasterServerWorker worker in workers.Values) {
            rooms.Add(worker.room.id, worker.room);
        }

        return rooms;
    }
}
