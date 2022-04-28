using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class torus : MonoBehaviour
{
    [SerializeField]
    GameObject cursor;
    [SerializeField]
    GameObject x_torus;
    [SerializeField]
    GameObject y_torus;
    [SerializeField]
    GameObject z_torus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        x_torus.transform.rotation = cursor.transform.rotation * Quaternion.Euler(0, 90, 0);
        y_torus.transform.rotation = Quaternion.Euler(-90, 0, 0);
        z_torus.transform.rotation = cursor.transform.rotation;
    }
}
