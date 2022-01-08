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
        //当对应值超过1，重新开始从 0 开始
        //if (rect.verticalNormalizedPosition > 1.0f)
       // {
        //    rect.verticalNormalizedPosition = 0;
        //}

        //逐渐递减 ScrollRect 竖直方向上的值
        rect.verticalNormalizedPosition = rect.verticalNormalizedPosition - 0.02f * Time.deltaTime;
    }

}