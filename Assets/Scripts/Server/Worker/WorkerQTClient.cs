using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class WorkerQTClient : BaseQTClient {

    public BaseServerManager manager;

    public WorkerQTClient(BaseServerManager _manager, TcpClient _client) : base(_client, clientType.WORKER_SERVER) {
        manager = _manager;
        eventHandler = new ClientEventHandler(this);

        onMessageRecieved += handleMessage;

        WorkerInfoMessage message = new WorkerInfoMessage();
        message.id = "1";
        message.ip = "127.0.0.1";
        sendMessage(message);
    }

    public void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.REQUEST_HEARTBEAT:
                double currentTimestamp = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
                HeartbeatMessage heartbeatMessage = new HeartbeatMessage();
                heartbeatMessage.serverTimestamp = ((RequestHeartbeatMessage)message).createdTimestamp;
                heartbeatMessage.createdTimestamp = currentTimestamp;

                sendMessage(heartbeatMessage);
                break;
        }
    }
}
