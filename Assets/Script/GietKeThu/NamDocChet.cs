using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamDocChet : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(NamBepBienMat());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator NamBepBienMat()
    {
        yield return new WaitForSeconds(3f);
        Destroy(gameObject);
    }
}
