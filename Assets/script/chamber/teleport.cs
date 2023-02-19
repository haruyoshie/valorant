using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class teleport : MonoBehaviour
{
    [Header("Teleport objects")]
    public GameObject tp,TpPrefab,TpTemporal,tempTP;
    [Header("Camara objects")]
    public GameObject cam, camPrefab, camTemporal, tempCAM;
    [Header("chamber skills objects")]
    public GameObject tourtheforce, headHunter;
    public GameObject parent;
    private float moveSpeed = 100f;
    private CharacterController player;
    private Vector3 place;
    public bool tempObjectExists,tempObjectCamExist;
    public bool canSenRay,canSenRayTwo;
    public GameObject[] guns;
    public GameObject deleteTP;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<CharacterController>();
        
    }


    // Update is called once per frame
    void Update()
    {
        //if (Input.GetMouseButtonDown(1))
        //{
        //    stopAllSkills();
        //}
        if (Input.GetKey(KeyCode.E) && tempObjectExists == false)
        {
            stopAllSkills();
            activeWeapons(false);
             canSenRay = true;

        }
        if (canSenRay)
        {
            SendRay();
        }
        if(Input.GetKey(KeyCode.C) && tempObjectCamExist == false)
        {
            stopAllSkills();
            activeWeapons(false);
            canSenRayTwo = true;
        }
        if (Input.GetKey(KeyCode.Q))
        {
            stopAllSkills();
            activeWeapons(false);
            headHunter.SetActive(true);
        }
        if (Input.GetKey(KeyCode.X))
        {
            stopAllSkills();
            activeWeapons(false);
            tourtheforce.SetActive(true);
        }
        if (canSenRayTwo)
        {
            SendRayTwo();
        }
        if (GameObject.FindGameObjectWithTag("teleport") == null /*|| GameObject.FindGameObjectWithTag("camara") == null*/)
        {
            
        }
        else
        {
            if (tp.GetComponentInChildren<getPositionPlayer>().StayActive == true && player.isGrounded == true)
            {
                Vector3 posfinal = new Vector3(tp.transform.position.x, parent.transform.position.y, tp.transform.position.z);
                if (Input.GetKey(KeyCode.E))
                {
                    MoveTpPosition(posfinal);
                }
            }
            else
            {
                return;
            }
        }
        if (tp != null)
        {
            SendRayToDelete();
        }
        if (cam != null)
        {
            SendRayToDeleteCam();
        }
    }

    public void skillOne()
    {
        if(tempObjectExists == false && tp == null)
        {
            stopAllSkills();
            activeWeapons(false);
            canSenRay = true;
        }
            
    }
    public void skillTwo()
    {
        if(tempObjectCamExist == false && cam == null)
        {
            stopAllSkills();
            activeWeapons(false);
            canSenRayTwo = true;
        }
        
    }

    public void skillThree()
    {
        stopAllSkills();
        activeWeapons(false);
        headHunter.SetActive(true);
    }

    public void skillFour()
    {
        stopAllSkills();
        activeWeapons(false);
        tourtheforce.SetActive(true);
    }

    public void stopAllSkills()
    {
        if(tempTP != null)
        {
            Destroy(tempTP.gameObject);
        }
        if(tempCAM != null)
        {
            Destroy(tempCAM.gameObject);
        }
        canSenRay = false;
        tempObjectExists = false;
        tempObjectCamExist = false;
        canSenRayTwo = false;
        tourtheforce.SetActive(false);
        headHunter.SetActive(false);
    }

    void MoveTpPosition(Vector3 tpPosition)
    {
        var offset = tpPosition - parent.transform.position;

        if(offset.magnitude > .1f)
        {
            offset = offset.normalized * moveSpeed;

            player.Move(offset * Time.deltaTime);
        }
    }

    public void SendRay()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if(Physics.Raycast(ray,out _hit))
        {
           // Debug.Log(_hit.transform.tag);
            place = new Vector3(_hit.point.x, _hit.point.y + 0.5f, _hit.point.z);
            #region
                if(_hit.transform.tag == "Finish")
                {
                    if(tempObjectExists == false)
                    {
                        Instantiate(TpTemporal, place, Quaternion.Euler(0, 0, 0));
                        tempTP = GameObject.Find("tp(Clone)");
                        tempObjectExists = true;
                    }
                    if(Input.GetMouseButtonDown(0))
                    {
                    canSenRay = false;
                    Instantiate(TpPrefab, place, Quaternion.Euler(0, 0, 0));
                    Destroy(tempTP);
                    tp = GameObject.FindGameObjectWithTag("teleport");
                    canSenRay = false;
                    activeWeapons(true);
                    }
                if (tempTP != null)
                {
                    tempTP.transform.position = place;
                }
            }
            #endregion
        }
    }
    public void SendRayTwo()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(ray, out _hit))
        {
            // Debug.Log(_hit.transform.tag);
            place = new Vector3(_hit.point.x, _hit.point.y + 0.5f, _hit.point.z);
            #region
            if (_hit.transform.tag == "Finish")
            {
                if (tempObjectCamExist == false)
                {
                    Instantiate(camTemporal, place, Quaternion.Euler(0, 0, 0));
                    tempCAM = GameObject.Find("CAM(Clone)");
                    tempObjectCamExist = true;
                }
                if (Input.GetMouseButtonDown(0))
                {
                    canSenRay = false;
                    canSenRayTwo = false;
                    Instantiate(camPrefab, place, Quaternion.Euler(0, 0, 0));
                    Destroy(tempCAM);
                    cam = GameObject.FindGameObjectWithTag("camara");
                    canSenRay = false;
                    canSenRayTwo = false;
                    activeWeapons(true);
                }
                if (tempCAM != null)
                {
                    tempCAM.transform.position = place;
                }
            }
            #endregion
        }
    }
    public void activeWeapons(bool state)
    {
        for (int i = 0; i < guns.Length; i++)
        {
            guns[i].SetActive(state);
        }
    }

    public void SendRayToDelete()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(ray, out _hit))
        {
            #region
            if (_hit.transform.tag == "teleport")
            {
                _hit.transform.gameObject.GetComponentInChildren<getPositionPlayer>().deleteOptions = true;
                if (Input.GetKey(KeyCode.F))
                {
                    Destroy(_hit.transform.gameObject);
                    tempObjectExists = false;
                }
            }
            else
            {
                tp.GetComponentInChildren<getPositionPlayer>().deleteOptions = false;
            }
            #endregion
        }
    }
    public void SendRayToDeleteCam()
    {
        Debug.Log("camara");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit _hit;

        if (Physics.Raycast(ray, out _hit))
        {
            Debug.Log(_hit);
            #region
            if (_hit.transform.tag == "camara")
            {
                
                _hit.transform.gameObject.GetComponentInChildren<getPositionPlayer>().deleteOptions = true;
                if (Input.GetKey(KeyCode.F))
                {
                    Destroy(_hit.transform.gameObject);
                    tempObjectCamExist = false;
                }
            }
            else
            {
                cam.GetComponentInChildren<getPositionPlayer>().deleteOptions = false;
            }
            #endregion
        }
    }
}
