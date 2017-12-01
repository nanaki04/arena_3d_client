using System.Collections.Generic;

namespace Arena.Presentation {

  public enum RenderCommandType {
    Print,
    RenderPopups,
    Delegate,
    SelectRadioButton,
    LoadContent,
    SendPackage,
    SendPhoenixPackage,
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

    public class Delegate : RenderCommand {
      public Delegate() {
        Type = RenderCommandType.Delegate;
      }
    }

    public class SelectRadioButton : RenderCommand {
      public int Index { get; }

      public SelectRadioButton(int index) {
        Index = index;
        Type = RenderCommandType.SelectRadioButton;
      }
    }

    public class LoadContent : RenderCommand {
      public int Index { get; }

      public LoadContent(int index) {
        Index = index;
        Type = RenderCommandType.LoadContent;
      }
    }

    public class SendPackage : RenderCommand {
      public DataPackage dataPackage;

      public SendPackage(DataPackage package) {
        dataPackage = package;
        Type = RenderCommandType.SendPackage;
      }
    }

    public class SendPhoenixPackage : RenderCommand {
      public PhoenixSocketPackage dataPackage;

      public SendPhoenixPackage(PhoenixSocketPackage package) {
        dataPackage = package;
        Type = RenderCommandType.SendPhoenixPackage;
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
