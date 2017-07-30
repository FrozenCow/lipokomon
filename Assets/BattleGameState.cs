using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.UI;

public class BattleGameState : MonoBehaviour {
  private Animator animator;
  private Player player;

  public Pokomon enemyPokomon;
  public BarUI enemyHealthBar;
  public RawImage enemyPicture;

  public Pokomon myPokomon;
  public BarUI myHealthBar;
  public RawImage myPicture;

  public Gym gym;

  public GameObject pokoballPrefab;
  public GameObject Pokoballs;

  void OnEnable() {
    animator = GetComponent<Animator>();
    player = FindObjectOfType<Player>();

    var me = transform.Find("Canvas/Me").gameObject;
    if (gym != null)
    {
      myPokomon = player.pokomons[0];
      enemyPokomon = gym.pokomon;
    }
    else
    {
      myPokomon = null;
      Assert.IsNotNull(enemyPokomon);
    }

    me.SetActive(myPokomon != null);
    if (myPokomon != null)
      myPicture.GetComponent<RawImage>().texture = myPokomon.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture;

    if (enemyPokomon != null)
      enemyPicture.GetComponent<RawImage>().texture = enemyPokomon.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture;

    StartCoroutine(Run());
	}

  IEnumerator Run()
  {
    enemyPicture.texture = enemyPokomon.transform.GetChild(0).GetComponentInChildren<SkinnedMeshRenderer>().material.mainTexture;

    yield return PlayAnimationAndWait("SlideIn");

    while (enemyPokomon.Health > 0.0f)
    {
      if (Input.GetKeyDown(KeyCode.A))
      {
        if (gym != null)
        {
          enemyPokomon.Damage(1.0f);
          yield return PlayAnimationAndWait("Attack");
        }
        else
        {
          var pokoballGameObject = Instantiate(pokoballPrefab, Pokoballs.transform);
          var pokoball = pokoballGameObject.GetComponent<Pokeball>();
          pokoball.enemy = enemyPokomon;
        }
      }
      yield return null;
    }

    yield return PlayAnimationAndWait("Died");

    if (gym == null)
    {
      player.AddPokomon(enemyPokomon);
    }

    yield return PlayAnimationAndWait("SlideOut");

    GameStateManager.Default.SwitchToPlay();
  }

  IEnumerator PlayAnimationAndWait(string animationName)
  {
    animator.Play(animationName);
    while (
         animator.GetCurrentAnimatorStateInfo(0).IsName(animationName)
      && animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 1.0f
      && !animator.IsInTransition(0)
      )
    {
      Debug.Log("animator.GetCurrentAnimatorStateInfo(0).IsName(animationName): " + animator.GetCurrentAnimatorStateInfo(0).IsName(animationName) + " " + animator.GetCurrentAnimatorStateInfo(0).normalizedTime);
      yield return new WaitForEndOfFrame();
    }
    Debug.Log("Done");
  }

  IEnumerator WaitForKeyDown(KeyCode keyCode)
  {
    while (!Input.GetKeyDown(keyCode))
      yield return null;
  }

  public void Update()
  {
    if (enemyPokomon != null)
      enemyHealthBar.SetFraction(enemyPokomon.Health / enemyPokomon.MaxHealth);

    if (myPokomon != null)
      myHealthBar.SetFraction(myPokomon.Health / myPokomon.MaxHealth);
  }

  void OnDisable()
  {
    foreach (var childTransform in Pokoballs.transform)
      Destroy((childTransform as Transform).gameObject);
    animator.StopPlayback();
    StopAllCoroutines();
  }
}
