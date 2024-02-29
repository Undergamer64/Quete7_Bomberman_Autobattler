using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_GridManager : MonoBehaviour
{
    public static S_GridManager Instance;

    public List<List<S_Tile>> m_GridList = new();

    public int m_Width, m_Height;
    [SerializeField] private S_Tile m_tile;

    [SerializeField] private List<GameObject> m_characters;

    [Header("Differents tile's types :")]
    [SerializeField] public Sprite m_TileSprite;
    [SerializeField] public Sprite m_WallSprite;
    [SerializeField] public Sprite m_DestructableWallSprite;

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
                var spawnedTile = Instantiate(m_tile, new Vector3(x, y, 0), Quaternion.identity, transform);
                m_GridList[x].Add(spawnedTile);

                //Placement of Destructable Walls
                if ((x % 2 == 0) || (y % 2 == 0))
                {
                    spawnedTile.tag = "Destructable";
                    spawnedTile.GetComponent<SpriteRenderer>().sprite = m_DestructableWallSprite;
                }

                //Placement of the Walls :
                //edge
                if (x == 0 || x == 16 || y == 12 || y == 0)
                {
                    spawnedTile.tag = "Wall";
                    spawnedTile.GetComponent<SpriteRenderer>().sprite = m_WallSprite;
                }
                //grid Wall
                if ((x >= 2 && y >= 2) && (x <= 14 && y <= 10))
                {
                    if ((x % 2 == 0) && (y % 2 == 0)) 
                    {
                        spawnedTile.tag = "Wall";
                        spawnedTile.GetComponent<SpriteRenderer>().sprite = m_WallSprite;
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
        //Spawn Characters
        S_Tile spawntile = m_GridList[1][1];
        m_characters[0].GetComponent<S_Character>().m_currentTile = spawntile;
        m_characters[0].transform.position = new Vector3(spawntile.m_TileX, spawntile.m_TileY, -1);

        spawntile = m_GridList[1][11];
        m_characters[1].GetComponent<S_Character>().m_currentTile = spawntile;
        m_characters[1].transform.position = new Vector3(spawntile.m_TileX, spawntile.m_TileY, -1);

        spawntile = m_GridList[15][1];
        m_characters[2].GetComponent<S_Character>().m_currentTile = spawntile;
        m_characters[2].transform.position = new Vector3(spawntile.m_TileX, spawntile.m_TileY, -1);

        spawntile = m_GridList[15][11];
        m_characters[3].GetComponent<S_Character>().m_currentTile = spawntile;
        m_characters[3].transform.position = new Vector3(spawntile.m_TileX, spawntile.m_TileY, -1);

        Camera.main.transform.position = new Vector3(m_Width / 2, m_Height / 2, -10);
        Camera.main.orthographicSize = 7;
    }
}
