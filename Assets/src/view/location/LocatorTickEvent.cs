using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using Arena.Presentation;

namespace Arena.View {

  public class LocatorTickEvent : MonoBehaviour {

    public string address;
    public EventParameters eventParameters = new EventParameters();
    string domain;
    bool paused = false;

    public void Start() {
      domain = findDomain();
      if (domain == null) {
        Debug.Log("WARNING: no domain root found");
      }
    }
    
    public void Update() {
      if (paused) {
        return;
      }
      fireEvent();
    }

    public void Pause() {
      paused = true;
    }

    public void Resume() {
      paused = false;
    }

    private void fireEvent() {
      var renderData = EventLocator.Dispatch(domain, address, eventParameters);
      PublicRenderer.Render(this.gameObject, renderData);
    }

    private string findDomain() {
      DomainRoot domainRoot = this.gameObject.GetComponentInParent<DomainRoot>();
      return domainRoot.domain;
    }

  }

}
