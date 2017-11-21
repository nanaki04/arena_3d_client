using UnityEngine;
using System;

public class WebsocketPackage : Package<string> {
  private string parsed;
  private string serialized;

  public void pack(string data) {
    parsed = data;
  }

  public void receive(string data) {
    serialized = data;
  }

  public string serialize() {
    if (serialized != null) {
      return serialized;
    }
    if (parsed == null) {
      Debug.Log("no data to serialize");
      return "";
    }
    // TODO serialize
    return parsed as object as string;
  }

  public string parse() {
    if (parsed != null) {
      return parsed;
    }
    if (serialized == null) {
      Debug.Log("no data to parse");
      return "";
    }
    // TODO parse
    return serialized;
  }
  
}
