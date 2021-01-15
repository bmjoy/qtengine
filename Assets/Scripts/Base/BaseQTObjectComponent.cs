using System;
using System.Reflection;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseQTObjectComponent : MonoBehaviour {

    [HideInInspector]
    public BaseQTObject obj;
    [HideInInspector]
    public int index;

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

    void OnTriggerEnter(Collider other) {
        if (obj.objectType == BaseQTObject.type.SERVER) {
            handleServerTriggerEnter(other);
        } else {
            handleClientTriggerEnter(other);
        }

        handleTriggerEnter(other);
    }

    void OnTriggerExit(Collider other) {
        if (obj.objectType == BaseQTObject.type.SERVER) {
            handleServerTriggerExit(other);
        } else {
            handleClientTriggerExit(other);
        }

        handleTriggerExit(other);
    }

    void OnTriggerStay(Collider other) {
        if (obj.objectType == BaseQTObject.type.SERVER) {
            handleServerTriggerStay(other);
        } else {
            handleClientTriggerStay(other);
        }

        handleTriggerStay(other);
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

    public virtual void handleClientTriggerEnter(Collider other) {

    }
    public virtual void handleServerTriggerEnter(Collider other) {

    }
    public virtual void handleTriggerEnter(Collider other) {

    }

    public virtual void handleClientTriggerExit(Collider other) {

    }
    public virtual void handleServerTriggerExit(Collider other) {

    }
    public virtual void handleTriggerExit(Collider other) {

    }

    public virtual void handleClientTriggerStay(Collider other) {

    }
    public virtual void handleServerTriggerStay(Collider other) {

    }
    public virtual void handleTriggerStay(Collider other) {

    }

    public void handleServerSync() {
        serverComponent.syncFields();
    }

    public void callFunction(string functionName) {
        foreach (MethodInfo mi in GetType().UnderlyingSystemType.GetMethods().Where(mi => mi.Name == functionName)) {
            QTDebugger.instance.debug(QTDebugger.debugType.NETWORK, "Calling function - " + functionName);

            object[] parameters = { };
            mi.Invoke(this, parameters);
        }
    }
}
