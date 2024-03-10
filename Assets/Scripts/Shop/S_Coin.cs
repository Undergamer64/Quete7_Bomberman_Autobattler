using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Coin : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 1000*Time.deltaTime);
        if(transform.localPosition.y < 400)
        {
            Destroy(gameObject);
        }
    }
}
