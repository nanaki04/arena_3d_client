using System.Collections.Generic;

namespace Arena.Presentation {

  public enum RenderCommandType {
    Print,
    RenderPopups,
    Debug,
    Error
  }

  public abstract class RenderCommand {
    public RenderCommandType Type { get; set; }

    public class Print : RenderCommand {
      public string Text { get; }

      public Print(string text) {
        Text = text;
        Type = RenderCommandType.Print;
      }
    }

    public class RenderPopups : RenderCommand {
      public List<PopupType> OpenPopups { get; }

      public RenderPopups(List<PopupType> openPopups) {
        OpenPopups = openPopups;
        Type = RenderCommandType.RenderPopups;
      }
    }

    public class Debug : RenderCommand {
      public string Log { get; }

      public Debug(string log) {
        Log = log;
        Type = RenderCommandType.Debug;
      }
    }

    public class Error : RenderCommand {
      public string Reason { get; }

      public Error(string reason) {
        Reason = reason;
        Type = RenderCommandType.Error;
      }
    }

  }

}
