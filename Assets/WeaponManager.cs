using UnityEngine;
using System.Collections;

public class WeaponManager : MonoBehaviour
{
  public MonoBehaviour[] Weapons = new MonoBehaviour[0];
  public int CurrentWeaponIndex = 0;

	void Start () {
    foreach (var weapon in Weapons)
      weapon.enabled = false;
    ActivateCurrentWeapon();
  }
	
  public bool CanUpgradeWeapon()
  {
    return CurrentWeaponIndex < Weapons.Length - 1;
  }

  public void UpgradeWeapon()
  {
    DeactivateCurrentWeapon();
    CurrentWeaponIndex++;
    ActivateCurrentWeapon();
  }

  private void DeactivateCurrentWeapon()
  {
    Weapons[CurrentWeaponIndex].enabled = false;
  }

  private void ActivateCurrentWeapon()
  {
    Weapons[CurrentWeaponIndex].enabled = true;
  }
}
