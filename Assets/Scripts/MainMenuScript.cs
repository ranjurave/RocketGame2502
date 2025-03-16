using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuScript : MonoBehaviour
{
    public void OpenLevel() {
        SceneManager.LoadScene("Level01");
    }
}
