using Arena.Modules;

namespace Arena.Presentation {

  public static class LoginPopupLocatorList {
    public static ImMap<string, LocatorTarget> List = Im.Map<LocatorTarget>()
      / "ok" * LoginEvent.Run
      ;
  }

}
