using UnityEngine;
using UnityEngine.EventSystems;

namespace Arena.View {

  public class DragListener : MonoBehaviour, IBeginDragHandler, IEndDragHandler, IDragHandler {
    public void OnBeginDrag(PointerEventData e) {
      Debug.Log("BEGIN DRAG");
    }

    public void OnEndDrag(PointerEventData e) {
      Debug.Log("END DRAG");
    }

    public void OnDrag(PointerEventData e) {
      Debug.Log("DRAGGING");
    }
  }

}
