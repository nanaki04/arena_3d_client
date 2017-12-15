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
        + EventF.UpdateEventStoreProgress(state => ThisEvent(state).Progress)
        + EventF.SpawnVerifyProgressEvent
        + EventF.OverlayOnEventStoreList(state => ThisEvent(state).Events)
        + EventF.UnpackEventArchivesInRange(EventF.GetEventStoreOldestSpawnTime)
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.Generate();
      DataPackage package = (eventParameters[EventParameterType.Package] as EventParameter.Package).Val;
      var progress = package.Payload.Progress;
      var events = package.Payload.Events;

      return new ReceivePackageEvent(eventId, progress, events);
    }

    private ReceivePackageEvent ThisEvent(State state) {
      return Store.GetProcessing(state) as ReceivePackageEvent;
    }

  }

}
