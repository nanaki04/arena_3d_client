using System.Collections;
using System;

public interface Connector {
  IEnumerator connect(string address);
  void onConnected(Action<Connector> onConnect);
  void send(Package package);
  void receive(Action<Package> onReceive);
  void disconnect();
}
