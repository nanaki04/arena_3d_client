using UnityEngine;
using UnityEngine.UI;
using Arena;

public class LocatorEvent : MonoBehaviour {

  public string address;
  public bool fireOnStart = false;

  public void Start() {
    if (fireOnStart == true) {
      fireEvent();
    }
  }

  public void fireEvent() {
    var domain = findDomain();
    if (domain == null) {
      Debug.Log("WARNING: no domain root found");
      return;
    }
    var renderData = Locators.locate(domain, address, I.Null());
    foreach(Core.RenderCommand<Core.State> renderCommand in renderData)
      print(renderCommand);
  }

  private string findDomain() {
    DomainRoot domainRoot = this.gameObject.GetComponentInParent<DomainRoot>();
    return domainRoot.domain;
  }

  // TEMP
  private void print(Core.RenderCommand<Core.State> renderCommand) {
    if (renderCommand.Tag != Core.RenderCommand<Core.State>.Tags.Print) {
      return;
    }
    string text = (renderCommand as Core.RenderCommand<Core.State>.Print).Item;
    Text textComponent = this.gameObject.GetComponent<Text>();
    textComponent.text = text;
  }

}
