using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Explosion : MonoBehaviour
{
    public S_Tile m_Tile;

    [SerializeField]
    private float m_explosionTime;
    private void Start()
    {
        StartCoroutine(Deflagration());
    }
    public IEnumerator Deflagration()
    {
        yield return new WaitForSeconds(m_explosionTime);
        m_Tile.m_MoveCost = 0;
        Destroy(gameObject);
    }
}
