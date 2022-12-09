using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TypingEffect : MonoBehaviour
{
    private bool checkEndInit = false;

    public AudioSource typingSound;
    public Text tx;
    private string m_text = "��� �� ���� ģ�� ģ���� �ѹ��̿��� ������ �Դ�.\n\n" +
        "�ƹ����� ģ�� ģ���� �ڽ��� �������� �ѹ��̸� �ҷ���\n\n" +
        "�� ������ ���� �ʾҴ� �ѹ��̴� ���� �ð��� ������\n\n" +
        "������ ������ ������ �ڱ⸦ ������ �� �޶�� �ּҸ� �˷��־���\n\n" +
        "\" ��⵵ ���� ���籸 �ֱ��� 577 - 25 ���Ϻ��� \"\n\n" +
        "�ѹ��̴� ��ӵ� �ð��� �Ǿ ������ ���� �ʾҰ�\n\n" +
        "���� �ѹ��̸� ������ ���Ϻ������� ����.\n\n";
       
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
