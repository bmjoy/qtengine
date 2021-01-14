using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SampleMenu : MonoBehaviour {

    public UISwitch roomsSwitch;
    public Transform roomsParent;
    public GameObject roomItem;

    private List<GameObject> roomObjects;

    void Start() {
        roomObjects = new List<GameObject>();

        ClientManager.instance.onMasterConnected += switchToRoomsUI;
        ClientManager.instance.onRoomsUpdated += updateRooms;
    }

    public void switchToRoomsUI(QTClient client) {
        roomsSwitch.process();
    }

    public void updateRooms() {
        foreach(GameObject roomObject in roomObjects) {
            Destroy(roomObject);
        }

        foreach(RoomInfo room in ClientManager.instance.rooms.Values) {
            GameObject roomObject = Instantiate(roomItem);
            roomObject.transform.SetParent(roomsParent);
            RoomInfoComponent roomInfoComponent = roomObject.GetComponent<RoomInfoComponent>();
            roomInfoComponent.updateRoom(room);

            roomObjects.Add(roomObject);
        }
    }
}
