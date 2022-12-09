using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    private bool checkEndInit = false;

    public AudioSource typingSound;
    public Text tx;
    private string m_text = "어느 날 가장 친한 친구인 한문이에게 연락이 왔다.\n\n" +
        "아버지의 친한 친구가 자신의 별장으로 한문이를 불렀고\n\n" +
        "영 느낌이 좋지 않았던 한문이는 일정 시간이 지나도\n\n" +
        "본인이 연락이 없으면 자기를 데리러 와 달라고 주소를 알려주었다\n\n" +
        "\" 경기도 고양시 덕양구 주교동 577 - 25 제일별장 \"\n\n" +
        "한문이는 약속된 시간이 되어도 연락이 오지 않았고\n\n" +
        "나는 한문이를 데리러 제일별장으로 갔다.\n\n";
       
    // Start is called before the first frame update
    void Start()
    {
        typingSound = GetComponent<AudioSource>();
        StartInit();
    }

     void Update()
    {
        if(checkEndInit)
        {
            EndInit();
        }
    }
    public void StartTyping()
    {
        StartCoroutine(typing());
    }

  IEnumerator typing()
    {
        yield return new WaitForSeconds(2f);

        typingSound.Play();
        for (int i= 0; i<= m_text.Length; i++)
        {
            tx.text = m_text.Substring(0, i);
            yield return new WaitForSeconds(0.1f);
        }
        typingSound.Stop();
    }

    private void StartInit()
    {
        GameObject.Find("Player").GetComponent<PlayerController>().isInput = false;
        GameObject.Find("Camera").GetComponent<DialogController>().ComputerPanel.SetActive(true);
        StartTyping();
        checkEndInit = true;
    }
    private void EndInit()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject.Find("Player").GetComponent<PlayerController>().isInput = true;
            GameObject.Find("Camera").GetComponent<DialogController>().ComputerPanel.SetActive(false);
            tx.gameObject.SetActive(false);
            checkEndInit = false;
        }
    }
}
