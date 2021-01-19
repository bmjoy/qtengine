using System;
using System.IO;
using System.Runtime.Serialization;
using ProtoBuf;

[ProtoContract]
[ProtoInclude(1, typeof(EventMessage))]
[ProtoInclude(3, typeof(InputMessage))]
[ProtoInclude(5, typeof(SyncPositionMessage))]
[ProtoInclude(6, typeof(SpawnMessage))]
[ProtoInclude(7, typeof(ReadyMessage))]
[ProtoInclude(8, typeof(RequestSyncMessage))]
[ProtoInclude(9, typeof(OwnerMessage))]
[ProtoInclude(10, typeof(SessionMessage))]
[ProtoInclude(11, typeof(DespawnMessage))]
[ProtoInclude(12, typeof(InputAxisMessage))]
[ProtoInclude(13, typeof(InputAxisMessage))]
[ProtoInclude(14, typeof(WorkerInfoMessage))]
[ProtoInclude(15, typeof(WorkerDebugMessage))]
[ProtoInclude(16, typeof(FieldInfoMessage))]
[ProtoInclude(17, typeof(CallFunctionMessage))]
[ProtoInclude(18, typeof(RoomInfoMessage))]
[ProtoInclude(19, typeof(WorkerReadyMessage))]
[ProtoInclude(20, typeof(AnimationParameterInfoMessage))]
[ProtoInclude(21, typeof(SyncRotationMessage))]
[ProtoInclude(22, typeof(ClientInfoMessage))]
[ProtoInclude(23, typeof(VRActionMessage))]
[ProtoInclude(24, typeof(SyncActiveMessage))]
[ProtoInclude(25, typeof(RequestNewInstanceMessage))]
[ProtoInclude(26, typeof(InstanceMessage))]
[ProtoInclude(27, typeof(QTResponsableMessage))]
public abstract class QTMessage {

    public enum type {
        EVENT, SYNC_POSITION, SYNC_ROTATION, HEARTBEAT, REQUEST_HEARTBEAT, INPUT, SPAWN, READY, REQUEST_SYNC, OWNER, SESSION, DESPAWN, INPUT_AXIS, REQUEST_ROOMS, ROOMS, WORKER_INFO, WORKER_DEBUG, SYNC_FIELD, CALL_FUNCTION, ROOM_INFO, WORKER_READY, SYNC_ANIMATION, CLIENT_INFO,
        VR_ACTION, SYNC_VR_POSITION, SYNC_VR_ROTATION, SYNC_ACTIVE, REQUEST_NEW_INSTANCE, INSTANCE, REGISTER, LOGIN, LOGIN_REPLY, REGISTER_REPLY
    }

    [ProtoMember(1001)]
    public type messageType;

}
