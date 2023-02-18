using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyParts : MonoBehaviour
{
    public float healt = 150f;
    public int enemyCount;
    public GameObject soundInstance;

    public void Die()
    {
        Destroy(gameObject);
        EnemyCount.instance.AddPoint(enemyCount);
        Instantiate(soundInstance, transform.position, transform.rotation);
    }
}
