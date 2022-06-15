using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGame : MonoBehaviour
{
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadEnter))
        {
            LoadRound();
        }
    }

    public static void LoadRound()
    {
        if(MarioScript.RoundCurrent == 1)
        {
            SceneManager.LoadScene("Wolrd_1-1");
        }
        else
        {
            SceneManager.LoadScene("Wolrd_1-2");
        }
    }
}
