using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkStorage : MonoBehaviour {

    public Dictionary<string, GameObject> syncedGameobjects;
    public List<GameObject> prefabs;

    public NetworkStorage() {
        syncedGameobjects = new Dictionary<string, GameObject>();
    }
}
