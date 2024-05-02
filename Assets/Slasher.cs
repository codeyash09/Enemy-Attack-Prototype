using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slasher : MonoBehaviour
{

    public bool pressureRising;
    public bool pressureFalling;
    // Start is called before the first frame update
    void Start()
    {
        pressureRising = false;
        pressureFalling = false;
        

    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).transform.localPosition = new Vector3(0, Mathf.Lerp(transform.GetChild(0).localPosition.y, 0f, 0.005f), 0);

        transform.GetChild(1).transform.localScale = new Vector3(transform.GetChild(0).localScale.x + 0.5f, transform.GetChild(1).transform.localScale.y, transform.GetChild(1).transform.localScale.z);
        transform.GetChild(1).transform.position = transform.GetChild(0).transform.position;

        if(transform.GetChild(0).transform.localPosition.y >= -1f && !pressureRising && !pressureFalling)
        {
           
            pressureRising = true;
            pressureFalling = false;
        }
        if (pressureRising && !pressureFalling)
        {
            transform.GetChild(0).transform.localScale = new Vector3(Mathf.Lerp(transform.GetChild(0).localScale.x, 1f, 0.01f), 500f, 0);
           

        }

        if (transform.GetChild(0).transform.localScale.x >= 0.95f && pressureRising && !pressureFalling)
        {
            pressureRising = false;
            pressureFalling = true;
            


        }

        if (pressureFalling && !pressureRising)
        {
            transform.GetChild(0).transform.localScale = new Vector3(Mathf.Lerp(transform.GetChild(0).localScale.x, 0f, 0.05f), 500f, 0);
        }

        if (transform.GetChild(0).transform.localScale.x <= 0.05f && pressureFalling && !pressureRising)
        {
            //Insert Code to Cause Damage to the Player Here




            //Insert the Code Above
            Destroy(gameObject);

        }
        


    }
}
