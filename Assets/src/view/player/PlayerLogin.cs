using UnityEngine;
using Arena.Presentation;

namespace Arena.View {

  public class PlayerLogin : MonoBehaviour {

    private LocatorEvent locatorEvent;

    public void Start() {
      locatorEvent = gameObject.AddComponent<LocatorEvent>();
      locatorEvent.address = "login";
      locatorEvent.fireEvent();
    }

  }

}
