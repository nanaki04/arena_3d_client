using System.Collections.Generic;

namespace Arena.Presentation {

  public static class Store {
    private static State ActiveState = State.InitialState();

    public static State LoadState(Event processing) {
      return ActiveState;
    }

    public static State SaveState(State state) {
      ActiveState = state;
      return ActiveState;
    }

    public static List<RenderCommand> GetRenderData(State state) {
      return state.RenderData;
    }

    public static string GetMe(State state) {
      return state.Me;
    }

    public static Event GetProcessing(State state) {
      return state.Processing;
    }

    public static EventStore GetEventStore(State state) {
      return state.ActiveEventStore;
    }

    public static List<Event> GetEventStoreList(State state) {
      var eventStore = GetEventStore(state);
      return new List<Event>(eventStore.Store);
    }

    public static List<EventArchive> GetEventArchive(State state) {
      return new List<EventArchive>(state.EventArchives);
    }

    public static GameState GetGameState(State state) {
      return state.CurrentGameState;
    }

    public static State UpdateMe(string me, State state) {
      state.Me = me;
      return state;
    }

    public static State UpdateProcessing(Event processing, State state) {
      state.Processing = processing;
      return state;
    }

    public static State UpdateEventStore(EventStore eventStore, State state) {
      state.ActiveEventStore = eventStore;
      return state;
    }

    public static State UpdateEventStoreList(List<Event> eventStoreList, State state) {
      var eventStore = GetEventStore(state);
      eventStore.Store = eventStoreList;
      return UpdateEventStore(eventStore, state);
    }

    public static State UpdateEventArchives(List<EventArchive> eventArchives, State state) {
      state.EventArchives = eventArchives;
      return state;
    }

    public static State UpdateGameState(GameState gameState, State state) {
      state.CurrentGameState = gameState;
      return state;
    }

    public static State UpdateRenderData(List<RenderCommand> renderData, State state) {
      state.RenderData = renderData;
      return state;
    }

    public static State PushEventToEventStore(Event evnt, State state) {
      var eventStoreList = GetEventStoreList(state);
      eventStoreList.Add(evnt);
      return UpdateEventStoreList(eventStoreList, state);
    }

    public static State PushProcessingEventToEventStore(State state) {
      var evnt = GetProcessing(state);
      return PushEventToEventStore(evnt, state);
    }

    public static State PushRenderCommand(RenderCommand renderCommand, State state) {
      var renderData = GetRenderData(state);
      renderData.Add(renderCommand);
      return UpdateRenderData(renderData, state);
    }

  }

}
