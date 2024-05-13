using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI pauseText;
    [SerializeField] private Transform pausePanel;

    public void Pause() {
        Time.timeScale = 0;
        pauseText.gameObject.SetActive(false);
        pausePanel.gameObject.SetActive(true);
    }
    public void Resume() {
        Time.timeScale = 1;
        PlayerInteract.instance._input.DeActivateUISelect();
        pauseText.gameObject.SetActive(true);
        pausePanel.gameObject.SetActive(false);
    }

    public void BackHome() {
        SaveManager.SaveAllGameData();
        SceneManager.LoadScene(0);
    }

    private void Update() {
        if (PlayerInteract.instance._input.esc) {
            PlayerInteract.instance._input.ActivateUISelect();
            Pause();
        }
    }
}
