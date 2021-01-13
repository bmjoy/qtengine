using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClientManager : BaseNetworking {

    public static ClientManager instance { get; protected set; }
    public ClientSpawnManager spawnManager;

    public Dictionary<string, RoomInfo> rooms;
    public QTClient masterClient;
    public QTClient workerClient;

    public Action<QTClient> onMasterConnected;
    public Action<QTClient> onWorkerConnected;
    public Action onRoomsUpdated;

    void Awake() {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start() {
        rooms = new Dictionary<string, RoomInfo>();

        spawnManager = new ClientSpawnManager();

        onMasterConnected += handleMasterConnected;
        onWorkerConnected += handleWorkerConnected;
        onRoomsUpdated += debugNewRooms;
        SceneManager.sceneLoaded += handleSceneLoaded;
    }

    void Update() {
        if (state != componentState.RUNNING) { return; }

        masterClient.processQueue();
        if(workerClient != null) {
            workerClient.processQueue();
        }
    }

    void OnApplicationQuit() {
        if (state != componentState.RUNNING) { return; }

        masterClient.closeConnection();
        if (workerClient != null) {
            workerClient.closeConnection();
        }

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Closing client...");
    }

    void handleSceneLoaded(Scene scene, LoadSceneMode mode) {
        if (state != componentState.RUNNING) { return; }

        if (scene.name == ClientSettings.instance.clientScene) {
            spawnManager.despawnAllObjects();

            RequestSyncMessage syncMessage = new RequestSyncMessage();
            workerClient.sendMessage(syncMessage);

            ReadyMessage readyMessage = new ReadyMessage();
            readyMessage.state = true;
            workerClient.sendMessage(readyMessage);
        }
    }

    public void setupMasterClient() {
        TcpClient tcpClient = new TcpClient("127.0.0.1", 8111);
        masterClient = new QTClient(this, tcpClient);
        masterClient.remoteType = BaseQTClient.clientType.MASTER_SERVER;

        onMasterConnected(masterClient);
    }

    public void setupWorkerClient(string ip, int port) {
        TcpClient tcpClient = new TcpClient("127.0.0.1", port);
        workerClient = new QTClient(this, tcpClient);
        workerClient.remoteType = BaseQTClient.clientType.WORKER_SERVER;

        onWorkerConnected(workerClient);
    }

    public void handleMasterConnected(QTClient client) {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Started master client...");
        state = componentState.RUNNING;

        RequestRoomsMessage message = new RequestRoomsMessage();
        client.sendMessage(message);
    }

    public void handleWorkerConnected(QTClient client) {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Started worker client...");

        SceneManager.LoadScene(ClientSettings.instance.clientScene, LoadSceneMode.Single);
    }

    public void debugNewRooms() {
        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Recieved new rooms list...");
    }
}