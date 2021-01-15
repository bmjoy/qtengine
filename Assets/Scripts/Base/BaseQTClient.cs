using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public abstract class BaseQTClient {

    public enum clientType { CLIENT, MASTER_SERVER, WORKER_SERVER };
    public clientType type;
    public clientType remoteType = clientType.CLIENT;

    public TcpClient client;
    public NetworkStream stream;
    public NetworkStreamThread thread;
    public BaseEventHandler eventHandler;

    public Action<QTMessage> onMessageRecieved;
    public Action<QTMessage> onMessageSent;
    public List<QTMessage> queuedMessages;

    public SessionInfo session;
    public string connectionIP;

    public BaseQTClient(TcpClient _client, clientType _clientType) {
        client = _client;
        type = _clientType;
        queuedMessages = new List<QTMessage>();
        connectionIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();

        onMessageRecieved += debugRecievedMessage;
        onMessageSent += debugSentMessage;

        stream = client.GetStream();
        thread = new NetworkStreamThread(this);
    }

    public void processQueue() {
        try {
            if(queuedMessages.Count < 1) { return; }

            List<QTMessage> queue = queuedMessages.ToList();
            foreach (QTMessage message in queue) {
                onMessageRecieved(message);
                queuedMessages.Remove(message);
            }
        } catch {

        }
    }

    public void sendMessage(QTMessage message) {
        if (client.Connected == false) { return; }

        byte[] bytes = QTUtils.MessageToByteArray(message);
        byte[] sizeBytes = BitConverter.GetBytes(bytes.Length);

        try {
            //debugSent(sizeBytes, bytes);

            stream.Write(sizeBytes, 0, sizeBytes.Length);
            stream.Write(bytes, 0, bytes.Length);
        } catch {
            QTDebugger.instance.debugWarning(QTDebugger.debugType.NETWORK, "[" + type + "_base] Error while writing the NetworkStream-");
            closeConnection();
        }

        onMessageSent(message);
    }

    public void closeConnection() {
        client.Close();
        thread.thread.Abort();

        if(type == clientType.MASTER_SERVER && MasterServerManager.instance.state == BaseNetworking.componentState.RUNNING) {
            MasterServerManager.instance.onClientDisconnected((MasterServerQTClient)this);
        }
        if (type == clientType.WORKER_SERVER && WorkerServerManager.instance.state == BaseNetworking.componentState.RUNNING) {
            WorkerServerManager.instance.onClientDisconnected((WorkerServerQTClient)this);
        }
    }

    public void debugSent(byte[] sizeBytes, byte[] bytes) {
        byte[] a = { sizeBytes[0], sizeBytes[1] };
        byte[] b = { sizeBytes[sizeBytes.Length - 1], sizeBytes[sizeBytes.Length - 2] };
        byte[] c = { bytes[0], bytes[1] };
        byte[] d = { bytes[bytes.Length - 1], bytes[bytes.Length - 2] };

        QTDebugger.instance.debug(QTDebugger.debugType.NETWORK,
            "[" + type.ToString() + "_thread] Sending data of size " + bytes.Length + "B (" + sizeBytes.Length + "B)-\n" +
            "sizeBytes [" + BitConverter.ToString(sizeBytes) + "] / messageBytes [" + BitConverter.ToString(c) + ", ..., " + BitConverter.ToString(d) + "]"
        , false);
    }

    public void debugSentMessage(QTMessage message) {
        if (message.messageType != QTMessage.type.WORKER_DEBUG) {
            //QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Sending message of type " + message.messageType.ToString());
        }
    }

    public void debugRecievedMessage(QTMessage message) {
        if (message.messageType != QTMessage.type.WORKER_DEBUG) {
            //QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Recieved message of type " + message.messageType.ToString());
        }
    }

    public string getIP() {
        return connectionIP;
    }
}
