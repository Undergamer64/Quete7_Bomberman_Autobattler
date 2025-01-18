using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Pathfinding
{

    private const int MOVE_STRAIGHT_COST = 10;
    private List<S_Tile> m_openList;
    private List<S_Tile> m_closedList;
    private List<List<S_Tile>> m_gridRef = S_GridManager.Instance.m_GridList;
    public List<S_Tile> FindPath(S_Tile startTile, S_Tile endTile)
    {
        m_openList = new List<S_Tile> { startTile };
        m_closedList = new List<S_Tile>();
        for (int i = 0;i< S_GridManager.Instance.m_Width;i++) 
        { 
            for (int j = 0; j < S_GridManager.Instance.m_Height; j++)
            {
                S_Tile tile = m_gridRef[i][j];
                tile.m_GCost = int.MaxValue;
                tile.CalculateFCost();
                tile.m_PreviousTile = null;

            }
        }
        startTile.m_GCost = 0;
        startTile.m_HCost = CalculateDistance(startTile, endTile);
        startTile.CalculateFCost();
        while(m_openList.Count > 0)
        {
            S_Tile currentTile = GetLowestFCost(m_openList);
            if (currentTile == endTile)
            {
                return CalculatePath(endTile);
            }
            m_openList.Remove(currentTile);
            m_closedList.Add(currentTile);

            foreach(S_Tile tile in GetNeighbourTile(currentTile))
            {
                if (m_closedList.Contains(tile)) continue;
                if (!tile.m_IsWalkable)
                {
                    if (tile == endTile)
                    {
                        tile.m_PreviousTile = currentTile;
                        return CalculatePath(tile);
                    }
                    m_closedList.Add(tile);
                    continue;
                }

                int tentativeGCost= CalculateDistance(currentTile, tile);
                if (tile.m_MoveCost > 0)
                {
                    tentativeGCost += tile.m_MoveCost;
                }
                if (tentativeGCost < tile.m_GCost && tentativeGCost <= 600)
                {
                    tile.m_PreviousTile=currentTile;
                    tile.m_GCost=tentativeGCost;
                    tile.m_HCost=CalculateDistance(tile,endTile);
                    tile.CalculateFCost();
                    if (!m_openList.Contains(tile))
                    {
                        m_openList.Add(tile);
                    }
                }
                
            }
        }
        //NO TILES LEFT ON OPENLIST
        return null;

    }

    public List<S_Tile> GetNeighbourTile(S_Tile tile)
    {
        List<S_Tile> neighbourList= new List<S_Tile>();

        //Left
        if(tile.m_TileX-1>=0)
        {
            neighbourList.Add(m_gridRef[tile.m_TileX - 1][tile.m_TileY]);
        }
        //Down
        if(tile.m_TileY-1>=0)
        {
            neighbourList.Add(m_gridRef[tile.m_TileX][tile.m_TileY - 1]);
        }
        //Right
        if(tile.m_TileX+1< S_GridManager.Instance.m_Width)
        {
            neighbourList.Add(m_gridRef[tile.m_TileX + 1][tile.m_TileY]);
        }
        //Up
        if (tile.m_TileY + 1 < S_GridManager.Instance.m_Height)
        {
            neighbourList.Add(m_gridRef[tile.m_TileX][tile.m_TileY + 1]);
        }
        return neighbourList;
    }
    private List<S_Tile> CalculatePath(S_Tile endTile) 
    {
        List<S_Tile> path= new List<S_Tile>();
        path.Add(endTile);
        S_Tile currentTile = endTile;
        while(currentTile.m_PreviousTile!=null) 
        {
            path.Add(currentTile.m_PreviousTile);
            currentTile = currentTile.m_PreviousTile;
        }
        path.Remove(path[path.Count-1]);
        path.Reverse();
        return path;
    }
    public int CalculateDistance(S_Tile a, S_Tile b)
    {
        int XDistance= Mathf.Abs(a.m_TileX - b.m_TileX);
        int YDistance= Mathf.Abs(a.m_TileY - b.m_TileY);
        int result = Mathf.Abs(XDistance - YDistance);

        return MOVE_STRAIGHT_COST * result;
    }
    private S_Tile GetLowestFCost(List<S_Tile> tiles)
    {
        S_Tile lowestFCostTile = tiles[0];
        foreach(S_Tile tile in tiles)
        {
            if (lowestFCostTile.m_FCost > tile.m_FCost)
            {
                lowestFCostTile = tile;
            }
        }
        return lowestFCostTile;
    }
}
