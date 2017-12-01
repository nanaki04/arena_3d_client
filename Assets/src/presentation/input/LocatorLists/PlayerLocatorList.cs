using Arena.Modules;

namespace Arena.Presentation {

  public static class PlayerLocatorList {
    public static ImMap<string, LocatorTarget> List = Im.Map<LocatorTarget>()
      / "login" * LoginEvent.Stock
      ;
  }

}
