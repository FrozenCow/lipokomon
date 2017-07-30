using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class Inventory : MonoBehaviour {
  public List<ItemInfo> contents = new List<ItemInfo>();

  void OnCollisionEnter(Collision collision)
  {
    Debug.Log("CollisionEnter");
    var collider = collision.collider;
    if (collider == null) return;
    var gameObject = collider.gameObject;
    var item = gameObject.GetComponent<Item>();
    if (item == null) return;
    PerformPickup(item);
  }

  void OnControllerColliderHit(ControllerColliderHit hit)
  {
    var collider = hit.collider;
    if (collider == null) return;
    var gameObject = collider.gameObject;
    if (gameObject == null) return;
    if (gameObject.GetComponent<Item>() != null)
      PerformPickup(gameObject.GetComponent<Item>());
  }

  void PerformPickup(Item item)
  {
    Debug.Log("PerformPickup");
    item.SendMessage("OnPickedUp", this);
    contents.Add(item.GetItemInfo());
    Destroy(item.gameObject);
  }

  void PerformDropoff(GameObject gameObject)
  {
    gameObject.SendMessage("OnDropoff", new Dropoff()
    {
      Dropper = gameObject,
      Contents = contents
    });
    contents.Clear();
  }
}

public class Dropoff
{
  public GameObject Dropper;
  public List<ItemInfo> Contents;
}
