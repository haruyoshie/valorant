using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class EnemyCount : MonoBehaviour
{
    public static EnemyCount instance;
    public TextMeshPro countEnemy;

    int cuenta = 0;
    // Start is called before the first frame update
    private void Awake(){
        instance = this;
    }
    void Start()
    {
        countEnemy.text = cuenta.ToString();
    }

    public void AddPoint(int EnemyPoint)
    {
        cuenta += EnemyPoint;
        countEnemy.text = cuenta.ToString() + "";
        
    }
}
