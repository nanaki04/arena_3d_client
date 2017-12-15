using System;
using Arena.Modules;

namespace Arena.Presentation {

  [Serializable]
  public class RequestCredentialsEvent : Event {
    public static string Type = (typeof(RequestCredentialsEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(RequestCredentialsEvent.Type);
    public RequestCredentialsEvent(EventId eventId) : base(eventId) {}
  }

  public class RequestCredentialsEventImplementation : EventImplementation {
    public RequestCredentialsEventImplementation() {
      Composition = EventComposition.EventComposer
        + Store.Curry(Store.PushOpenPopup, PopupType.Login)
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.Generate();
      return new RequestCredentialsEvent(eventId);
    }
  }

}
