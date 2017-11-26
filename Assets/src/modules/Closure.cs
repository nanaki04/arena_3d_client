using System;

namespace Arena.Modules {

  public class Closure<T> {
    Action<T> Handler;
    T Val;

    public Closure(Action<T> handler, T val) {
      Handler = handler;
      Val = val;
    }

    public void Run() {
      Handler(Val);
    }

    public static Action New(Action<T> handler, T val) {
      var closure = new Closure<T>(handler, val);
      return closure.Run;
    }
  }

  public class Closure<T, R> {
    Func<T, R> Handler;
    T Val;

    public Closure(Func<T, R> handler, T val) {
      Handler = handler;
      Val = val;
    }

    public R Run() {
      return Handler(Val);
    }

    public static Func<R> New(Func<T, R> handler, T val) {
      var closure = new Closure<T, R>(handler, val);
      return closure.Run;
    }
  }

}
