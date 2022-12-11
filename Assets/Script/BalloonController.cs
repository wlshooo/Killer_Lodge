using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    float rotSpeed = 100f;
    public bool B1 = true;
    public bool B2 = true;
    public bool B3 = true;
    public bool B4 = true;

    public GameObject Balloon1;
    public GameObject Balloon2;
    public GameObject Balloon3;
    public GameObject Balloon4;

    public GameObject Q1;
    public GameObject Q2;
    public GameObject Q3;
    public GameObject Q4;

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
        if (B3)
        {
            Balloon3.transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
        }
        if (B4)
        {
            Balloon4.transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
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
        Q1.SetActive(true);
        ShowBalloon2();


    }
    public void ShowBalloon2()
    {
        Balloon2.SetActive(true);
    }

    public void ShowB2Question()
    {
        Q1.SetActive(false);
        B2 = false;
        Balloon2.SetActive(false);
        audioSource.Play();
        Q2.SetActive(true);
        ShowBalloon3();

    }

    public void ShowBalloon3()
    {
        Balloon3.SetActive(true);
    }

    public void ShowB3Question()
    {
        Q2.SetActive(false);
        Balloon3.SetActive(false);
        audioSource.Play();
        Q3.SetActive(true);
        ShowBalloon4();
    }
    public void ShowBalloon4()
    {
        Balloon4.SetActive(true);
    }

    public void ShowB4Question()
    {
        Q3.SetActive(false);
        Balloon4.SetActive(false);
        Q4.SetActive(true);
        audioSource.Play();

    }

}
