using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    

    

    // Update is called once per frame
    void Update()
    {
        



        


        Vector3 mouse = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0);
        Vector3 rot = mouse - new Vector3(transform.position.x, transform.position.y);
        rot = rot.normalized;
        mouse = transform.position + (2f * rot);


        Vector3 player = GameObject.FindGameObjectWithTag("Player").transform.position;
        Vector3 mp = new Vector3((mouse.x + player.x) / 2f, (mouse.y + player.y) / 2f, -10f);

        transform.position = Vector3.Lerp(transform.position, mp, 0.01f);
    }
}
