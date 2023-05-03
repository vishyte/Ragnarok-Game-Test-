using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ButtonScrollbarController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    public Scrollbar scrollbar;
    public float targetValue;

    private bool isHovering;
    private bool isClicked;

    private void Update()
    {
        if (isHovering || isClicked)
        {
            scrollbar.value = Mathf.Lerp(scrollbar.value, targetValue, Time.deltaTime * 5f);
            if (Mathf.Abs(scrollbar.value - targetValue) < 0.01f)
            {
                scrollbar.value = targetValue;
                if (isClicked)
                {
                    isHovering = false;
                }
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isHovering = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isClicked)
        {
            isHovering = false;
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        isClicked = !isClicked;
    }
}
