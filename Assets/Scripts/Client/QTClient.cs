using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class QTClient : BaseQTClient {

    public ClientManager manager;

    public ClientSyncHandler syncHandler;
    public ClientSpawnHandler spawnHandler;
    public ClientSessionHandler sessionHandler;
    public ClientOwnerHandler ownerHandler;

    public QTClient(ClientManager _manager, TcpClient _client) : base(_client, clientType.CLIENT) {
        manager = _manager;
        eventHandler = new ClientEventHandler(this);
        syncHandler = new ClientSyncHandler(this);
        spawnHandler = new ClientSpawnHandler(this);
        sessionHandler = new ClientSessionHandler(this);
        ownerHandler = new ClientOwnerHandler(this);

        onMessageRecieved += handleMessage;
    }

    public void handleMessage(QTMessage message) {
        switch(message.messageType) {
            case QTMessage.type.REQUEST_HEARTBEAT:
                double currentTimestamp = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
                RequestHeartbeatMessage requestMessage = (RequestHeartbeatMessage)message;
                HeartbeatMessage heartbeatMessage = new HeartbeatMessage(requestMessage);
                heartbeatMessage.serverTimestamp = ((RequestHeartbeatMessage)message).createdTimestamp;
                heartbeatMessage.createdTimestamp = currentTimestamp;

                sendMessage(heartbeatMessage);
                break;
        }
    }
}
