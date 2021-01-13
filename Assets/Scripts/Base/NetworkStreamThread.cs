using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;

public class NetworkStreamThread {

    public BaseQTClient qtClient;
    public Thread thread;

    public NetworkStreamThread(BaseQTClient _qtClient) {
        qtClient = _qtClient;

        thread = new Thread(new ThreadStart(handleStream));
        thread.Start();
    }

    public void handleStream() {
        while (true) {
            try {
                byte[] sizeData = new byte[4];
                int sizeBytes = qtClient.stream.Read(sizeData, 0, sizeData.Length);
                int nextPacketSize = BitConverter.ToInt32(sizeData, 0);
                if (nextPacketSize == 0) { continue; }

                byte[] data = new byte[nextPacketSize];
                int bytes = qtClient.stream.Read(data, 0, data.Length);
                //debugRecieved(sizeData, data);
                QTMessage message = QTUtils.ByteArrayToMessage(data);

                qtClient.queuedMessages.Add(message);
            } catch {
                QTDebugger.instance.debugWarning(QTDebugger.debugType.NETWORK,"[" + qtClient.type.ToString() + "_thread] Error while reading the NetworkStream-");
                qtClient.closeConnection();
            }
        }
    }

    public void debugRecieved(byte[] sizeData, byte[] data) {
        byte[] a = { sizeData[0], sizeData[1] };
        byte[] b = { sizeData[sizeData.Length - 1], sizeData[sizeData.Length - 2] };
        byte[] c = { data[0], data[1] };
        byte[] d = { data[data.Length - 1], data[data.Length - 2] };

        QTDebugger.instance.debug(QTDebugger.debugType.NETWORK,
            "[" + qtClient.type.ToString() + "_thread] Recieved data of size " + data.Length + "B (" + sizeData.Length + "B)-\n" +
            "sizeBytes [" + BitConverter.ToString(sizeData) + "] / messageBytes [" + BitConverter.ToString(c) + ", ..., " + BitConverter.ToString(d) + "]"
        );
    }
}
