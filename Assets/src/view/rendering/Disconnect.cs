using UnityEngine;
using UnityEngine.UI;
using Arena.Presentation;

namespace Arena.View {

  public static class Disconnect {

    public static void Render(GameObject gameObject, RenderCommand renderCommand) {
      var connection = GameObject.Find("connection");
      if (connection != null) {
        Object.Destroy(connection);
      }
    }

  }

}
