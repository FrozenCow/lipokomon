using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class PokemonInventoryUI : MonoBehaviour {
  public class PokomonImage
  {
    public Pokomon pokomon;
    public RawImage image;
  }
  public float separation;
  public Player player;
  public List<PokomonImage> images = new List<PokomonImage>();
  public GameObject imagePrefab;

	void Update ()
  {
    if (!Enumerable.SequenceEqual(images.Select(image => image.pokomon), player.pokomons))
    {
      var unhandledPokomons = new HashSet<Pokomon>(player.pokomons);
      var imageToBeRemoved = new HashSet<PokomonImage>();
      foreach(var image in images)
      {
        var index = player.pokomons.IndexOf(image.pokomon);

        // Handle removed pokomons.
        if (index == -1)
        {
          imageToBeRemoved.Add(image);
        }
        else // Handle moved pokomons.
        {
          unhandledPokomons.Remove(image.pokomon);
          image.image.transform.localPosition = new Vector3(index * separation, 0, 0);
        }
      }

      // Handle new pokomons
      foreach(var pokomon in unhandledPokomons.OrderBy(pokemon => player.pokomons.IndexOf(pokemon)))
      {
        var index = player.pokomons.IndexOf(pokomon);
        var image = Instantiate(imagePrefab, transform);
        var rawImage = image.GetComponent<RawImage>();
        var pokomonImage = new PokomonImage()
        {
          pokomon = pokomon,
          image = rawImage
        };
        rawImage.texture = pokomon.Texture;
        image.transform.localPosition = new Vector3(index * separation, 0, 0);
        images.Insert(index, pokomonImage);
      }

      foreach(var pokemonImage in imageToBeRemoved)
        images.Remove(pokemonImage);
    }
	}
}
