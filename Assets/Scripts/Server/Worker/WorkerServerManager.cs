using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.XR;

public class WorkerServerManager : BaseServerManager {

    public static WorkerServerManager instance { get; protected set; }

    public RoomInfo room;
    public WorkerServerSpawnManager spawnManager;

    public Action<int> onServerStart;
    public Action onServerSceneLoad;
    public Action<WorkerServerQTClient> onClientReady;

    void Awake() {
        onStart += handleStart;
        onStart += handleWorkerStart;
        onUpdate += handleUpdate;
        onApplicationExit += handleApplicationQuit;
        onApplicationExit += handleWorkerApplicationQuit;
        onServerStart += handleServerStart;
        onServerSceneLoad += handleSceneLoaded;

        SceneManager.sceneLoaded += onSceneLoaded;

        instance = this;
    }

    public void handleWorkerStart() {
        connections = new WorkerServerConnectionsManager(this);
        spawnManager = new WorkerServerSpawnManager();

        WorkerHelper.instance.checkStart();
    }

    public void handleWorkerApplicationQuit() {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Closing worker server...");
    }

    public override void setupServer(int port) {
        XRSettings.enabled = false;

        setupTCPServer(port);
        state = componentState.RUNNING;

        SceneManager.LoadScene(ServerSettings.instance.serverScene, LoadSceneMode.Single);
        onServerStart(port);
    }

    public void onSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (state == componentState.RUNNING && scene.name == ServerSettings.instance.serverScene) {
            onServerSceneLoad();

            WorkerServerConnectionsManager workerConnection = (WorkerServerConnectionsManager)connections;
            WorkerReadyMessage readyMessage = new WorkerReadyMessage();
            readyMessage.id = room.id;
            workerConnection.masterClient.sendMessage(readyMessage);

            QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Sending worker ready message...");
        }
    }

    public void handleServerStart(int port) {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Started worker server on port " + port + "...");
    }

    public void handleSceneLoaded() {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Loaded the server's scene...");
    }

    public void sendMessageToAllReady(QTMessage message) {
        foreach (ServerQTClient qtRemoteClient in connections.clients.ToList()) {
            WorkerServerQTClient workerClient = (WorkerServerQTClient)qtRemoteClient;
            if (workerClient.ready) {
                qtRemoteClient.sendMessage(message);
            }
        }
    }
}
