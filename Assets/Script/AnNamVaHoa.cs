using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnNamVaHoa : MonoBehaviour
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

        if (collision.collider.tag == "Player")
        {
            if (Mario.GetComponent<MarioScript>().CapDo < 2)
            {
                Mario.GetComponent<MarioScript>().CapDo += 1;
                Mario.GetComponent<MarioScript>().BienHinh = true;
                Destroy(gameObject); 
            }
        }
    }
}
