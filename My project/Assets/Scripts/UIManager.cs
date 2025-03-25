using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject GameOverPanel;
    public Image GameOverPanelImage;
    public TextMeshProUGUI TextMeshProUGUI;

    [SerializeField] private float fadeDuration = 3f; // Fade süresi

    private bool _isGameOver = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        Time.timeScale = 1.0f;
    }

    private void Update()
    {
        if (_isGameOver)
        {
            if (Input.anyKeyDown)
                SceneManager.LoadScene(0);
        }
    }

    public void ShowGameOverUI()
    {
        _isGameOver = true;
        Time.timeScale = 0;
        GameOverPanel.SetActive(true);

        // Baþlangýçta alpha deðerlerini 0 yapýyoruz
        Color imageColor = GameOverPanelImage.color;
        imageColor.a = 0f;
        GameOverPanelImage.color = imageColor;

        Color textColor = TextMeshProUGUI.color;
        textColor.a = 0f;
        TextMeshProUGUI.color = textColor;

        StartCoroutine(FadeInUI());
    }

    private IEnumerator FadeInUI()
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            float alpha = Mathf.Clamp01(elapsedTime / fadeDuration);

            // Image ve Text'in alpha deðerlerini güncelle
            Color imageColor = GameOverPanelImage.color;
            imageColor.a = alpha;
            GameOverPanelImage.color = imageColor;

            Color textColor = TextMeshProUGUI.color;
            textColor.a = alpha;
            TextMeshProUGUI.color = textColor;

            elapsedTime += Time.unscaledDeltaTime; // Oyun zamaný 0 olsa bile çalýþýr
            yield return null;
        }

        // Son durumda alpha deðerlerini tam 1 olarak ayarla
        Color finalImageColor = GameOverPanelImage.color;
        finalImageColor.a = 1f;
        GameOverPanelImage.color = finalImageColor;

        Color finalTextColor = TextMeshProUGUI.color;
        finalTextColor.a = 1f;
        TextMeshProUGUI.color = finalTextColor;
    }
}
