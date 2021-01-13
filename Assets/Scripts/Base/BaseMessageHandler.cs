using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseMessageHandler {

    public BaseQTClient client;

    public BaseMessageHandler(BaseQTClient _client) {
        client = _client;
        client.onMessageRecieved += handleMessage;
    }

    public abstract void handleMessage(QTMessage message);
}
