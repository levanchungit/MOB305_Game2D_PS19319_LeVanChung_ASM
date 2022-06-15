using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextRound : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(StartRound());
    }

    IEnumerator StartRound()
    {
        int roundCurrent = MarioScript.RoundCurrent;

        Debug.Log("Next Round: "+roundCurrent);
        yield return new WaitForSeconds(3f);
        if(roundCurrent == 1)
        {
            SceneManager.LoadScene("1-1");
            MarioScript.RoundCurrent += 1;
        }
        else
        {
            SceneManager.LoadScene("1-2");
        }
    }
}
