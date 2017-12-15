using System;
using Arena.Modules;

namespace Arena.Presentation {

  public static class EventF {

    public static State SortEventStoreList(State state) {
      var eventStoreList = Store.GetEventStoreList(state);
      return Store.UpdateEventStoreList(Im.Sort(eventStoreList), state);
    }

    public static State OverlayOnEventStoreList(ImList<Event> eventList, State state) {
      var eventStoreList = Store.GetEventStoreList(state);
      return Store.UpdateEventStoreList(
        eventList
        & Im.Overlay((evnt1, evnt2) => evnt1.Equals(evnt2), eventStoreList)
        & Im.Sort
        , state
      );
    }

    public static State OverlayOnEventStoreList(Func<State, ImList<Event>> getEventList, State state) {
      return OverlayOnEventStoreList(getEventList(state), state);
    }

    public static Func<State, State> OverlayOnEventStoreList(ImList<Event> eventList) {
      return Curry<ImList<Event>, State, State>.New(OverlayOnEventStoreList)(eventList);
    }

    public static Func<State, State> OverlayOnEventStoreList(Func<State, ImList<Event>> getEventList) {
      return Curry<Func<State, ImList<Event>>, State, State>.New(OverlayOnEventStoreList)(getEventList);
    }

    public static Func<State, State> UpdateEventStoreProgress(string progress) {
      return Curry<string, State, State>.New(Store.UpdateEventStoreProgress)(progress);
    }

    public static State UpdateEventStoreProgress(Func<State, string> getProgress, State state) {
      return Store.UpdateEventStoreProgress(getProgress(state), state);
    }

    public static Func<State, State> UpdateEventStoreProgress(Func<State, string> getProgress) {
      return Curry<Func<State, string>, State, State>.New(UpdateEventStoreProgress)(getProgress);
    }

    public static State ArchiveEventStore(State state) {
      var eventStore = Store.GetEventStoreList(state);
      var archive = new EventArchive(
        eventStore,
        eventStore[0].Id.SpawnTime,
        Store.GetGameState(state)
      );

      var eventArchives = Store.GetEventArchive(state);
      return state
        & Store.UpdateEventArchives(new ImList<EventArchive>(archive) * eventArchives)
        & Store.UpdateEventStoreList(new ImList<Event>())
        ;
    }

    public static State UnpackEventArchivesInRange(long oldestSpawnTime, State state) {
      var eventArchives = Store.GetEventArchive(state);
      if (!eventArchives) {
        return state;
      }
      if (eventArchives[0].ArchivedUntil < oldestSpawnTime) {
        return state;
      }

      return state
        & UnpackNewestEventArchive(eventArchives)
        & UnpackEventArchivesInRange(oldestSpawnTime)
        ;
    }

    public static State UnpackEventArchivesInRange(Func<State, long> getOldestSpawnTime, State state) {
      return UnpackEventArchivesInRange(getOldestSpawnTime(state), state);
    }

    public static Func<State, State> UnpackEventArchivesInRange(long oldestSpawnTime) {
      return Curry<long, State, State>.New(UnpackEventArchivesInRange)(oldestSpawnTime);
    }

    public static Func<State, State> UnpackEventArchivesInRange(Func<State, long> getOldestSpawnTime) {
      return Curry<Func<State, long>, State, State>.New(UnpackEventArchivesInRange)(getOldestSpawnTime);
    }

    public static long GetEventStoreOldestSpawnTime(State state) {
      var eventStoreList = Store.GetEventStoreList(state);
      return Im.Last(eventStoreList).Id.SpawnTime;
    }

    public static State UnpackNewestEventArchive(ImList<EventArchive> eventArchives, State state) {
      return state
        & Store.UpdateGameState(eventArchives[0].Snapshot)
        & Store.UpdateEventStoreList((Store.GetEventStoreList(state) * eventArchives[0].Archive) & Im.Sort)
        & Store.UpdateEventArchives(Im.Tail(eventArchives))
        ;
    }

    public static Func<State, State> UnpackNewestEventArchive(ImList<EventArchive> eventArchives) {
      return Curry<ImList<EventArchive>, State, State>.New(UnpackNewestEventArchive)(eventArchives);
    }

    public static State SpawnVerifyProgressEvent(State state) {
      var processing = Store.GetProcessing(state);
      var progress = Store.GetEventStoreProgress(state);
      var eventParameters = new EventParameters() + new EventParameter.Id(progress);
      var eventImplementation = EventHandler.EventImplementations[VerifyProgressEvent.Type];
      var evnt = eventImplementation.Create(eventParameters);

      return state
        & Store.UpdateProcessing(evnt)
        & eventImplementation.Handle
        & Store.UpdateProcessing(processing)
        ;
    }
  }

}
