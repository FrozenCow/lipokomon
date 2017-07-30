using UnityEngine;
using System.Collections;

public class WinGameState : MonoBehaviour {
  public GameObject player;

	void Start () {
    StartCoroutine(Run());
	}

  IEnumerator Run()
  {
    var PlayerInput = player.GetComponent<PlayerInput>();
    PlayerInput.enabled = false;
    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    PlayerInput.enabled = true;
    GameStateManager.Default.SwitchToPlay();
  }

  void OnDisable()
  {
    StopAllCoroutines();
  }
}
