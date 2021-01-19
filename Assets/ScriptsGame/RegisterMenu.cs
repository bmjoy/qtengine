using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RegisterMenu : MonoBehaviour {

    public InputField username;
    public InputField password;

    public void register() {
        RegisterMessage message = new RegisterMessage();
        message.username = username.text;
        message.password = password.text;
        message.onResponse += (QTResponsableMessage message) => {
            RegisterReplyMessage reply = (RegisterReplyMessage)message;

            Debug.Log("Registered with ID - " + reply.user.id);
        };

        ClientManager.instance.masterClient.sendMessage(message);
    }

}
