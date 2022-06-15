using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuaXanhBep : MonoBehaviour
{
    Vector2 ViTriChet;
    GameObject Mario;
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
        ViTriChet = transform.localPosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.collider.tag == "Player" && (collision.contacts[0].normal.y < 0))
        {
            Mario.GetComponent<MarioScript>().TaoAmThanh("smb_stomp");
            Destroy(gameObject);
            GameObject HinhBep = (GameObject)Instantiate(Resources.Load("Prefabs/RuaXanhBep"));
            HinhBep.transform.localPosition = ViTriChet;
        }
    }
}
