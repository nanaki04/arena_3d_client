using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Connection : MonoBehaviour {

  private Connector<PhoenixSocketMessage> connector;

  public IEnumerator Start() {
    connector = gameObject.AddComponent<PhoenixSocketConnector>() as PhoenixSocketConnector;
    connector.onConnected(onConnect);
    connector.receive(onReceive);
    Debug.Log("connecting");
    yield return connector.connect("ws://localhost:4000/socket/websocket?vsn=1.0.0");
  }

  private void onReceive(Package<PhoenixSocketMessage> data) {
    PhoenixSocketMessage message = data.parse();
    Debug.Log("received: " + message.@event);
  }

  private void onConnect(Connector<PhoenixSocketMessage> connector) {
    Debug.Log("sending message");

    var payload = new PhoenixSocketPayload();
    payload.hi = "lol";

    PhoenixSocketMessage msg = new PhoenixSocketMessage(
      "lobby:global",
      "phx_join",
      payload
    );

    PhoenixSocketPackage package = new PhoenixSocketPackage(msg);
    connector.send(package);
  }
}
