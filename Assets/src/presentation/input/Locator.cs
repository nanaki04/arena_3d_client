using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public static class EventLocator {

    private static ImMap<string, ImMap<string, LocatorTarget>> RegistrationList =
      Im.Map<ImMap<string, LocatorTarget>>()
        / "player"        * PlayerLocatorList.List
        / "package"       * PackageLocatorList.List
        / "mypage"        * MypageLocatorList.List
        / "popup"         * PopupLocatorList.List
        / "dummy_popup"   * DummyPopupLocatorList.List
        / "login_popup"   * LoginPopupLocatorList.List
        ;

    private static ImList<LocatorPlug> Plugs =
      new ImList<LocatorPlug>()
        + new PhoenixPackageAdapterPlug()
        + new LocatorLoggerPlug()
        ;

    private static Locator locator = new Locator(RegistrationList, Plugs);

    public static ImList<RenderCommand> Dispatch(string domain, string address, EventParameters eventParameters) {
      var eventHandler = locator.Locate(domain, address) as EventHandler;
      return eventHandler.Handle(eventParameters);
    }
  }

}
