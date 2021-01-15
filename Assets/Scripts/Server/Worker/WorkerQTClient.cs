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

    public WorkerQTClient(BaseServerManager _manager, TcpClient _client) : base(_client, clientType.CLIENT) {
        manager = _manager;
        eventHandler = new ClientEventHandler(this);

        onMessageRecieved += handleMessage;
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

            case QTMessage.type.ROOM_INFO:
                RoomInfoMessage infoMessage = (RoomInfoMessage)message;
                WorkerServerManager.instance.room = infoMessage.room;

                WorkerInfoMessage workerMessage = new WorkerInfoMessage();
                workerMessage.id = WorkerServerManager.instance.room.id;
                workerMessage.ip = "127.0.0.1";
                sendMessage(workerMessage);

                QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Sending worker info message...");
                break;
        }
    }
}
