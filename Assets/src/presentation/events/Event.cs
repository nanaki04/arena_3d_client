using System;

namespace Arena.Presentation {

  [Serializable]
  public struct EventId {
    public string Id;
    public string Spawner;
    public float SpawnTime;
    public string Progress;

    public EventId(string id, string spawner, float spawnTime, string progress) {
      Id = id;
      Spawner = spawner;
      SpawnTime = spawnTime;
      Progress = progress;
    }

    public static EventId InitialState() {
      return new EventId(
        "0",
        "",
        0.0f,
        "0"
      );
    }
  }

  [Serializable]
  public abstract class Event {
    protected EventId id;
    public EventId Id {
      get { return id; }
    }

    protected Event(EventId eventId) {
      id = eventId;
    }
  }

}
