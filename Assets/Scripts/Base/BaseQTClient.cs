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
    public NetworkStreamThread thread;
    public BaseEventHandler eventHandler;

    public Action<QTMessage> onMessageRecieved;
    public Action<QTMessage> onMessageSent;
    public Action<BaseQTClient> onConnectionClosed;

    public List<QTMessage> queuedMessages;
    public Dictionary<string, QTResponsableMessage> awaitingResponse;

    public SessionInfo session;
    public string connectionIP;

    private static readonly object recieveLock = new object();
    private static readonly object sendLock = new object();

    public BaseQTClient(TcpClient _client, clientType _clientType) {
        client = _client;
        type = _clientType;
        queuedMessages = new List<QTMessage>();
        awaitingResponse = new Dictionary<string, QTResponsableMessage>();

        onMessageRecieved += debugRecievedMessage;
        onMessageSent += debugSentMessage;
        onConnectionClosed += debugConnectionClosed;

        thread = new NetworkStreamThread(this);

        connectionIP = ((IPEndPoint)client.Client.RemoteEndPoint).Address.ToString();
    }

    public void processQueue() {
        lock (recieveLock) {
            try {
                if (queuedMessages.Count < 1) { return; }

                List<QTMessage> queue = queuedMessages.ToList();
                foreach (QTMessage message in queue) {
                    try {
                        onMessageRecieved(message);

                        var responsableValid = message as QTResponsableMessage;
                        if (responsableValid != null) {
                            QTResponsableMessage responseMessage = (QTResponsableMessage)message;
                            if (awaitingResponse.ContainsKey(responseMessage.responseID)) {
                                awaitingResponse[responseMessage.responseID].onResponse(responseMessage);
                                awaitingResponse.Remove(responseMessage.responseID);
                            }
                        }
                    } catch (Exception e) {
                        Debug.LogError(e);
                    }

                    queuedMessages.Remove(message);
                }
            } catch (Exception e) {
                Debug.LogWarning(e);
            }
        }
    }

    public void sendMessage(QTMessage message) {
        lock (sendLock) {
            try {
                if (client.Connected == false) { return; }

                byte[] bytes = QTUtils.MessageToByteArray(message);
                byte[] sizeBytes = BitConverter.GetBytes(bytes.Length);

                try {
                    debugSent(sizeBytes, bytes);

                    thread.stream.Write(sizeBytes, 0, sizeBytes.Length);
                    thread.stream.Write(bytes, 0, bytes.Length);
                } catch (Exception e) {
                    QTDebugger.instance.debugWarning(QTDebugger.debugType.NETWORK, "[" + type + "_base] Error while writing the NetworkStream-");
                    QTDebugger.instance.debugError(QTDebugger.debugType.NETWORK, e);

                    closeConnection();
                }

                var responsableValid = message as QTResponsableMessage;
                if (responsableValid != null) {
                    QTResponsableMessage responseMessage = (QTResponsableMessage)message;
                    awaitingResponse.Add(responseMessage.responseID, responseMessage);
                }

                onMessageSent(message);
            } catch(Exception e) {
                Debug.LogWarning(e);
            }
        }
    }

    public void closeConnection() {
        client.Close();
        thread.thread.Abort();

        onConnectionClosed(this);
    }

    public void debugSent(byte[] sizeBytes, byte[] bytes) {
        byte[] a = { sizeBytes[0], sizeBytes[1] };
        byte[] b = { sizeBytes[sizeBytes.Length - 1], sizeBytes[sizeBytes.Length - 2] };
        byte[] c = { bytes[0], bytes[1] };
        byte[] d = { bytes[bytes.Length - 1], bytes[bytes.Length - 2] };

        QTDebugger.instance.debug(QTDebugger.debugType.NETWORK_DETAILED,
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

    public void debugConnectionClosed(BaseQTClient client) {

    }

    public string getIP() {
        return connectionIP;
    }
}
