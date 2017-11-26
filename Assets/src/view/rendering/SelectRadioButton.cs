using UnityEngine;
using UnityEngine.UI;
using Arena.Presentation;

namespace Arena.View {

  public static class SelectRadioButton {

    public static void Render(GameObject gameObject, RenderCommand renderCommand) {
      int index = (renderCommand as RenderCommand.SelectRadioButton).Index;
      RadioButtonGroup radioButtonGroup = gameObject.GetComponent<RadioButtonGroup>();
      if (radioButtonGroup == null) {
        Debug.Log("Tring to select a radio button, but no radio button group component found");
        return;
      }
      radioButtonGroup.SelectButton(index);
    }

  }

}
