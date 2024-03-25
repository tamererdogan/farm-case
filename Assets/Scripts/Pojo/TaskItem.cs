public class TaskItem
{
    private int currentCount;

    public int totalCount { get; private set; }

    public int itemId { get; private set; }

    public string action { get; private set; }

    public TaskItem(int totalCount, int itemId, string action)
    {
        currentCount = 0;
        this.totalCount = totalCount;
        this.itemId = itemId;
        this.action = action;
    }

    public bool IsDone()
    {
        return totalCount == currentCount;
    }

    public void IncreaseCount()
    {
        currentCount++;
        currentCount = currentCount >= totalCount ? totalCount : currentCount;
    }

    public string GetTaskText()
    {
        return totalCount
            + " adet "
            + DataManager.Instance.GetItemName(itemId)
            + " "
            + TaskManager.Instance.actionTranslate[action]
            + " ("
            + currentCount
            + "/"
            + totalCount
            + ")";
    }
}
