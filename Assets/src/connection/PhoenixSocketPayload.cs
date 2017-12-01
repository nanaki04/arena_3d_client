using System;
using System.Collections.Generic;

//public enum PhoenixSocketPayloadType {
//  Message,
//  JoinReply
//}

[Serializable]
public class PhoenixSocketPayload {
  public string status;
}

[Serializable]
public class PhoenixSocketMessagePayload : PhoenixSocketPayload {
  public string message;
}

[Serializable]
public class Evnt {
  public string type;
  public string id;
  public string name;
  public Evnt() {
    type = "evnt";
  }
}

public class Evnt2 : Evnt {
  public int time;
  public Evnt2() {
    type = "evnt2";
  }
}


[Serializable]
public class PhoenixSocketEventPayload : PhoenixSocketPayload {
  public List<Evnt> events;
}

//public class PhoenixSocketMessagePayload : PhoenixSocketPayload {
//  public string message;
//
//  public PhoenixSocketMessagePayload(string msg) {
//    message = msg;
//  }
//}
//
//[Serializable]
//public class PhoenixSocketJoinReplyPayload : PhoenixSocketPayload {
//  public string status;
//}
