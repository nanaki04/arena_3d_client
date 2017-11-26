using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections.Generic;
using Arena.Presentation;
using Arena.Modules;

namespace Arena.View {

  public class RadioButtonGroup : MonoBehaviour {

    public int defaultButtonIndex = 0;
    public List<GameObject> radioButtons;
    private LocatorEvent locatorEvent;

    public void Start() {
      locatorEvent = this.gameObject.AddComponent<LocatorEvent>();
      locatorEvent.address = "restore_radio_button";
      locatorEvent.eventParameters += new EventParameter.RadioButton(defaultButtonIndex);
      locatorEvent.fireEvent();

      InitButtons();
    }

    public void SelectButton(int index) {
      Debug.Log("select button");
      foreach (GameObject button in radioButtons) {
        var buttonComponent = button.GetComponent<Button>();
        Debug.Log("set interactable");
        buttonComponent.interactable = true;
      }
      var activeButton = radioButtons[index];
      var activeButtonComponent = activeButton.GetComponent<Button>();
      Debug.Log("set inactive");
      activeButtonComponent.interactable = false;
    }

    private void InitButtons() {
      for (int i = 0; i < radioButtons.Count; i++) {
        InitButton(i);
      }
    }

    private void InitButton(int index) {
      var button = radioButtons[index];
      var buttonComponent = button.GetComponent<Button>();
      if (buttonComponent == null) {
        Debug.Log("No button component found on radio button");
        return;
      }
      var buttonCallback = Closure<int>.New(OnButtonTouch, index);
      buttonComponent.onClick.AddListener(new UnityAction(buttonCallback));
    }

    private void OnButtonTouch(int index) {
      locatorEvent.address = "select_radio_button";
      locatorEvent.eventParameters = new EventParameters() + new EventParameter.RadioButton(index);
      locatorEvent.fireEvent();
    }

  }

}
