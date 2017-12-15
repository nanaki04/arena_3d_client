using System;

namespace Arena.Presentation {

  [Serializable]
  public class PopupOverlayTickEvent : Event {
    public static string Type = (typeof(PopupOverlayTickEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(PopupOverlayTickEvent.Type);
    public static EventHandler Stock = new EventHandler.Stock(PopupOverlayTickEvent.Type);
    public PopupOverlayTickEvent(EventId eventId) : base(eventId) {}
  }

  public class PopupOverlayTickEventImplementation : EventImplementation {
    public PopupOverlayTickEventImplementation() {
      Composition = EventComposition.EventComposer
        + RenderPopups
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.Generate();
      return new PopupOverlayTickEvent(eventId);
    }

    private State RenderPopups(State state) {
      var openPopups = Store.GetOpenPopups(state);
      var renderCommand = new RenderCommand.RenderPopups(openPopups);
      return Store.PushRenderCommand(renderCommand, state);
    }

  }

}
