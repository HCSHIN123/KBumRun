using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum GameState
{
    Intro,
    Playing,
    Dead,
}

public class GameManager : MonoBehaviour
{
    
    public TMP_Text scoreText;
    public float playStartTime;
    public GameObject introUI;
    public GameObject deadUI;
    public GameObject JumpBtn;
    public GameObject EnemySpawner;
    public GameObject FoodSpawner;
    public GameObject GoldSpawner;
    public Player player;
    public static GameManager Instance;
    public GameState state = GameState.Intro;
    public int lives = 3;
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    void Start()
    {
        SetResolution();
        introUI.SetActive(true);
    }
    
    // Update is called once per frame
    void Update()
    {
        if (state == GameState.Playing)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(CalulateScore());
        }
        else if (state == GameState.Dead)
        { 
            scoreText.text = "HighScore: " + GetHighScore();
        }

        
        if(state == GameState.Playing && lives == 0)
        {
            player.KillPlayer();
            EnemySpawner.SetActive(false);
            FoodSpawner.SetActive(false);
            GoldSpawner.SetActive(false);
            JumpBtn.SetActive(false);
            deadUI.SetActive(true);
            SaveHighScore();
            state = GameState.Dead;
        }
        if(state == GameState.Dead && Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("main");
        }
    }
    public void SetResolution()
    {
        int setWidth = 1920; // ����� ���� �ʺ�
        int setHeight = 1080; // ����� ���� ����

        int deviceWidth = Screen.width; // ��� �ʺ� ����
        int deviceHeight = Screen.height; // ��� ���� ����

        Screen.SetResolution(setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true); // SetResolution �Լ� ����� ����ϱ�

        if ((float)setWidth / setHeight < (float)deviceWidth / deviceHeight) // ����� �ػ� �� �� ū ���
        {
            float newWidth = ((float)setWidth / setHeight) / ((float)deviceWidth / deviceHeight); // ���ο� �ʺ�
            Camera.main.rect = new Rect((1f - newWidth) / 2f, 0f, newWidth, 1f); // ���ο� Rect ����
        }
        else // ������ �ػ� �� �� ū ���
        {
            float newHeight = ((float)deviceWidth / deviceHeight) / ((float)setWidth / setHeight); // ���ο� ����
            Camera.main.rect = new Rect(0f, (1f - newHeight) / 2f, 1f, newHeight); // ���ο� Rect ����
        }
    }
    public void PlayGame()
    {
        if (state != GameState.Intro)
            return;
        state = GameState.Playing;
        introUI.SetActive(false);
        EnemySpawner.SetActive(true);
        FoodSpawner.SetActive(true);
        GoldSpawner.SetActive(true);
        playStartTime = Time.time;
        JumpBtn.SetActive(true);
    }
    public float CalculateGameSpeed()
    {
        if(state != GameState.Playing)
        {
            return 5f;
        }
        float speed = 8f + (0.5f * Mathf.Floor(CalulateScore() / 10f));
        return Mathf.Min(speed, 30f);
    }
    int GetHighScore()
    {
        return PlayerPrefs.GetInt("highScore");
    }
    float CalulateScore()
    {
        return Time.time - playStartTime; 
    }

    void SaveHighScore()
    {
        int score = Mathf.FloorToInt(CalulateScore());
        int currentHighScore = PlayerPrefs.GetInt("highScore");
        if(score > currentHighScore)
        {
            PlayerPrefs.SetInt("highScore", score);
            PlayerPrefs.Save();
        }
    }
}
