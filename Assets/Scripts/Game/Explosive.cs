using UnityEngine;

public class Explosive : Block
{
    public float range;

    bool isDestroying;

    public override void GetDamage(int damage)
    {
        if(isDestroying)
        {
            return;
        }
        isDestroying = true;
        for (int i = 0; i < Controller.blocksToDestroy.Length; i++)
        {
            if(Controller.blocksToDestroy[i] == this)
            {
                continue;
            }
            if (Vector2.Distance(transform.position, Controller.blocksToDestroy[i].transform.position) < range)
            {
                Controller.blocksToDestroy[i].GetDamage(int.MaxValue);
            }
        }
        for (int i = 0; i < Controller.notNecessaryBlocks.Length; i++)
        {
            if (Controller.blocksToDestroy[i] == this)
            {
                continue;
            }
            if (Vector2.Distance(transform.position, Controller.notNecessaryBlocks[i].transform.position) < range)
            {
                Controller.notNecessaryBlocks[i].GetDamage(int.MaxValue);
            }
        }
        Controller.Sound.PlaySound("Explode");
        Controller.AddScore(score);
        gameObject.SetActive(false);
        Controller.RemoveBlock(this);
    }
}