using System.Collections.Generic;
using Arena.Modules;
using UnityEngine;

namespace Arena.Presentation {

  public static class EventLocator {

    private static ImMap<string, ImMap<string, LocatorTarget>> RegistrationList =
      Im.Map<ImMap<string, LocatorTarget>>()
        / "mypage" * MypageLocatorList.List
        / "popup" * PopupLocatorList.List
        / "dummy_popup" * DummyPopupLocatorList.List
        ;

    private static ImList<LocatorPlug> Plugs =
      new ImList<LocatorPlug>()
        ;

    private static Locator locator = new Locator(RegistrationList, Plugs);

    public static List<RenderCommand> Dispatch(string domain, string address, EventParameters eventParameters) {
      var eventHandler = locator.Locate(domain, address) as EventHandler;
      return eventHandler.Handle(eventParameters);
    }
  }

}
