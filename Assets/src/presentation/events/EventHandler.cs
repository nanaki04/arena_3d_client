using System;
using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public enum EventHandlerType {
    Run,
    Stock,
    RunFactory,
    StockFactory
  }

  public abstract class EventImplementation {
    protected Note<State> Composition;
    public abstract Event Create(EventParameters eventParameters);
    public virtual State Handle(State state) {
      return Composition.Play(state);
    }
  }

  public class EventHandler : LocatorTarget {
    public static Dictionary<string, EventImplementation> EventImplementations =
      new Dictionary<string, EventImplementation>() {
        { RequestCredentialsEvent.Type, new RequestCredentialsEventImplementation() },
        { LoginEvent.Type, new LoginEventImplementation() },
        { ReceivePackageEvent.Type, new ReceivePackageEventImplementation() },
        { PollEvent.Type, new PollEventImplementation() },
        { ShowMailEvent.Type, new ShowMailEventImplementation() },
        { DebugEvent.Type, new DebugEventImplementation() },
        { PopupOverlayTickEvent.Type, new PopupOverlayTickEventImplementation() },
        { OpenPopupEvent.Type, new OpenPopupEventImplementation() },
        { SelectRadioButtonEvent.Type, new SelectRadioButtonEventImplementation() },
        { RestoreRadioButtonEvent.Type, new RestoreRadioButtonEventImplementation() },
        { LoadTabContentEvent.Type, new LoadTabContentEventImplementation() },
        { ClosePopupEvent.Type, new ClosePopupEventImplementation() },
        { VerifyProgressEvent.Type, new VerifyProgressEventImplementation() },
      };

    protected EventHandlerType type;
    protected string targetEventType;
    public EventHandlerType Type {
      get { return type; }
    }
    public string TargetEventType {
      get { return targetEventType; }
    }

    public virtual ImList<RenderCommand> Handle(EventParameters eventParameters) {
      return new ImList<RenderCommand>();
    }

    public class Run : EventHandler {
      public Run(string eventType) {
        targetEventType = eventType;
        type = EventHandlerType.Run;
      }

      public override ImList<RenderCommand> Handle(EventParameters eventParameters) {
        if (!EventImplementations.ContainsKey(targetEventType)) {
          Logger.Log("unmatched event type: " + targetEventType.ToString());
          return new ImList<RenderCommand>();
        }

        var eventImplementation = EventImplementations[targetEventType];
        var evnt = eventImplementation.Create(eventParameters);
        var state = Store.LoadState(evnt);
        evnt.SetSpawner(Store.GetMe(state));
        state = eventImplementation.Handle(state);

        var renderData = Store.GetRenderData(state);
        state = Store.ClearRenderData(state);
        state = Store.SaveState(state);

        return renderData;
      }
    }

    public class RunFactory : EventHandler.Run {
      EventParameters CurriedParameters;

      public RunFactory(string eventType, EventParameters curriedParameters) : base(eventType) {
        CurriedParameters = curriedParameters;
        type = EventHandlerType.RunFactory;
      }

      public override ImList<RenderCommand> Handle(EventParameters eventParameters) {
        return base.Handle(CurriedParameters + eventParameters);
      }
    }

    public class Stock : EventHandler {
      public Stock(string eventType) {
        targetEventType = eventType;
        type = EventHandlerType.Stock;
      }

      public override ImList<RenderCommand> Handle(EventParameters eventParameters) {
        if (!EventImplementations.ContainsKey(targetEventType)) {
          return new ImList<RenderCommand>();
        }

        var eventImplementation = EventImplementations[targetEventType];
        var evnt = eventImplementation.Create(eventParameters);
        var state = Store.LoadState(evnt);
        evnt.SetSpawner(Store.GetMe(state));
        state = Store.PushProcessingEventToEventStore(state);
        state = Store.PushProcessingEventToOutbox(state);

        var renderData = Store.GetRenderData(state);
        state = Store.ClearRenderData(state);
        state = Store.SaveState(state);

        return renderData;
      }
    }

    public class StockFactory : EventHandler.Stock {
      EventParameters CurriedParameters;

      public StockFactory(string eventType, EventParameters curriedParameters) : base(eventType) {
        CurriedParameters = curriedParameters;
        type = EventHandlerType.StockFactory;
      }

      public override ImList<RenderCommand> Handle(EventParameters eventParameters) {
        return base.Handle(CurriedParameters + eventParameters);
      }
    }

  }

}
