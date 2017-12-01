using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;

public class PhoenixSocketConnector : Connector {
  private WebSocketConnector Connector;
  private Action<Package> receiveCallback;
  private Action<Connector> connectCallback;
//  private Dictionary<string, Channel> openChannels;

  public PhoenixSocketConnector(WebSocketConnector connector) {
    Connector = connector;
    Connector.onConnected(onConnectCallback);
    Connector.receive(onReceiveCallback);
  }

  public IEnumerator connect(string address) {
    return Connector.connect(address);
  }

  public void onConnected(Action<Connector> onConnect) {
    Debug.Log("on connected");
    connectCallback += onConnect;
  }

  private void onConnectCallback(WebSocketConnector _) {
    if (connectCallback != null) {
      connectCallback(this);
    }
  }

  private void onReceiveCallback(string message) {
    Debug.Log(message);
    if (receiveCallback != null) {
      receiveCallback(convert(message));
    }
  }

  private PhoenixSocketPackage convert(string message) {
    return new PhoenixSocketPackage(message);
  }

  public void send(Package package) {
    string serializedMessage = package.serialize();
    Connector.send(serializedMessage);
//    Channel channel = getChannel(message.channel);
//    channel.Push(message.eventName, message.payload);

    Debug.Log("send message to phoenix: " + serializedMessage);
  }

  public void receive(Action<Package> onReceive) {
    Debug.Log("set receive handler");
    receiveCallback += onReceive;
  }

  public void disconnect() {
    Connector.disconnect();
  }

//  private Channel getChannel(string channelName) {
//    Channel channel;
//    if (openChannels.TryGetValue(channelName, out channel)) {
//      return channel;
//    } else {
//      return joinChannel(channelName);
//    }
//  }

//  private Channel joinChannel(string channelName) {
//    Channel channel = socket.MakeChannel(channelName);
//    channel.Join();
//    // TODO error handling
//
//    openChannels.Add(channelName, channel);
//    return channel;
//  }
}

