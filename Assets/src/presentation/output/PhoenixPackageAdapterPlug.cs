using Arena.Modules;
using System.Collections.Generic;
using System;

namespace Arena.Presentation {

  public class PhoenixPackageAdapterPlug : LocatorPlug {
    public PlugState Transform(PlugState plugState) {
      return plugState;
    }

    public LocatorTarget Wrap(LocatorTarget locatorTarget) {
      return new EventHandlerWrapper(locatorTarget as EventHandler);
    }

    private class EventHandlerWrapper : EventHandler {
      private EventHandler Origin;

      public EventHandlerWrapper(EventHandler origin) {
        Origin = origin;
      }

      // TODO think about how to improve performance
      public override ImList<RenderCommand> Handle(EventParameters eventParameters) {
        var transformedParameters = TransformEventParameters(eventParameters);
        var renderCommandList = Origin.Handle(transformedParameters);
        return Im.Transform(TransformPackage, renderCommandList);
      }

      private RenderCommand TransformPackage(RenderCommand renderCommand) {
        if (renderCommand.Type != RenderCommandType.SendPackage) {
          return renderCommand;
        }
        var package = (renderCommand as RenderCommand.SendPackage).dataPackage;
        var phoenixPackage = PhoenixPackageAdapter.Transform(package);

        return new RenderCommand.SendPhoenixPackage(phoenixPackage);
      }

      private EventParameters TransformEventParameters(EventParameters eventParameters) {
        if (!eventParameters.Has(EventParameterType.PhoenixPackage)) {
          return eventParameters;
        }
        return eventParameters.Transform((EventParameter eventParameter) => {
          if (eventParameter.Type != EventParameterType.PhoenixPackage) {
            return eventParameter;
          }
          var package = PhoenixPackageAdapter.Transform((eventParameter as EventParameter.PhoenixPackage).Val);

          return new EventParameter.Package(package);
        });
      }

    }

  }

}
