using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyController : MonoBehaviour
{
    GameObject Key;
    // Start is called before the first frame update
    void Start()
    {
        Key = GameObject.Find("key");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Getkey()
    {
        Key.SetActive(false);
    }
}
