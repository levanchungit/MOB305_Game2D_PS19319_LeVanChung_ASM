using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraScript : MonoBehaviour
{
    private Transform Player;
    //lấy vị trí X, Y nhỏ nhất của camera
    private float minX = 0;
    private float maxX = 195;
    private void Start()
    {
        Player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        if (Player != null)
        {
            Vector3 vitri = transform.position;
            vitri.x = Player.position.x;
            if (vitri.x < minX) vitri.x = 0;
            else if (vitri.x > maxX) vitri.x = maxX;
            transform.position = vitri;
        }

        ReloadGame();

    }
    void ReloadGame()
    {
        if (Input.GetKey(KeyCode.R))
        {
            Debug.Log("RELOAD GAME");
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }

}


