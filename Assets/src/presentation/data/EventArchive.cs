using System.Collections.Generic;

namespace Arena.Presentation {

  public struct EventArchive {
    public List<Event> Archive;
    public float ArchivedUntil;
    public GameState Snapshot;

    public EventArchive(
      List<Event> archive,
      float archivedUntil,
      GameState snapshot
    ) {
      Archive = archive;
      ArchivedUntil = archivedUntil;
      Snapshot = snapshot;
    }

    public static EventArchive InitialState() {
      return new EventArchive(
        new List<Event>(),
        0.0f,
        GameState.InitialState()
      );
    }
  }

}
