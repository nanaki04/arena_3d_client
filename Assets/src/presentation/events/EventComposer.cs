using System.Collections.Generic;
using Arena.Modules;

namespace Arena.Presentation {

  public static class EventComposition {
    static List<CompositionPlug<State>> Plugs =
      new List<CompositionPlug<State>>() {

      };

    public static Composer<State> EventComposer =
      new Composer<State>(Plugs);
  }

}
