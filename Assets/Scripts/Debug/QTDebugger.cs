using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QTDebugger {

    public static QTDebugger instance { get; protected set; } = new QTDebugger();
    public WorkerQTClient networkClient;

    public enum unityDebugType { LOG, LOGWARNING, LOGERROR }
    public enum debugType { BASE, EVENTS, NETWORK, NETWORK_WORKER }
    public List<debugType> enabledDebugTypes;

    public QTDebugger() {
        enabledDebugTypes = new List<debugType>();
        enabledDebugTypes.Add(debugType.BASE);
        enabledDebugTypes.Add(debugType.EVENTS);
        //enabledDebugTypes.Add(debugType.NETWORK);
        enabledDebugTypes.Add(debugType.NETWORK_WORKER);
    }

    public void _debug(unityDebugType unityDebugType, debugType type, object message) {
        if(enabledDebugTypes.Contains(type)) {
            string debugMessage;

            switch (unityDebugType) {
                case unityDebugType.LOG:
                    switch (type) {
                        case debugType.BASE:
                            debugMessage = "<color=gray>[base] " + message + "</color>";
                            break;

                        case debugType.EVENTS:
                            debugMessage = "<color=yellow>[events] " + message + "</color>";
                            break;

                        case debugType.NETWORK:
                            debugMessage = "<color=cyan>[network] " + message + "</color>";
                            break;

                        case debugType.NETWORK_WORKER:
                            debugMessage = "<color=lightblue>[network_worker] " + message + "</color>";
                            break;

                        default:
                            debugMessage = message.ToString();
                            break;
                    }

                    Debug.Log(debugMessage);
                    break;

                case unityDebugType.LOGWARNING:
                    Debug.LogWarning(message);
                    break;

                case unityDebugType.LOGERROR:
                    Debug.LogError(message);
                    break;
            }
        }
    }

    public void debug(debugType type, object message, bool sync = true) {
        _debug(unityDebugType.LOG, type, message);
        if (sync) {
            syncDebug(unityDebugType.LOG, message);
        }
    }

    public void debugWarning(debugType type, object message, bool sync = true) {
        _debug(unityDebugType.LOGWARNING, type, message);
        if (sync) {
            syncDebug(unityDebugType.LOGWARNING, message);
        }
    }

    public void debugError(debugType type, object message, bool sync = true) {
        _debug(unityDebugType.LOGERROR, type, message);
        if(sync) {
            syncDebug(unityDebugType.LOGERROR, message);
        }
    }

    public void syncDebug(unityDebugType type, object message) {
        if(networkClient == null) { return; }

        WorkerDebugMessage debugMessage = new WorkerDebugMessage();
        debugMessage.message = message.ToString();
        debugMessage.debugType = type;
        networkClient.sendMessage(debugMessage);
    }
}
