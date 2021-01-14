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
        setupTCPServer(port);
        state = componentState.RUNNING;

        SceneManager.LoadScene(ServerSettings.instance.serverScene, LoadSceneMode.Single);
        onServerStart(port);
    }

    public void onSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (scene.name == ServerSettings.instance.serverScene) {
            onServerSceneLoad();
        }
    }

    public void handleServerStart(int port) {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Started worker server on port " + port + "...");
    }

    public void handleSceneLoaded() {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Loaded the server's scene...");
    }
}
