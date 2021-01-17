using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using Valve.VR;

public class WorkerServerQTClient : ServerQTClient {

    public ServerInputHandler inputHandler;
    public ServerReadyHandler readyHandler;
    public ServerRequestSyncHandler requestSyncHandler;
    public bool ready = false;

    public Dictionary<KeyCode, bool> syncedKeys;
    public Dictionary<string, float> syncedAxis;
    public Dictionary<string, object> syncedVRActions;

    public Dictionary<SteamVR_Input_Sources, bool> syncedVRActive;
    public Dictionary<SteamVR_Input_Sources, Vector3> syncedVRPositions;
    public Dictionary<SteamVR_Input_Sources, Vector3> syncedVRRotations;

    public WorkerServerQTClient(BaseServerManager _manager, TcpClient _client) : base(_manager, _client, clientType.WORKER_SERVER) {
        inputHandler = new ServerInputHandler(this);
        readyHandler = new ServerReadyHandler(this);
        requestSyncHandler = new ServerRequestSyncHandler(this);

        syncedKeys = new Dictionary<KeyCode, bool>();
        syncedAxis = new Dictionary<string, float>();
        syncedVRActions = new Dictionary<string, object>();

        syncedVRActive = new Dictionary<SteamVR_Input_Sources, bool>();
        syncedVRPositions = new Dictionary<SteamVR_Input_Sources, Vector3>();
        syncedVRRotations = new Dictionary<SteamVR_Input_Sources, Vector3>();
    }
}
