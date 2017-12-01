using UnityEngine;
using UnityEngine.UI;
using Arena.Presentation;

namespace Arena.View {

  public static class SendPhoenixPackage {

    public static void Render(GameObject gameObject, RenderCommand renderCommand) {
      Package package = (renderCommand as RenderCommand.SendPhoenixPackage).dataPackage;
      PhoenixWebSocketConnection connection = gameObject.GetComponent<PhoenixWebSocketConnection>();
      if (connection == null) {
        Debug.Log("Tring to send a package on a gameObject without PhoenixWebSocketConnection component");
        return;
      }
      connection.Send(package);
    }

  }

}
