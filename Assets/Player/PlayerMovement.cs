using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
  public PlayerInput Input;
  public float movementSpeed = 1f;

  private CharacterController characterController;

  void Start()
  {
    characterController = GetComponent<CharacterController>();
  }

  void Update()
  {
    var movement = Input.GetMovementDirection() * movementSpeed;

    characterController.SimpleMove(new Vector3(movement.x, 0, movement.y));
  }

  void OnDied()
  {
    GameStateManager.Default.SwitchToDied();
  }
}
