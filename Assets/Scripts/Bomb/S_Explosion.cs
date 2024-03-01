using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Explosion : MonoBehaviour
{
    
    private void Start()
    {
        StartCoroutine(Deflagration());
    }
    public IEnumerator Deflagration()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
