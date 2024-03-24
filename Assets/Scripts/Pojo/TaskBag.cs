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
}
