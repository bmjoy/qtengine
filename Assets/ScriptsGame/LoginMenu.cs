using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginMenu : MonoBehaviour {

    public InputField username;
    public InputField password;

    public void login() {
        LoginMessage message = new LoginMessage();
        message.username = username.text;
        message.password = password.text;
        message.onResponse += (QTResponsableMessage message) => {
            LoginReplyMessage reply = (LoginReplyMessage)message;

            Debug.Log("Logged in with ID - " + reply.user.id);
        };

        ClientManager.instance.masterClient.sendMessage(message);
    }

}
