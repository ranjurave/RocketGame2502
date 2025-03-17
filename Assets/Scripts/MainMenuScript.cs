using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    [SerializeField] GameObject MainMenuGO;
    [SerializeField] GameObject OptionsMenuGO;
    public static bool muteaudio;

    private void Start() {
        MainMenuGO.SetActive(true);
        OptionsMenuGO.SetActive(false);
    }

    public void OpenLevel() {
        SceneManager.LoadScene("Level01");
    }
}
