using UnityEngine;
using System.Collections;

public class TutorialGameState : MonoBehaviour {

  public CanvasGroup[] groups = new CanvasGroup[0];
  private int CurrentGroupIndex = 0;
  public GameObject player;

  void OnEnable () {
    SetGroup(0);
    StartCoroutine(Run());
	}

  void SetGroup(int i)
  {
    CurrentGroupIndex = i;
    foreach (var group in groups)
      group.alpha = 0.0f;
    groups[CurrentGroupIndex].alpha = 1.0f;
  }
	
  IEnumerator Wait()
  {
    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));
    yield return new WaitForSeconds(0.2f);
  }

  IEnumerator Run()
  {
    player.GetComponent<PlayerInput>().enabled = false;
    for (var i=0;i<groups.Length;i++)
    {
      SetGroup(i);
      yield return Wait();
    }
    player.GetComponent<PlayerInput>().enabled = true;

    GameStateManager.Default.SwitchToPlay();
  }

  void OnDisable()
  {
    StopAllCoroutines();
  }

}
