using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEx : MonoBehaviour
{
    [SerializeField]
    GameObject obj;
    [SerializeField]
    [Range(0.1f, 1.0f)]
    float speed = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.RotateAround(obj.transform.position, new Vector3(0, 1, 1), Time.deltaTime + speed);
    }
}
