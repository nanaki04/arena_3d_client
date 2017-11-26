using UnityEngine;
using System.Collections.Generic;

namespace Arena.View {

  public class Operator : MonoBehaviour {
    public List<GameObject> Connections;

    public void Delegate() {
      foreach (GameObject connection in Connections) {
        var locatorEvent = connection.GetComponent<LocatorEvent>();
        if (locatorEvent == null) {
          return;
        }
        locatorEvent.fireEvent();
      }
    }
  }

}
