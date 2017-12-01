using System;
using Arena.Modules;

namespace Arena.Presentation {

  [Serializable]
  public class LoginEvent : Event {
    public static string Type = (typeof(LoginEvent)).ToString();
    public static EventHandler Stock = new EventHandler.Stock(LoginEvent.Type);
    public string PlayerId;
    public LoginEvent(EventId eventId, string playerId) : base(eventId) {
      PlayerId = playerId;
    }
  }

  public class LoginEventImplementation : EventImplementation {
    public LoginEventImplementation() {
      Composition = EventComposition.EventComposer
        + SetMyId
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      if (eventParameters.Parameters.Count == 0) {
        return new LoginEvent(eventId, "");
      }
      var id = eventParameters.Parameters[0] as EventParameter.Id;
      return new LoginEvent(eventId, id.Val);
    }

    private State SetMyId(State state) {
      var evnt = Store.GetProcessing(state) as LoginEvent;
      return Store.UpdateMe(evnt.PlayerId, state);
    }
  }

}
