using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MemoryTileController : MonoBehaviour
{
    [SerializeField] private TMP_Text buttonLabel;
    [SerializeField] private Button button;
    public int ButtonHiddenValue;

    public void ShowValue() =>
        buttonLabel.text = ButtonHiddenValue.ToString();

    public void HideValue() =>
        buttonLabel.text = "?";

    public Button Button => button;
}