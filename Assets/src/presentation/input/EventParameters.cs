using System.Collections.Generic;

namespace Arena.Presentation {

  public enum EventParameterType {
    Id,
    KeyCode,
    Index,
    Page,
    RecordsPerPage,
    Message,
    Popup,
    RadioButton
  }

  public abstract class EventParameter {
    public EventParameterType Type { get; set; }

    public static EventParameters operator +(EventParameter parameter1, EventParameter parameter2) {
      var parameterList = new List<EventParameter>() {
        parameter1,
        parameter2
      };
      return new EventParameters(parameterList);
    }

    public class Id : EventParameter {
      public string Val { get; }

      public Id(string val) {
        Val = val;
        Type = EventParameterType.Id;
      }
    }

    public class KeyCode : EventParameter {
      public string Val { get; }

      public KeyCode(string val) {
        Val = val;
        Type = EventParameterType.KeyCode;
      }
    }

    public class Index : EventParameter {
      public int Val { get; }

      public Index(int val) {
        Val = val;
        Type = EventParameterType.Index;
      }
    }

    public class Page : EventParameter {
      public int Val { get; }

      public Page(int val) {
        Val = val;
        Type = EventParameterType.Page;
      }
    }

    public class RecordsPerPage : EventParameter {
      public int Val { get; }

      public RecordsPerPage(int val) {
        Val = val;
        Type = EventParameterType.RecordsPerPage;
      }
    }

    public class Message : EventParameter {
      public string Val { get; }

      public Message(string val) {
        Val = val;
        Type = EventParameterType.Message;
      }
    }

    public class Popup : EventParameter {
      public PopupType Val { get; }

      public Popup(PopupType val) {
        Val = val;
        Type = EventParameterType.Popup;
      }
    }

    public class RadioButton : EventParameter {
      public int Val { get; }

      public RadioButton(int val) {
        Val = val;
        Type = EventParameterType.RadioButton;
      }
    }
  }

  public class EventParameters {
    public List<EventParameter> Parameters { get; }

    public EventParameters() {
      Parameters = new List<EventParameter>();
    }

    public EventParameters(List<EventParameter> parameters) {
      Parameters = parameters;
    }

    public EventParameters Push(EventParameter parameter) {
      var list = new List<EventParameter>(Parameters);
      list.Add(parameter);
      return new EventParameters(list);
    }

    public static EventParameters operator +(EventParameters parameters, EventParameter parameter) {
      return parameters.Push(parameter);
    }

    public static EventParameters operator +(EventParameters parameters1, EventParameters parameters2) {
      foreach (EventParameter parameter in parameters2.Parameters) {
        parameters1 += parameter;
      }
      return parameters1;
    }
  }
}
