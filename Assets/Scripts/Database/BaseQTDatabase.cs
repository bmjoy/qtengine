using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using StackExchange.Redis;

public class BaseQTDatabase {

    public ConnectionMultiplexer muxer;
    public IDatabase conn;

    public BaseQTDatabase() {

    }

    public void connect() {
        ConfigurationOptions options = new ConfigurationOptions {
            AbortOnConnectFail = false,
            EndPoints = { "127.0.0.1:6397" },
            Password = "testingpassword"
        };

        muxer = ConnectionMultiplexer.Connect(options);
        conn = muxer.GetDatabase();
    }

    public void close() {
        if (muxer != null) {
            muxer.Close();
        }
    }
}
