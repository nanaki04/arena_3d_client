using System.Collections.Generic;

namespace Arena.Presentation {

  public struct EventStore {
    public List<Event> Store;
    public string ProgressId;

    public EventStore(List<Event> store, string progressId) {
      Store = store;
      ProgressId = progressId;
    }

    public static EventStore InitialState() {
      return new EventStore(
        new List<Event>(),
        "0"
      );
    }
  }

}
