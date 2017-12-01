using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using Arena.Presentation;

public class Connection : MonoBehaviour {

  private Connector connector;

  public IEnumerator Start() {
    var webSocketConnector = gameObject.AddComponent<WebSocketConnector>() as WebSocketConnector;
    connector = new PhoenixSocketConnector(webSocketConnector);
    connector.onConnected(onConnect);
    connector.receive(onReceive);
    Debug.Log("connecting");
    yield return connector.connect("ws://localhost:4000/socket/websocket?vsn=1.0.0");
  }

  private void onReceive(Package package) {
    var phoenixMessage = package.parse<PhoenixSocketMessage<PhoenixSocketEventPayload>>();
    Debug.Log("received: " + phoenixMessage.@event);
    Debug.Log("payload: " + phoenixMessage.payload.events);
    Debug.Log("topic: " + phoenixMessage.topic);
    Debug.Log("ref: " + phoenixMessage.@ref);

    if (phoenixMessage.payload.events != null) {
      foreach (Evnt evnt in phoenixMessage.payload.events) {
        Debug.Log(evnt);
        Debug.Log(evnt.type);
        Debug.Log(evnt.id);
        Debug.Log(evnt.name);
        Debug.Log(evnt.GetType());
        if (evnt.type == "evnt2") {
          Debug.Log((evnt as Evnt2).time);
        }
      }
    }

    if (phoenixMessage.@event == "phx_reply") {
      var payload = new PhoenixSocketEventPayload();
      payload.status = "ok";
      var evnt1 = new Evnt();
      evnt1.id = "1";
      evnt1.name = "name";
      var evnt2 = new Evnt2();
      evnt2.id = "2";
      evnt2.name = "name1";
      evnt2.time = 1234;
      payload.events = new List<Evnt>() {
        evnt1,
        evnt2
      };

      var msg = new PhoenixSocketMessage<PhoenixSocketEventPayload>(
          "lobby:global",
          "ping",
          payload
      );
      var newPackage = new PhoenixSocketPackage();
      newPackage.pack<PhoenixSocketMessage<PhoenixSocketEventPayload>>(msg);
      connector.send(newPackage); 
    }
  }

  private void onConnect(Connector phoenixSocketConnector) {
    Debug.Log("sending message");

    //var payload = new PhoenixSocketPayload<string>();
    //payload.hi = "lol";
    var payload = new PhoenixSocketMessagePayload();
    payload.message = "lol";

    var msg = new PhoenixSocketMessage<PhoenixSocketMessagePayload>(
      "lobby:global",
      "phx_join",
      payload
    );
    var package = new PhoenixSocketPackage();
    package.pack<PhoenixSocketMessage<PhoenixSocketMessagePayload>>(msg);

    phoenixSocketConnector.send(package);
  }
}
