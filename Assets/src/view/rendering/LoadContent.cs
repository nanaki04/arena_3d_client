using UnityEngine;
using UnityEngine.UI;
using Arena.Presentation;

namespace Arena.View {

  public static class LoadContent {

    public static void Render(GameObject gameObject, RenderCommand renderCommand) {
      int index = (renderCommand as RenderCommand.LoadContent).Index;
      ContentSpawner contentSpawner = gameObject.GetComponent<ContentSpawner>();
      if (contentSpawner == null) {
        Debug.Log("Tring to load content, but no content spawner component found");
        return;
      }
      contentSpawner.Spawn(index);
    }

  }

}
