using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public static class EventLocator {
    private static Dictionary<string, LocatorTarget> mypageTargetList =
      new Dictionary<string, LocatorTarget>() {
        { "show", ShowMailEvent.Run }
      };

    private static Dictionary<string, LocatorTarget> popupTargetList =
      new Dictionary<string, LocatorTarget>() {
        { "tick", PopupOverlayTickEvent.Run }
      };

    private static Dictionary<string, LocatorTarget> dummyPopupTargetList =
      new Dictionary<string, LocatorTarget>() {
        { "ok", ClosePopupEvent.Run },
        { "cancel", ClosePopupEvent.Run }
      };

    private static Dictionary<string, Dictionary<string, LocatorTarget>> RegistrationList =
      new Dictionary<string, Dictionary<string, LocatorTarget>>() {
        { "mypage", mypageTargetList },
        { "popup", popupTargetList },
        { "dummy_popup", dummyPopupTargetList }
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
