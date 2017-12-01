using System;
using Arena.Modules;

namespace Arena.Presentation {

  [Serializable]
  public class LoadTabContentEvent : Event {
    public static string Type = (typeof(LoadTabContentEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(LoadTabContentEvent.Type);
    public string ButtonGroupId;
    public LoadTabContentEvent(EventId eventId, string id) : base(eventId) {
      ButtonGroupId = id;
    }
    public static EventHandler Create(string radioButtonGroupId) {
      var eventParameters = new EventParameters() + new EventParameter.Id(radioButtonGroupId);
      return new EventHandler.RunFactory(LoadTabContentEvent.Type, eventParameters);
    }
  }

  public class LoadTabContentEventImplementation : EventImplementation {
    public LoadTabContentEventImplementation() {
      Composition = EventComposition.EventComposer
        + LoadContent
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      var id = eventParameters.Parameters[0] as EventParameter.Id;
      return new LoadTabContentEvent(eventId, id.Val);
    }

    private State LoadContent(State state) {
      var evnt = Store.GetProcessing(state) as LoadTabContentEvent;
      var selectedButton = Store.GetSelectedRadioButton(evnt.ButtonGroupId, state);
      var renderCommand = new RenderCommand.LoadContent(selectedButton);
      return Store.PushRenderCommand(renderCommand, state);
    }
  }

}
