using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Battery : MonoBehaviour {

  public float maxCharge = 10.0f;
  public float charge = 10.0f;

  void Update () {
    if (charge > 0.0f)
    {
      charge -= Time.deltaTime;
      if (charge < 0.0f)
      {
        charge = 0.0f;
        BroadcastMessage("OnBatteryEmpty");
      }
    }
	}
}
