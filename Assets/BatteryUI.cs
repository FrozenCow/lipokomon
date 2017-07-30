using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatteryUI : MonoBehaviour {
  public Battery battery;
  public BarUI barUI;
  	
	void Update () {
    barUI.SetFraction(battery.charge / battery.maxCharge);
	}
}
