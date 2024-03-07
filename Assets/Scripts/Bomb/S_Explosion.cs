using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Explosion : MonoBehaviour
{
    public S_Tile m_Tile;

    private void Start()
    {
        StartCoroutine(Deflagration());
    }
    public IEnumerator Deflagration()
    {
        yield return new WaitForSeconds(1);
        m_Tile.m_MoveCost = 0;
        Destroy(gameObject);
    }
}
