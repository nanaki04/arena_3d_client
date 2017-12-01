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
        Im.Each((evnt) => Logger.Log(evnt.GetType().ToString()), eventStoreListRemoved);
      }

      if (eventStoreListAdded.Count > 0) {
        Logger.Log("added events:");
        Im.Each((evnt) => Logger.Log(evnt.GetType().ToString()), eventStoreListAdded);
      }

      var outboxRemoved = eventStoreBefore.Outbox / eventStoreAfter.Outbox;
      var outboxAdded = eventStoreAfter.Outbox / eventStoreBefore.Outbox;
      if (outboxRemoved.Count > 0) {
        Logger.Log("removed events from outbox:");
        Im.Each((evnt) => Logger.Log(evnt.GetType().ToString()), outboxRemoved);
      }

      if (outboxAdded.Count > 0) {
        Logger.Log("added events to outbox:");
        Im.Each((evnt) => Logger.Log(evnt.GetType().ToString()), outboxAdded);
      }

      if (LogLevel != StateEventLogLevel.Full) {
        return;
      }

      if (eventStoreListRemoved.Count > 0 || eventStoreListAdded.Count > 0) {
        Logger.Log("event store");
        Im.Each((evnt) => Logger.Log(evnt.GetType().ToString()), eventStoreAfter.Store);
      }

      if (outboxRemoved.Count > 0 || outboxAdded.Count > 0) {
        Logger.Log("event outbox");
        Im.Each((evnt) => Logger.Log(evnt.GetType().ToString()), eventStoreAfter.Outbox);
      }

    }

  }

}
