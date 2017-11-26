using System;

namespace Arena.Modules {

  public class Curry<A1, A2, R> {
    public Func<A1, A2, R> Handler;
    A1 Arg1;

    public Curry(Func<A1, A2, R> handler) {
      Handler = handler;
    }

    public static Func<A1, Func<A2, R>> New(Func<A1, A2, R> handler) {
      var curry = new Curry<A1, A2, R>(handler);
      return new Func<A1, Func<A2, R>>(curry.AddFirst);
    }

    public Func<A2, R> AddFirst(A1 arg1) {
      Arg1 = arg1;
      return new Func<A2, R>(Resolve);
    }

    public R Resolve(A2 arg2) {
      return Handler(Arg1, arg2);
    }
  }

  public class Curry<A1, A2, A3, R> {
    public Func<A1, A2, A3, R> Handler;
    A1 Arg1;
    Func<A1, A2, Func<A3, R>> SubCurry;

    public Curry(Func<A1, A2, A3, R> handler) {
      Handler = handler;
    }

    public static Func<A1, Func<A2, Func<A3, R>>> New(Func<A1, A2, A3, R> handler) {
      var curry = new Curry<A1, A2, A3, R>(handler);
      return new Func<A1, Func<A2, Func<A3, R>>>(curry.AddFirst);
    }

    public R SubCurryHandler(A2 arg2, A3 arg3) {
      return Handler(Arg1, arg2, arg3);
    }

    public Func<A2, Func<A3, R>> AddFirst(A1 arg1) {
      Arg1 = arg1;
      return Curry<A2, A3, R>.New(SubCurryHandler);
    }
  }

}
