using UnityEngine;

public class Block : MonoBehaviour
{
    public int hp;
    public int score;
    public int[] armorHp;
    public int[] armorScore;
    public Sprite[] armorSprite;

    int armorLayers;
    bool isDestroying;
    SpriteRenderer armor;

    LocationController controller;
    protected LocationController Controller
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

    void Start()
    {
        armor = transform.GetChild(0).GetComponent<SpriteRenderer>();
        if (armorSprite.Length > 0)
        {
            armorLayers = armorSprite.Length;
            armor.sprite = armorSprite[armorLayers - 1];
        }
    }

    public virtual void GetDamage(int damage)
    {
        if(isDestroying)
        {
            return;
        }
        if(armorLayers > 0)
        {
            Controller.Sound.PlaySound("ArmorHit");
        }
        else
        {
            Controller.Sound.PlaySound("Hit");
        }
        while (damage > 0 && armorLayers > 0)
        {
            if (damage >= armorHp[armorLayers - 1])
            {
                damage -= armorHp[armorLayers - 1];
                Controller.AddScore(armorScore[armorLayers - 1]);
                if (--armorLayers == 0)
                {
                    armor.gameObject.SetActive(false);
                }
            }
            else
            {
                armorHp[armorLayers - 1] -= damage;
                return;
            }
        }
        if (damage > 0)
        {
            hp -= damage;
            if (hp <= 0)
            {
                isDestroying = true;
                Controller.AddScore(score);
                gameObject.SetActive(false);
                Controller.RemoveBlock(this);
            }
        }
        else if (armorLayers > 0)
        {
            armor.sprite = armorSprite[armorLayers - 1];
        }
    }
}