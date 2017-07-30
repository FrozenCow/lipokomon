using UnityEngine;
using System.Collections;
using System.Linq;

public class Pokomon : MonoBehaviour
{
  public CharacterController characterController;
  public SkinnedMeshRenderer Renderer;

  public float MaxHealth = 10.0f;
  public float Health = 10.0f;

  public float SeeRadius = 2.0f;
  public float NearRadius = 0.6f;
  public bool IsPassive = true;
  public bool IsScared = false;

  public bool IsHurd = false;
  public float HurdRadius = 6.0f;

  public float SlackTimeMin = 0.5f;
  public float SlackTimeMax = 5.0f;
  public float SlackRangeMin = 1.0f;
  public float SlackRangeMax = 5.0f;

  public float FleeSpeed = 1.0f;
  public float ApproachSpeed = 1.0f;
  public float IdleSpeed = 1.0f;

  public bool WasHurt = false;


  private GameObject player;
  private IEnumerator aiCoroutine = null;

  // Use this for initialization
  void Start()
  {
    characterController = GetComponent<CharacterController>();
    player = GameObject.FindGameObjectWithTag("Player");
    ResetAI();
  }

  void ResetAI()
  {
    if (aiCoroutine != null)
    {
      // Does not work?
      StopCoroutine(aiCoroutine);

      // Does work.
      StopAllCoroutines();
    }
    aiCoroutine = AI();
    StartCoroutine(aiCoroutine);
  }

  void Update()
  {

  }

  bool IsPlayerWithinRange(float radius)
  {
    return ((player.transform.position - transform.position).magnitude < radius);
  }

  bool CanSeePlayer()
  {
    return IsPlayerWithinRange(SeeRadius);
  }

  void MoveToward(Vector3 point, float speed)
  {
    var diff = (point - transform.position);
    diff.y = 0;
    diff.Normalize();
    Move(diff, speed);
  }

  void Move(Vector3 direction, float speed)
  {
    var motion = direction * speed * Time.deltaTime;
    motion.y = -1f;
    characterController.Move(motion);
  }

  Vector3 GetDirection(Vector3 point)
  {
    var diff = point - transform.position;
    diff.y = 0;
    return diff.normalized;
  }

  Vector3 GetRandomDirection()
  {
    var quaternion = Quaternion.Euler(0, Random.Range(0, 360), 0);
    return quaternion * Vector3.forward;
  }

  IEnumerator AI()
  {
    while (true)
      yield return Idle();
  }

  bool IsNear(Vector3 point)
  {
    var flattenedPoint = point;
    flattenedPoint.y = 0;
    var flattenedPosition = transform.position;
    flattenedPosition.y = 0;

    return (flattenedPoint - flattenedPosition).magnitude < NearRadius;
  }

  IEnumerator MoveTo(Vector3 point, float speed)
  {
    while (!IsNear(point))
    {
      MoveToward(point, speed);
      yield return new WaitForEndOfFrame();
    }
    Move(Vector3.zero, 0);
  }

  IEnumerator Idle()
  {
    while (true)
    {
      if ((!IsPassive || WasHurt) && CanSeePlayer())
      {
        if (IsScared)
          yield return Flee();
        else
          yield return Approach();
      }

      yield return Slack();
    }
  }

  IEnumerator Slack()
  {
    var direction = GetRandomDirection();
    var range = Random.Range(SlackRangeMin, SlackRangeMax);
    yield return MoveTo(transform.position + direction * range, IdleSpeed);
    var waitTime = Random.Range(SlackTimeMin, SlackTimeMax);
    yield return new WaitForSeconds(waitTime);
  }

  IEnumerator Flee()
  {
    while (CanSeePlayer())
    {
      Move(-GetDirection(player.transform.position), FleeSpeed);
      yield return new WaitForEndOfFrame();
    }
  }

  IEnumerator Approach()
  {
    while (CanSeePlayer())
    {
      MoveToward(player.transform.position, ApproachSpeed);
      yield return new WaitForEndOfFrame();
    }
  }

  public void Damage(float damage)
  {
    Health -= damage;
  }

  void OnHurdHelp()
  {
    AngerTriggered();
  }

  void AngerTriggered()
  {
    if (WasHurt) return;
    WasHurt = true;
    ResetAI();
  }

  public Texture Texture
  {
    get
    {
      return gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture;
    }
  }
}
