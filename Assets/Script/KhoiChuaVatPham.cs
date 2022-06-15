using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KhoiChuaVatPham : MonoBehaviour
{
    private float DoNayCuaKhoi = 0.5f;
    private float ToDoNay = 4f;
    private bool DuocNay = true;
    private Vector3 ViTriLucDau;

    //Các biến để gán Item cho nó (xu, nấm, sao)
    public bool ChuaNam = false;
    public bool ChuaXu = false;
    public bool ChuaSao = false;
    //Số lượng xu hiện thị
    public int SoLuongXu = 1;

    //Lấy cấp độ để check Mario ra Item
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

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        //va chạm phía dưới block
        if (collision.collider.tag == "VaCham" && collision.contacts[0].normal.y > 0)
        {
            ViTriLucDau = transform.position;
            KhoiNayLen();
        }
    }

    void KhoiNayLen()
    {
        if (DuocNay)
        {
            StartCoroutine(KhoiNay());
            DuocNay = false;
            if (ChuaNam ==  true)
            {
                NamVaHoa();
            }
            else if (ChuaXu == true)
            {
                HienThiXu();
            }

        }
    }

    IEnumerator KhoiNay()
    {
        //đẩy block lên khi Mario nhảy lên đụng
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y + ToDoNay * Time.deltaTime);
            if (transform.localPosition.y >= ViTriLucDau.y + DoNayCuaKhoi) break;
            yield return null;
        }
        //hạ block xuống
        while (true)
        {
            transform.localPosition = new Vector2(transform.localPosition.x, transform.localPosition.y - ToDoNay * Time.deltaTime);
            if (transform.localPosition.y <= ViTriLucDau.y) break;
            Destroy(gameObject);
            GameObject KhoiRong = (GameObject)Instantiate(Resources.Load("Prefabs/KhoiTrong"));
            KhoiRong.transform.position = ViTriLucDau;
            yield return null;
        }
    }

    void NamVaHoa()
    {
        int CapDoHienTai = Mario.GetComponent<MarioScript>().CapDo;
        GameObject Nam = null;
        if (CapDoHienTai == 0) Nam = (GameObject)Instantiate(Resources.Load("Prefabs/NamAn"));
        else Nam = (GameObject)Instantiate(Resources.Load("Prefabs/NamAn"));
        Mario.GetComponent<MarioScript>().TaoAmThanh("smb_powerup_appears");
        //spawn ngay trên đầu block
        Nam.transform.SetParent(this.transform.parent);
        Nam.transform.localPosition = new Vector2(ViTriLucDau.x, ViTriLucDau.y + 1f);
    }

    void HienThiXu()
    {
        GameObject DongXu = (GameObject)Instantiate(Resources.Load("Prefabs/DongXu"));
        DongXu.transform.SetParent(this.transform.parent);
        DongXu.transform.localPosition = new Vector2(ViTriLucDau.x, ViTriLucDau.y + 1f);
        StartCoroutine(XuNayLen(DongXu));
    }

    IEnumerator XuNayLen(GameObject DongXu)
    {
        ScoreScript.scoreValue += 1;
        Mario.GetComponent<MarioScript>().TaoAmThanh("smb_coin");
        while (true)
        {
            DongXu.transform.localPosition = new Vector2(DongXu.transform.localPosition.x, DongXu.transform.localPosition.y + 1f * Time.deltaTime);
            if (DongXu.transform.localPosition.y >= ViTriLucDau.y + 100f) break;
            yield return null;
        }
        //hạ block xuống
        while (true)
        {
            DongXu.transform.localPosition = new Vector2(DongXu.transform.localPosition.x, DongXu.transform.localPosition.y - 1f * Time.deltaTime);
            if (DongXu.transform.localPosition.y <= ViTriLucDau.y) break;
            /*Destroy(DongXu.gameObject);*/
            yield return null;
        }
    }
}
