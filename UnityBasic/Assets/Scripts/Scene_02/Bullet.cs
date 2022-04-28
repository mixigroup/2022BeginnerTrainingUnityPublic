using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Vector3 _direction;
    private float Speed = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        transform.position += _direction * Speed * Time.fixedDeltaTime;
    }

    public void SetDirection(Vector3 vec)
    {
        _direction = vec;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Stage"))
        {
            Destroy(this.gameObject);
        }
    }
}
