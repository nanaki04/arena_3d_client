using System;
using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public enum EventParameterType {
    Id,
    Password,
    KeyCode,
    Index,
    Page,
    RecordsPerPage,
    Message,
    Package,
    PhoenixPackage,
    Popup,
    RadioButton
  }

  public abstract class EventParameter {
    public EventParameterType Type { get; set; }

    public static EventParameters operator +(EventParameter parameter1, EventParameter parameter2) {
      var parameterList = new ImList<EventParameter>()
        + parameter1
        + parameter2
        ;  
      return new EventParameters(parameterList);
    }

    public class Id : EventParameter {
      public string Val { get; }

      public Id(string val) {
        Val = val;
        Type = EventParameterType.Id;
      }
    }

    public class Password : EventParameter {
      public string Val { get; }

      public Password(string val) {
        Val = val;
        Type = EventParameterType.Password;
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

    public class Package : EventParameter {
      public DataPackage Val { get; }

      public Package(DataPackage val) {
        Val = val;
        Type = EventParameterType.Package;
      }
    }

    public class PhoenixPackage : EventParameter {
      public PhoenixSocketPackage Val { get; }

      public PhoenixPackage(PhoenixSocketPackage val) {
        Val = val;
        Type = EventParameterType.PhoenixPackage;
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
    public ImList<EventParameter> Parameters { get; }
    public int Count {
      get { return Parameters.Count; }
    }

    public EventParameters() {
      Parameters = new ImList<EventParameter>();
    }

    public EventParameters(List<EventParameter> parameters) {
      Parameters = new ImList<EventParameter>(parameters);
    }

    public EventParameters(ImList<EventParameter> parameters) {
      Parameters = parameters;
    }

    public EventParameters Push(EventParameter parameter) {
      return new EventParameters(Parameters + parameter);
    }

    public bool Has(EventParameterType type) {
      return this[type] != null;
    }

    public void Each(Action<EventParameter> iterator) {
      Im.Each(iterator, Parameters);
    }

    public EventParameters Transform(Func<EventParameter, EventParameter> transformer) {
      return new EventParameters(Im.Transform(transformer, Parameters));
    }

    public EventParameter this[EventParameterType type] {
      get {
        return Im.Find((EventParameter eventParameter) => eventParameter.Type == type, Parameters);
      }
    }

    public static EventParameters operator +(EventParameters parameters, EventParameter parameter) {
      return parameters.Push(parameter);
    }

    public static EventParameters operator +(EventParameters parameters1, EventParameters parameters2) {
      var parameters = parameters1.Parameters * parameters2.Parameters;
      return new EventParameters(parameters);
    }
  }
}
