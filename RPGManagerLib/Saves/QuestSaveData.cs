using RPGManagerLib.Quests;

namespace RPGManagerLib.Saves
{
    /// <summary>
    /// Serializable snapshot of a quest for saving and loading.
    /// </summary>
    public class QuestSaveData
    {
        public string Title { get; set; } = "";
        public string Description { get; set; } = "";
        public QuestState State { get; set; }

        public QuestSaveData() { }

        public QuestSaveData(Quest q)
        {
            Title = q.Title;
            Description = q.Description;
            State = q.State;
        }

        public Quest ToQuest()
        {
            var q = new Quest(Title, Description);
            switch (State)
            {
                case QuestState.NotStarted: break;
                case QuestState.InProgress: q.Start(); break;
                case QuestState.Completed: q.Complete(); break;
                case QuestState.Failed: q.Fail(); break;
            }
            return q;
        }
    }
}

