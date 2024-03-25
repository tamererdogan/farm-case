using System.Collections.Generic;

public class TaskBag
{
    public List<TaskItem> taskItems { get; private set; }

    public TaskBag(TaskItem taskItem)
    {
        taskItems = new List<TaskItem> { taskItem };
    }

    public TaskBag(List<TaskItem> taskItems)
    {
        this.taskItems = taskItems;
    }

    public bool IsDone()
    {
        int doneTaskCount = 0;
        TaskItem[] taskItems = this.taskItems.ToArray();
        foreach (var taskItem in taskItems)
        {
            if (taskItem.IsDone())
                doneTaskCount++;
        }

        return taskItems.Length == doneTaskCount;
    }

    public int GetGainExp()
    {
        int gainExp = 0;
        TaskItem[] taskItems = this.taskItems.ToArray();
        foreach (var taskItem in taskItems)
            gainExp +=
                DataManager.Instance.GetItemExp(taskItem.itemId)
                * TaskManager.Instance.actionRates[taskItem.action]
                * taskItem.totalCount;

        return gainExp;
    }
}
