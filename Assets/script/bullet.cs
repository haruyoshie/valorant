using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private Rigidbody bulletRigidBody;
    public float speed;
    public GameObject acid;
    public bool isAcid,isOrbe;

    private void Awake()
    {
        bulletRigidBody = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {
        bulletRigidBody.velocity = transform.forward * speed;
        if(isAcid == true)
            StartCoroutine("crearAcido");
    }

    // Update is called once per frame
    void Update()
    {
        if(isOrbe == false)
        Object.Destroy(gameObject, 1.5f);
    }
     IEnumerator crearAcido()
    {
        yield return new WaitForSeconds(1.3f);
        Instantiate(acid, bulletRigidBody.position, bulletRigidBody.rotation);
    }
}
