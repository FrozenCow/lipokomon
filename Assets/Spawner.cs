using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {
  private Dictionary<CellId, Cell> spawnedCells = new Dictionary<CellId, Cell>();
  public float SpawnRadius = 10f;
  public float DestroyRadius = 50f;

  public Transform[] objects;

  private float cellSize = 20f;

  // Use this for initialization
  void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
    var position = new Vector2(transform.position.x, transform.position.z);
    var topLeft = position + new Vector2(-SpawnRadius, -SpawnRadius);
    var bottomRight = position + new Vector2(SpawnRadius, SpawnRadius);

    var spawnCellLeft = Mathf.FloorToInt((position.x - SpawnRadius) / cellSize);
    var spawnCellRight = Mathf.CeilToInt((position.x + SpawnRadius) / cellSize);
    var spawnCellTop = Mathf.CeilToInt((position.y + SpawnRadius) / cellSize);
    var spawnCellBottom = Mathf.FloorToInt((position.y - SpawnRadius) / cellSize);

    for (var x = spawnCellLeft; x <= spawnCellRight; x++)
      for (var y = spawnCellBottom; y <= spawnCellTop; y++)
      {
        var cellId = CellId.Create(x, y);
        if (!spawnedCells.ContainsKey(cellId))
        {
          var cell = SpawnCell(cellId);
          spawnedCells.Add(cellId, cell);
        }
      }

    var unspawnCellLeft = Mathf.FloorToInt((position.x - SpawnRadius) / cellSize);
    var unspawnCellRight = Mathf.CeilToInt((position.x + SpawnRadius) / cellSize);
    var unspawnCellTop = Mathf.CeilToInt((position.y + SpawnRadius) / cellSize);
    var unspawnCellBottom = Mathf.FloorToInt((position.y - SpawnRadius) / cellSize);

    List<Cell> unspawnedCells = new List<Cell>();

    foreach (var cell in spawnedCells.Values)
    {
      if (cell.CellId.x < unspawnCellLeft
        || cell.CellId.x > unspawnCellRight
        || cell.CellId.y < unspawnCellBottom
        || cell.CellId.y > unspawnCellTop)
      {
        unspawnedCells.Add(cell);
      }
    }

    foreach (var cell in unspawnedCells)
    {
      spawnedCells.Remove(cell.CellId);
      UnspawnCell(cell);
    }
  }

  int mod(int x, int m)
  {
    int r = x % m;
    return r < 0 ? r + m : r;
  }

  Cell SpawnCell(CellId cellId)
  {
    Cell cell = new Cell();
    cell.CellId = cellId;

    var index = cellId.GetHashCode();
    var obj = objects[mod(index, objects.Length)];

    var position = new Vector3(
      cellId.x * cellSize,
      0,
      cellId.y * cellSize
      );
    cell.content = ((Transform)Instantiate(obj, position, Quaternion.identity)).gameObject;
    return cell;
  }

  void UnspawnCell(Cell cell)
  {
    Destroy(cell.content);
  }
}


public struct CellId
{
  public int x;
  public int y;

  public static CellId Create(int x, int y)
  {
    CellId cell;
    cell.x = x;
    cell.y = y;
    return cell;

  }
  public override bool Equals(object obj)
  {
    if (obj is CellId)
    {
      var cell = ((CellId)obj);
      return cell.x == x && cell.y == y;
    }
    else
      return base.Equals(obj);
  }

  public override int GetHashCode()
  {
    return x.GetHashCode() * 7 + y.GetHashCode() * 11;
  }
}

public class Cell
{
  public CellId CellId;
  public GameObject content;
}