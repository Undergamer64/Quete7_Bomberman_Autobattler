using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Explosion : MonoBehaviour
{
    [SerializeField]
    private float m_explosionTime;
    private void Start()
    {
        StartCoroutine(Deflagration());
    }
    public IEnumerator Deflagration()
    {
        yield return new WaitForSeconds(m_explosionTime);
        Destroy(gameObject);
    }
}
