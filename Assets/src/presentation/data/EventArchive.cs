using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public struct EventArchive {
    public ImList<Event> Archive;
    public float ArchivedUntil;
    public GameState Snapshot;

    public EventArchive(
      ImList<Event> archive,
      float archivedUntil,
      GameState snapshot
    ) {
      Archive = archive;
      ArchivedUntil = archivedUntil;
      Snapshot = snapshot;
    }

    public static EventArchive InitialState() {
      return new EventArchive(
        new ImList<Event>(),
        0.0f,
        GameState.InitialState()
      );
    }
  }

}
