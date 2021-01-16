using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerQTObjectComponent {

    public BaseQTObjectComponent component;
    public Dictionary<string, object> lastFields;

    public ServerQTObjectComponent(BaseQTObjectComponent _component) {
        component = _component;

        lastFields = new Dictionary<string, object>();
    }

    public void syncFields() {
        foreach (FieldInfo fi in component.GetType().UnderlyingSystemType.GetFields().Where(fi => Attribute.IsDefined(fi, typeof(QTSynced)))) {
            object latestValue = fi.GetValue(component);

            if (lastFields.ContainsKey(fi.Name) == false) {
                lastFields.Add(fi.Name, latestValue);

                onSyncedFieldChanged(fi.Name, lastFields[fi.Name]);
            } else if (QTUtils.hasSyncedFieldChanged(this, fi, lastFields[fi.Name])) {
                lastFields[fi.Name] = latestValue;

                onSyncedFieldChanged(fi.Name, lastFields[fi.Name]);
            }
        }
    }

    public void syncAllFields() {
        foreach (FieldInfo fi in component.GetType().UnderlyingSystemType.GetFields().Where(fi => Attribute.IsDefined(fi, typeof(QTSynced)))) {
            object latestValue = fi.GetValue(component);
            onSyncedFieldChanged(fi.Name, latestValue);
        }
    }

    public void onSyncedFieldChanged(string fieldName, object fieldValue) {
        if (fieldValue == null) {
            QTDebugger.instance.debugWarning(QTDebugger.debugType.NETWORK, "Setting to null -> " + fieldName);
            return;
        }

        FieldInfoMessage message = QTUtils.getSyncFieldMessage(fieldName, fieldValue);
        message.objectID = component.obj.objectID;
        message.index = component.index;
        WorkerServerManager.instance.sendMessageToAll(message);

        QTDebugger.instance.debug(QTDebugger.debugType.NETWORK, "Sending sync of value(" + fieldName + "=" + fieldValue + ")");
    }

    public void callNetworkFunction(string functionName) {
        object[] parameters = { };
        callNetworkFunction(functionName, parameters);
    }

    public void callNetworkFunction(string functionName, object[] parameters) {
        component.callFunction(functionName, parameters);

        List<FieldInfoMessage> parameterMessages = new List<FieldInfoMessage>();
        foreach(object parameter in parameters) {
            FieldInfoMessage fieldMessage = QTUtils.getSyncFieldMessage("parameter", parameter);
            parameterMessages.Add(fieldMessage);
        }

        CallFunctionMessage message = new CallFunctionMessage();
        message.objectID = component.obj.objectID;
        message.index = component.index;
        message.functionName = functionName;
        message.parameters = parameterMessages.ToArray();
        WorkerServerManager.instance.sendMessageToAll(message);
    }
}
