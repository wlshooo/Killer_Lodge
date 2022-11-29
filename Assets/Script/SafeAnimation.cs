using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SafeAnimation : MonoBehaviour
{

    public Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SafeAnimationPlay()
    {
        anim.SetTrigger("Correct");
        
    }
}
