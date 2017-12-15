using UnityEngine;
using UnityEngine.UI;
using Arena.Presentation;

namespace Arena.View {

  public static class Connect {

    public static void Render(GameObject gameObject, RenderCommand renderCommand) {
      var connectCommand = (renderCommand as RenderCommand.Connect);
      var identification = connectCommand.Identification;
      var connection = new GameObject("connection");
      var caller = connection.AddComponent<Caller>();

      if (connectCommand.Token == null) {
        caller.ConnectAs(identification);
        return;
      }

      caller.ConnectAs(identification, connectCommand.Token);
    }

  }

}
