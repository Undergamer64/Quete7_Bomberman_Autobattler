using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GridManager : MonoBehaviour
{
    public static S_GridManager Instance;

    public List<List<S_Tile>> m_GridList = new();

    public int m_Width, m_Height;
    [SerializeField] private S_Tile m_tile;

    [Header("Differents tile's types :")]
    [SerializeField] private Sprite m_TileSprite;
    [SerializeField] private Sprite m_wallSprite;
    [SerializeField] private Sprite m_destructableWallSprite;

    private Dictionary<Vector2, S_Tile> m_tilesDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        GenerateGrid();
    }

    // Generate the grid
    void GenerateGrid()
    {
        m_tilesDictionary = new Dictionary<Vector2, S_Tile>();

        for (int x = 0; x < m_Width; x++)
        {
            m_GridList.Add(new List<S_Tile>());
            for (int y = 0; y < m_Height; y++)
            {
                var spawnedTile = Instantiate(m_tile, new Vector3(x, y), Quaternion.identity, transform);
                m_GridList[x].Add(spawnedTile);

                //Placement of Destructable Walls
                if ((x % 2 == 0) || (y % 2 == 0))
                {
                    spawnedTile.tag = "Destructable";
                    spawnedTile.GetComponent<SpriteRenderer>().sprite = m_destructableWallSprite;
                }

                //Placement of the Walls :
                //edge
                if (x == 0 || x == 16 || y == 12 || y == 0)
                {
                    spawnedTile.tag = "Wall";
                    spawnedTile.GetComponent<SpriteRenderer>().sprite = m_wallSprite;
                }
                //grid Wall
                if ((x >= 2 && y >= 2) && (x <= 14 && y <= 10))
                {
                    if ((x % 2 == 0) && (y % 2 == 0)) 
                    {
                        spawnedTile.tag = "Wall";
                        spawnedTile.GetComponent<SpriteRenderer>().sprite = m_wallSprite;
                    }
                }

                //Replace Destructable Walls on spawn
                if ((x == 1 && (y == 2 || y == 10)) ||
                    (x == 2 && (y == 1 || y == 11)) ||
                    (x == 14 && (y == 1 || y == 11)) ||
                    (x == 15 && (y == 2 || y == 10))) 
                {
                    spawnedTile.tag = "Untagged";
                    spawnedTile.GetComponent<SpriteRenderer>().sprite = m_TileSprite;
                }

                spawnedTile.m_TileX = x;
                spawnedTile.m_TileY = y;
                spawnedTile.name = $"Tile X {x} Y {y}";
                m_tilesDictionary[new Vector2(x, y)] = spawnedTile;
            }
        }
        Camera.main.transform.position = new Vector3(m_Width / 2, m_Height / 2, -10);
        Camera.main.orthographicSize = 7;
    }
}
