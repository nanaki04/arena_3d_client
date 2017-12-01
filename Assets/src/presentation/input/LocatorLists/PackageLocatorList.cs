using Arena.Modules;

namespace Arena.Presentation {

  public static class PackageLocatorList {
    public static ImMap<string, LocatorTarget> List = Im.Map<LocatorTarget>()
      / "receive" * ReceivePackageEvent.Run
      / "poll"    * PollEvent.Run
      ;
  }

}
