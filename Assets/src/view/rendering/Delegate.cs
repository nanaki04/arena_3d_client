using UnityEngine;
using UnityEngine.UI;
using Arena.Presentation;

namespace Arena.View {

  public static class Delegate {

    public static void Render(GameObject gameObject, RenderCommand renderCommand) {
      var operatorComponent = gameObject.GetComponent<Operator>();
      if (operatorComponent == null) {
        Debug.Log("Tring to delegate an event, but no operator in place to relay the message");
        return;
      }
      operatorComponent.Delegate();
    }

  }

}
