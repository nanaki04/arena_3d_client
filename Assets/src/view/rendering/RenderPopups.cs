using UnityEngine;
using UnityEngine.UI;
using Arena.Presentation;

namespace Arena.View {

  public static class RenderPopups {

    public static void Render(GameObject gameObject, RenderCommand renderCommand) {
      var openPopups = (renderCommand as RenderCommand.RenderPopups).OpenPopups;
      PopupHandler popupHandler = gameObject.GetComponent<PopupHandler>();
      if (popupHandler == null) {
        Debug.Log("RenderPopups: No popup handler found");
        return;
      }
      popupHandler.UpdatePopups(openPopups);
    }

  }

}
