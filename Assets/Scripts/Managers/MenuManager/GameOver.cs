using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    [SerializeField] private Transform gameOverPanel;
    public void OpenGameOverPanel() {
        AudioManager.instance.PlayGameOverSound();
        PlayerInteract.instance._input.ActivateUISelect();
        gameOverPanel.gameObject.SetActive(true);
    }

    public void StartFromLastSaved() {
        SceneManager.LoadScene(1);
    }
    public void BackHome() {
        SceneManager.LoadScene(0);
    }
}
