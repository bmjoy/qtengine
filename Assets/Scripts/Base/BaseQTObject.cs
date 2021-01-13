using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseQTObject : MonoBehaviour {

    public enum type { CLIENT, SERVER }

    public Dictionary<int, BaseQTObjectComponent> objectComponents;
    public string objectID;
    public string ownerID;
    public string prefabName;
    public type objectType;

    public Action<string, string> onOwnerChanged;

    public BaseQTObject(type _objectType) {
        objectType = _objectType;
        objectComponents = new Dictionary<int, BaseQTObjectComponent>();

        onOwnerChanged += debugOwnerChange;
    }

    public void setOwner(string id) {
        string oldID = ownerID;
        ownerID = id;

        onOwnerChanged(oldID, id);
    }

    public void debugOwnerChange(string oldOwnerID, string newOwnerID) {

    }
}
