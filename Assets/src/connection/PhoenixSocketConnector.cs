using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;

public class PhoenixSocketConnector : MonoBehaviour, Connector<PhoenixSocketMessage> {
  private WebSocket socket;
  private Action<Package<PhoenixSocketMessage>> receiveCallback;
  private Action<Connector<PhoenixSocketMessage>> connectCallback;
//  private Dictionary<string, Channel> openChannels;

  public IEnumerator connect(string address) {
    Debug.Log("connected");
    socket = new WebSocket(new Uri(address));
    yield return StartCoroutine(socket.Connect());

    if (connectCallback != null) {
      connectCallback(this);
    }

    while (true) {
      string reply = socket.RecvString();

      if (socket.error != null) {
        Debug.Log("Websocket error: " + socket.error);
        break;
      }

      if (reply != null && receiveCallback != null) {
        onMessage(reply);
      }

      yield return 0;
    }

    socket.Close();
  }

  public void onConnected(Action<Connector<PhoenixSocketMessage>> onConnect) {
    Debug.Log("on connected");
    connectCallback += onConnect;
  }

  public void send(Package<PhoenixSocketMessage> package) {
    string message = package.serialize();
    socket.SendString(message);
//    Channel channel = getChannel(message.channel);
//    channel.Push(message.eventName, message.payload);

    Debug.Log("send message to phoenix: " + message);
  }

  public void sendTo(string path, Package<PhoenixSocketMessage> message) {
  }

  public void receive(Action<Package<PhoenixSocketMessage>> onReceive) {
    Debug.Log("set receive handler");
    receiveCallback += onReceive;
  }

  public void disconnect() {
    socket.Close();
  }

  private void onOpen(object sender, EventArgs args) {
    if (connectCallback != null) {
      connectCallback(this);
    }
  }

  private void onClose() {
  }

  private void onError() {
  }

  private void onMessage(string reply) {
    Debug.Log("phoenix socket receive: " + reply);
    if (receiveCallback != null) {
//      PhoenixSocketPackage package = new PhoenixSocketPackage(msg);
//      receiveCallback(package);
    }
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

