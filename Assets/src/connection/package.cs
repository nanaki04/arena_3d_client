using System;

public interface Package<T> {
  void pack(T data);
  void receive(string data);
  string serialize();
  T parse();
}
