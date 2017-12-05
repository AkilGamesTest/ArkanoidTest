public interface ILevels
{
    event System.Action<int> onHpChange;

    event System.Action<int> onScoreChange;

    event System.Action<int> onTimeChange;

    int LevelNumber { get; }

    int Score { get; set; }

    int Hp { get; set; }

    int ElapcedTime { get; set; }

    bool IsPaused { get; set; }

    void StartLevel(int level);

    void NextLevel();

    void RestartLevel();

    void DestroyLevel();
}