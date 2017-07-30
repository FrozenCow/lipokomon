using UnityEngine;
using System.Collections;

public class Randomized : MonoBehaviour {
  public float Chance = 0.5f;
	void Start () {
    if (Random.Range(0.0f, 1.0f) > Chance)
      Destroy(gameObject);
	}
}
