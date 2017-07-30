using UnityEngine;
using System.Collections;

public class Pointing : MonoBehaviour {
  public Transform source;
  public Transform target;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    var sourcePosition = source.transform.position;
    var targetPosition = target.transform.position;

    sourcePosition.y = 0;
    targetPosition.y = 0;

    var difference = sourcePosition - targetPosition;
    var distance = difference.magnitude;
    var direction = difference.normalized;
    transform.localRotation = Quaternion.FromToRotation(Vector3.right, direction);
    transform.localScale = new Vector3(1f, 1f, Mathf.Clamp((distance - 15.0f) / 5.0f, 0, 1));
	}
}
