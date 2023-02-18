using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    public GameObject sound;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        sound = GameObject.Find("SoundInstance(Clone)");

        Object.Destroy(sound, 2.0f);
    }
}
