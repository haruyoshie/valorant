using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAcid : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Object.Destroy(gameObject, 15f);
    }

    
}
