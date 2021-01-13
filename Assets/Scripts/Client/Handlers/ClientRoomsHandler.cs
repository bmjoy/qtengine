using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClientRoomsHandler : BaseMessageHandler {

    public ClientRoomsHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.ROOMS:
                QTClient qtClient = (QTClient)client;
                RoomsMessage roomsMessage = (RoomsMessage)message;

                ClientManager.instance.rooms = roomsMessage.rooms;
                ClientManager.instance.onRoomsUpdated();
                break;
        }
    }
}
