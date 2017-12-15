using System;
using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public struct State {
    public string Me;
    public Event Processing;
    public EventStore ActiveEventStore;
    public ImList<EventArchive> EventArchives;
    public GameState CurrentGameState;
    public UIState CurrentUIState;
    public ImList<RenderCommand> RenderData;

    public static State operator &(State state, Func<State, State> curry) {
      return curry(state);
    }

    public State(
      string me,
      Event processing,
      EventStore eventStore,
      ImList<EventArchive> eventArchives,
      GameState gameState,
      UIState uiState,
      ImList<RenderCommand> renderData
    ) {
      Me = me;
      Processing = processing;
      ActiveEventStore = eventStore;
      EventArchives = eventArchives;
      CurrentGameState = gameState;
      CurrentUIState = uiState;
      RenderData = renderData;
    }

    public static State InitialState() {
      return new State(
        "",
        new DebugEvent(EventId.InitialState(), "No event to process"),
        EventStore.InitialState(),
        new ImList<EventArchive>(),
        GameState.InitialState(),
        UIState.InitialState(),
        new ImList<RenderCommand>()
      );
    }
  }

}
