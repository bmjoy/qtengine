using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseEventHandler : BaseMessageHandler {

    public Action<EventMessage> onEvent;

    public BaseEventHandler(BaseQTClient _client) : base(_client) {
        onEvent += debugEvent;
    }

    public override void handleMessage(QTMessage message) {
        switch(message.messageType) {
            case QTMessage.type.EVENT:
                onEvent((EventMessage)message);
                break;
        }
    }

    public void debugEvent(EventMessage message) {
        QTDebugger.instance.debug(QTDebugger.debugType.EVENTS, "Got event of type " + message.eventType);
    }
}
