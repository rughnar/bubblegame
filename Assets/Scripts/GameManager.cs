using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using Cinemachine;
public class GameManager : MonoBehaviour
{   
    public GameObject velocityBar;
    public TMP_Text heightText;
    public TMP_Text velocityText;
    public float cameraOffsetBeforeShot;
    public float cameraOffsetAfterShot = 0f;
    public GameObject scoreMenu;
    public GameObject pauseScreen;
    public CinemachineCameraOffset cinemachineCameraOffset;
    private AudioManager audioManager;
    private UIController uIController;
    private ScoreMenuController scoreMenuController;
    private ShopController shopController;
    [SerializeField] private int playerMoney = 0;
    void Awake()
    {
        velocityBar.gameObject.SetActive(true);
        uIController = FindObjectOfType<UIController>();
        scoreMenuController = scoreMenu.GetComponent<ScoreMenuController>();
        shopController = FindObjectOfType<ShopController>();
        cameraOffsetBeforeShot = cinemachineCameraOffset.m_Offset.y;
        heightText.enabled = false;
        velocityText.enabled = false;

    }
    // Start is called before the first frame update


    public void End(float maxVelocityReached, float maxDistanceReached)
    {
        Debug.Log("maxVelocityReached: " + maxVelocityReached);
        Debug.Log("maxDistanceReached: " + maxDistanceReached);
        pauseScreen.SetActive(true);
        scoreMenu.SetActive(true);
        float score = Mathf.Ceil(Mathf.Round(maxDistanceReached) + maxVelocityReached * 0.66f);
        playerMoney += (int)score;
        Debug.Log(playerMoney);
        PlayerPrefs.SetInt("playerMoney", playerMoney);
        PlayerPrefs.Save();
        scoreMenuController.SetMaxValues(maxDistanceReached, maxVelocityReached, score);
    }

    public void Restart()
    {
        SceneManager.LoadScene(1);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void ShowShop()
    {
        shopController.gameObject.SetActive(true);
    }

    public int GetPlayerMoney()
    {
        return playerMoney;
    }

    public void PlayerWasShot()
    {
        cinemachineCameraOffset.m_Offset = new Vector3 (0f, cameraOffsetAfterShot, 0f);
        velocityBar.gameObject.SetActive(false);
        heightText.enabled = true;
        velocityText.enabled = true;
        velocityBar.SetActive(false);
    }

}

/*public int score = 0;
public float multiplier = 1;
[SerializeField] private GameObject loseScreen;
[SerializeField] private GameObject winScreen;
[SerializeField] private GameObject pauseScreen;
[SerializeField] private GameObject helpMenu;
[SerializeField] private AudioClip winSound;
[SerializeField] private AudioClip loseSound;
public KeyCode resetKey = KeyCode.R;
public KeyCode pauseKey = KeyCode.P;
public KeyCode alternativeResumeKey = KeyCode.Escape;
public KeyCode helpKey = KeyCode.H;
public int firstLevelBuildIndex = 4;
private bool gameEnded = false;
private bool gamePaused = false;
private int sceneIndexToLoadIfReset;


    void Start()
{
    Time.timeScale = 1;
    sceneIndexToLoadIfReset = 1;
}



// Update is called once per frame
void Update()
{

    if (Input.GetKeyDown(alternativeResumeKey)) if (gamePaused) Resume();

    if (Input.GetKeyDown(pauseKey))
    {
        if (!gamePaused) Pause();
        else Resume();
    }

    if (Input.GetKeyDown(resetKey))
    {
        if (gameEnded) SceneManager.LoadScene(sceneIndexToLoadIfReset, LoadSceneMode.Single);
    }
    tiempoDeJuegoReal += Time.deltaTime;

    if (Input.GetKeyDown(helpKey))
    {
        if (helpMenu.activeSelf) helpMenu.SetActive(false);
        else helpMenu.SetActive(true);
    }

}

public void LoseGame()
{
    Time.timeScale = 0;
    sceneIndexToLoadIfReset = SceneManager.GetActiveScene().buildIndex;
    Score();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);

    //StartCoroutine(ShowLoseScreen(1.5f));
    //soundManager.PlaySFX(loseSound);

}

public IEnumerator ShowLoseScreen(float secondsToWait)
{
    yield return new WaitForSecondsRealtime(secondsToWait);
    loseScreen.SetActive(true);
    gameEnded = true;
}

public void EndLevel()
{
    Score();
    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    if (SceneManager.sceneCountInBuildSettings - 1 != SceneManager.GetActiveScene().buildIndex)
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    else
    {
        WinGame();
    }


}

public void WinGame()
{
    //   playerBehaviourController.Celebrate();

    winScreen.SetActive(true);
    gameEnded = true;
    sceneIndexToLoadIfReset = 0;
    Time.timeScale = 0;
    //soundManager.PlaySFX(winSound);
}

public void Pause()
{
    Time.timeScale = 0;
    pauseScreen.SetActive(true);
    gamePaused = true;
}

public void Resume()
{
    Time.timeScale = 1;
    pauseScreen.SetActive(false);
    gamePaused = false;
}

public void BackToMainMenu() { SceneManager.LoadScene(0); }

private void Score()
{
    PlayerPrefs.SetString("score", "" + score);
    PlayerPrefs.Save();
}

public void AddToScore(int scorePoints)
{
    score += (int)(scorePoints * multiplier);
    uIController.SetScoreSilently(score);
}*/