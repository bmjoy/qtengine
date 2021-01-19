using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class MasterServerQTClient : ServerQTClient {

    public ServerWorkerInfoHandler workerInfoHandler;
    public ServerRequestRoomsHandler requestRoomsHandler;
    public ServerRequestInstanceHandler instanceHandler;
    public ServerWorkerDebugHandler debugHandler;
    public ServerAuthHandler authHandler;

    public MasterServerQTClient(BaseServerManager _manager, TcpClient _client) : base(_manager, _client, clientType.MASTER_SERVER) {
        requestRoomsHandler = new ServerRequestRoomsHandler(this);
        workerInfoHandler = new ServerWorkerInfoHandler(this);
        debugHandler = new ServerWorkerDebugHandler(this);
        instanceHandler = new ServerRequestInstanceHandler(this);
        authHandler = new ServerAuthHandler(this);
    }
}
