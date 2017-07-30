using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class IconShow : MonoBehaviour {
  public Image image;
  public float separation = 10f;
  public int Amount = 0;
  private List<Image> images = new List<Image>();
  
	void Update () {
    while (images.Count > Amount)
    {
      Destroy(images[images.Count - 1].gameObject);
      images.RemoveAt(images.Count - 1);
    }

    while (images.Count < Amount)
    {
      var newImage = Instantiate<Image>(image);
      newImage.transform.SetParent(gameObject.transform);
      newImage.transform.localPosition = new Vector3(images.Count * separation, 0, 0);
      images.Add(newImage);
    }
	}
}
