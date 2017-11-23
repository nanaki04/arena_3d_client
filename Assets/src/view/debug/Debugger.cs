using UnityEngine;
using Arena;

public class Debugger : MonoBehaviour {

  public void Start() {
    //var renderData = Locators.locate("debug", "state", I.Null());
    var renderData = Locators.locate("battle", "login", I.Null());
    Debug.Log((renderData[0] as Core.RenderCommand.Print).Item);
  }

}
