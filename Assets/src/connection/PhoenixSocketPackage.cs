using System;
using UnityEngine;

[Serializable]
public class PhoenixSocketPackage : Package<PhoenixSocketMessage> {

  private PhoenixSocketMessage parsed;
  private string serialized;

  public PhoenixSocketPackage(PhoenixSocketMessage data) {
    pack(data);
  }

  public void pack(PhoenixSocketMessage data) {
    parsed = data;
  }

  public void receive(string data) {
    serialized = data;
  }

  public string serialize() {
    return JsonUtility.ToJson(parsed);
  }

  public PhoenixSocketMessage parse() {
    return JsonUtility.FromJson<PhoenixSocketMessage>(serialized);;
  }
}
