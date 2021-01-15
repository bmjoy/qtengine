using System;
using System.Linq;
using System.IO;
using System.IO.Compression;
using System.Runtime.Serialization.Formatters.Binary;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ProtoBuf;

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

    public static GameObject spawnGameobject(GameObject obj, SyncMessage spawnPosition) {
        GameObject gameObject = Instantiate(obj);
        gameObject.transform.SetPositionAndRotation(new Vector3(spawnPosition.posX, spawnPosition.posY, spawnPosition.posZ), Quaternion.Euler(spawnPosition.rotX, spawnPosition.rotY, spawnPosition.rotZ));
        return gameObject;
    }

    public static void despawnGameobject(GameObject obj) {
        Destroy(obj);
    }

    public static SyncMessage getSyncMessage(float posX = 0f, float posY = 0f, float posZ = 0f, float rotX = 0f, float rotY = 0f, float rotZ = 0f) {
        SyncMessage message = new SyncMessage();
        message.posX = posX;
        message.posY = posY;
        message.posZ = posZ;

        message.rotX = rotX;
        message.rotY = rotY;
        message.rotZ = rotZ;

        return message;
    }

    public static SyncMessage getSyncMessageFromObject(BaseQTObject obj) {
        SyncMessage message = new SyncMessage();
        message.posX = obj.gameObject.transform.position.x;
        message.posY = obj.gameObject.transform.position.y;
        message.posZ = obj.gameObject.transform.position.z;

        message.rotX = obj.gameObject.transform.rotation.eulerAngles.x;
        message.rotY = obj.gameObject.transform.rotation.eulerAngles.y;
        message.rotZ = obj.gameObject.transform.rotation.eulerAngles.z;

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

    public static SyncFieldMessage getSyncFieldMessage(string fieldName, object fieldValue) {
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
            return null;
        }

        message.fieldName = fieldName;
        return message;
    }

   public static object getValueFromSyncFieldMessage(SyncFieldMessage message) {
        switch (message.syncedValueType) {
            case SyncFieldMessage.syncType.INT: {
                SyncIntMessage syncMessageDetailed = (SyncIntMessage)message;
                return syncMessageDetailed.value;
            }

            case SyncFieldMessage.syncType.FLOAT: {
                SyncFloatMessage syncMessageDetailed = (SyncFloatMessage)message;
                return syncMessageDetailed.value;
            }

            case SyncFieldMessage.syncType.BOOL: {
                SyncBoolMessage syncMessageDetailed = (SyncBoolMessage)message;
                return syncMessageDetailed.value;
            }

            case SyncFieldMessage.syncType.STRING: {
                SyncStringMessage syncMessageDetailed = (SyncStringMessage)message;
                return syncMessageDetailed.value;
            }

            default: {
                return null;
            }
        }
    }
}
