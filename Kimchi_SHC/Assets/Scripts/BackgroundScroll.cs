using UnityEngine;

public class BackgroundScroll : MonoBehaviour
{
    [Header("Settings")]
    [Tooltip("스크롤속도")]
    public float scrollSpeed = 1f;
    [Header("Reference")]
    public MeshRenderer meshRenderer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        meshRenderer.material.mainTextureOffset += new Vector2(GameManager.Instance.CalculateGameSpeed() / 20  * scrollSpeed * Time.deltaTime, 0);
    }
}
