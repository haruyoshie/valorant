using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class expandOrbViper : MonoBehaviour
{
    public float speed;
    private Vector3 scaleChange = new Vector3(5,5,5);
    private Vector3 scaleChangeminus = new Vector3(0,0,0);
    public bool isActive, expand,finish;
    public GameObject orbe;
    public Animator animController;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        orbe = GameObject.Find("Sphere");
        
        //transform.localScale = Vector3.Lerp(transform.localScale, scaleChange, speed * Time.deltaTime);
        if (Input.GetKey(KeyCode.Q) && isActive == false)
        {
            expand = true;
        }
        if (Input.GetKey(KeyCode.Q) && finish == true )
        {
            expand = false;
        }
        if (expand)
        {
            orb();
        }
        if(!expand)
        {
            orbFinish();
        }
        if(orbe.transform.localScale.x <  0)
        {
            orbe.SetActive(false);
        }
        if (orbe.transform.localScale.x >= scaleChangeminus.x)
        {
            orbe.SetActive(true);
        }
    }
    public void orb()
    {
        isActive = true;
        StartCoroutine(Fade());
        orbe.transform.localScale = Vector3.Lerp(orbe.transform.localScale, scaleChange, speed * Time.deltaTime);
    }
    public void orbFinish()
    {
        isActive = false;
        orbe.transform.localScale = Vector3.Lerp(orbe.transform.localScale, scaleChangeminus, speed * Time.deltaTime);
        StartCoroutine(Fade2());
    }
    IEnumerator Fade2()
    {
        animController.SetBool("active", false);
        yield return new WaitForSeconds(4f);
        finish = false;
    }

    IEnumerator Fade()
    {
        animController.SetBool("active", true);
        yield return new WaitForSeconds(2f);
        finish = true;
        yield return new WaitForSeconds(6f);
        //orbFinish();
    }
}
