using UnityEngine;

public class Heart : MonoBehaviour
{
    public Sprite OnHeart;
    public Sprite OffHeart;
    public int LiveNumber;
    public SpriteRenderer sr;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(GameManager.Instance.lives >= LiveNumber)
        {
            sr.sprite = OnHeart;
        }
        else
        {
            sr.sprite = OffHeart;
        }
    }
}
