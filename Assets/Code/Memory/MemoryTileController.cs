using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemoryTileController : MonoBehaviour
{
    [SerializeField] private List<Sprite> allIcons;

    [SerializeField] private TMP_Text buttonLabel;
    [SerializeField] private Button button;
    [SerializeField] private Image icon;

    private int buttonHiddenValue;

    public void Start()
    {
        buttonLabel.text = "?";
    }

    public int ButtonHiddenValue
    {
        get => buttonHiddenValue;
        set
        {
            buttonHiddenValue = value;
            icon.sprite = allIcons[value - 1];
        }
    }

    public void ShowValue()
    {
        buttonLabel.gameObject.SetActive(false);
        icon.gameObject.SetActive(true);
    }

    public void HideValue()
    {
        buttonLabel.gameObject.SetActive(true);
        icon.gameObject.SetActive(false);
    }

    public Button Button => button;
}