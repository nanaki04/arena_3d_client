using Arena.Modules;

namespace Arena.Presentation {

  public enum PopupLogLevel {
    None,
    Full
  }

  public class PopupLoggerPlug : StorePlug {
    static PopupLogLevel LogLevel = PopupLogLevel.Full;
    State stateBefore;

    public State TransformOnLoad(State state) {
      stateBefore = state;
      return state;
    }

    public State TransformOnSave(State state) {
      LogPopupState(state);
      return state;
    }

    private void LogPopupState(State stateAfter) {
      if (LogLevel == PopupLogLevel.None) {
        return;
      }

      var popupsBefore = Store.GetOpenPopups(stateBefore);
      var popupsAfter = Store.GetOpenPopups(stateAfter);

      for (int i = 0; i < popupsAfter.Count; i++) {
        if (i >= popupsBefore.Count) {
          Logger.Log("Popup added: " + popupsAfter[i].ToString());
          return;
        }
        if (popupsBefore[i] != popupsAfter[i]) {
          Logger.Log("Popup changed: " + popupsAfter[i].ToString());
        }
      }
    }

  }

}
