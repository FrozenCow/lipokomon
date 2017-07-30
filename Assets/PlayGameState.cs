using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Linq;

public class PlayGameState : MonoBehaviour {

  public GameObject Player;

  public void OnEnable()
  {
    Debug.Log("Enabled");
    Player.GetComponent<PlayerInput>().enabled = true;
  }

  void OnDisable() {
    Debug.Log("Disabled");
    Player.GetComponent<PlayerInput>().enabled = false;
  }
}
