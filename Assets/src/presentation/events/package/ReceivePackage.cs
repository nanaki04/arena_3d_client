using System;
using System.Collections.Generic;
using System.Linq;
using Arena.Modules;

namespace Arena.Presentation {

  [Serializable]
  public class ReceivePackageEvent : Event {
    public static string Type = (typeof(ReceivePackageEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(ReceivePackageEvent.Type);
    public string Progress;
    public ImList<Event> Events;
    public ReceivePackageEvent(EventId eventId, string progress, ImList<Event> events) : base(eventId) {
      Progress = progress;
      Events = events;
    }
  }

  public class ReceivePackageEventImplementation : EventImplementation {
    public ReceivePackageEventImplementation() {
      Composition = EventComposition.EventComposer
        + PushEventStore
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      DataPackage package = (eventParameters.Parameters[0] as EventParameter.Package).Val;
      var progress = package.Payload.Progress;
      var events = package.Payload.Events;

      return new ReceivePackageEvent(eventId, progress, events);
    }

    private State PushEventStore(State state) {
      var evnt = Store.GetProcessing(state) as ReceivePackageEvent;
      var eventStore = Store.GetEventStore(state);

      // TODO overwrite duplicates
      var eventStoreList = evnt.Events * eventStore.Store;
      eventStore.ProgressId = evnt.Progress;
      eventStore.Store = eventStoreList;

      return Store.UpdateEventStore(eventStore, state);
    }
  }

}
