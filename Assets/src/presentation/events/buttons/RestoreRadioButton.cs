using System;
using Arena.Modules;

namespace Arena.Presentation {

  [Serializable]
  public class RestoreRadioButtonEvent : Event {
    public static string Type = (typeof(RestoreRadioButtonEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(RestoreRadioButtonEvent.Type);
    public string ButtonGroupId;
    public int ButtonIndex;
    public RestoreRadioButtonEvent(EventId eventId, string id, int index) : base(eventId) {
      ButtonGroupId = id;
      ButtonIndex = index;
    }
    public static EventHandler Create(string radioButtonGroupId) {
      var eventParameters = new EventParameters() + new EventParameter.Id(radioButtonGroupId);
      return new EventHandler.RunFactory(RestoreRadioButtonEvent.Type, eventParameters);
    }
  }

  public class RestoreRadioButtonEventImplementation : EventImplementation {
    public RestoreRadioButtonEventImplementation() {
      Composition = EventComposition.EventComposer
        + RestoreSelectedButton
        + SelectButton
        + Store.Curry<RenderCommand>(Store.PushRenderCommand, new RenderCommand.Delegate())
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      var id = eventParameters.Parameters[0] as EventParameter.Id;
      var index = eventParameters.Parameters[1] as EventParameter.RadioButton;
      return new RestoreRadioButtonEvent(eventId, id.Val, index.Val);
    }

    private State RestoreSelectedButton(State state) {
      var evnt = Store.GetProcessing(state) as RestoreRadioButtonEvent;
      var selectedButton = Store.GetSelectedRadioButton(evnt.ButtonGroupId, state);
      if (selectedButton < 0) {
        selectedButton = evnt.ButtonIndex;
      }
      return Store.UpdateSelectedRadioButton(evnt.ButtonGroupId, selectedButton, state);
    }

    private State SelectButton(State state) {
      var evnt = Store.GetProcessing(state) as RestoreRadioButtonEvent;
      var selectedButton = Store.GetSelectedRadioButton(evnt.ButtonGroupId, state);
      var renderCommand = new RenderCommand.SelectRadioButton(selectedButton);
      return Store.PushRenderCommand(renderCommand, state);
    }
  }

}
