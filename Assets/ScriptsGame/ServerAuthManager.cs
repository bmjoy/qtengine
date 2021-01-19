using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StackExchange.Redis;

public class ServerAuthManager {

    public BaseServerManager manager;

    public ServerAuthManager(BaseServerManager _manager) {
        manager = _manager;
    }

    public RegisterReplyMessage addUser(RegisterMessage message, UserInfo user) {
        RegisterReplyMessage replyMessage = new RegisterReplyMessage(message);

        if (manager.database.conn.KeyExists("usernames:" + user.username)) {
            replyMessage.reply = RegisterReplyMessage.registerReply.USEREXISTS;
            return replyMessage;
        }

        string id = Guid.NewGuid().ToString();
        HashEntry[] entries = {
            new HashEntry("id", id),
            new HashEntry("username", user.username),
            new HashEntry("password", user.password),
            new HashEntry("permissions", (int)user.permissions)
        };
        manager.database.conn.HashSet("user:" + id, entries);

        HashEntry[] entries2 = {
            new HashEntry("username", user.username),
            new HashEntry("id", id)
        };
        manager.database.conn.HashSet("usernames:" + user.username, entries2);

        replyMessage.reply = RegisterReplyMessage.registerReply.SUCCESS;
        replyMessage.user = user;
        return replyMessage;
    }

    public LoginReplyMessage loginUser(LoginMessage message) {
        LoginReplyMessage replyMessage = new LoginReplyMessage(message);

        UserInfo user = getUser(message.username);
        if(user == null) {
            replyMessage.reply = LoginReplyMessage.loginReply.NOUSER;
        } else if(message.password != user.password) {
            replyMessage.reply = LoginReplyMessage.loginReply.WRONGPASSWORD;
        } else {
            replyMessage.user = user;
            replyMessage.reply = LoginReplyMessage.loginReply.SUCCESS;
        }

        return replyMessage;
    }

    public UserInfo getUser(string username) {
        if (manager.database.conn.KeyExists("user:" + username) == false) {
            return null;
        }

        string id = manager.database.conn.HashGet("usernames:" + username, "id");
        string password = manager.database.conn.HashGet("user:" + id, "password");

        UserInfo user = new UserInfo();
        user.username = username;
        user.password = password;

        return user;
    }
}
