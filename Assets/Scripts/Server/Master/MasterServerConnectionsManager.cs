using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class MasterServerConnectionsManager : BaseServerConnectionsManager {

    public MasterServerConnectionsManager(BaseServerManager _manager) : base(_manager) {

    }

    public override void handleConnects() {
        while (true) {
            TcpClient tcpClient = manager.server.AcceptTcpClient();
            MasterServerQTClient client = new MasterServerQTClient(manager, tcpClient);
            client.onConnectionClosed += manager.onClientDisconnected;

            manager.onClientConnected(client);
        }
    }

    public override void handleDisconnects() {
        double currentTimestamp = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;

        foreach (MasterServerQTClient client in clients.ToList()) {
            if (currentTimestamp - client.lastHeartbeatTimestamp > ServerSettings.instance.heartbeatTimeout) {
                manager.onClientDisconnected(client);
            }

            if (currentTimestamp - lastHeartbeatRequest > ServerSettings.instance.heartbeatRate) {
                RequestHeartbeatMessage message = new RequestHeartbeatMessage();
                message.createdTimestamp = currentTimestamp;
                message.onResponse += (QTResponsableMessage message) => {
                    double currentTimestamp = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;
                    client.lastHeartbeatTimestamp = currentTimestamp;
                };

                client.sendMessage(message);
            }
        }

        if (currentTimestamp - lastHeartbeatRequest > ServerSettings.instance.heartbeatRate) {
            lastHeartbeatRequest = currentTimestamp;
        }
    }
}
