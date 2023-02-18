using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValorantPhoenix : MonoBehaviour
{
    public Camera cam;
    public GameObject projectile;
    public GameObject firePoint;
    public GameObject wall;
    public float wallDelay = 0.5f;
    public float destroyDelay;    

	private Ray rayMouse;

    
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {            
            if (cam != null) 
            {
                var mousePos = Input.mousePosition;
                rayMouse = cam.ScreenPointToRay (mousePos);
            }

            StartCoroutine (SpawnProjectile());
        }
    }

    IEnumerator SpawnProjectile ()
    {   
        RaycastHit hit;
        if(Physics.Raycast(firePoint.transform.position, -Vector3.up, out hit))
        {
            var projectileVFX = Instantiate (projectile, firePoint.transform.position, Quaternion.identity);  
            RotateToMouse (projectileVFX, rayMouse.GetPoint(1000));
        
            #region WALL

            yield return new WaitForSeconds (wallDelay);
            var wallVFX = Instantiate (wall, hit.point, Quaternion.identity);  
            RotateToMouse (wallVFX, rayMouse.GetPoint(1000), true);
        
            var wallAnim = wallVFX.transform.GetChild(0).GetComponent<Animator>();
            yield return new WaitForSeconds (destroyDelay-1.5f);
            wallAnim.SetBool("isDown", true);        
            yield return new WaitForSeconds (2.5f);
            Destroy (wallVFX);

            #endregion
        }      
    }

    void RotateToMouse (GameObject obj, Vector3 destination, bool lockY = false) 
    {
		var direction = destination - obj.transform.position;
		var rotation = Quaternion.LookRotation (direction);

        if(lockY)
        {
            rotation.z = 0; 
            rotation.x = 0;
        }

		obj.transform.localRotation = Quaternion.Lerp (obj.transform.rotation, rotation, 1);

        if(lockY)
            obj.transform.Rotate(0,90,0);
	}
}
