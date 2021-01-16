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

public abstract class BaseServerManager : BaseNetworking {

    public BaseServerConnectionsManager connections;
    public BaseQTDatabase database;
    public TcpListener server;
    public List<QTMessageLog> messageLog;

    public Action<ServerQTClient> onClientConnected;
    public Action<ServerQTClient> onClientDisconnected;
    public Action<ServerQTClient, QTMessage> onMessageReceived;
    public Action<ServerQTClient, QTMessage> onMessageSent;

    public void handleStart() {
        messageLog = new List<QTMessageLog>();

        onMessageReceived += logReceivedMessage;
        onMessageSent += logSentMessage;
    }

    public void handleUpdate() {
        if (state != componentState.RUNNING) { return; }

        connections.processQueue();
        connections.handleDisconnects();
    }

    public void handleApplicationQuit() {
        if (state != componentState.RUNNING) { return; }

        foreach (ServerQTClient qtRemoteClient in connections.clients.ToList()) {
            qtRemoteClient.closeConnection();
        }

        database.close();
        server.Stop();
        connections.thread.Abort();
    }

    public abstract void setupServer(int port);

    public void setupTCPServer(int port) {
        IPAddress ip = IPAddress.Parse("127.0.0.1");
        server = new TcpListener(ip, port);
        server.Start();

        connections.startHandlingConnects();
    }

    public void logReceivedMessage(BaseQTClient client, QTMessage message) {
        QTMessageLog log = new QTMessageLog(1, message, client);
        log.direction = QTMessageLog.messageDirection.CLIENTTOSERVER;
        messageLog.Add(log);
    }

    public void logSentMessage(BaseQTClient client, QTMessage message) {
        QTMessageLog log = new QTMessageLog(1, message, client);
        log.direction = QTMessageLog.messageDirection.SERVERTOCLIENT;
        messageLog.Add(log);
    }

    public void sendMessageToAll(QTMessage message) {
        foreach (ServerQTClient qtRemoteClient in connections.clients.ToList()) {
            qtRemoteClient.sendMessage(message);
        }
    }
}
