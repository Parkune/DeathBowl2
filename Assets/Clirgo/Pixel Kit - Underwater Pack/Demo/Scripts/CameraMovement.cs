using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] float speed;

    void Update()
    {
        float displacement = 0;

        if(Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            displacement = 1;
        }
        else if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            if(transform.position.x > 0) displacement = -1;
        }



        transform.position = new Vector3(transform.position.x + (displacement * speed * Time.deltaTime), 
                                         transform.position.y, 
                                         transform.position.z);
    }
}
