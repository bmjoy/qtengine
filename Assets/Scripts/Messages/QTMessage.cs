using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
[ProtoInclude(1, typeof(EventMessage))]
[ProtoInclude(2, typeof(HeartbeatMessage))]
[ProtoInclude(3, typeof(InputMessage))]
[ProtoInclude(4, typeof(RequestHeartbeatMessage))]
[ProtoInclude(5, typeof(SyncMessage))]
[ProtoInclude(6, typeof(SpawnMessage))]
[ProtoInclude(7, typeof(ReadyMessage))]
[ProtoInclude(8, typeof(RequestSyncMessage))]
[ProtoInclude(9, typeof(OwnerMessage))]
[ProtoInclude(10, typeof(SessionMessage))]
[ProtoInclude(11, typeof(DespawnMessage))]
[ProtoInclude(12, typeof(InputAxisMessage))]
[ProtoInclude(13, typeof(RequestRoomsMessage))]
[ProtoInclude(14, typeof(RoomsMessage))]
[ProtoInclude(15, typeof(WorkerInfoMessage))]
[ProtoInclude(16, typeof(WorkerDebugMessage))]
[ProtoInclude(17, typeof(SyncFieldMessage))]
public abstract class QTMessage {

    public enum type { EVENT, SYNC, HEARTBEAT, REQUEST_HEARTBEAT, INPUT, SPAWN, READY, REQUEST_SYNC, OWNER, SESSION, DESPAWN, INPUT_AXIS, REQUEST_ROOMS, ROOMS, WORKER_INFO, WORKER_DEBUG, SYNC_FIELD }

    [ProtoMember(1001)]
    public type messageType;

}
