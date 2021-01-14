using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQTObjectComponent : MonoBehaviour {

    [HideInInspector]
    public BaseQTObject obj;

    public ClientQTObjectComponent clientComponent;
    public ServerQTObjectComponent serverComponent;

    void Update() {
        if (obj.objectType == BaseQTObject.type.SERVER) {
            handleServerUpdate();
        } else {
            handleClientUpdate();
        }

        handleUpdate();
    }

    public void handleSpawn() {
        if (obj.objectType == BaseQTObject.type.SERVER) {
            serverComponent = new ServerQTObjectComponent(this);
            handleServerObjectSpawn();

            obj.onOwnerChanged += handleServerOwnerChange;

            InvokeRepeating("handleServerSync", 0f, ServerSettings.instance.syncRate);
        } else {
            clientComponent = new ClientQTObjectComponent(this);
            handleClientObjectSpawn();

            obj.onOwnerChanged += handleClientOwnerChange;
        }

        obj.onOwnerChanged += handleOwnerChange;
        handleObjectSpawn();
    }

    public virtual void handleClientObjectSpawn() {

    }
    public virtual void handleServerObjectSpawn() {

    }
    public virtual void handleObjectSpawn() {

    }

    public virtual void handleClientUpdate() {

    }
    public virtual void handleServerUpdate() {

    }
    public virtual void handleUpdate() {

    }

    public virtual void handleClientOwnerChange(string oldOwnerID, string newOwnerID) {

    }
    public virtual void handleServerOwnerChange(string oldOwnerID, string newOwnerID) {

    }
    public virtual void handleOwnerChange(string oldOwnerID, string newOwnerID) {

    }

    public void handleServerSync() {
        serverComponent.syncFields();
    }
}
