using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    [SerializeField]
    GameObject turret;
    [SerializeField]
    GameObject shotPoint;
    [SerializeField]
    GameObject Bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // –C“ƒ‚Ì‰ñ“]
        if(Input.GetKey(KeyCode.A))
        {
            turret.transform.localRotation *= Quaternion.Euler(0.0f, -1.0f, 0.0f);
        }
        if(Input.GetKey(KeyCode.D))
        {
            turret.transform.localRotation *= Quaternion.Euler(0.0f, 1.0f, 0.0f);
        }

        // ŽËŒ‚
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet;
            bullet = Instantiate(Bullet, shotPoint.transform.position, Quaternion.identity);

            bullet.GetComponent<Bullet>().SetDirection(turret.transform.forward);
            
        }
    }
}
