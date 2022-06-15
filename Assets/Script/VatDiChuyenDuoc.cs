using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VatDiChuyenDuoc : MonoBehaviour
{
    public float VanTocConVat = 2;
    public bool DiChuyenTrai = true;
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
        Vector2 DiChuyen = transform.localPosition;
        if (DiChuyenTrai) DiChuyen.x += VanTocConVat * Time.deltaTime;
        else DiChuyen.x -= VanTocConVat * Time.deltaTime;
        transform.localPosition = DiChuyen;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.contacts[0].normal.x > 0)
        {
            DiChuyenTrai = false;
            QuayMat();
        }
        else
        {
            DiChuyenTrai = true;
            QuayMat();
        }
    }

    void QuayMat()
    {
        DiChuyenTrai = !DiChuyenTrai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;
    }
}
