using UnityEngine;
using System.Collections.Generic;

public class BonusCreator : Singleton<BonusCreator>, IFabric<Bonus>, IPool<Bonus>
{
    class PoolBonus
    {
        public Bonus bonus;
        public bool isBusy;

        public PoolBonus(Bonus bonus, bool isBusy)
        {
            this.bonus = bonus;
            this.isBusy = isBusy;
        }

        public PoolBonus(Bonus bonus) : this(bonus, false) { }
    }

    GameObject bonusPrefab;
    GameObject BonusPrefab
    {
        get
        {
            if((object)bonusPrefab == null)
            {
                bonusPrefab = Resources.Load<GameObject>("Bonus");
            }
            return bonusPrefab;
        }
    }

    List<PoolBonus> objects = new List<PoolBonus>();

    public Bonus GetObject()
    {
        Bonus res;
        for (int i = 0; i < objects.Count; i++)
        {
            if (!objects[i].isBusy)
            {
                objects[i].isBusy = true;
                return objects[i].bonus;
            }
        }
        res = Create();
        objects.Add(new PoolBonus(res, true));
        return res;
    }

    public void Release(Bonus obj)
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if(objects[i].bonus == obj)
            {
                objects[i].isBusy = false;
                return;
            }
        }
    }

    public Bonus Create()
    {
        return Instantiate(BonusPrefab).GetComponent<Bonus>();
    }
}