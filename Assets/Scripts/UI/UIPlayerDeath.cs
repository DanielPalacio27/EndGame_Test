using UnityEngine;
using Character;
using UnityEngine.SceneManagement;

public class UIPlayerDeath : MonoBehaviour
{
    [SerializeField] private PlayerController playerController = null;
    [SerializeField] private GameObject deathUI = null;

    private void Awake() => deathUI.gameObject.SetActive(false);
    private void OnEnable() => playerController.OnDeath += ShowDeathUI;
    private void OnDestroy() => playerController.OnDeath -= ShowDeathUI;
    private void ShowDeathUI() => deathUI.SetActive(true);
    public void RestartScene() => SceneManager.LoadScene(0);

}
