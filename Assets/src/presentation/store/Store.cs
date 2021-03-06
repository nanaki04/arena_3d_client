using System.Collections.Generic;
using System;
using Arena.Modules;

namespace Arena.Presentation {

  public interface StorePlug {
    State TransformOnLoad(State state);
    State TransformOnSave(State state);
  }

  public static class Store {
    private static State ActiveState = State.InitialState();

    private static ImList<StorePlug> Plugs =
      new ImList<StorePlug>()
        + new StateLoggerPlug()
        + new PopupLoggerPlug()
        ;

    public static State LoadState(Event processing) {
      var state = UpdateProcessing(processing, ActiveState);
      return Im.Fold<StorePlug, State>((State acc, StorePlug plug) => plug.TransformOnLoad(acc), state, Plugs);
    }

    public static State SaveState(State state) {
      ActiveState = Im.Fold<StorePlug, State>((State acc, StorePlug plug) => plug.TransformOnSave(acc), state, Plugs);
      return ActiveState;
    }

    public static Func<State, State> Curry<T>(Func<T, State, State> handler, T arg1) {
      return Curry<T, State, State>.New(handler)(arg1);
    }

    public static Func<State, State> Curry<T1, T2>(Func<T1, T2, State, State> handler, T1 arg1, T2 arg2) {
      return Curry<T1, T2, State, State>.New(handler)(arg1)(arg2);
    }

    public static ImList<RenderCommand> GetRenderData(State state) {
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

    public static ImList<Event> GetEventStoreList(State state) {
      var eventStore = GetEventStore(state);
      return eventStore.Store;
    }

    public static ImList<Event> GetEventStoreOutbox(State state) {
      var eventStore = GetEventStore(state);
      return eventStore.Outbox;
    }

    public static string GetEventStoreProgress(State state) {
      return GetEventStore(state).ProgressId;
    }

    public static ImList<EventArchive> GetEventArchive(State state) {
      return state.EventArchives;
    }

    public static GameState GetGameState(State state) {
      return state.CurrentGameState;
    }

    public static UIState GetUIState(State state) {
      return state.CurrentUIState;
    }

    public static List<PopupType> GetOpenPopups(State state) {
      var uiState = GetUIState(state);
      return new List<PopupType>(uiState.Popups);
    }

    public static ImMap<string, int> GetSelectedRadioButtons(State state) {
      var uiState = GetUIState(state);
      return uiState.RadioButtons;
    }

    public static int GetSelectedRadioButton(string id, State state) {
      var radioButtons = GetSelectedRadioButtons(state);
      if (!radioButtons.Has(id)) {
        return -1;
      }
      return radioButtons[id];
    }

    public static State UpdateMe(string me, State state) {
      state.Me = me;
      return state;
    }

    public static State UpdateProcessing(Event processing, State state) {
      state.Processing = processing;
      return state;
    }

    public static Func<State, State> UpdateProcessing(Event processing) {
      return Curry<Event, State, State>.New(UpdateProcessing)(processing);
    }

    public static State UpdateEventStore(EventStore eventStore, State state) {
      state.ActiveEventStore = eventStore;
      return state;
    }

    public static State UpdateEventStoreList(ImList<Event> eventStoreList, State state) {
      var eventStore = GetEventStore(state);
      eventStore.Store = eventStoreList;
      return UpdateEventStore(eventStore, state);
    }

    public static Func<State, State> UpdateEventStoreList(ImList<Event> eventStoreList) {
      return Curry<ImList<Event>, State, State>.New(UpdateEventStoreList)(eventStoreList);
    }

    public static State UpdateEventStoreOutbox(ImList<Event> eventStoreOutbox, State state) {
      var eventStore = GetEventStore(state);
      eventStore.Outbox = eventStoreOutbox;
      return UpdateEventStore(eventStore, state);
    }

    public static State UpdateEventStoreProgress(string progress, State state) {
      var eventStore = GetEventStore(state);
      eventStore.ProgressId = progress;
      return UpdateEventStore(eventStore, state);
    }

    public static State UpdateEventArchives(ImList<EventArchive> eventArchives, State state) {
      state.EventArchives = eventArchives;
      return state;
    }

    public static Func<State, State> UpdateEventArchives(ImList<EventArchive> eventArchives) {
      return Curry<ImList<EventArchive>, State, State>.New(UpdateEventArchives)(eventArchives);
    }

    public static State UpdateGameState(GameState gameState, State state) {
      state.CurrentGameState = gameState;
      return state;
    }

    public static Func<State, State> UpdateGameState(GameState gameState) {
      return Curry<GameState, State, State>.New(UpdateGameState)(gameState);
    }

    public static State UpdateUIState(UIState uiState, State state) {
      state.CurrentUIState = uiState;
      return state;
    }

    public static State UpdateRenderData(ImList<RenderCommand> renderData, State state) {
      state.RenderData = renderData;
      return state;
    }

    public static State ClearRenderData(State state) {
      return UpdateRenderData(new ImList<RenderCommand>(), state);
    }

    public static State UpdateOpenPopups(List<PopupType> openPopups, State state) {
      var uiState = GetUIState(state);
      uiState.Popups = openPopups;
      return UpdateUIState(uiState, state);
    }

    public static State RemoveLastOpenedPopup(State state) {
      var openPopups = GetOpenPopups(state);
      var index = openPopups.Count - 1;
      if (index < 0) {
        return state;
      }
      openPopups.RemoveAt(index);
      return UpdateOpenPopups(openPopups, state);
    }

    public static State RemoveOpenedPopup(PopupType popupType, State state) {
      var openPopups = GetOpenPopups(state);
      openPopups.Remove(popupType);
      return UpdateOpenPopups(openPopups, state);
    }

    public static State UpdateRadioButtons(ImMap<string, int> radioButtons, State state) {
      var uiState = GetUIState(state);
      uiState.RadioButtons = radioButtons;
      return UpdateUIState(uiState, state);
    }

    public static State UpdateSelectedRadioButton(string id, int index, State state) {
      var selectedRadioButtons = GetSelectedRadioButtons(state);
      selectedRadioButtons = selectedRadioButtons / id * index;
      return UpdateRadioButtons(selectedRadioButtons, state);
    }

    public static State PushEventToEventStore(Event evnt, State state) {
      var eventStoreList = GetEventStoreList(state);
      eventStoreList += evnt;
      return UpdateEventStoreList(eventStoreList, state);
    }

    public static State PushProcessingEventToEventStore(State state) {
      var evnt = GetProcessing(state);
      return PushEventToEventStore(evnt, state);
    }

    public static State PushEventToOutbox(Event evnt, State state) {
      var eventStoreOutbox = GetEventStoreOutbox(state);
      eventStoreOutbox += evnt;
      return UpdateEventStoreOutbox(eventStoreOutbox, state);
    }

    public static State PushProcessingEventToOutbox(State state) {
      var evnt = GetProcessing(state);
      return PushEventToOutbox(evnt, state);
    }

    public static State PushOpenPopup(PopupType popupType, State state) {
      var openPopups = GetOpenPopups(state);
      if (openPopups.Contains(popupType)) {
        return state;
      }
      openPopups.Add(popupType);
      return UpdateOpenPopups(openPopups, state);
    }

    public static State PushRenderCommand(RenderCommand renderCommand, State state) {
      var renderData = GetRenderData(state) + renderCommand;
      return UpdateRenderData(renderData, state);
    }

  }

}
