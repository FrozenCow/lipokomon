using UnityEngine;
using System.Collections;

public class GameStateManager : MonoBehaviour {
  public GameObject player;

  public GameObject PlayGameState;
  public GameObject DiedGameState;
  public GameObject WinGameState;
  public GameObject BattleGameState;

  private GameObject CurrentGameState;

  void Start () {
    CurrentGameState = PlayGameState;
    CurrentGameState.SetActive(true);
	}

  public void SwitchTo(GameObject gameState)
  {
    Debug.Log("Switching to " + gameState.name);
    CurrentGameState.SetActive(false);
    CurrentGameState = gameState;
    CurrentGameState.SetActive(true);
  }

  public void SwitchToDied()
  {
    SwitchTo(DiedGameState);
  }

  public void SwitchToPlay()
  {
    SwitchTo(PlayGameState);
  }

  public void SwitchToWin()
  {
    SwitchTo(WinGameState);
  }

  public void SwitchToBattle(Pokomon enemy)
  {
    BattleGameState.GetComponent<BattleGameState>().enemyPokomon = enemy;
    SwitchTo(BattleGameState);
  }

  public void SwitchToGym(Gym gym)
  {
    BattleGameState.GetComponent<BattleGameState>().gym = gym;
    SwitchTo(BattleGameState);
  }

  //public void SwitchToTutorial()
  //{
  //  SwitchTo(TutorialGameState);
  //}

  public void OnPlayerDied()
  {
    SwitchToDied();
  }

  public static GameStateManager Default
  {
    get
    {
      return GameObject.FindGameObjectWithTag("GameState").GetComponent<GameStateManager>();
    }
  }
}
