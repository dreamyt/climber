using UnityEngine;
using UnityEngine.UI;

public class TextScroll : MonoBehaviour
{

    //set ScrollRect
    ScrollRect rect;

    void Start()
    {
        //get ScrollRect var
        rect = this.GetComponent<ScrollRect>();
    }

    void Update()
    {
        //Update ScrollValue
        ScrollValue();
    }

    private void ScrollValue()
    {
        //����Ӧֵ����1�����¿�ʼ�� 0 ��ʼ
        //if (rect.verticalNormalizedPosition > 1.0f)
       // {
        //    rect.verticalNormalizedPosition = 0;
        //}

        //�𽥵ݼ� ScrollRect ��ֱ�����ϵ�ֵ
        rect.verticalNormalizedPosition = rect.verticalNormalizedPosition - 0.02f * Time.deltaTime;
    }

}