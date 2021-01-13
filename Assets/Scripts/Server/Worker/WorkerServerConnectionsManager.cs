using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class WorkerServerConnectionsManager : BaseServerConnectionsManager {

    public WorkerQTClient masterClient;

    public WorkerServerConnectionsManager(BaseServerManager _manager) : base(_manager) {
        processQueue += handleMasterClientQueue;
    }

    public void handleMasterClientQueue() {
        if(masterClient != null) {
            masterClient.processQueue();
        }
    }

    public override void handleConnects() {
        TcpClient masterTCPClient = new TcpClient(AppSettings.instance.serverIP, 8111);
        masterClient = new WorkerQTClient(manager, masterTCPClient);
        masterClient.remoteType = BaseQTClient.clientType.MASTER_SERVER;
        QTDebugger.instance.networkClient = masterClient;

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Connected to master server...");

        while (true) {
            TcpClient tcpClient = manager.server.AcceptTcpClient();
            WorkerServerQTClient client = new WorkerServerQTClient(manager, tcpClient);
            clients.Add(client);

            manager.onClientConnected(client);
        }
    }

    public override void handleDisconnects() {
        double currentTimestamp = (DateTime.Now - DateTime.MinValue).TotalMilliseconds;

        foreach (WorkerServerQTClient client in clients.ToList()) {
            if(client.remoteType == BaseQTClient.clientType.MASTER_SERVER) { continue; }
            if (currentTimestamp - client.lastHeartbeatTimestamp > ServerSettings.instance.heartbeatTimeout) {
                manager.onClientDisconnected(client);
            }

            if (currentTimestamp - lastHeartbeatRequest > ServerSettings.instance.heartbeatRate) {
                RequestHeartbeatMessage message = new RequestHeartbeatMessage();
                message.createdTimestamp = currentTimestamp;

                client.sendMessage(message);
            }
        }

        if (currentTimestamp - lastHeartbeatRequest > ServerSettings.instance.heartbeatRate) {
            lastHeartbeatRequest = currentTimestamp;
        }
    }
}
