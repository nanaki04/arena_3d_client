namespace Arena.Presentation {

  public class DebugEvent : Event {
    public static string Type = (typeof(DebugEvent)).ToString();
    public string Message;

    public DebugEvent(EventId eventId, string message) : base(eventId) {
      Message = message;
    }
  }

  public class DebugEventImplementation : EventImplementation {
    public DebugEventImplementation() {
      Composition = EventComposition.EventComposer
        + PrintMessage;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      return new DebugEvent(eventId, "TODO");
    }

    private State PrintMessage(State state) {
      var evnt = Store.GetProcessing(state) as DebugEvent;
      var renderCommand = new RenderCommand.Print(evnt.Message);
      return Store.PushRenderCommand(renderCommand, state);
    }
  }

}
