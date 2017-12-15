using UnityEngine;
using Arena.Presentation;
using Arena.Modules;
using System;
using System.Collections.Generic;

namespace Arena.View {

  public static class PublicRenderer {

    private static ImMap<RenderCommandType, Action<GameObject, RenderCommand>> renderers =
      new ImMap<RenderCommandType, Action<GameObject, RenderCommand>>()
        / RenderCommandType.Connect             * Connect.Render
        / RenderCommandType.Disconnect          * Disconnect.Render
        / RenderCommandType.Print               * Print.Render
        / RenderCommandType.Delegate            * Delegate.Render
        / RenderCommandType.SelectRadioButton   * SelectRadioButton.Render
        / RenderCommandType.LoadContent         * LoadContent.Render
        / RenderCommandType.RenderPopups        * RenderPopups.Render
        / RenderCommandType.SendPhoenixPackage  * SendPhoenixPackage.Render
        ;

    public static void Render(GameObject gameObject, ImList<RenderCommand> renderData) {
      Im.Fold(HandleRenderCommand, gameObject, renderData);
    }

    private static GameObject HandleRenderCommand(GameObject gameObject, RenderCommand renderCommand) {
      Im.Maybe((render, _) => render(gameObject, renderCommand), renderCommand.Type, renderers);
      return gameObject;
    }

  }

}
