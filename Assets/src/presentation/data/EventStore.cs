using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public struct EventStore {
    public ImList<Event> Store;
    public ImList<Event> Outbox;
    public string ProgressId;

    public EventStore(List<Event> store, List<Event> outbox, string progressId) {
      Store = new ImList<Event>(store);
      Outbox = new ImList<Event>(outbox);
      ProgressId = progressId;
    }

    public EventStore(ImList<Event> store, ImList<Event> outbox, string progressId) {
      Store = store;
      Outbox = outbox;
      ProgressId = progressId;
    }

    public static EventStore InitialState() {
      return new EventStore(
        new ImList<Event>(),
        new ImList<Event>(),
        "0"
      );
    }
  }

}
