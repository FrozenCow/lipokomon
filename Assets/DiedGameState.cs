using UnityEngine;
using System.Collections;

public class DiedGameState : MonoBehaviour {
  public GameObject player;
  public CanvasGroup CanvasGroup;

	void OnEnable () {
    StartCoroutine(Died());
	}

  void OnDisable()
  {
    StopAllCoroutines();
  }

  public AnimationCurve TimeCurve;
  public AnimationCurve CameraZoomCurve;
  public float CameraZoomAlive = 5.0f;
  public float CameraZoomDead = 3.0f;

  IEnumerator Died()
  {
    var Damageable = player.GetComponent<Damageable>();
    var PlayerInput = player.GetComponent<PlayerInput>();

    PlayerInput.enabled = false;
    var time = 0.0f;
    var maxtime = 3.0f;
    Time.timeScale = 0.0f;
    while (time < maxtime)
    {
      time += Time.unscaledDeltaTime;
      var fraction = time / maxtime;
      Camera.main.orthographicSize = CameraZoomDead + (CameraZoomAlive - CameraZoomDead) * Mathf.Clamp01(CameraZoomCurve.Evaluate(fraction));
      CanvasGroup.alpha = Mathf.Clamp01(TimeCurve.Evaluate(fraction));
      yield return new WaitForEndOfFrame();
    }

    yield return new WaitUntil(() => Input.GetMouseButtonDown(0));

    Damageable.Health = Damageable.MaxHealth;
    Camera.main.orthographicSize = CameraZoomAlive;
    var fireplace = GameObject.FindGameObjectWithTag("Fireplace");
    player.transform.position = fireplace.transform.position + Vector3.back * 1.0f;
    Time.timeScale = 1.0f;
    PlayerInput.enabled = true;

    GameStateManager.Default.SwitchToPlay();
  }
}
