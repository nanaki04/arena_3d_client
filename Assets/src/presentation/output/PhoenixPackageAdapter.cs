using Arena.Modules;

namespace Arena.Presentation {

  public static class PhoenixPackageAdapter {

    public static PhoenixSocketPackage Transform(DataPackage package) {
      var phoenixEventPayload = new PhoenixEventPayload();
      phoenixEventPayload.events = package.Payload.Events.GetList();
      phoenixEventPayload.progress = package.Payload.Progress;

      var phoenixMessage = new PhoenixSocketMessage<PhoenixEventPayload>();
      phoenixMessage.topic = package.Path;
      phoenixMessage.@event = package.Method;
      phoenixMessage.payload = phoenixEventPayload;

      var phoenixPackage = new PhoenixSocketPackage();
      phoenixPackage.pack<PhoenixSocketMessage<PhoenixEventPayload>>(phoenixMessage);

      return phoenixPackage;
    }

    public static DataPackage Transform(PhoenixSocketPackage phoenixPackage) {
      var phoenixMessage = phoenixPackage.parse<PhoenixSocketMessage<PhoenixEventPayload>>();
      var phoenixEventPayload = phoenixMessage.payload;

      var payload = new DataPackagePayload();
      payload.Events = new ImList<Event>(phoenixEventPayload.events);
      payload.Progress = phoenixEventPayload.progress;

      var package = new DataPackage();
      package.Path = phoenixMessage.topic;
      package.Method = phoenixMessage.@event;
      package.Payload = payload;

      return package;
    }

  }

}
