using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MemoryController: MonoBehaviour
{
    [SerializeField] private Button tileButtonPrefab;
    [SerializeField] private Transform tileHolder;
    private readonly List<MemoryTileController> buttonControllers = new List<MemoryTileController>();
    private MemoryTileController lastClickedButton;
    
    private IEnumerator Start()
    {
        var buttonValues = GenerateValues();

        for (var i = 0; i < 16; i++)
        {
            var button = Instantiate(tileButtonPrefab, tileHolder);

            var controller = button.gameObject.GetComponent<MemoryTileController>();
            controller.ButtonHiddenValue = buttonValues[i];
            buttonControllers.Add(controller);

            button.onClick.AddListener(() => OnButtonClicked(controller));
        }

        var grid = tileHolder.GetComponent <GridLayoutGroup>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(tileHolder.GetComponent<RectTransform>());
        grid.enabled = false;

        //while (buttons.Count > 0)
        while (true)
        {
            yield return new WaitUntil(() => lastClickedButton != null);
            var button1Controller = lastClickedButton;
            lastClickedButton = null;

            yield return new WaitUntil(() => lastClickedButton != null);
            var button2Controller = lastClickedButton;
            lastClickedButton = null;

            ToggleAllButtonsInteraction(false);
            yield return new WaitForSeconds(2);
            ToggleAllButtonsInteraction(true);

            if (button1Controller.ButtonHiddenValue == button2Controller.ButtonHiddenValue)
            {
                buttonControllers.Remove(button1Controller);
                buttonControllers.Remove(button2Controller);

                Destroy(button1Controller.gameObject);
                Destroy(button2Controller.gameObject);
            }
            else
            {
                button1Controller.HideValue();
                button2Controller.HideValue();
            }
        }

        yield break;
    }

    private void ToggleAllButtonsInteraction(bool isClickable)
    {
        foreach (var controller in buttonControllers)
        {
            controller.Button.interactable = isClickable;
        }
    }

    private void OnButtonClicked(MemoryTileController controller)
    {
        lastClickedButton = controller;
        controller.ShowValue();
    }

    private List<int> GenerateValues()
    {
        var result = new List<int>(16);

        for (var i = 1; i <= 8; i++)
        {
            result.Add(i);
            result.Add(i);
        }

        Shuffle(result);

        return result;
    }

    private void Shuffle(List<int> values)
    {
        for (var i = 0; i < values.Count; i++)
        {
            var j = Random.Range(0, values.Count);

            (values[i], values[j]) = (values[j], values[i]);
        }
    }
}