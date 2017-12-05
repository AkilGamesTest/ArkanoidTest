using UnityEngine;

public enum BonusType : byte
{
    INCREASE_BALLS_SIZE,
    REDUCE_BALLS_SIZE,
}

public class Bonus : MonoBehaviour
{
    public BonusType type;
    public Sprite[] sprites;

    const float fallSpeed = 0.05f;

    LocationController controller;
    LocationController Controller
    {
        get
        {
            if (controller == null)
            {
                controller = LocationController.Instance;
            }
            return controller;
        }
    }

    public void Set(BonusType type = BonusType.INCREASE_BALLS_SIZE)
    {
        this.type = type;
        GetComponent<SpriteRenderer>().sprite = sprites[(int)type];
    }

    void FixedUpdate()
    {
        transform.position = new Vector3(transform.position.x, transform.position.y - fallSpeed, transform.position.z);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Controller.ApplyBonus(this);
            gameObject.SetActive(false);
        }
    }
}