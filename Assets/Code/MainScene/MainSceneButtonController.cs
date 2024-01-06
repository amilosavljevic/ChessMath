using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainSceneButtonController : MonoBehaviour
{
    [SerializeField] private string sceneToOpen;
    
    void Start()
    {
        var button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }

    private void OnButtonClicked()
    {
        SceneManager.LoadScene(sceneToOpen);
    }
}
