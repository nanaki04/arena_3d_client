using System;
using Arena.Modules;

namespace Arena.Presentation {

  [Serializable]
  public class LoginEvent : Event {
    public static string Type = (typeof(LoginEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(LoginEvent.Type);
    public string PlayerId;
    public string Password;
    public LoginEvent(EventId eventId, string playerId, string password) : base(eventId) {
      PlayerId = playerId;
      Password = password;
    }
  }

  public class LoginEventImplementation : EventImplementation {
    public LoginEventImplementation() {
      Composition = EventComposition.EventComposer
        + SetMyId
        + Store.PushProcessingEventToOutbox
        + Store.RemoveLastOpenedPopup
        + PushConnectRenderCommand
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.Generate();
      if (eventParameters.Parameters.Count < 2) {
        return new LoginEvent(eventId, "", "");
      }
      var id = eventParameters[EventParameterType.Id] as EventParameter.Id;
      var pw = eventParameters[EventParameterType.Password] as EventParameter.Password;
      return new LoginEvent(eventId, id.Val, pw.Val);
    }

    private State SetMyId(State state) {
      var evnt = Store.GetProcessing(state) as LoginEvent;
      return Store.UpdateMe(evnt.PlayerId, state);
    }

    private State PushConnectRenderCommand(State state) {
      var evnt = Store.GetProcessing(state) as LoginEvent;
      var renderCommand = new RenderCommand.Connect(evnt.PlayerId);
      return Store.PushRenderCommand(renderCommand, state);
    }
  }

}
