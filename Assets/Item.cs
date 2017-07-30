using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {
  public float Weight = 1.0f;

  public ItemInfo GetItemInfo()
  {
    Debug.Log(Weight);
    return new ItemInfo()
    {
      Weight = Weight
    };
  }

  void OnPickedUp()
  {
  }
}

public class ItemInfo
{
  public float Weight = 1.0f;
}