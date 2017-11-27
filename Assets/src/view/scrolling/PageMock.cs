using UnityEngine;
using System.Collections.Generic;
using Arena.View;

public class PageMock : MonoBehaviour {

  public List<GameObject> Pages;

  public void Start() {
    var pageView = this.gameObject.GetComponent<PageView>() as PageView;
    var instantiatedPages = new List<GameObject>();
    foreach (GameObject page in Pages) {
      instantiatedPages.Add(Instantiate(page));
    }
    Debug.Log("adding pages");
    pageView.AddPages(instantiatedPages);
  }

}
