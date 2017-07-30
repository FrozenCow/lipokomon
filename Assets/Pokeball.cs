using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pokeball : MonoBehaviour {
  public float minAngle;
  public float maxAngle;
  public float minSpeed;
  public float maxSpeed;
  public float minAngularVelocity;
  public float maxAngularVelocity;
  private Vector2 lineairVelocity;
  private float angularVelocity;
  public float lifetime;
  [HideInInspector]
  public Pokomon enemy;

  private void Start()
  {
    var angle = Random.Range(minAngle, maxAngle) * Mathf.Deg2Rad;
    lineairVelocity = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Random.Range(minSpeed, maxSpeed);
    angularVelocity = Mathf.PI * 2 * Random.Range(minAngularVelocity, maxAngularVelocity) * (Random.Range(0, 2) * 2 - 1);
  }

  void Update () {
    var add = lineairVelocity * Time.deltaTime;
    transform.Translate(add.x, add.y, 0);
    //transform.RotateAround(transform.localPosition, Vector3.forward, angularVelocity * Time.deltaTime);
    lineairVelocity.y -= 3000.0f * Time.deltaTime;
    var oldLifetime = lifetime;
    lifetime -= Time.deltaTime;
    if (oldLifetime > 1.0f && lifetime <= 1.0f)
    {
      var randomAngle = Random.Range(0, Mathf.PI * 2.0f);
      lineairVelocity = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)) * 2000.0f;
      enemy.Damage(1.0f);
    }
    if (oldLifetime > 0.0f && lifetime <= 0.0f)
    {
      Destroy(gameObject);
    }
  }
}
