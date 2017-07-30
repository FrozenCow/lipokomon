using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

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
    var PlayerInput = this.player.GetComponent<PlayerInput>();

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

    yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.R));

    Time.timeScale = 1.0f;
    Camera.main.orthographicSize = CameraZoomAlive;
    CanvasGroup.alpha = 0.0f;

    var player = this.player.GetComponent<Player>();
    var charger = player.lastCharger;
    player.transform.position = new Vector3(charger.transform.position.x, player.transform.position.y, charger.transform.position.z - 1);
    player.GetComponent<Battery>().charge = player.GetComponent<Battery>().maxCharge;

    GameStateManager.Default.SwitchToPlay();
  }
}
