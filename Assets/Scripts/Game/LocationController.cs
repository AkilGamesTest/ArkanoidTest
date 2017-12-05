using UnityEngine;
using System.Collections.Generic;

public class LocationController : Singleton<LocationController>
{
    public GameObject shipPrefab;
    public GameObject ballPrefab;
    public Transform ballContainer;
    public Transform bonusContainer;
    public Block[] blocksToDestroy;
    public Block[] notNecessaryBlocks;

    int ballCount = 1;
    int blockCount = 1;
    Ship ship;
    List<Ball> balls = new List<Ball>();

    const int blockHitScore = 1;
    const int bonusDropChance = 25;

    ISound sound;
    public ISound Sound
    {
        get
        {
            if (sound == null)
            {
                sound = ManagersContainer.Instance.SoundController;
            }
            return sound;
        }
    }

    ILevels levels;
    public ILevels Levels
    {
        get
        {
            if (levels == null)
            {
                levels = ManagersContainer.Instance.LevelsController;
            }
            return levels;
        }
    }

    IActivities activities;
    public IActivities Activities
    {
        get
        {
            if (activities == null)
            {
                activities = ManagersContainer.Instance.ActivityController;
            }
            return activities;
        }
    }

    IPool<Bonus> bonusGenerator;
    IPool<Bonus> BonusGenerator
    {
        get
        {
            if(bonusGenerator == null)
            {
                bonusGenerator = BonusCreator.Instance;
            }
            return bonusGenerator;
        }
    }

    void Start()
    {
        blockCount = blocksToDestroy.Length;
        ship = Instantiate(shipPrefab, transform).GetComponent<Ship>();
        ship.transform.localPosition = new Vector3(0.0f, -3.5f, -0.02f);
        Ball b = Instantiate(ballPrefab, transform).GetComponent<Ball>();
        balls.Add(b);
        ship.SetBall(b);
    }

    public void ReleaseBall(Ball ball)
    {
        ball.transform.SetParent(ballContainer);
        ball.Launch();
    }

    public void RemoveBall(Ball ball)
    {
        ballCount--;
        ball.gameObject.SetActive(false);
        if(ballCount == 0)
        {
            if(Levels.Hp == 1)
            {
                Sound.PlaySound("GameOver");
            }
            else
            {
                Sound.PlaySound("Lose");
                ship.transform.localPosition = new Vector3(0.0f, ship.transform.localPosition.y, ship.transform.localPosition.z);
                ship.SetBall(balls[0]);
                ballCount = 1;
            }
            Levels.Hp--;
        }
    }

    public void RemoveBlock(Block block)
    {
        if(System.Array.IndexOf<Block>(blocksToDestroy, block) < 0)
        {
            // not necessary block has been destroyed
            return;
        }
        if(--blockCount == 0)
        {
            Sound.PlaySound("Win");
            Levels.IsPaused = true;
            if (Levels.LevelNumber < LevelManager.totalLevels)
            {
                Activities[ActivityName.WIN].Show();
            }
            else
            {
                Levels.NextLevel();
            }
        }
        else
        {
            if (Random.Range(0, 100) < bonusDropChance)
            {
                Bonus bonus = BonusGenerator.GetObject();
                bonus.Set((BonusType)Random.Range(0, 2));
                bonus.transform.SetParent(bonusContainer);
                bonus.transform.position = block.transform.position + new Vector3(0.0f, 0.0f, 0.01f);
            }
        }
    }

    public void AddHitBallScore()
    {
        Levels.Score += blockHitScore;
    }

    public void AddScore(int score)
    {
        // can be added a difficulty multiplier
        Levels.Score += score;
    }

    public void ApplyBonus(Bonus bonus)
    {
        switch (bonus.type)
        {
            case BonusType.INCREASE_BALLS_SIZE:
                Sound.PlaySound("Expand");
                for (int i = 0; i < balls.Count; i++)
                {
                    balls[i].Size++;
                }
                AddScore(20);
                break;
            case BonusType.REDUCE_BALLS_SIZE:
                Sound.PlaySound("Shrink");
                for (int i = 0; i < balls.Count; i++)
                {
                    balls[i].Size--;
                }
                AddScore(20);
                break;
        }
    }
}