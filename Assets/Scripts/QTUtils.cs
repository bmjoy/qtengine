using System;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;
using System.Reflection;

public class QTUtils : MonoBehaviour {

    public static QTMessage ByteArrayToMessage(byte[] arrBytes) {
        //return (QTMessage)(ByteArrayToObject(Decompress(arrBytes)));
        return (QTMessage)(ByteArrayToObject(arrBytes));
    }

    public static byte[] MessageToByteArray(QTMessage message) {
        //return Compress(ObjectToByteArray(message));
        return ObjectToByteArray(message);
    }

    public static byte[] ObjectToByteArray(object obj) {
        using (var ms = new MemoryStream()) {
            Serializer.Serialize(ms, obj);
            return ms.ToArray();
        }
    }

    public static object ByteArrayToObject(byte[] arrBytes) {
        using (var memStream = new MemoryStream()) {
            var binForm = new BinaryFormatter();
            memStream.Write(arrBytes, 0, arrBytes.Length);
            memStream.Seek(0, SeekOrigin.Begin);
            var obj = Serializer.Deserialize<QTMessage>(memStream);
            return obj;
        }
    }

    public static byte[] Compress(byte[] raw) {
        using (MemoryStream memory = new MemoryStream()) {
            using (GZipStream gzip = new GZipStream(memory, CompressionMode.Compress, true)) {
                gzip.Write(raw, 0, raw.Length);
            }
            return memory.ToArray();
        }
    }

