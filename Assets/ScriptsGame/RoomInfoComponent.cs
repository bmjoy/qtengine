using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomInfoComponent : MonoBehaviour {

    public RoomInfo room;
    public Text roomID;
    public Text port;

    public void updateRoom(RoomInfo _room) {
        room = _room;

        roomID.text = room.id;
        port.text = room.port.ToString();
    }

    public void joinRoom() {
        ClientManager.instance.setupWorkerClient(room.ip, room.port);
    }
}
