using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;  // 씬 관리에 필요한 네임스페이스

public class gamemanager : MonoBehaviour
{
    public static gamemanager Instance;  // 싱글톤 패턴

    public GameObject itemPrefab; // 스폰할 아이템의 프리팹
    public Vector3 spawnAreaMin;  // 스폰 영역의 최소 좌표
    public Vector3 spawnAreaMax;  // 스폰 영역의 최대 좌표
    public Text ui_score;         // 점수를 표시할 텍스트
    public Text ui_time;          // 플레이 시간을 표시할 텍스트

    private bool isGameOver = false;
    public int score;
    private float startTime;      // 게임 시작 시간을 저장하는 변수

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
        Time.timeScale = 1f;  // 게임 속도 정상화
        startTime = Time.time; // 게임 시작 시점 기록
        SpawnItem(); // 게임 시작 시 첫 아이템 스폰
    }

    private void Update()
    {
        if (!isGameOver)
        {
            UpdateTime();  // 게임이 진행 중일 때 시간 업데이트
        }

        // 게임 오버 상태에서 R 키 입력 시 재시작
        if (isGameOver && Input.GetKeyDown(KeyCode.R))
        {
            RestartGame();
        }
    }

    public void PlusScore()
    {
        if (isGameOver) return; // 게임 종료 시 추가 점수 불가

        score++;
        UpdateScoreText(); // 점수 갱신

        if (score >= 10)
        {
            GameOver();
        }
        else
        {
            SpawnItem(); // 점수를 올린 후 새로운 아이템 스폰
        }
    }

    void GameOver()
    {
        isGameOver = true;
        Debug.Log("Game Over!");
        Time.timeScale = 0f;  // 게임을 멈춤
    }

    // 아이템을 랜덤한 위치에 스폰하는 함수
    public void SpawnItem()
    {
        Vector3 randomPosition = GetRandomPosition();
        Instantiate(itemPrefab, randomPosition, Quaternion.identity);
    }

    // 스폰 영역 내에서 랜덤한 위치를 반환하는 함수
    Vector3 GetRandomPosition()
    {
        float randomX = Random.Range(spawnAreaMin.x, spawnAreaMax.x);
        float randomY = Random.Range(spawnAreaMin.y, spawnAreaMax.y);
        float randomZ = Random.Range(spawnAreaMin.z, spawnAreaMax.z);

        return new Vector3(randomX, randomY, randomZ);
    }

    // 점수를 표시하는 함수
    void UpdateScoreText()
    {
        ui_score.text = "Score: " + score.ToString();  // 점수와 라벨 업데이트
    }

    // 플레이 시간을 표시하는 함수
    void UpdateTime()
    {
        float timeElapsed = Time.time - startTime;
        int minutes = (int)(timeElapsed / 60);
        int seconds = (int)(timeElapsed % 60);
        ui_time.text = string.Format("Time: {0:D2}:{1:D2}", minutes, seconds);  // "Time: MM:SS" 형식으로 표시
    }

    // 게임을 다시 시작하는 함수
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // 현재 씬을 다시 로드
    }
}
