using System;
using UnityEngine;
using Newtonsoft.Json;

public class PhoenixSocketPackage : Package {

  static JsonSerializerSettings settings = new JsonSerializerSettings();

  static PhoenixSocketPackage() {
    settings.CheckAdditionalContent = false;
    settings.NullValueHandling = NullValueHandling.Ignore;
    settings.MetadataPropertyHandling = MetadataPropertyHandling.ReadAhead;
    settings.TypeNameHandling = TypeNameHandling.Auto;
  }

  private string serialized;

  public PhoenixSocketPackage() {
    serialized = "";
  }

  public PhoenixSocketPackage(string serializedData) {
    receive(serializedData);
  }

  public void pack<T>(T data) {
    serialized = JsonConvert.SerializeObject(data, settings);
  }

  public void receive(string data) {
    serialized = data;
  }

  public string serialize() {
    return serialized;
  }

  public T parse<T>() {
    return JsonConvert.DeserializeObject<T>(serialized, settings);
  }
}
