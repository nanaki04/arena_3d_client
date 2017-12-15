using UnityEngine;

namespace Arena.View {

  public class Caller : MonoBehaviour {

    const string version = "1.0.0";
    const string serverAddress = "ws://localhost:4000/socket/websocket";
    public string ServerAddress;
    PhoenixWebSocketConnection connection;

    public void ConnectAs(string name) {
      if (connection != null) {
        Destroy(connection);
      }
      ServerAddress = serverAddress + "?vsn=" + version + "&duelist=" + name;
      connection = gameObject.AddComponent<PhoenixWebSocketConnection>();
    }

    public void ConnectAs(string name, string token) {
      if (connection != null) {
        Destroy(connection);
      }
      ServerAddress = serverAddress + "?vsn=" + version + "&duelist=" + name + "&token=" + token;
      connection = gameObject.AddComponent<PhoenixWebSocketConnection>();
    }

    public void Disconnect() {
      Destroy(connection);
      connection = null;
    }
  }

}
