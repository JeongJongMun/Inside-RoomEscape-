using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections;
/* InGameManager.cs
 * 게임 내부에서 발생하는 이벤트를 관리하는 스크립트
 */
public class InGameManager : MonoBehaviour
{
#region Private Variables
    private static InGameManager instance = null;
#endregion

#region Public Variables
    public static InGameManager Instance { get { return instance; } }
    [SerializeField] private AudioMixer masterMixer;
    [SerializeField] private Slider audioSlider;
    public GameObject[] mentalImage;
    public GameObject gameoverPanel;
    public Image fadeImage;
    [SerializeField]
    [Range(0.01f, 10f)]
    private float fadeTime;

    [Header("UI Canvas")]
    public Canvas uiCanvas;
#endregion

#region Private Methods
    private void Awake()
    {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
#endregion

#region Public Methods
    // for AudioMixer Slide
    public void SetMusicVolume(Slider slider)
    {
        float volume = slider.value;
        masterMixer.SetFloat("BGM", Mathf.Log10(volume)*20);
    }
#endregion
    public void OnClickExitBtn(GameObject panel)
    {
        panel.SetActive(false);
        SoundManager.instance.SFXPlay("buttonSound");
        StartCoroutine(LoadMain());
    }
    public void OnClickItem(GameObject _item)
    {
        Inventory.instance.AcquireItem(_item.GetComponent<Item>());
        Destroy(_item);
    }

    private IEnumerator LoadMain()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene("Main");
    }

    public void FadeInOut()
    {
        StartCoroutine(DoFadeInOut());
    }
    private IEnumerator DoFadeInOut()
    {
        yield return StartCoroutine(Fade(0, 1));

        yield return StartCoroutine(Fade(1, 0));
    }
    private IEnumerator Fade(float start, float end)
    {
        float currentTime = 0.0f;
        float percent = 0.0f;

        while (percent < 1)
        {
            currentTime += Time.deltaTime;
            percent = currentTime / fadeTime;

            Color color = fadeImage.color;
            color.a = Mathf.Lerp(start, end, percent);
            fadeImage.color = color;
            yield return null;
        }
    }

    public void MentalBreak()
    {
        DatabaseManager.Instance.MentalPointData--;
        
        for (int i = 0; i < 3; i++)
        {
            if (i < DatabaseManager.Instance.MentalPointData)
                mentalImage[i].SetActive(true);
            else 
                mentalImage[i].SetActive(false);
        }
        // Game Over
        if (DatabaseManager.Instance.MentalPointData == 0)
        {
            gameoverPanel.SetActive(true);
        }
    }
    public void MentalRecovery()
    {
        DatabaseManager.Instance.MentalPointData = 3;

        for (int  i = 0; i < 3; i++)
        {
            mentalImage[i].SetActive(true);
        }
    }
}