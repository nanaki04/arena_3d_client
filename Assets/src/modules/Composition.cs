using System.Collections.Generic;

namespace Arena.Modules {

  public delegate T Handler<T>(T input);

  public interface CompositionPlug<T> {
    T TransformIn(T input);
    T TransformOut(T output, Note<T> next);
  }

  public class Composer<T> {
    List<CompositionPlug<T>> Plugs;

    public Composer(List<CompositionPlug<T>> plugs) {
      Plugs = plugs;
    }

    public Note<T> Compose(Handler<T> handler) {
      return new Note<T>(handler, Plugs);
    }

    public static Note<T> operator +(Composer<T> composer, Handler<T> handler) {
      return composer.Compose(handler);
    }
  }

  public class Note<T> {
    public Note<T> Next { get; set; }
    public List<CompositionPlug<T>> Plugs { get; set; }
    Handler<T> noteHandler;

    public Note(Handler<T> handler) {
      noteHandler = handler;
      Plugs = new List<CompositionPlug<T>>();
    }

    public Note(Handler<T> handler, List<CompositionPlug<T>> plugs) {
      noteHandler = handler;
      Plugs = plugs;
    }

    public T Play(T input) {
      input = TransformIn(input);
      var output = noteHandler(input);
      if (Next == null) {
        return output;
      }
      return TransformOut(output, Next);
    }

    private T TransformIn(T input) {
      foreach (CompositionPlug<T> plug in Plugs) {
        input = plug.TransformIn(input);
      }
      return input;
    }

    private T TransformOut(T output, Note<T> next) {
      if (Plugs.Count == 0) {
        return next.Play(output);
      }
      foreach (CompositionPlug<T> plug in Plugs) {
        output = plug.TransformOut(output, next);
      }
      return output;
    }

    public static Note<T> operator +(Note<T> current, Note<T> next) {
      var last = GetLast(current);
      last.Next = next;
      return current;
    }

    public static Note<T> operator +(Note<T> current, Handler<T> handler) {
      var next = new Note<T>(handler, current.Plugs);
      return current + next;
    }

    public static Note<T> GetLast(Note<T> current) {
      if (current.Next == null) {
        return current;
      }
      return GetLast(current.Next);
    }

  }

}
