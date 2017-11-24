using System.Collections.Generic;

namespace Arena.Presentation {

  public struct State {
    public string Me;
    public Event Processing;
    public EventStore ActiveEventStore;
    public List<EventArchive> EventArchives;
    public GameState CurrentGameState;
    public UIState CurrentUIState;
    public List<RenderCommand> RenderData;

    public State(
      string me,
      Event processing,
      EventStore eventStore,
      List<EventArchive> eventArchives,
      GameState gameState,
      UIState uiState,
      List<RenderCommand> renderData
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
        new List<EventArchive>(),
        GameState.InitialState(),
        UIState.InitialState(),
        new List<RenderCommand>()
      );
    }
  }

}
