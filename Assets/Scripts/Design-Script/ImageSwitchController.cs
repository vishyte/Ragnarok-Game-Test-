using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageSwitchController : MonoBehaviour
{
    public List<GameObject> imagePanels;
    public List<Button> ellipsisButtons;

    private int currentIndex;

    private void Start()
    {
        currentIndex = 0;

        for (int i = 0; i < ellipsisButtons.Count; i++)
        {
            int index = i;
            ellipsisButtons[i].onClick.AddListener(() => SwitchImage(index));
        }
    }

    public void SwitchImage(int index)
    {
        if (index == currentIndex || index >= imagePanels.Count)
        {
            return;
        }

        imagePanels[currentIndex].SetActive(false);
        imagePanels[index].SetActive(true);
        currentIndex = index;
    }
}
