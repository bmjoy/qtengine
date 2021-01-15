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
            } else if (lastFields[fi.Name].Equals(latestValue) == false) {
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

        SyncFieldMessage message = new SyncFieldMessage();
        if (fieldValue.GetType() == typeof(int)) {
            message = new SyncIntMessage();
            ((SyncIntMessage)message).value = (int)fieldValue;
            message.syncedValueType = SyncFieldMessage.syncType.INT;
        } else if (fieldValue.GetType() == typeof(float)) {
            message = new SyncFloatMessage();
            ((SyncFloatMessage)message).value = (float)fieldValue;
            message.syncedValueType = SyncFieldMessage.syncType.FLOAT;
        } else if (fieldValue.GetType() == typeof(bool)) {
            message = new SyncBoolMessage();
            ((SyncBoolMessage)message).value = (bool)fieldValue;
            message.syncedValueType = SyncFieldMessage.syncType.BOOL;
        } else if (fieldValue.GetType() == typeof(string)) {
            message = new SyncStringMessage();
            ((SyncStringMessage)message).value = (string)fieldValue;
            message.syncedValueType = SyncFieldMessage.syncType.STRING;
        } else {
            QTDebugger.instance.debugWarning(QTDebugger.debugType.NETWORK, "Unknown synced type -> " + fieldName + " of " + fieldValue.GetType().Name);
            return;
        }

        message.objectID = component.obj.objectID;
        message.index = component.index;
        message.fieldName = fieldName;
        WorkerServerManager.instance.sendMessageToAll(message);

        //QTDebugger.instance.debug(QTDebugger.debugType.NETWORK, "Sending sync of value(" + fieldName + "=" + fieldValue + ")");
    }

    public void callNetworkFunction(string functionName) {
        component.callFunction(functionName);

        CallFunctionMessage message = new CallFunctionMessage();
        message.objectID = component.obj.objectID;
        message.index = component.index;
        message.functionName = functionName;
        WorkerServerManager.instance.sendMessageToAllReady(message);
    }
}
