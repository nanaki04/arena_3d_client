using System;

namespace Arena.Presentation {

  [Serializable]
  public struct EventId {
    public static int LastId = 0;
    public string Id;
    public string Spawner;
    public long SpawnTime;
    public string Progress;

    public EventId(string id, string spawner, long spawnTime, string progress) {
      Id = id;
      Spawner = spawner;
      SpawnTime = spawnTime;
      Progress = progress;
    }

    public static EventId InitialState() {
      return new EventId(
        "0",
        "",
        0,
        "0"
      );
    }

    public static EventId Generate() {
      var spawnTime = (DateTime.UtcNow.Ticks - DateTime.Parse("01/01/1970 00:00:00").Ticks) / 10000;
      return new EventId(
        (++EventId.LastId).ToString(),
        "",
        spawnTime,
        "0"
      );
    }
  }

  [Serializable]
  public abstract class Event : IComparable<Event>, IEquatable<Event> {
    protected EventId id;
    public string EventType {
      get {
        var split = (GetType()).ToString().Split('.');
        return split[split.Length - 1];
      }
    }
    public EventId Id {
      get { return id; }
    }

    protected Event(EventId eventId) {
      id = eventId;
    }

    public void SetSpawner(string spawner) {
      if (id.Spawner == "") {
        id.Spawner = spawner;
      }
    }

    public int CompareTo(Event evnt) {
      if (this.id.SpawnTime > evnt.id.SpawnTime) {
        return 1;
      }
      if (this.id.SpawnTime < evnt.id.SpawnTime) {
        return -1;
      }
      return 0;
    }

    public bool Equals(Event other) {
      return id.Id == other.Id.Id && id.Spawner == other.Id.Spawner;
    }

  }

}
