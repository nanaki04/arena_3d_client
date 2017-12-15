using System;
using System.Collections.Generic;
using System.Linq;
using Arena.Modules;

namespace Arena.Presentation {

  [Serializable]
  public class VerifyProgressEvent : Event {
    public static string Type = (typeof(VerifyProgressEvent)).ToString();
    public string Progress;
    public VerifyProgressEvent(EventId eventId, string progress) : base(eventId) {
      Progress = progress;
    }
  }

  public class VerifyProgressEventImplementation : EventImplementation {
    public VerifyProgressEventImplementation() {
      Composition = EventComposition.EventComposer
        + ImprintMeAsSpawner
        + Store.PushProcessingEventToOutbox
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.Generate();
      var progress = eventParameters[EventParameterType.Id] as EventParameter.Id;
      return new VerifyProgressEvent(eventId, progress.Val);
    }

    private VerifyProgressEvent ThisEvent(State state) {
      return Store.GetProcessing(state) as VerifyProgressEvent;
    }

    private State ImprintMeAsSpawner(State state) {
      ThisEvent(state).SetSpawner(Store.GetMe(state));
      return state;
    }

  }

}
