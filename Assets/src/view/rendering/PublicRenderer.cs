using UnityEngine;
using Arena.Presentation;
using System;
using System.Collections.Generic;

public static class PublicRenderer {

  private static Dictionary<RenderCommandType, Action<GameObject, RenderCommand>> renderers =
    new Dictionary<RenderCommandType, Action<GameObject, RenderCommand>>() {
      { RenderCommandType.Print, Print.Render }
    };

  public static void Render(GameObject gameObject, List<RenderCommand> renderData) {
    foreach(RenderCommand renderCommand in renderData)
      HandleRenderCommand(gameObject, renderCommand);
  }

  private static void HandleRenderCommand(GameObject gameObject, RenderCommand renderCommand) {
    if (renderers.ContainsKey(renderCommand.Type)) {
      var render = renderers[renderCommand.Type];
      render(gameObject, renderCommand);
    }
  }

}
