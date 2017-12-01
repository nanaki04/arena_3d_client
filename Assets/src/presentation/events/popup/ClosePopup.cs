using System;

namespace Arena.Presentation {

  [Serializable]
  public class ClosePopupEvent : Event {
    public static string Type = (typeof(ClosePopupEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(ClosePopupEvent.Type);
    public static EventHandler Stock = new EventHandler.Stock(ClosePopupEvent.Type);
    public PopupType PopupToClose;
    public ClosePopupEvent(EventId eventId, PopupType popupType) : base(eventId) {
      PopupToClose = popupType;
    }
  }

  public class ClosePopupEventImplementation : EventImplementation {
    public ClosePopupEventImplementation() {
      Composition = EventComposition.EventComposer
        + UpdateState
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      var popupType = PopupType.Undefined;
      if (eventParameters.Parameters.Count > 0) {
        popupType = (eventParameters.Parameters[0] as EventParameter.Popup).Val;
      }
      return new ClosePopupEvent(eventId, popupType);
    }

    private State UpdateState(State state) {
      var evnt = Store.GetProcessing(state) as ClosePopupEvent;
      var popupType = evnt.PopupToClose;
      if (popupType == PopupType.Undefined) {
        return Store.RemoveLastOpenedPopup(state);
      }
      return Store.RemoveOpenedPopup(popupType, state);
    }

  }

}
