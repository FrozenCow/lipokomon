using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
  public List<Pokomon> pokomons;
  public Charger lastCharger;

  public void OnBatteryEmpty()
  {
    GameStateManager.Default.OnPlayerDied();
  }

  public void OnControllerColliderHit(ControllerColliderHit hit)
  { 
    var pokomon = hit.gameObject.GetComponent<Pokomon>();
    if (pokomon == null)
      return;
    GameStateManager.Default.SwitchToBattle(pokomon);
  }

  public void OnTriggerEnter(Collider other)
  {
    var charger = other.GetComponent<Charger>();
    if (charger != null)
    {
      lastCharger = charger;
      StartCoroutine(Charging());
      return;
    }
    var gym = other.GetComponent<Gym>();
    if (gym != null && pokomons.Count > 0)
    {
      GameStateManager.Default.SwitchToGym(gym);
    }
  }

  public IEnumerator Charging()
  {
    var battery = GetComponent<Battery>();
    gameObject.GetComponentInChildren<Renderer>().enabled = false;
    gameObject.GetComponentInChildren<PlayerInput>().enabled = false;
    do
    {
      yield return null;
      battery.charge = Mathf.Min(battery.maxCharge, battery.charge + Time.deltaTime * 10.0f);
    }
    while (battery.charge < battery.maxCharge);
    gameObject.GetComponentInChildren<Renderer>().enabled = true;
    gameObject.GetComponentInChildren<PlayerInput>().enabled = true;
  }

  public void AddPokomon(Pokomon pokomon)
  {
    pokomon.transform.parent = transform.Find("Pokomons");
    pokomon.gameObject.SetActive(false);
    pokomon.Health = pokomon.MaxHealth;
    pokomons.Add(pokomon);
  }
}
