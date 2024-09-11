using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

public class Star : MonoBehaviour
{
    public SpriteRenderer sr;
    public Vector2 direction;
    public float moveSpeed = 0.005f;
    public float sizeSpeed = 1f;
    public float colorSpeed = 3f;
    public float maxSize = 0.5f;
    public float minSize = 0.3f;
    public Color[] colors;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        direction = new Vector2(Random.Range(-1.0f, 1.0f), Random.Range(-1.0f, 1.0f));
        float size = Random.Range(minSize, maxSize);
        transform.localScale = new Vector2(size, size);
        sr.color = colors[Random.Range(0, colors.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        //이동
        transform.Translate(direction * moveSpeed);
        //크기
        transform.localScale = Vector2.Lerp(transform.localScale, Vector2.zero, Time.deltaTime * sizeSpeed);
        //투명
        Color color = sr.color;
        color.a = Mathf.Lerp(sr.color.a, 0, Time.deltaTime * colorSpeed);
        sr.color = color;
        //파괴
        if (sr.color.a <= 0.01f)
        {
            Destroy(gameObject);
        }
    }
}
