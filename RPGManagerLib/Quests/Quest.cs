namespace RPGManagerLib.Quests
{
    /// <summary>
    /// Lightweight quest model capturing a title, description and state.
    /// Extend with objectives, rewards, and persistence when needed.
    /// </summary>
    public class Quest
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public QuestState State { get; private set; } = QuestState.NotStarted;

        public Quest(string title, string description)
        {
            Title = title;
            Description = description;
        }

        public void Start() => State = QuestState.InProgress;
        public void Complete() => State = QuestState.Completed;
        public void Fail() => State = QuestState.Failed;
    }

    /// <summary>
    /// High-level lifecycle of a quest.
    /// </summary>
    public enum QuestState
    {
        NotStarted,
        InProgress,
        Completed,
        Failed
    }
}

