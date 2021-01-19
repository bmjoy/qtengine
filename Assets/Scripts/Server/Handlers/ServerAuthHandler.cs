using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class ServerAuthHandler : BaseMessageHandler {

    public ServerAuthHandler(BaseQTClient _client) : base(_client) {

    }

    public override void handleMessage(QTMessage message) {
        switch (message.messageType) {
            case QTMessage.type.REGISTER:
                RegisterMessage registerMessage = (RegisterMessage)message;
                UserInfo info = new UserInfo();
                info.username = registerMessage.username;
                info.password = registerMessage.password;
                info.permissions = UserInfo.userPermissions.USER;

                RegisterReplyMessage replyMessage = MasterServerManager.instance.authManager.addUser(registerMessage, info);
                client.sendMessage(replyMessage);
                break;

            case QTMessage.type.LOGIN:
                LoginMessage loginMessage = (LoginMessage)message;

                LoginReplyMessage loginReplyMessage = MasterServerManager.instance.authManager.loginUser(loginMessage);
                client.sendMessage(loginReplyMessage);
                break;
        }
    }
}
