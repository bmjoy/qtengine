using System.Linq;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

public class WebServer {
    public enum htmlDataType { INDEX, PACKETITEM, CLIENTITEM };

    public MasterServerManager manager;
    public HttpListener server;
    public Dictionary<htmlDataType, string> htmlData;

    public WebServer(MasterServerManager _manager) {
        manager = _manager;
        loadHTML();
        //setupWebServer();
    }

    public void loadHTML() {
        htmlData = new Dictionary<htmlDataType, string>();
        htmlData.Add(htmlDataType.INDEX, readHTML("Web/index"));
        htmlData.Add(htmlDataType.PACKETITEM, readHTML("Web/packetItem"));
        htmlData.Add(htmlDataType.CLIENTITEM, readHTML("Web/clientItem"));
    }

    public string readHTML(string path) {
        return Resources.Load<TextAsset>(path).text;
    }

    public void setupWebServer() {
        Thread t = new Thread(new ThreadStart(handleListening));
        t.Start();

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Started web server...");
    }

    public void handleListening() {
        server = new HttpListener();
        server.Prefixes.Add("http://localhost:8112/");
        server.Start();

        Task listenTask = handleRequests();
        listenTask.GetAwaiter().GetResult();
        server.Close();
    }

    public async Task handleRequests() {
        while (true) {
            HttpListenerContext ctx = await server.GetContextAsync();

            HttpListenerRequest req = ctx.Request;
            HttpListenerResponse resp = ctx.Response;
            //loadHTML();

            int totalRecieved = 0;
            int totalSent = 0;
            string html = htmlData[htmlDataType.INDEX];
            string packetItems = "";

            List<QTMessageLog> logs = manager.messageLog.GetRange(Mathf.Clamp(manager.messageLog.Count - 100, 0, manager.messageLog.Count), Mathf.Clamp(manager.messageLog.Count, 0, 100));
            logs.Reverse();

            foreach (QTMessageLog log in logs) {
                string packetItem =  htmlData[htmlDataType.PACKETITEM];
                packetItem = packetItem.Replace("//ID//", log.index.ToString());
                packetItem = packetItem.Replace("//TYPE//", log.type.ToString());
                packetItem = packetItem.Replace("//SIZE//", log.messageSize.ToString() + "B");
                packetItem = packetItem.Replace("//IDENTITY//", log.identity);
                packetItem = packetItem.Replace("//ICON_SRC//", (log.direction == QTMessageLog.messageDirection.CLIENTTOSERVER ? "https://files.catbox.moe/ws9x00.png" : "https://files.catbox.moe/6q6uf1.png"));
                packetItem = packetItem.Replace("//ICON_CLASS//", (log.direction == QTMessageLog.messageDirection.CLIENTTOSERVER ? "packetDir1" : "packetDir2"));

                packetItems += packetItem;
            }

            foreach(QTMessageLog log in manager.messageLog.ToList()) {
                if (log.direction == QTMessageLog.messageDirection.CLIENTTOSERVER) {
                    totalRecieved += log.messageSize;
                } else {
                    totalSent += log.messageSize;
                }
            }

            string clientItems = "";
            foreach (ServerQTClient client in manager.connections.clients.ToList()) {
                string clientItem = htmlData[htmlDataType.CLIENTITEM];
                clientItem = clientItem.Replace("//ID//", client.thread.thread.ManagedThreadId.ToString());
                clientItem = clientItem.Replace("//IDENTITY//", ((IPEndPoint)(client.client.Client.RemoteEndPoint)).Address.ToString());
                clientItem = clientItem.Replace("//PING//", client.ping + "ms");

                clientItems += clientItem;
            }

            html = html.Replace("//PACKET_ITEMS//", packetItems);
            html = html.Replace("//CLIENT_ITEMS//", clientItems);

            html = html.Replace("//TOTAL_SENT//", QTUtils.FormatBytes(totalSent));
            html = html.Replace("//TOTAL_RECIEVED//", QTUtils.FormatBytes(totalRecieved));
            html = html.Replace("//TOTAL_MESSAGES//", manager.messageLog.Count.ToString());

            byte[] data = Encoding.UTF8.GetBytes(html);
            resp.ContentType = "text/html";
            resp.ContentEncoding = Encoding.UTF8;
            resp.ContentLength64 = data.LongLength;

            await resp.OutputStream.WriteAsync(data, 0, data.Length);
            resp.Close();
        }
    }
}
