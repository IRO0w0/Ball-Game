using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // �� ������ �ʿ��� ���ӽ����̽�

public class gamemanager : MonoBehaviour
{
    public static gamemanager Instance;  // �̱��� ����

    public GameObject itemPrefab; // ������ �������� ������
    public Vector3 spawnAreaMin;  // ���� ������ �ּ� ��ǥ
    public Vector3 spawnAreaMax;  // ���� ������ �ִ� ��ǥ
    public Text ui_score;         // ������ ǥ���� �ؽ�Ʈ
    public Text ui_time;          // �÷��� �ð��� ǥ���� �ؽ�Ʈ

    private bool isGameOver = false;
    public int score;
    private float startTime;      // ���� ���� �ð��� �����ϴ� ����

    private void Awake()
    {
        if (Instance != null)
            Destroy(this);
        Instance = this;
    }

    private void Start()
    {
        score = 0;
        isGameOver = false;
        Time.timeScale = 1f;  // ���� �ӵ� ����ȭ
        startTime = Time.time; // ���� ���� ���� ���
        SpawnItem(); // ���� ���� �� ù ������ ����
    }

    private void Update()
    {
        if (!isGameOver)
        {
            UpdateTime();  // ������ ���� ���� �� �ð� ������Ʈ
        }

        // ���� ���� ���¿��� R Ű �Է� �� �����
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void PlusScore()
    {
        if (isGameOver) return; // ���� ���� �� �߰� ���� �Ұ�

        score++;
        UpdateScoreText(); // ���� ����

        if (score >= 10)
        {
            GameOver();
        }
        else
        {
            SpawnItem(); // ������ �ø� �� ���ο� ������ ����
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        Time.timeScale = 0f;  // ������ ����
    }

    // �������� ������ ��ġ�� �����ϴ� �Լ�
    public void SpawnItem()
    {
        Vector3 randomPosition = GetRandomPosition();
        Instantiate(itemPrefab, randomPosition, Quaternion.identity);
    }

    // ���� ���� ������ ������ ��ġ�� ��ȯ�ϴ� �Լ�
    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        float randomZ = Random.Range(spawnAreaMin.z, spawnAreaMax.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    // ������ ǥ���ϴ� �Լ�
    void UpdateScoreText()
    {
        ui_score.text = "Score: " + score.ToString();  // ������ �� ������Ʈ
    }

    // �÷��� �ð��� ǥ���ϴ� �Լ�
    void UpdateTime()
    {
        float timeElapsed = Time.time - startTime;
        int minutes = (int)(timeElapsed / 60);
        int seconds = (int)(timeElapsed % 60);
        ui_time.text = string.Format("Time: {0:D2}:{1:D2}", minutes, seconds);  // "Time: MM:SS" �������� ǥ��
    }

    // ������ �ٽ� �����ϴ� �Լ�
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // ���� ���� �ٽ� �ε�
    }
}
