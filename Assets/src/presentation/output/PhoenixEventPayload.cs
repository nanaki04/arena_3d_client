using System;
using System.Collections.Generic;

namespace Arena.Presentation {

  [Serializable]
  public class PhoenixEventPayload : PhoenixSocketPayload {
    public string progress;
    public List<Event> events;

    public PhoenixEventPayload() {
      progress = "";
      events = new List<Event>();
    }
  }

}
