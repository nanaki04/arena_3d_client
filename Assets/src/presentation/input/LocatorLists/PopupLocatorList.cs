using Arena.Modules;

namespace Arena.Presentation {

  public static class PopupLocatorList {
    public static ImMap<string, LocatorTarget> List = Im.Map<LocatorTarget>()
      / "tick" * PopupOverlayTickEvent.Run
      ;
  }

}
