using UnityEngine;
using System.Collections;

public class WobbleMovement : MonoBehaviour {
  public Transform wobbleObject;
  public float wobbleRotation = 0.1f;
  public Vector2 wobbleTranslationA = new Vector2(0.1f, 0.05f);
  public Vector2 wobbleTranslationB = new Vector2(-0.1f, 0.05f);
  public float wobbleSpeed = 10f;

  private float distanceTravelled = 0;
  private CharacterController characterController;

  // Use this for initialization
  void Start () {
    characterController = GetComponent<CharacterController>();
  }
	
	// Update is called once per frame
	void Update () {
    var velocity = characterController.velocity;
    var currentSpeed = velocity.magnitude;
    if (currentSpeed < 0.01f)
      distanceTravelled = 0;
    else
      distanceTravelled += currentSpeed;

    var steps = Mathf.CeilToInt(distanceTravelled / wobbleSpeed);
    var state = steps % 4;
    var rotationZ = 0f;
    var translation = Vector2.zero;
    switch (state)
    {
      case 0:
      case 2:
        rotationZ = 0;
        translation = Vector2.zero;
        break;
      case 1:
        rotationZ = -wobbleRotation;
        translation = wobbleTranslationA;
        break;
      case 3:
        rotationZ = wobbleRotation;
        translation = wobbleTranslationB;
        break;
    }

    wobbleObject.localRotation = new Quaternion(0, 0, rotationZ, 1);
    wobbleObject.localPosition = new Vector3(translation.x, translation.y, 0);

    if (Mathf.Abs(velocity.x) > 0.1f)
      wobbleObject.localScale = new Vector3(Mathf.Abs(wobbleObject.localScale.x) * (velocity.x > 0 ? -1 : 1), wobbleObject.localScale.y, wobbleObject.localScale.z);
  }
}
