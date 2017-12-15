using System;

namespace Arena.Presentation {

  [Serializable]
  public class OpenPopupEvent : Event {
    public static string Type = (typeof(OpenPopupEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(OpenPopupEvent.Type);
    public static EventHandler Stock = new EventHandler.Stock(OpenPopupEvent.Type);
    public PopupType PopupToOpen;
    public OpenPopupEvent(EventId eventId, PopupType popupType) : base(eventId) {
      PopupToOpen = popupType;
    }
  }

  public class OpenPopupEventImplementation : EventImplementation {
    public OpenPopupEventImplementation() {
      Composition = EventComposition.EventComposer
        + UpdateState
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.Generate();
      var popupType = (eventParameters[EventParameterType.Popup] as EventParameter.Popup).Val;
      return new OpenPopupEvent(eventId, popupType);
    }

    private State UpdateState(State state) {
      var evnt = Store.GetProcessing(state) as OpenPopupEvent;
      return Store.PushOpenPopup(evnt.PopupToOpen, state);
    }

  }

}
