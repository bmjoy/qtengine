using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQTObjectComponent : MonoBehaviour {

    [HideInInspector]
    public BaseQTObject obj;

    public Dictionary<string, object> lastFields;

    public void handleSpawn() {
        lastFields = new Dictionary<string, object>();

        handleServerSpawn();
        handleObjectSpawn();
    }

    public abstract void handleObjectSpawn();

    public void handleServerSpawn() {
        if (obj.objectType != BaseQTObject.type.SERVER) { return; }
        InvokeRepeating("syncFields", 0f, ServerSettings.instance.syncRate);
    }

    public void syncFields() {
        foreach (FieldInfo fi in GetType().UnderlyingSystemType.GetFields().Where(pi => Attribute.IsDefined(pi, typeof(QTSynced)))) {
            object latestValue = fi.GetValue(this);

            if (lastFields.ContainsKey(fi.Name) == false) {
                lastFields.Add(fi.Name, latestValue);
            } else if(lastFields[fi.Name].Equals(latestValue) == false) {
                lastFields[fi.Name] = latestValue;
                onSyncedFieldChanged(fi.Name, lastFields[fi.Name]);
            }
        }
    }

    public void onSyncedFieldChanged(string fieldName, object fieldValue) {
        SyncFieldMessage message = new SyncFieldMessage();
        if (fieldValue.GetType() == typeof(int)) {
            message = new SyncIntMessage();
            ((SyncIntMessage)message).value = (int)fieldValue;
        } else if (fieldValue.GetType() == typeof(float)) {
            message = new SyncFloatMessage();
            ((SyncFloatMessage)message).value = (float)fieldValue;
        } else if (fieldValue.GetType() == typeof(bool)) {
            message = new SyncBoolMessage();
            ((SyncBoolMessage)message).value = (bool)fieldValue;
        } else if (fieldValue.GetType() == typeof(string)) {
            message = new SyncStringMessage();
            ((SyncStringMessage)message).value = (string)fieldValue;
        } else {
            QTDebugger.instance.debugWarning(QTDebugger.debugType.NETWORK, "Unknown synced type -> " + fieldName + " of " + fieldValue.GetType().Name);
            return;
        }

        message.fieldName = fieldName;
        WorkerServerManager.instance.sendMessageToAll(message);

        QTDebugger.instance.debug(QTDebugger.debugType.NETWORK, "Sending sync of value(" + fieldName + "=" + fieldValue + ")");
    }

    public void setSyncedField(string fieldName, object fieldValue) {
        GetType().UnderlyingSystemType.GetField(fieldName).SetValue(fieldName, fieldValue);

        QTDebugger.instance.debug(QTDebugger.debugType.NETWORK, "Syncing value(" + fieldName + "=" + fieldValue + ")");
    }
}
