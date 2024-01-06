using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BackController : MonoBehaviour
{
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(OnBackClicked);
    }

    private void OnBackClicked()
    {
        SceneManager.LoadScene("MainScreen");
    }
}
