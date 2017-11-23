namespace Arena.Presentation {

  public class ShowMailEvent : Event {
    public static string Type = (typeof(ShowMailEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(ShowMailEvent.Type);
    public ShowMailEvent(EventId eventId) : base(eventId) {}
  }

  public class ShowMailEventImplementation : EventImplementation {
    public ShowMailEventImplementation() {
      Actions = Actions
        + PrintMessage;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      return new ShowMailEvent(eventId);
    }

    private State PrintMessage(State state) {
      var renderCommand = new RenderCommand.Print("54");
      return Store.PushRenderCommand(renderCommand, state);
    }
  }

}
