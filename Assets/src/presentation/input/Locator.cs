using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public static class EventLocator {
    private static Dictionary<string, LocatorTarget> mypageTargetList =
      new Dictionary<string, LocatorTarget>() {
        { "show", ShowMailEvent.Run }
      };

    private static Dictionary<string, Dictionary<string, LocatorTarget>> RegistrationList =
      new Dictionary<string, Dictionary<string, LocatorTarget>>() {
        { "mypage", mypageTargetList }
      };

    private static List<LocatorPlug> Plugs =
      new List<LocatorPlug>() {

      };

    private static Locator locator = new Locator(RegistrationList, Plugs);

    public static List<RenderCommand> Dispatch(string domain, string address, EventParameters eventParameters) {
      var eventHandler = locator.Locate(domain, address) as EventHandler;
      return eventHandler.Handle(eventParameters);
    }
  }

}
