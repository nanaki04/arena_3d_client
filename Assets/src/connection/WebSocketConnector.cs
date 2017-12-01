using UnityEngine;
using System.Collections;
using System.Net;
using System;

public class WebSocketConnector : MonoBehaviour {
  WebSocket socket;
  Action<string> receiveCallback;
  Action<WebSocketConnector> connectCallback;

  public IEnumerator connect(string address) {
    socket = new WebSocket(new Uri(address));
    yield return StartCoroutine(socket.Connect());

    if (connectCallback != null) {
      connectCallback(this);
    }

    while (true) {
      string reply = socket.RecvString();

      if (socket.error != null) {
        Debug.Log("WebSocket error: " + socket.error);
        break;
      }

      if (reply != null) {
        onMessage(reply);
      }

      yield return 0;
    }

    socket.Close();
  }

  public void onConnected(Action<WebSocketConnector> onConnect) {
    connectCallback += onConnect;
  }

  public void send(string message) {
    socket.SendString(message);
  }

  public void receive(Action<string> onReceive) {
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
    // TODO
  }

  private void onError() {
    // TODO
  }

  private void onMessage(string reply) {
    if (receiveCallback != null) {
      receiveCallback(reply);
    }
  }

}
