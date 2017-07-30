using UnityEngine;
using System.Collections;

public class Spear : MonoBehaviour {
  public PlayerInput Input;
  public Renderer Renderer;
  public Material IdleMaterial;
  
  public Transform Projectile;

	void Start () {
    Renderer.material = IdleMaterial;

    // Ignore projectile-player collisions
    Physics.IgnoreLayerCollision(8, 9);

  }
	
	void Update () {
	  if (Input.GetFireDown())
    {
      var hits = Physics.RaycastAll(Camera.main.ScreenPointToRay(Input.mousePosition), 100.0f);
      if (hits.Length == 0)
        return;
      var playerPosition = transform.position;
      var aimPosition = hits[0].point;
      var diff = aimPosition - playerPosition;
      // We are not interested in rotation upwards.
      diff.y = 0;
      diff.y = 1;
      var angle = Quaternion.FromToRotation(Vector3.forward, diff);
      var newProjectileTransform = (Transform)Instantiate(Projectile, transform.position, angle);
      var newProjectile = newProjectileTransform.gameObject;
    }
	}
}
