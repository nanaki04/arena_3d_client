using System;
using System.Collections.Generic;
using System.Linq;
using Arena.Modules;

namespace Arena.Presentation {

  [Serializable]
  public class PollEvent : Event {
    public static string Type = (typeof(PollEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(PollEvent.Type);
    public PollEvent(EventId eventId) : base(eventId) {}
  }

  public class PollEventImplementation : EventImplementation {
    public PollEventImplementation() {
      Composition = EventComposition.EventComposer
        + SendEventOutbox
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      return new PollEvent(eventId);
    }

    private State SendEventOutbox(State state) {
      var eventStore = Store.GetEventStore(state);
      if (eventStore.Outbox.GetList().Count < 1) {
        return state;
      }

      var payload = new DataPackagePayload();
      payload.Events = eventStore.Outbox;
      payload.Progress = eventStore.ProgressId;

      state = Store.UpdateEventStoreOutbox(new ImList<Event>(), state);

      var package = new DataPackage();
      // TODO
      package.Path = "lobby:global";
      package.Method = "push";
      package.Payload = payload;

      var renderCommand = new RenderCommand.SendPackage(package);

      return Store.PushRenderCommand(renderCommand, state);
    }
  }

}
