using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeThuScript : MonoBehaviour
{
    GameObject Mario;

    //gọi trc khi gọi hàm Start
    private void Awake()
    {
        Mario = GameObject.FindGameObjectWithTag("Player");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player" && (collision.contacts[0].normal.x > 0 || collision.contacts[0].normal.x < 0))
        {
            if(Mario.GetComponent<MarioScript>().CapDo > 1)
            {
                Mario.GetComponent<MarioScript>().CapDo = 1;
                Mario.GetComponent<MarioScript>().BienHinh = true;
            }
            else if(Mario.GetComponent<MarioScript>().CapDo == 1)
            {
                Mario.GetComponent<MarioScript>().CapDo = 0;
                Mario.GetComponent<MarioScript>().BienHinh = true;
            }
            else
            {
                Mario.GetComponent<MarioScript>().MarioChet();
            }
        }
    }
}
