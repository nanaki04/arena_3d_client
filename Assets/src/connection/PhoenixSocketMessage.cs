using System;
using System.Collections.Generic;

[Serializable]
public struct PhoenixSocketMessage<T> : Message {
  public string topic;
  public string @event;
  public int @ref;
  public T payload;

  public PhoenixSocketMessage(
    string topic,
    string @event,
    T payload
  ) {
    this.topic = topic;
    this.@event = @event;
    this.payload = payload;
    @ref = 1;
  }

}
