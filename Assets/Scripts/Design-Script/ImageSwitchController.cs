using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ImageSwitchController : MonoBehaviour
{
    public List<GameObject> imagePanels;
    public List<Button> ellipsisButtons;
    public List<GameObject> hoverStates;
    public List<GameObject> clickedStates;

    private int currentIndex;
    private int previousIndex;

    private void Start()
    {
        currentIndex = 0;
        previousIndex = -1;
        Debug.Log("Ellipsis buttons count: " + ellipsisButtons.Count);

        for (int i = 0; i < ellipsisButtons.Count; i++)
        {
            int index = i;
            ellipsisButtons[i].onClick.AddListener(() => SwitchImage(index));

            // Add event triggers using delegates
            var trigger = ellipsisButtons[i].gameObject.AddComponent<EventTrigger>();
            AddEventTrigger(trigger, EventTriggerType.PointerEnter, (eventData) => OnPointerEnter(index));
            AddEventTrigger(trigger, EventTriggerType.PointerExit, (eventData) => OnPointerExit(index));
            AddEventTrigger(trigger, EventTriggerType.PointerClick, (eventData) => OnPointerClick(index));
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

    private void OnPointerEnter(int index)
    {
        if (previousIndex != -1 && previousIndex != currentIndex)
        {
            ShowUnionGameObject(previousIndex, hoverStates[previousIndex], false);
            ShowUnionGameObject(previousIndex, clickedStates[previousIndex], false);
        }

        if (currentIndex != index)
        {
            ShowUnionGameObject(index, hoverStates[index], true);
        }
        previousIndex = index;
    }

    private void OnPointerExit(int index)
    {
        if (currentIndex != index)
        {
            ShowUnionGameObject(index, hoverStates[index], false);
        }
    }

    private void OnPointerClick(int index)
    {
        if (previousIndex != -1 && previousIndex != currentIndex)
        {
            ShowUnionGameObject(previousIndex, hoverStates[previousIndex], false);
            ShowUnionGameObject(previousIndex, clickedStates[previousIndex], false);
        }

        ShowUnionGameObject(currentIndex, clickedStates[currentIndex], false);
        ShowUnionGameObject(index, hoverStates[index], false);
        ShowUnionGameObject(index, clickedStates[index], true);
        currentIndex = index;
    }

    private void ShowUnionGameObject(int index, GameObject state, bool show)
    {
        state.SetActive(show);
    }

    private void AddEventTrigger(EventTrigger trigger, EventTriggerType type, UnityEngine.Events.UnityAction<BaseEventData> callback)
    {
        var entry = new EventTrigger.Entry { eventID = type };
        entry.callback.AddListener(callback);
        trigger.triggers.Add(entry);
    }
}
