using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerQTObject : BaseQTObject {

    public ServerQTObject() : base(type.SERVER) {

    }

    public void setServerOwner(string id) {
        setOwner(id);

        OwnerMessage message = new OwnerMessage();
        message.objectID = objectID;
        message.ownerID = ownerID;

        WorkerServerManager.instance.sendMessageToAll(message);
    }

}
