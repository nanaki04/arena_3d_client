using Arena.Modules;

namespace Arena.Presentation {

  public enum StateEventLogLevel {
    None,
    Diff,
    Full
  }

  public class StateLoggerPlug : StorePlug {
    static StateEventLogLevel LogLevel = StateEventLogLevel.Full;
    State stateBefore;

    public State TransformOnLoad(State state) {
      stateBefore = state;
      return state;
    }

    public State TransformOnSave(State state) {
      LogEventStore(state);
      return state;
    }

    private void LogEventStore(State stateAfter) {
      if (LogLevel == StateEventLogLevel.None) {
        return;
      }
      var eventStoreBefore = Store.GetEventStore(stateBefore);
      var eventStoreAfter = Store.GetEventStore(stateAfter);

      if (eventStoreBefore.ProgressId != eventStoreAfter.ProgressId) {
        Logger.Log(
          "EventStoreProgress - before: " + eventStoreBefore.ProgressId + ", after: " + eventStoreAfter.ProgressId
        );
      }

      var eventStoreListRemoved = eventStoreBefore.Store / eventStoreAfter.Store;
      var eventStoreListAdded = eventStoreAfter.Store / eventStoreBefore.Store;
      if (eventStoreListRemoved.Count > 0) {
        Logger.Log("removed events:");
        Im.Each(LogEvent, eventStoreListRemoved);
      }

      if (eventStoreListAdded.Count > 0) {
        Logger.Log("added events:");
        Im.Each(LogEvent, eventStoreListAdded);
      }

      var outboxRemoved = eventStoreBefore.Outbox / eventStoreAfter.Outbox;
      var outboxAdded = eventStoreAfter.Outbox / eventStoreBefore.Outbox;
      if (outboxRemoved.Count > 0) {
        Logger.Log("removed events from outbox:");
        Im.Each(LogEvent, outboxRemoved);
      }

      if (outboxAdded.Count > 0) {
        Logger.Log("added events to outbox:");
        Im.Each(LogEvent, outboxAdded);
      }

      if (LogLevel != StateEventLogLevel.Full) {
        return;
      }

      if (eventStoreListRemoved.Count > 0 || eventStoreListAdded.Count > 0) {
        Logger.Log("event store");
        Im.Each(LogEvent, eventStoreAfter.Store);
      }

      if (outboxRemoved.Count > 0 || outboxAdded.Count > 0) {
        Logger.Log("event outbox");
        Im.Each(LogEvent, eventStoreAfter.Outbox);
      }

    }

    private void LogEvent(Event evnt) {
      Logger.Log(evnt.GetType().ToString() + ": " + evnt.Id.Id + ":" + evnt.Id.Spawner);
    }

  }

}
