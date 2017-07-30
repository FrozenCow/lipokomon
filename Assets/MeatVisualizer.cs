using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class MeatVisualizer : MonoBehaviour {
  public Image image;
  public float separation = 10f;
  private List<Image> images = new List<Image>();

  void Update()
  {
    var amount = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>().contents.Count;
    while (images.Count > amount)
    {
      Destroy(images[images.Count - 1].gameObject);
      images.RemoveAt(images.Count - 1);
    }

    while (images.Count < amount)
    {
      var newImage = Instantiate<Image>(image);
      newImage.transform.SetParent(gameObject.transform);
      newImage.transform.localPosition = new Vector3(images.Count * separation, 0, 0);
      images.Add(newImage);
    }
  }
}
