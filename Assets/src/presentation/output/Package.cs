using System.Collections.Generic;
using System;
using Arena.Modules;

namespace Arena.Presentation {

  public struct DataPackagePayload {
    public ImList<Event> Events;
    public string Progress;
  }

  public struct DataPackage {
    public string Path;
    public string Method;
    public DataPackagePayload Payload;
  }

}
