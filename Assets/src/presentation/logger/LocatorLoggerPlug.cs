using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public enum LocatorLogLevel {
    None,
    NoTicks,
    Full
  }

  public class LocatorLoggerPlug : LocatorPlug {
    static LocatorLogLevel LogLevel = LocatorLogLevel.NoTicks;
    bool ignore = false;

    public PlugState Transform(PlugState plugState) {
      if (LogLevel == LocatorLogLevel.None) {
        ignore = true;
        return plugState;
      }

      if (LogLevel == LocatorLogLevel.NoTicks && (plugState.Address == "poll" || plugState.Address == "tick")) {
        ignore = true;
        return plugState;
      }

      Logger.Log("Dispatch: [" + plugState.Domain + "][" + plugState.Address + "]");

      return plugState;
    }

    public LocatorTarget Wrap(LocatorTarget locatorTarget) {
      if (LogLevel == LocatorLogLevel.None || ignore) {
        return locatorTarget;
      }

      return new EventHandlerWrapper(locatorTarget as EventHandler);
    }

    private class EventHandlerWrapper : EventHandler {
      private EventHandler Origin;

      public EventHandlerWrapper(EventHandler origin) {
        Origin = origin;
      }

      public override List<RenderCommand> Handle(EventParameters eventParameters) {
        eventParameters.Each((EventParameter eventParameter) => Logger.Log(eventParameter.Type.ToString()));
        var renderCommandList = Origin.Handle(eventParameters);

        return renderCommandList;
      }
    }

  }

}
