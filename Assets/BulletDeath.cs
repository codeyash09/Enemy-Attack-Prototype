using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDeath : MonoBehaviour
{
    public float death;

    public void nTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            //Cause Damage Here

            Destroy(gameObject);
        }

        if (collision.tag == "Throwable")
        {
            

            Destroy(gameObject);
        }

        if (collision.tag == "Wall")
        {


            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        death -= Time.deltaTime;

        if(death <= 0)
        {
            Destroy(gameObject);
        }
    }
}
