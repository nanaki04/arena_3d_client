using UnityEngine;
using UnityEngine.UI;
using Arena.Presentation;

public static class Print {

  public static void Render(GameObject gameObject, RenderCommand renderCommand) {
    string text = (renderCommand as RenderCommand.Print).Text;
    Text textComponent = gameObject.GetComponent<Text>();
    if (textComponent == null) {
      Debug.Log("Tring to print {" + text + "} on a gameObject without Text component");
      return;
    }
    textComponent.text = text;
  }

}
