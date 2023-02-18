using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class scope : MonoBehaviour
{
    public Animator animator;
    public GameObject scopeOverlay;
    public GameObject WeaponCamera;
    public CinemachineVirtualCamera MainCamera;
    private bool isScoped = false;
    public bool isTourtheForce;

    public float scopeFOV = 15f;
    private float normalFOV;
    private void Update()
    {
        if (!gameObject.activeSelf)
        {
            scopeOverlay.SetActive(false);
        }
        if (Input.GetButtonDown("Fire2"))
        {
            isScoped = !isScoped;
            animator.SetBool("Scoped", isScoped);

            if (isScoped)
                StartCoroutine( OnScope());
            else
                OnUnScoped();
        }
    }
    void OnUnScoped()
    {
        scopeOverlay.SetActive(false);
        WeaponCamera.SetActive(true);
        MainCamera.m_Lens.FieldOfView = normalFOV;
    }
    IEnumerator OnScope()
    {
        yield return new WaitForSeconds(.15f);
        if(isTourtheForce == true)
        {
            scopeOverlay.SetActive(true);
            WeaponCamera.SetActive(false);

            normalFOV = MainCamera.m_Lens.FieldOfView;
            MainCamera.m_Lens.FieldOfView = scopeFOV;
        }
        else
        {
            scopeOverlay.SetActive(true);
            normalFOV = MainCamera.m_Lens.FieldOfView;
            MainCamera.m_Lens.FieldOfView = scopeFOV;
        }
        
    }
}
