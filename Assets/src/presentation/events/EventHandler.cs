using System;
using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public enum EventHandlerType {
    Run,
    Stock
  }

  public abstract class EventImplementation {
    protected Converter<State, State> Actions;
    public abstract Event Create(EventParameters eventParameters);
    public virtual State Handle(State state) {
      return Actions(state);
    }
  }

  public class EventHandler : LocatorTarget {
    public static Dictionary<string, EventImplementation> EventImplementations =
      new Dictionary<string, EventImplementation>() {
        { ShowMailEvent.Type, new ShowMailEventImplementation() },
        { DebugEvent.Type, new DebugEventImplementation() }
      };

    protected EventHandlerType type;
    protected string targetEventType;
    public EventHandlerType Type {
      get { return type; }
    }
    public string TargetEventType {
      get { return targetEventType; }
    }

    public virtual List<RenderCommand> Handle(EventParameters eventParameters) {
      return new List<RenderCommand>();
    }

    public class Run : EventHandler {
      public Run(string eventType) {
        targetEventType = eventType;
        type = EventHandlerType.Run;
      }

      public override List<RenderCommand> Handle(EventParameters eventParameters) {
        if (!EventImplementations.ContainsKey(targetEventType)) {
          return new List<RenderCommand>();
        }
        var eventImplementation = EventImplementations[targetEventType];
        var evnt = eventImplementation.Create(eventParameters);
        var state = Store.LoadState(evnt);
        state = eventImplementation.Handle(state);
        state = Store.SaveState(state);
        return Store.GetRenderData(state);
      }
    }

    public class Stock : EventHandler {
      public Stock(string eventType) {
        targetEventType = eventType;
        type = EventHandlerType.Stock;
      }

      public override List<RenderCommand> Handle(EventParameters eventParameters) {
        if (!EventImplementations.ContainsKey(targetEventType)) {
          return new List<RenderCommand>();
        }
        var eventImplementation = EventImplementations[targetEventType];
        var evnt = eventImplementation.Create(eventParameters);
        var state = Store.LoadState(evnt);
        state = Store.PushProcessingEventToEventStore(state);
        state = Store.SaveState(state);
        return Store.GetRenderData(state);
      }
    }
  }

}
