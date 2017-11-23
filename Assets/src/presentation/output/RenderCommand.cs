namespace Arena.Presentation {

  public enum RenderCommandType {
    Print,
    OpenPopup,
    ClosePopup,
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

    public class OpenPopup : RenderCommand {
      public PopupType Popup { get; }

      public OpenPopup(PopupType popup) {
        Popup = popup;
        Type = RenderCommandType.OpenPopup;
      }
    }

    public class ClosePopup : RenderCommand {
      public PopupType Popup { get; }

      public ClosePopup(PopupType popup) {
        Popup = popup;
        Type = RenderCommandType.ClosePopup;
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
