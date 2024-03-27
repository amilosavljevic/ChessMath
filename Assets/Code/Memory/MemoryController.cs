using System.Collections;
using System.Collections.Generic;
using Solitaire.Common.Coroutines;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Random = UnityEngine.Random;
using WaitUntil = UnityEngine.WaitUntil;

public class MemoryController: MonoBehaviour
{
    [SerializeField] private Button tileButtonPrefab;
    [SerializeField] private Transform tileHolder;
    [SerializeField] private TMP_Text countLabel;

    private readonly List<MemoryTileController> buttonControllers = new List<MemoryTileController>();
    private MemoryTileController lastClickedButton;
    private MemoryTileController firstSelectedButton;
    private MemoryTileController secondsSelectedButton;
    private int tryCount;
    
    private IEnumerator Start()
    {
        var buttonValues = GenerateValues();

        InstantiateTiles(buttonValues);
        
        while (buttonControllers.Count > 0)
        {
            yield return new WaitUntil(() => lastClickedButton != null);
            firstSelectedButton = lastClickedButton;
            lastClickedButton = null;

            yield return new WaitUntil(() => lastClickedButton != null);
            secondsSelectedButton = lastClickedButton;
            lastClickedButton = null;

            ToggleAllButtonsInteraction(false);

            tryCount++;
            countLabel.text = tryCount.ToString();

            if (firstSelectedButton.ButtonHiddenValue == secondsSelectedButton.ButtonHiddenValue)
            {
                buttonControllers.Remove(firstSelectedButton);
                buttonControllers.Remove(secondsSelectedButton);
                
                yield return PlayMatchAnimation();

                Destroy(firstSelectedButton.gameObject);
                Destroy(secondsSelectedButton.gameObject);
            }
            else
            {
                yield return new WaitForSeconds(1);
                firstSelectedButton.HideValue();
                secondsSelectedButton.HideValue();
            }
            
            firstSelectedButton = null;
            secondsSelectedButton = null;
            ToggleAllButtonsInteraction(true);
        }

        // Game ended
        MainPopup.Open("Igra Memorije", $"Kraj igre. Pobeda u {tryCount} koraka.", "Kreni ponovo", OnRetryButtonClicked);
    }

    private IEnumerator PlayMatchAnimation()
    {
        firstSelectedButton.StartExplosion();
        secondsSelectedButton.StartExplosion();
        return new TweenAnimation()
        {
            Update = t =>
            {
                var alpha = (float)(1 - t);
                firstSelectedButton.GetComponent<CanvasGroup>().alpha = alpha;
                secondsSelectedButton.GetComponent<CanvasGroup>().alpha = alpha;
                var scale = 1f + (float)(t  / 2);
                firstSelectedButton.transform.localScale = scale * Vector3.one;
                secondsSelectedButton.transform.localScale = scale * Vector3.one;
            }
        }.StartAndWait();
    }

    private void InstantiateTiles(List<int> buttonValues)
    {
        for (var i = 0; i < 16; i++)
        {
            var button = Instantiate(tileButtonPrefab, tileHolder);

            var controller = button.gameObject.GetComponent<MemoryTileController>();
            controller.ButtonHiddenValue = buttonValues[i];
            buttonControllers.Add(controller);

            button.onClick.AddListener(() => OnButtonClicked(controller));
        }

        var grid = tileHolder.GetComponent<GridLayoutGroup>();
        LayoutRebuilder.ForceRebuildLayoutImmediate(tileHolder.GetComponent<RectTransform>());
        grid.enabled = false;
    }

    private void OnButtonClicked(MemoryTileController controller)
    {
        if (controller == firstSelectedButton)
            return;

        lastClickedButton = controller;
        controller.ShowValue();
    }

    private void OnRetryButtonClicked() => SceneManager.LoadScene("Memory");

    private void ToggleAllButtonsInteraction(bool isClickable)
    {
        foreach (var controller in buttonControllers)
        {
            controller.Button.interactable = isClickable;
        }
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