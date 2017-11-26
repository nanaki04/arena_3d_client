using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public struct UIState {
    public List<PopupType> Popups;
    public ImMap<string, int> RadioButtons;

    public UIState(List<PopupType> popups, ImMap<string, int> radioButtons) {
      Popups = popups;
      RadioButtons = radioButtons;
    }

    public static UIState InitialState() {
      return new UIState(
        new List<PopupType>(),
        Im.Map<int>()
      );
    }
  }

}
