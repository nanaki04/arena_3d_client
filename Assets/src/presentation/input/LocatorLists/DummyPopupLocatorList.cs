using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public static class DummyPopupLocatorList {
    public static ImMap<string, LocatorTarget> List = Im.Map<LocatorTarget>()
      / "ok" * ClosePopupEvent.Run
      / "cancel" * ClosePopupEvent.Run
      ;
  }

}
