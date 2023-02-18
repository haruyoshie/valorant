using UnityEngine;
using TMPro;
public class Target : MonoBehaviour
{
    public BodyParts bodyParts; 
    public GameObject spawn,pracOn,pracOff;
    public float time; 
    bool isActive = false;
    public TextMeshPro timer;
    public bool practicaON = true;
    public GameObject enemySpawn;
    public AudioSource death;

    public void TakeDamage(float amount)
    {
        //death.Play();
        bodyParts.healt -= amount;
        if(bodyParts.healt <= 0f)
        {
           // death.Play();
            bodyParts.Die();
        }
    }
    public void iniciarPractica()
    {
        practicaON = true;
        spawn.SetActive(true);
        isActive = true;
        pracOn.SetActive(false);
        pracOff.SetActive(true);
    }
    public void SalirPractica()
    {
        practicaON = false;
        spawn.SetActive(false);
        isActive = false;
        pracOn.SetActive(true);
        pracOff.SetActive(false);
    }
    private void Update() 
    {
        
        if(isActive)
        {
            enemySpawn = GameObject.Find("Enemy(Clone)");
            time += Time.deltaTime;
            timer.text = time.ToString("F0");
        }
        else
        {
            time = 0;
            Destroy(enemySpawn); 
            //timer.text = time.ToString();
        }    
    }
    
}
