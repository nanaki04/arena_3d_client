using UnityEngine;
using Arena.Modules;

namespace Arena.Presentation {

  public class SelectRadioButtonEvent : Event {
    public static string Type = (typeof(SelectRadioButtonEvent)).ToString();
    public static EventHandler Run = new EventHandler.Run(SelectRadioButtonEvent.Type);
    public string ButtonGroupId;
    public int ButtonIndex;
    public SelectRadioButtonEvent(EventId eventId, string id, int index) : base(eventId) {
      ButtonGroupId = id;
      ButtonIndex = index;
    }
    public static EventHandler Create(string radioButtonGroupId) {
      var eventParameters = new EventParameters() + new EventParameter.Id(radioButtonGroupId);
      return new EventHandler.RunFactory(SelectRadioButtonEvent.Type, eventParameters);
    }
  }

  public class SelectRadioButtonEventImplementation : EventImplementation {
    public SelectRadioButtonEventImplementation() {
      Composition = EventComposition.EventComposer
        + UpdateStore
        + SelectButton
        + DelegateEvent
        ;
    }

    public override Event Create(EventParameters eventParameters) {
      var eventId = EventId.InitialState();
      var id = eventParameters.Parameters[0] as EventParameter.Id;
      var index = eventParameters.Parameters[1] as EventParameter.RadioButton;
      return new SelectRadioButtonEvent(eventId, id.Val, index.Val);
    }

    private State UpdateStore(State state) {
      var evnt = Store.GetProcessing(state) as SelectRadioButtonEvent;
      return Store.UpdateSelectedRadioButton(evnt.ButtonGroupId, evnt.ButtonIndex, state);
    }

    private State SelectButton(State state) {
      var evnt = Store.GetProcessing(state) as SelectRadioButtonEvent;
      var selectedButton = Store.GetSelectedRadioButton(evnt.ButtonGroupId, state);
      var renderCommand = new RenderCommand.SelectRadioButton(selectedButton);
      return Store.PushRenderCommand(renderCommand, state);
    }

    private State DelegateEvent(State state) {
      var renderCommand = new RenderCommand.Delegate();
      return Store.PushRenderCommand(renderCommand, state);
    }
  }

}
