using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; // UI 네임스페이스를 추가해야 합니다.

public class StarEF : MonoBehaviour
{
    public GameObject star; // 생성할 별 오브젝트
    public Button starButton; // UI 버튼

    void Start()
    {
        // 버튼에 클릭 이벤트를 연결
        starButton.onClick.AddListener(MakeStar);
    }

    void MakeStar()
    {
        // 마우스 위치에 별 생성
        Vector3 mouse = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mouse.z = 0;
        Instantiate(star, mouse, Quaternion.identity);
    }
}
