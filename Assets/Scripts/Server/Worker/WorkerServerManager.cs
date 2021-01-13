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

public class WorkerServerManager : BaseServerManager {

    public static WorkerServerManager instance { get; protected set; }

    public WorkerServerPlayerManager playerManager;
    public WorkerServerSpawnManager spawnManager;

    void Awake() {
        onStart += handleStart;
        onStart += handleWorkerStart;
        onUpdate += handleUpdate;
        onApplicationQuit += handleApplicationQuit;
        onApplicationQuit += handleWorkerApplicationQuit;

        instance = this;
    }

    public void handleWorkerStart() {
        connections = new WorkerServerConnectionsManager(this);
        spawnManager = new WorkerServerSpawnManager();
        playerManager = new WorkerServerPlayerManager();

        onClientDisconnected += playerManager.despawnPlayer;
        onClientDisconnected += connections.handleClientDisconnect;
        onClientConnected += connections.handleClientConnect;

        WorkerHelper.instance.checkStart();
    }

    public void handleWorkerApplicationQuit() {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Closing worker server...");
    }

    public override void setupServer(int port) {
        setupTCPServer(port);

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Started worker server on port " + port + "...");
        state = componentState.RUNNING;

        SceneManager.LoadScene(ServerSettings.instance.serverScene, LoadSceneMode.Single);
    }
}
