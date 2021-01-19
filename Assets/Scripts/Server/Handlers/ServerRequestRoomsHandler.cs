using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerRequestRoomsHandler : BaseMessageHandler {

    public ServerRequestRoomsHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.REQUEST_ROOMS:
                RequestRoomsMessage requestMessage = (RequestRoomsMessage)message;
                MasterServerQTClient qtRemoteClient = (MasterServerQTClient)client;

                RoomsMessage roomsMessage = new RoomsMessage(requestMessage);
                roomsMessage.rooms = MasterServerManager.instance.workersManager.getRooms();
                qtRemoteClient.sendMessage(roomsMessage);
                break;
        }
    }

}
