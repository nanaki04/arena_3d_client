using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Arena.Presentation;

public class LocatorEvent : MonoBehaviour {

  public string address;
  public bool fireOnStart = false;
  public EventParameters eventParameters = new EventParameters();

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
    var renderData = EventLocator.Dispatch(domain, address, eventParameters);
    PublicRenderer.Render(this.gameObject, renderData);
  }

  private string findDomain() {
    DomainRoot domainRoot = this.gameObject.GetComponentInParent<DomainRoot>();
    return domainRoot.domain;
  }

}
