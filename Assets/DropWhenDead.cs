using UnityEngine;
using System.Collections;

public class DropWhenDead : MonoBehaviour {

  public Transform droppedObject;

  void OnDied()
  {
    Instantiate(droppedObject, transform.position, Quaternion.identity);

    Destroy(gameObject);
  }
}
