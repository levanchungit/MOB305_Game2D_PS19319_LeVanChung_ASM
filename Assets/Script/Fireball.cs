using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball : MonoBehaviour
{
    public Transform firePoint;
    public GameObject fireball;
    GameObject Mario;


    // Start is called before the first frame update
    void Start()
    {
        Mario = GameObject.Find("Mario");

    }

    // Update is called once per frame
    void Update()
    {
        int CapDo = Mario.GetComponent<MarioScript>().CapDo;

        if(CapDo > 1)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                Shoot();
            }
        }
    }

    void Shoot()
    {
         Instantiate(fireball, firePoint.position, firePoint.rotation);
    }

}
