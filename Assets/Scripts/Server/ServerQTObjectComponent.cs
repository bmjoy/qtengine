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
        foreach (FieldInfo fi in GetType().UnderlyingSystemType.GetFields().Where(pi => Attribute.IsDefined(pi, typeof(QTSynced)))) {
            object latestValue = fi.GetValue(this);

            if (lastFields.ContainsKey(fi.Name) == false) {
                lastFields.Add(fi.Name, latestValue);
            } else if (lastFields[fi.Name].Equals(latestValue) == false) {
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

}
