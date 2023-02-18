using UnityEngine;
using StarterAssets;
using System.Collections;

public class ControllSkill : MonoBehaviour
{

    public float range = 100f;
    public Camera fpsCam;
    public StarterAssetsInputs starterAssetsInput;
    public GameObject  bulletAcid, firePoint, wall, projectile;
    bool isAcidActive, wallActive ;

    public GameObject[] guns;
    public float wallDelay = 0.5f;
    public float destroyDelay;
    private Ray rayMouse;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (starterAssetsInput.shoot)
        {
            if (Input.GetMouseButtonDown(0) && wallActive == true)
            {
                Shoot();
                if (fpsCam != null)
                {
                    var mousePos = Input.mousePosition;
                    rayMouse = fpsCam.ScreenPointToRay(mousePos);
                }

                StartCoroutine(SpawnProjectile());

            }
            if(Input.GetMouseButtonDown(0) && isAcidActive == true)
            {
                Shoot();
            }
        }
        for(int i = 0; i < guns.Length; i++)
        {
            if (Input.GetKey(KeyCode.E))
            {
                guns[i].SetActive(false);
                isAcidActive = true;
                wallActive = false;
            }
            if (Input.GetKey(KeyCode.C))
            {
                guns[i].SetActive(false);
                wallActive = true;
                isAcidActive = false;
            }
        }

        

    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range))
        {
            Debug.Log(hit.transform.name);
            Target target = hit.transform.GetComponent<Target>();
            
            if (isAcidActive == true)
            {
                Instantiate(bulletAcid, firePoint.transform.position, firePoint.transform.rotation);
                isAcidActive = false;
            }
            starterAssetsInput.shoot = false;
        }
        starterAssetsInput.shoot = false;
        StartCoroutine(activeWeapons());
        
    }
    IEnumerator activeWeapons()
    {
        yield return new WaitForSeconds(1.5f);
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(true);
        }
    }
    IEnumerator SpawnProjectile()
    {
        wallActive = false;
        RaycastHit hit;
        if (Physics.Raycast(firePoint.transform.position, -Vector3.up, out hit))
        {
            var projectileVFX = Instantiate(projectile, firePoint.transform.position, Quaternion.identity);
            RotateToMouse(projectileVFX, rayMouse.GetPoint(1000));

            #region WALL

            yield return new WaitForSeconds(wallDelay);
            var wallVFX = Instantiate(wall, hit.point, Quaternion.identity);
            RotateToMouse(wallVFX, rayMouse.GetPoint(1000), true);

            var wallAnim = wallVFX.transform.GetChild(0).GetComponent<Animator>();
            yield return new WaitForSeconds(destroyDelay - 1.5f);
            wallAnim.SetBool("isDown", true);
            yield return new WaitForSeconds(2.5f);
            Destroy(wallVFX);

            #endregion
        }
    }
    void RotateToMouse(GameObject obj, Vector3 destination, bool lockY = false)
    {
        var direction = destination - obj.transform.position;
        var rotation = Quaternion.LookRotation(direction);

        if (lockY)
        {
            rotation.z = 0;
            rotation.x = 0;
        }

        obj.transform.localRotation = Quaternion.Lerp(obj.transform.rotation, rotation, 1);

        if (lockY)
            obj.transform.Rotate(0, 90, 0);
    }

}
