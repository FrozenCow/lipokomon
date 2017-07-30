using UnityEngine;
using System.Collections;

public class PlayerInput : MonoBehaviour
{
  public Vector2 mousePosition
  {
    get
    {
      return Input.mousePosition;
    }
  }

  public bool GetKey(KeyCode keyCode)
  {
    return isActiveAndEnabled && Input.GetKey(keyCode);
  }

  public bool GetKeyDown(KeyCode keyCode)
  {
    return isActiveAndEnabled && Input.GetKeyDown(keyCode);
  }

  public bool GetKeyUp(KeyCode keyCode)
  {
    return isActiveAndEnabled && Input.GetKeyUp(keyCode);
  }

  public bool GetMouseButton(int button)
  {
    return isActiveAndEnabled && Input.GetMouseButton(button);
  }

  public bool GetMouseButtonDown(int button)
  {
    return isActiveAndEnabled && Input.GetMouseButtonDown(button);
  }

  public bool GetMouseButtonUp(int button)
  {
    return isActiveAndEnabled && Input.GetMouseButtonUp(button);
  }

  public Vector2 GetMovementDirection()
  {
    return new Vector2(
      (GetKey(KeyCode.D) ? 1 : 0) - (GetKey(KeyCode.A) ? 1 : 0),
      (GetKey(KeyCode.W) ? 1 : 0) - (GetKey(KeyCode.S) ? 1 : 0)
    ).normalized;
  }

  public bool GetFire()
  {
    return GetMouseButton(0);
  }

  public bool GetFireDown()
  {
    return GetMouseButtonDown(0);
  }

  public bool GetFireUp()
  {
    return GetMouseButtonUp(0);
  }

  void Update() { }
}
