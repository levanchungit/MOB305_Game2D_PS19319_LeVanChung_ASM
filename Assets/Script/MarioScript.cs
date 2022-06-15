using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MarioScript : MonoBehaviour
{
    private float VanToc = 7;
    private float TocDo = 0;
    private bool DuoiDat = true;
    private bool ChuyenHuong = false;
    private bool QuayPhai = true;
    private float NhayCao = 550;
    private float NhayThap = 5;
    private float RoiXuong = 5;
    private float KTGiuPhim = 0.2f;
    private float TGGiuPhim = 0;
    private float VanTocToiDa = 12f;

    private Animator HanhDong;
    private Rigidbody2D r2d;

    public int CapDo = 0;
    public bool BienHinh = false;

    private Vector2 ViTriChet;

    public AudioSource AmThanh;

    GameObject Mario;

    ParticleSystem PhaoHoaWinGame;

    public static int RoundCurrent = 1;

    // Start is called before the first frame update
    void Start()
    {
        HanhDong = GetComponent<Animator>();
        r2d = GetComponent<Rigidbody2D>();
        AmThanh = GetComponent<AudioSource>();
        Mario = GameObject.Find("Mario");
        PhaoHoaWinGame = GetComponent<ParticleSystem>();
        PhaoHoaWinGame = GameObject.Find("PhaoHoaWinGame").GetComponent<ParticleSystem>();
        PhaoHoaWinGame.Pause();
    }

    // Update is called once per frame
    void Update()
    {
        HanhDong.SetFloat("TocDo", TocDo);
        HanhDong.SetBool("DuoiDat", DuoiDat);
        HanhDong.SetBool("ChuyenHuong", ChuyenHuong);
        NhayLen();
        TangToc();
        if (BienHinh == true)
        {
            switch (CapDo)
            {
                case 0:
                    {
                        StartCoroutine(MarioThuNho());
                        TaoAmThanh("smb_pipe");
                        BienHinh = false;
                        break;
                    }
                case 1:
                    {
                        StartCoroutine(MarioAnNam());
                        TaoAmThanh("smb_powerup");
                        BienHinh = false;
                        break;
                    }
                case 2:
                    {
                        StartCoroutine(MarioAnHoa());
                        BienHinh = false;
                        TaoAmThanh("smb_powerup");
                        break;
                    }
                default:
                    BienHinh = false;
                    break;
            }
        }
    }

    private void FixedUpdate()
    {
        DiChuyen();

        //Mario té vực chết
        if (gameObject.transform.position.y <= -20f)
        {
            TaoAmThanh("smb_mariodie");
            AmThanh.volume = 0.3f;
        }
    }

    void DiChuyen()
    {
        float PhimTraiPhai = Input.GetAxis("Horizontal");
        r2d.velocity = new Vector2(VanToc * PhimTraiPhai, r2d.velocity.y);
        TocDo = Mathf.Abs(VanToc * PhimTraiPhai);
        if (PhimTraiPhai > 0 && !QuayPhai) HuongMatMario();
        if (PhimTraiPhai < 0 && QuayPhai) HuongMatMario();
    }

    void HuongMatMario()
    {
        QuayPhai = !QuayPhai;
        Vector2 HuongQuay = transform.localScale;
        HuongQuay.x *= -1;
        transform.localScale = HuongQuay;
        if (TocDo > 1) StartCoroutine(MarioChuyenHuong());
        
    }

    void NhayLen()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (DuoiDat == false) return;
            r2d.AddForce((Vector2.up) * NhayCao);
            DuoiDat = false;
            TaoAmThanh("smb_jump-super");

        }
        //áp dụng lực hút trái đất để Mario rơi xuống nhanh hơn
        if (r2d.velocity.y < 0)
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (RoiXuong - 1) * Time.deltaTime;
        }
        else if (r2d.velocity.y > 0 && !Input.GetKey(KeyCode.Space))
        {
            r2d.velocity += Vector2.up * Physics2D.gravity.y * (NhayThap - 1) * Time.deltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "NenDat")
        {
            DuoiDat = true;
        }

        if (collision.gameObject.tag == "Win")
        {
            r2d.constraints = RigidbodyConstraints2D.FreezeAll;
            TaoAmThanh("smb_stage_clear");
        }

        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "NenDat")
        {
            DuoiDat = true;
        }

        if (collision.tag == "End_Round")
        {
            AmThanh.Stop();
            r2d.constraints = RigidbodyConstraints2D.FreezePosition;
            TaoAmThanh("smb_stage_clear");
            StartCoroutine(PhaoHoa());
            StartCoroutine(End_NextRound());
        }

        if (collision.tag == "Gold")
        {
            Destroy(collision.gameObject);
            ScoreScript.scoreValue += 1;
            TaoAmThanh("smb_coin");
        }
    }

    IEnumerator PhaoHoa()
    {
        yield return new WaitForSeconds(2f);
        TaoAmThanh("smb_fireworks");
        AmThanh.loop = true;
        PhaoHoaWinGame.Play();
    }

    IEnumerator End_NextRound()
    {
        yield return new WaitForSeconds(6f);
        Destroy(gameObject);
        StartGame.LoadRound();/*
        SceneManager.LoadScene("Wolrd_1-1");
        SceneManager.LoadScene("1-2");*/
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "NenDat")
        {
            DuoiDat = true;
        }
    }

    //Chờ đợi 1 khoảng thời gian để thay đổi 1 trạng thái dựa vào seconds
    IEnumerator MarioChuyenHuong()
    {
        ChuyenHuong = true;
        yield return new WaitForSeconds(0.2f);
        ChuyenHuong = false;
    }

    void TangToc()
    {
        if (Input.GetKey(KeyCode.Z))
        {
            TGGiuPhim += Time.deltaTime;
            if (TGGiuPhim > KTGiuPhim)
            {
                VanToc *= 1.01f;
                if (VanToc > VanTocToiDa) VanToc = VanTocToiDa;
            }
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            VanToc = 7f;
            TGGiuPhim = 0;
        }
    }

    //Thay đổi độ lớn của Mario
    IEnumerator MarioAnNam()
    {
        float DoTre = 0.1f;
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
    }

    IEnumerator MarioAnHoa()
    {
        float DoTre = 0.1f;
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 1);
        yield return new WaitForSeconds(DoTre);
    }

    IEnumerator MarioThuNho()
    {
        float DoTre = 0.1f;
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioNho"), 1);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("MarioLon"), 0);
        HanhDong.SetLayerWeight(HanhDong.GetLayerIndex("AnHoa"), 0);
        yield return new WaitForSeconds(DoTre);
    }

    public void TaoAmThanh(string fileAmThanh)
    {
        //lấy path resources
        AmThanh.PlayOneShot(Resources.Load<AudioClip>("Media/" + fileAmThanh));
    }

    public void MarioChet()
    {
        ViTriChet = transform.localPosition;
        GameObject MarioChet = (GameObject)Instantiate(Resources.Load("Prefabs/MarioChet"));
        MarioChet.transform.localPosition = ViTriChet;
        Destroy(gameObject);
    }
}
