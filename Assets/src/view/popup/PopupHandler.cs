using Arena.Presentation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Arena.View {

  public class PopupHandler : MonoBehaviour {
    static Dictionary<PopupType, string> Paths =
      new Dictionary<PopupType, string>() {
        { PopupType.Dummy, "popups/DummyPopup" }
      };

    public GameObject PopupOverlay;

    public void Start() {
    }

    Dictionary<PopupType, GameObject> OpenPopups =
      new Dictionary<PopupType, GameObject>();

    public void UpdatePopups(List<PopupType> openPopups) {
      var previousOpenPopups = new List<PopupType>(OpenPopups.Keys);
      var toClose = previousOpenPopups.Except(openPopups);
      var toOpen = openPopups.Except(previousOpenPopups);
      foreach (PopupType popupToClose in toClose) {
        Close(popupToClose);
      }
      foreach (PopupType popupToOpen in toOpen) {
        Open(popupToOpen);
      }
    }

    public void Open(PopupType popupType) {
      if (OpenPopups.ContainsKey(popupType)) {
        return;
      }
      if (PopupOverlay == null) {
        return;
      }
      var popup = Instantiate(popupType);
      OpenPopups.Add(popupType, popup);
      PopupOverlay.SetActive(true);
      popup.transform.SetParent(PopupOverlay.transform, false);
    }

    public void Close(PopupType popupType) {
      if (!OpenPopups.ContainsKey(popupType)) {
        return;
      }
      var popup = OpenPopups[popupType];
      OpenPopups.Remove(popupType);
      Object.Destroy(popup);
      if (OpenPopups.Count > 0) {
        return;
      }
      PopupOverlay.SetActive(false);
    }

    private GameObject Instantiate(PopupType popupType) {
      if (!Paths.ContainsKey(popupType)) {
        return new GameObject();
      }
      return Instantiate(Resources.Load(Paths[popupType])) as GameObject;
    }
  }

}
