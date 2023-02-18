using UnityEngine;
using StarterAssets;
using System.Collections;
public class Gun : MonoBehaviour
{
    public float damage = 10f;
    public float range = 100f; 
 
    public Camera fpsCam;
    public StarterAssetsInputs starterAssetsInput;
    public AudioSource shoot,select;
    public GameObject muzzle;
    public Transform spawnMuzzle;
    public GameObject deleteMuzzle;
    public GameObject bullet;
   

    private Ray rayMouse;

    // Update is called once per frame
    void Update()
    {
        if (starterAssetsInput.shoot)
        {
            Shoot();
            //}
            if (Input.GetMouseButtonDown(0))
            {
                if (fpsCam != null)
                {
                    var mousePos = Input.mousePosition;
                    rayMouse = fpsCam.ScreenPointToRay(mousePos);
                }

                //    StartCoroutine(SpawnProjectile());
            }
                deleteMuzzle = GameObject.Find("MuzzleFlash Variant(Clone)");
            Object.Destroy(deleteMuzzle, 0.5f);
        }
    }
    void Shoot()
    {
        RaycastHit hit;

        shoot.Play();
        Instantiate(muzzle, spawnMuzzle.position, spawnMuzzle.rotation);
        Instantiate(bullet, spawnMuzzle.position, spawnMuzzle.rotation);

        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);

            Target target = hit.transform.GetComponent<Target>();
            if (target != null && hit.transform.name == "legs")
            {
                target.TakeDamage(30f);
            }
            if (target != null && hit.transform.name == "head")
            {
                target.TakeDamage(160f);
            }
            if (target != null && hit.transform.name == "chest")
            {
                target.TakeDamage(60f);
            }
            if (target != null && hit.transform.name == "practica")
            {
                select.Play();
                if (target.practicaON == false)
                {
                    target.iniciarPractica();
                }
                else
                {
                    target.SalirPractica();
                }
            }

            starterAssetsInput.shoot = false;
            Object.Destroy(deleteMuzzle, 2.0f);
        }
        starterAssetsInput.shoot = false;

    }
}
