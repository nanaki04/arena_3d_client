using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using Arena.Presentation;

namespace Arena.View {

  public class PhoenixWebSocketConnection : MonoBehaviour {
    public const string serverAddress = "ws://localhost:4000/socket/websocket?vsn=1.0.0";

    private Connector connector;
    private LocatorEvent locatorEvent;

    public IEnumerator Start() {
      var domainRoot = gameObject.AddComponent<DomainRoot>();
      domainRoot.domain = "package";

      locatorEvent = gameObject.AddComponent<LocatorEvent>();
      locatorEvent.address = "receive";

      var webSocketConnector = gameObject.AddComponent<WebSocketConnector>() as WebSocketConnector;
      connector = new PhoenixSocketConnector(webSocketConnector);
      connector.onConnected(onConnect);
      connector.receive(onReceive);
      enabled = false;
      yield return connector.connect(serverAddress);
    }

    public void Update() {
      locatorEvent.address = "poll";
      locatorEvent.fireEvent();
    }

    public void Send(Package package) {
      connector.send(package);
    }

    private void onReceive(Package package) {
      enabled = true;
      var phoenixMessage = package.parse<PhoenixSocketMessage<PhoenixEventPayload>>();
      if (phoenixMessage.@event == "phx_reply") {
        Debug.Log(phoenixMessage.payload.status);
        return;
      }
      if (phoenixMessage.@event == "phx_error") {
        return;
      }
      var eventParameter = new EventParameter.PhoenixPackage((PhoenixSocketPackage)package);
      locatorEvent.address = "receive";
      locatorEvent.eventParameters = new EventParameters() + eventParameter;
      locatorEvent.fireEvent();
    }

    private void onConnect(Connector phoenixSocketConnector) {
      // TODO move to presentation layer
      var phoenixSocketMessage = new PhoenixSocketMessage<PhoenixSocketPayload>(
        "lobby:global",
        "phx_join",
        new PhoenixSocketPayload()
      );
      var package = new PhoenixSocketPackage();
      package.pack<PhoenixSocketMessage<PhoenixSocketPayload>>(phoenixSocketMessage);
      phoenixSocketConnector.send(package);
    }

  }

}
