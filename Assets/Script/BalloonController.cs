using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    float rotSpeed = 100f;
    public bool B1 = true;
    public bool B2 = true;

    public GameObject Balloon1;
    public GameObject Balloon2;

    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        RotateBalloon();
    }

    private void RotateBalloon()
    {
        if(B1)
        {
            Balloon1.transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
        }
        if (B2)
        {
            Balloon2.transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
        }

    }

    public void ShowBalloon1()
    {
        Balloon1.SetActive(true);
    }

    public void ShowB1Question()        
    {
        B1 = false;
        Balloon1.SetActive(false);
        audioSource.Play();
        ShowBalloon2();


    }
    public void ShowBalloon2()
    {
        Balloon2.SetActive(true);
    }

    public void ShowB2Question()
    {
        Balloon2.SetActive(false);
        audioSource.Play();

    }

}
