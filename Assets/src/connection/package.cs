using System;

public interface Package {
  void pack<T>(T data);
  void receive(string data);
  string serialize();
  T parse<T>();
}
