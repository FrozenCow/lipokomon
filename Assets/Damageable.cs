using UnityEngine;
using System.Collections;

public class Damageable : MonoBehaviour {
  public float Health = 1f;
  public float MaxHealth = 1f;

	void Start ()
  {
    Health = MaxHealth;
	}

  void OnDamage(Damage damage)
  {
    if (Health <= 0.0f)
      return;
    damage.Handled = true;

    Health -= damage.Amount;
    if (Health <= 0.0f)
    {
      SendMessage("OnDied", SendMessageOptions.DontRequireReceiver);
    }
  }
}

public class Damage
{
  public bool Handled { get; set; }
  public float Amount { get; set; }
  public Vector3 Position { get; set; }

  public Damage(Vector3 position, float amount)
  {
    this.Position = position;
    this.Amount = amount;
  }
}
