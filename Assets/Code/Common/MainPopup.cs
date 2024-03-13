using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using Object = UnityEngine.Object;

public class MainPopup : MonoBehaviour
{
    [SerializeField] private TMP_Text titleLabel;
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private TMP_Text buttonLabel;
    [SerializeField] private Button button;

    public static MainPopup Open(string title, string content, string buttonText, UnityAction clickCallback)
    {
        var prefab = Resources.Load<MainPopup>("Ui/MainPopup");
        var popup = Object.Instantiate(prefab);

        popup.titleLabel.text = title;
        popup.textLabel.text = content;
        popup.buttonLabel.text = buttonText;

        popup.button.onClick.AddListener(clickCallback);

        return popup;
    }
}