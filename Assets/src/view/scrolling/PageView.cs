using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

namespace Arena.View {

  public class PageView : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler, IScrollHandler {

    public GameObject Content;
    RectTransform ContentTransform;
    List<GameObject> Pages;
    float Spacing;
    float PageHeight;
    bool Dirty = false;
    int CurrentPage;

    public void Start() {
      if (!Content) {
        return;
      }
      ContentTransform = Content.GetComponent<RectTransform>() as RectTransform;
      var Layout = Content.GetComponent<VerticalLayoutGroup>() as VerticalLayoutGroup;
      Spacing = Layout.spacing;
      CurrentPage = 0;
    }

    public void Update() {
      if (!Dirty) {
        return;
      }

      var dt = Time.deltaTime;
      var dest = PageHeight * CurrentPage;
      var currentPos = GetScrollPos();
      if (dest == currentPos) {
        return;
      }

      var direction = dest > currentPos ? 1 : -1;
      var pos = ContentTransform.anchoredPosition;
      pos.y += 800 * dt * direction;
      if (Mathf.Abs(pos.y - dest) < 10) {
        pos.y = dest;
        Dirty = false;
      }
      ContentTransform.anchoredPosition = pos;
    }

    public void AddPages(List<GameObject> pages) {
      Pages = pages;
      if (pages.Count < 1) {
        return;
      }
      foreach (GameObject page in Pages) {
        page.transform.SetParent(Content.transform);
      }

      var first = pages[0].GetComponent<RectTransform>() as RectTransform;
      Canvas.ForceUpdateCanvases();
      PageHeight = first.rect.height + Spacing;
    }

    public void OnBeginDrag(PointerEventData _) {
      Dirty = false;
    }

    public void OnDrag(PointerEventData _) {
      Dirty = false;
    }

    public void OnEndDrag(PointerEventData _) {
      CurrentPage = GetClosestPage();
      Dirty = true;
    }

    public void OnScroll(PointerEventData _) {
    }

    public void ScrollToPage(int index) {
      Debug.Log("scroll to page: " + index);
      Debug.Log("CurrentPage: " + CurrentPage);
      var currentPage = CurrentPage;
      if (index < 0) {
        CurrentPage = 0;
      } else if (index >= Pages.Count) {
        CurrentPage = Pages.Count - 1;
      } else {
        CurrentPage = index;
      }
      Dirty = currentPage != CurrentPage;
      Debug.Log("Dirty: " + Dirty);
    }

    public void ScrollToNextPage() {
      Debug.Log("scroll next page");
      ScrollToPage(CurrentPage + 1);
    }

    public void ScrollToPreviousPage() {
      Debug.Log("scroll prev page");
      ScrollToPage(CurrentPage - 1);
    }

    public void JumpToPage(int index) {
      var y = PageHeight * index;
      var pos = ContentTransform.anchoredPosition;
      pos.y = y;
      ContentTransform.anchoredPosition = pos;
    }

    private float GetScrollPos() {
      return ContentTransform.anchoredPosition.y;
    }

    private int GetClosestPage() {
      var closestPage = Mathf.FloorToInt((GetScrollPos() + PageHeight / 2) / PageHeight);
      if (closestPage < 0) {
        return 0;
      }
      if (closestPage >= Pages.Count) {
        return Pages.Count - 1;
      }
      return closestPage;
    }

  }

}
