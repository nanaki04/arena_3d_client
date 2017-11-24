namespace Arena.Presentation {

  public class ShowMailEvent : Event {
    public static string Type = (typeof(ShowMailEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(ShowMailEvent.Type);
    public ShowMailEvent(EventId eventId) : base(eventId) {}
  }

  public class ShowMailEventImplementation : EventImplementation {
    public ShowMailEventImplementation() {
      Composition = EventComposition.EventComposer
        + PrintMessage
        + Append;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      return new ShowMailEvent(eventId);
    }

    private State PrintMessage(State state) {
      //var renderCommand = new RenderCommand.Print("54");
      //return Store.PushRenderCommand(renderCommand, state);
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
