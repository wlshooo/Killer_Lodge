using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BalloonController : MonoBehaviour
{
    float rotSpeed = 100f;
    public bool B1Rotate = true;

    public GameObject Balloon1;

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
        if(B1Rotate)
        {
            Balloon1.transform.Rotate(new Vector3(0, 0, rotSpeed * Time.deltaTime));
        }

    }

    public void ShowBalloon1()
    {
        Balloon1.SetActive(true);
    }

    public void ShowB1Question()        
    {
        Balloon1.SetActive(false);
        audioSource.Play();
       
    }
       
}
