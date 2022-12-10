using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClearSceneChange : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SceneChangeDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator SceneChangeDelay()
    {
        yield return new WaitForSeconds(5.0f);
        SceneManager.LoadScene("ClearScene2");
    }
}
