using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 input = Vector3.zero;

        // X axis
        if(Input.GetKey(KeyCode.Q))
        {
            input.x = -1;
        }
        if(Input.GetKey(KeyCode.W))
        {
            input.x = 1;
        }

        // Y axis
        if(Input.GetKey(KeyCode.A))
        {
            input.y = -1;
        }
        if(Input.GetKey(KeyCode.S))
        {
            input.y = 1;
        }

        // Z axis
        if(Input.GetKey(KeyCode.Z))
        {
            input.z = -1;
        }
        if(Input.GetKey(KeyCode.X))
        {
            input.z = 1;
        }

        // 現在の回転に入力を加算
        Vector3 newAngle = transform.rotation.eulerAngles + (input);

        // rotationに代入
        transform.rotation = Quaternion.Euler(newAngle);
    }
}
