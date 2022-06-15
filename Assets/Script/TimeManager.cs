using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeManager : MonoBehaviour
{
    public float startTime = 400;
    private Text theText;
    // Start is called before the first frame update
    void Start()
    {
        theText = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        startTime -= Time.deltaTime;
        theText.text = "" + Mathf.Round(startTime);
    }
}
