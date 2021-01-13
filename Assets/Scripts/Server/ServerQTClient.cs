using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public abstract class ServerQTClient : BaseQTClient {

    public BaseServerManager manager;
    public double lastHeartbeatTimestamp = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
    public double ping;

    public ServerQTClient(BaseServerManager _manager, TcpClient _client, clientType _type) : base(_client, _type) {
        manager = _manager;
        eventHandler = new ServerEventHandler(this);

        onMessageRecieved += handleMessage;
        onMessageSent += handleSentMessage;
    }

    public void handleMessage(QTMessage message) {
        manager.onMessageReceived(this, message);
    }

    public void handleSentMessage(QTMessage message) {
        manager.onMessageSent(this, message);
    }
}
