using UnityEngine;
using Arena.Modules;

namespace Arena.Presentation {

  public class ShowMailEvent : Event {
    public static string Type = (typeof(ShowMailEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(ShowMailEvent.Type);
    public ShowMailEvent(EventId eventId) : base(eventId) {}
  }

  public class ShowMailEventImplementation : EventImplementation {
    public ShowMailEventImplementation() {
      Composition = EventComposition.EventComposer
        + Store.Curry<PopupType>(Store.PushOpenPopup, PopupType.Dummy)
        + PrintMessage
        + Append
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      return new ShowMailEvent(eventId);
    }

    private State OpenPopup(State state) {
      return Store.PushOpenPopup(PopupType.Dummy, state);
    }

    private State PrintMessage(State state) {
      return Store.UpdateMe("hi ", state);
    }

    private State Append(State state) {
      var me = Store.GetMe(state);
      var txt = me + "lol";
      var renderCommand = new RenderCommand.Print(txt);
      return Store.PushRenderCommand(renderCommand, state);
    }
  }

}