    public static byte[] Decompress(byte[] gzip) {
        using (GZipStream stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress)) {
            const int size = 4096;
            byte[] buffer = new byte[size];
            using (MemoryStream memory = new MemoryStream()) {
                int count = 0;
                do {
                    count = stream.Read(buffer, 0, size);
                    if (count > 0) {
                        memory.Write(buffer, 0, count);
                    }
                }
                while (count > 0);
                return memory.ToArray();
            }
        }
    }

    public static string FormatBytes(float bytes, bool appendSize=true) {
        float k = 1024;
        string[] sizes = { "Bytes", "KB", "MB", "GB", "TB", "PB", "EB", "ZB", "YB" };
        if (bytes == 0) { return bytes + (appendSize ? sizes[0] : ""); }

        int i = Mathf.FloorToInt(Mathf.Log(bytes) / Mathf.Log(k));
        double result = Math.Round((double)(bytes / Math.Pow(k, i)), 2);
        return result + (appendSize ? sizes[i] : "");
    }

    public static List<TEnum> GetEnumList<TEnum>() where TEnum : Enum
    => ((TEnum[])Enum.GetValues(typeof(TEnum))).ToList();

    public static GameObject spawnGameobject(GameObject obj, SyncPositionMessage spawnPosition, SyncRotationMessage spawnRotation) {
        GameObject gameObject = Instantiate(obj);
        gameObject.transform.SetPositionAndRotation(new Vector3(spawnPosition.posX, spawnPosition.posY, spawnPosition.posZ), Quaternion.Euler(spawnRotation.rotX, spawnRotation.rotY, spawnRotation.rotZ));
        return gameObject;
    }

    public static void despawnGameobject(GameObject obj) {
        Destroy(obj);
    }

    public static SyncPositionMessage getSyncPositionMessage(float posX = 0f, float posY = 0f, float posZ = 0f) {
        SyncPositionMessage message = new SyncPositionMessage();
        message.posX = posX;
        message.posY = posY;
        message.posZ = posZ;

        return message;
    }

    public static SyncRotationMessage getSyncRotationMessage(float rotX = 0f, float rotY = 0f, float rotZ = 0f) {
        SyncRotationMessage message = new SyncRotationMessage();
        message.rotX = rotX;
        message.rotY = rotY;
        message.rotZ = rotZ;

        return message;
    }

    public static SyncPositionMessage getSyncPositionMessageFromObject(BaseQTObject obj) {
        SyncPositionMessage message = new SyncPositionMessage();
        message.posX = obj.gameObject.transform.position.x;
        message.posY = obj.gameObject.transform.position.y;
        message.posZ = obj.gameObject.transform.position.z;

        return message;
    }

    public static SyncRotationMessage getSyncRotationMessageFromObject(BaseQTObject obj) {
        SyncRotationMessage message = new SyncRotationMessage();
        message.rotX = obj.gameObject.transform.eulerAngles.x;
        message.rotY = obj.gameObject.transform.eulerAngles.y;
        message.rotZ = obj.gameObject.transform.eulerAngles.z;

        return message;
    }

    public static Dictionary<string, string> getCommandLineArgs() {
        Dictionary<string, string> cmdArgs = new Dictionary<string, string>();
        string[] args = System.Environment.GetCommandLineArgs();

        for (int i = 1; i < args.Length; i += 2) {
            if (args.Length > i + 1) {
                cmdArgs.Add(args[i], args[i + 1]);
            }
        }

        return cmdArgs;
    }

    public static FieldInfoMessage getSyncFieldMessage(string fieldName, object fieldValue) {
        FieldInfoMessage message = new FieldInfoMessage();

        if (fieldValue.GetType() == typeof(int)) {
            message = new FieldInfoIntMessage();
            ((FieldInfoIntMessage)message).value = (int)fieldValue;
            message.syncedValueType = FieldInfoMessage.syncType.INT;
        } else if (fieldValue.GetType() == typeof(float)) {
            message = new FieldInfoFloatMessage();
            ((FieldInfoFloatMessage)message).value = (float)fieldValue;
            message.syncedValueType = FieldInfoMessage.syncType.FLOAT;
        } else if (fieldValue.GetType() == typeof(bool)) {
            message = new FieldInfoBoolMessage();
            ((FieldInfoBoolMessage)message).value = (bool)fieldValue;
            message.syncedValueType = FieldInfoMessage.syncType.BOOL;
        } else if (fieldValue.GetType() == typeof(string)) {
            message = new FieldInfoStringMessage();
            ((FieldInfoStringMessage)message).value = (string)fieldValue;
            message.syncedValueType = FieldInfoMessage.syncType.STRING;
        } else if (fieldValue.GetType().IsArray) {
            message = new FieldInfoArrayMessage();

            List<FieldInfoMessage> messages = new List<FieldInfoMessage>();
            IEnumerable originalEnumerable = fieldValue as IEnumerable;
            object[] originalArray = originalEnumerable.OfType<object>().ToArray();

            for(int i = 0; i < originalArray.Length; i++) {
                messages.Add(getSyncFieldMessage(i.ToString(), originalArray[i]));
            }

            ((FieldInfoArrayMessage)message).value = messages.ToArray();
            message.syncedValueType = FieldInfoArrayMessage.syncType.ARRAY;
        } else if (typeof(IDictionary).IsAssignableFrom(fieldValue.GetType())) {
            message = new FieldInfoDictionaryMessage();

            Dictionary<FieldInfoMessage, FieldInfoMessage> messages = new Dictionary<FieldInfoMessage, FieldInfoMessage>();
            IDictionary originalDictionary = fieldValue as IDictionary;

            int i = 0;
            foreach(object key in originalDictionary.Keys) {
                messages.Add(getSyncFieldMessage(i + "-0", key), getSyncFieldMessage(i + "-1", originalDictionary[key]));
                i++;
            }

            ((FieldInfoDictionaryMessage)message).value = messages;
            message.syncedValueType = FieldInfoDictionaryMessage.syncType.DICTIONARY;
        } else {
            QTDebugger.instance.debugWarning(QTDebugger.debugType.NETWORK, "Unknown synced type -> " + fieldName + " of " + fieldValue.GetType().Name);
            return null;
        }

        message.fieldName = fieldName;
        return message;
    }

   public static object getValueFromSyncFieldMessage(FieldInfoMessage message) {
        switch (message.syncedValueType) {
            case FieldInfoMessage.syncType.INT: {
                FieldInfoIntMessage syncMessageDetailed = (FieldInfoIntMessage)message;
                return syncMessageDetailed.value;
            }

            case FieldInfoMessage.syncType.FLOAT: {
                FieldInfoFloatMessage syncMessageDetailed = (FieldInfoFloatMessage)message;
                return syncMessageDetailed.value;
            }

            case FieldInfoMessage.syncType.BOOL: {
                FieldInfoBoolMessage syncMessageDetailed = (FieldInfoBoolMessage)message;
                return syncMessageDetailed.value;
            }

            case FieldInfoMessage.syncType.STRING: {
                FieldInfoStringMessage syncMessageDetailed = (FieldInfoStringMessage)message;
                return syncMessageDetailed.value;
            }

            case FieldInfoMessage.syncType.ARRAY: {
                FieldInfoArrayMessage syncMessageDetailed = (FieldInfoArrayMessage)message;

                List<object> values = new List<object>();
                if (syncMessageDetailed.value == null) { return values; }
                for (int i = 0; i < syncMessageDetailed.value.Length; i++) {
                    values.Add(getValueFromSyncFieldMessage(syncMessageDetailed.value[i]));
                }

                return values.ToArray();
            }

            case FieldInfoMessage.syncType.DICTIONARY: {
                FieldInfoDictionaryMessage syncMessageDetailed = (FieldInfoDictionaryMessage)message;

                Dictionary<object, object> values = new Dictionary<object, object>();
                if (syncMessageDetailed.value == null) { return values; }
                foreach (FieldInfoMessage key in syncMessageDetailed.value.Keys) {
                    values.Add(getValueFromSyncFieldMessage(key), getValueFromSyncFieldMessage(syncMessageDetailed.value[key]));
                }

                return values;
            }

            default: {
                return null;
            }
        }
   }

    public static AnimationParameterInfoMessage getSyncAnimationMessage(string fieldName, object fieldValue) {
        AnimationParameterInfoMessage message = new AnimationParameterInfoMessage();
        if (fieldValue.GetType() == typeof(int)) {
            message = new AnimationParameterInfoIntMessage();
            ((AnimationParameterInfoIntMessage)message).value = (int)fieldValue;
            message.syncedValueType = AnimationParameterInfoMessage.syncType.INT;
        } else if (fieldValue.GetType() == typeof(float)) {
            message = new AnimationParameterInfoFloatMessage();
            ((AnimationParameterInfoFloatMessage)message).value = (float)fieldValue;
            message.syncedValueType = AnimationParameterInfoMessage.syncType.FLOAT;
        } else if (fieldValue.GetType() == typeof(bool)) {
            message = new AnimationParameterInfoBoolMessage();
            ((AnimationParameterInfoBoolMessage)message).value = (bool)fieldValue;
            message.syncedValueType = AnimationParameterInfoMessage.syncType.BOOL;
        } else {
            QTDebugger.instance.debugWarning(QTDebugger.debugType.NETWORK, "Unknown animator parameter type -> " + fieldName + " of " + fieldValue.GetType().Name);
            return null;
        }

        message.fieldName = fieldName;
        return message;
    }

    public static void applySyncAnimationMessageToAnimator(Animator animator, AnimationParameterInfoMessage message) {
        switch (message.syncedValueType) {
            case AnimationParameterInfoMessage.syncType.INT: {
                AnimationParameterInfoIntMessage syncMessageDetailed = (AnimationParameterInfoIntMessage)message;
                animator.SetInteger(syncMessageDetailed.fieldName, syncMessageDetailed.value);
                break;
            }

            case AnimationParameterInfoMessage.syncType.FLOAT: {
                AnimationParameterInfoFloatMessage syncMessageDetailed = (AnimationParameterInfoFloatMessage)message;
                animator.SetFloat(syncMessageDetailed.fieldName, syncMessageDetailed.value);
                break;
            }

            case AnimationParameterInfoMessage.syncType.BOOL: {
                AnimationParameterInfoBoolMessage syncMessageDetailed = (AnimationParameterInfoBoolMessage)message;
                animator.SetBool(syncMessageDetailed.fieldName, syncMessageDetailed.value);
                break;
            }
        }
    }

    public static object getParameterValueFromAnimator(Animator animator, AnimatorControllerParameter parameter) {
        switch(parameter.type) {
            case AnimatorControllerParameterType.Int:
                return animator.GetInteger(parameter.name);

            case AnimatorControllerParameterType.Float:
                return animator.GetFloat(parameter.name);

            case AnimatorControllerParameterType.Bool:
                return animator.GetBool(parameter.name);

            default:
                return null;
        }
    }

    public static void setCorrectSyncedObject(ClientQTObjectComponent clientComponent, FieldInfo field, object newValue) {
        object currentValue = field.GetValue(clientComponent.component);

        if (isArray(currentValue)) {
            IEnumerable oldArray = currentValue as IEnumerable;
            if (newValue == null) { return; }

            IEnumerable newArray = newValue as IEnumerable;
            oldArray = newArray;
        } else if (isDictionary(currentValue)) {
            IDictionary oldDictionary = currentValue as IDictionary;
            oldDictionary.Clear();
            if (newValue == null) { return; }

            IDictionary newDictionary = newValue as IDictionary;
            foreach (object key in newDictionary.Keys) {
                object value = newDictionary[key];
                oldDictionary.Add(key, value);
            }
        } else {
            field.SetValue(clientComponent.component, newValue);
        }
    }

    public static bool hasSyncedFieldChanged(ServerQTObjectComponent serverComponent, FieldInfo fi, object oldValue) {
        object currentValue = fi.GetValue(serverComponent.component);

        return (oldValue == null && currentValue != null)
            || (oldValue != null && oldValue.Equals(currentValue) == false);
    }

    public static bool isArray(object obj) {
        return obj.GetType().IsArray;
    }

    public static bool isDictionary(object obj) {
        return typeof(IDictionary).IsAssignableFrom(obj.GetType());
    }
}
