using System.Collections.Generic;

namespace Arena.Presentation {

  public enum EventParameterType {
    Id,
    KeyCode,
    Index,
    Page,
    RecordsPerPage,
    Message
  }

  public abstract class EventParameter {
    public EventParameterType Type { get; set; }

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
  }

  public class EventParameters {
    public List<EventParameter> Parameters { get; }

    public EventParameters() {
      Parameters = new List<EventParameter>();
    }

    public EventParameters(List<EventParameter> parameters) {
      Parameters = parameters;
    }
  }
}
