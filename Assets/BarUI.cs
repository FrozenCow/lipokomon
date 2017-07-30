using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarUI : MonoBehaviour {
  public RectTransform bar;
  public RectTransform indicator;

  public void SetFraction(float fraction)
  {
    indicator.sizeDelta = new Vector2(bar.rect.width * fraction, indicator.sizeDelta.y);
  }
}
