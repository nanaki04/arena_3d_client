using Arena.Presentation;
using System.Collections.Generic;
using UnityEngine;

namespace Arena.View {

  public static class PopupHandler {
    static Dictionary<PopupType, string> Paths =
      new Dictionary<PopupType, string>() {
        { PopupType.Dummy, "popups/DummyPopup" }
      };

    static Dictionary<PopupType, GameObject> OpenPopups =
      new Dictionary<PopupType, GameObject>();

    public static void Open(PopupType popupType) {
      if (OpenPopups.ContainsKey(popupType)) {
        return;
      }
      var overlay = GameObject.FindWithTag("popup_overlay");
      if (overlay == null) {
        return;
      }
      var popup = Instantiate(popupType);
      OpenPopups.Add(popupType, popup);
      popup.transform.parent = overlay.transform;
    }

    public static void Close(PopupType popupType) {
      if (!OpenPopups.ContainsKey(popupType)) {
        return;
      }
      var popup = OpenPopups[popupType];
      OpenPopups.Remove(popupType);
      Object.Destroy(popup);
    }

    private static GameObject Instantiate(PopupType popupType) {
      if (!Paths.ContainsKey(popupType)) {
        return new GameObject();
      }
      return Resources.Load(Paths[popupType]) as GameObject;
    }
  }

}
