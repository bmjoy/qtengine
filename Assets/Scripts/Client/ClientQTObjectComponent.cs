using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientQTObjectComponent {

    public BaseQTObjectComponent component;

    public ClientQTObjectComponent(BaseQTObjectComponent _component) {
        component = _component;
    }

    public void setSyncedField(string fieldName, object fieldValue) {
        GetType().UnderlyingSystemType.GetField(fieldName).SetValue(fieldName, fieldValue);
        QTDebugger.instance.debug(QTDebugger.debugType.NETWORK, "Syncing value(" + fieldName + "=" + fieldValue + ")");
    }

    public bool isOwner() {
        return ClientManager.instance.workerClient != null && ClientManager.instance.workerClient.session != null && component.obj.ownerID == ClientManager.instance.workerClient.session.id;
    }

}
