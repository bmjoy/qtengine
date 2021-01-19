using System;
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
            EndPoints = { "127.0.0.1:6379" },
            Password = "testingpassword"
        };

        muxer = ConnectionMultiplexer.Connect(options);
        conn = muxer.GetDatabase();

        QTDebugger.instance.debug(QTDebugger.debugType.BASE, "Connected to a Redis database...");
    }

    public void close() {
        if (muxer != null) {
            muxer.Close();
        }
    }

}
