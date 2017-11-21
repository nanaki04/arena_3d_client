using UnityEngine;
using System.Collections;
using System;

public class WebsocketConnector : MonoBehaviour, Connector<string> {
  private WebSocket ws;
  private Action<Package<string>> receiveCallback;
  private Action<Connector<string>> connectCallback;

  public IEnumerator connect(string address) {
    Debug.Log("connected");
    ws = new WebSocket(new Uri(address));
    yield return StartCoroutine(ws.Connect());

    if (connectCallback != null) {
      connectCallback(this);
    }

    while (true) {
      string reply = ws.RecvString();

      if (ws.error != null) {
        Debug.Log("Websocket error: " + ws.error);
        break;
      }

      if (reply != null && receiveCallback != null) {
        WebsocketPackage package = new WebsocketPackage();
        package.receive(reply);
        receiveCallback(package);
      }

      yield return 0;
    }

    ws.Close();
  }

  public void onConnected(Action<Connector<string>> onConnect) {
    connectCallback += onConnect;
  }

  public void send(Package<string> message) {
    Debug.Log("send message: " + message.serialize());
    ws.SendString(message.serialize());
  }

  public void receive(Action<Package<string>> onReceive) {
    Debug.Log("set receive handler");
    receiveCallback += onReceive;
  }

  public void disconnect() {
    Debug.Log("disconnected");
  }
}
