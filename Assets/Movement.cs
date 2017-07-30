using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
  public GameObject wobbleObject;
  public float wobbleRotation = 0.1f;
  public Vector2 wobbleTranslationA = new Vector2(0.1f, 0.05f);
  public Vector2 wobbleTranslationB = new Vector2(-0.1f, 0.05f);
  public float wobbleSpeed = 0.5f;
  public float movementSpeed = 1f;
  float distanceTravelled;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
    var movement = new Vector2(
      (Input.GetKey(KeyCode.RightArrow) ? 1 : 0) - (Input.GetKey(KeyCode.LeftArrow) ? 1 : 0),
      (Input.GetKey(KeyCode.UpArrow) ? 1 : 0) - (Input.GetKey(KeyCode.DownArrow) ? 1 : 0)
    ).normalized * movementSpeed;

    if (movement.x == 0.0f && movement.y == 0.0f)
      distanceTravelled = 0;

    distanceTravelled += movement.magnitude / 50f;


    SpriteRenderer renderer = GetComponent<SpriteRenderer>();
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

    wobbleObject.transform.localRotation = new Quaternion(0, 0, rotationZ, 1);
    wobbleObject.transform.localPosition = new Vector3(translation.x, translation.y, 0);

    //transform.Translate(movement.x, 0f, movement.y);

    CharacterController collider = GetComponent<CharacterController>();
    collider.SimpleMove(new Vector3(movement.x, 0, movement.y));
  }
}
