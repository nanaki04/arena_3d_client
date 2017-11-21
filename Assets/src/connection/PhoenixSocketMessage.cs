using System;
using System.Collections.Generic;

[Serializable]
public struct PhoenixSocketMessage {
  public string topic;
  public string @event;
  public int @ref;
  public PhoenixSocketPayload payload;

  public PhoenixSocketMessage(
    string topic,
    string @event,
    PhoenixSocketPayload payload
  ) {
    this.topic = topic;
    this.@event = @event;
    this.payload = payload;
    @ref = 1;
  }

}
