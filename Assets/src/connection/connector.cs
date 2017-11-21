using System.Collections;
using System;

public interface Connector<T> {
  IEnumerator connect(string address);
  void onConnected(Action<Connector<T>> onConnect);
  void send(Package<T> message);
  void receive(Action<Package<T>> onReceive);
  void disconnect();
}
