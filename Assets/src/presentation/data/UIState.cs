using System.Collections.Generic;

namespace Arena.Presentation {

  public struct UIState {
    public List<PopupType> Popups;

    public UIState(List<PopupType> popups) {
      Popups = popups;
    }

    public static UIState InitialState() {
      return new UIState(
        new List<PopupType>()
      );
    }
  }

}
