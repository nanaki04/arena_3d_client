using Arena.Modules;

namespace Arena.Presentation {

  public static class MypageLocatorList {
    public static ImMap<string, LocatorTarget> List = Im.Map<LocatorTarget>()
      / "show" * ShowMailEvent.Run
      / "restore_radio_button" * RestoreRadioButtonEvent.Create("mypage")
      / "select_radio_button" * SelectRadioButtonEvent.Create("mypage")
      / "content" * LoadTabContentEvent.Create("mypage")
      ;
  }

}
