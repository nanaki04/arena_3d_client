using System.Collections.Generic;
using UnityEngine;

namespace Arena.View {

  public class ContentSpawner : MonoBehaviour {
    public List<GameObject> ContentList;
    GameObject SpawnedObject;

    public void Spawn(int index) {
      if (ContentList.Count <= index) {
        return;
      }
      if (SpawnedObject != null) {
        Object.Destroy(SpawnedObject);
      }
      var contentPrefab = ContentList[index];
      var content = Instantiate(contentPrefab) as GameObject;
      content.transform.SetParent(this.gameObject.transform, false);
      SpawnedObject = content;
    }

  }

}
