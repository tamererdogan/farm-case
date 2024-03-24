using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    #region SINGLETON
    public static TaskManager Instance { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }
    #endregion SINGLETON

    private TaskBag currentTaskBag;

    private List<TaskItem> tutorialTasks;

    Dictionary<string, int[]> actionItems;

    public Dictionary<string, string> actionTranslate { get; private set; }

    void Start()
    {
        actionItems = new Dictionary<string, int[]>();
        actionItems["buy"] = new int[] { 0, 1, 2, 3, 4, 5 };
        actionItems["plant"] = new int[] { 3, 4, 5 };
        actionItems["harvest"] = new int[] { 6, 7, 8 };
        actionItems["sell"] = new int[] { 6, 7, 8 };

        actionTranslate = new Dictionary<string, string>();
        actionTranslate["buy"] = "satın al";
        actionTranslate["plant"] = "ek";
        actionTranslate["harvest"] = "hasat et";
        actionTranslate["sell"] = "sat";

        tutorialTasks = new List<TaskItem>
        {
            new TaskItem(1, 0, "buy"),
            new TaskItem(1, 4, "buy"),
            new TaskItem(1, 4, "plant"),
            new TaskItem(1, 4, "harvest"),
            new TaskItem(1, 7, "sell")
        };

        NextTask();
    }

    public void NextTask()
    {
        if (tutorialTasks.Count > 0)
        {
            currentTaskBag = new TaskBag(tutorialTasks[0]);
            tutorialTasks.RemoveAt(0);
        }
        else
        {
            currentTaskBag = GenerateRandTaskBag();
        }

        UpdateDisplay();
    }

    private TaskBag GenerateRandTaskBag()
    {
        List<TaskItem> generatedTaskItems = new List<TaskItem>();
        int level = LevelManager.Instance.GetLevel();
        string[] actions = actionItems.Keys.ToArray();

        int taskItemCount = Random.Range(1, level + 1);
        for (int i = 0; i < taskItemCount; i++)
        {
            string selectedAction = actions[Random.Range(0, 4)];
            int totalCount = Random.Range(1, level * 2);

            int[] filteredItems = DataManager.Instance.GetItemWithMaxLevelFilter(level);
            int[] availableItems = actionItems[selectedAction].Intersect(filteredItems).ToArray();

            generatedTaskItems.Add(
                new TaskItem(
                    totalCount,
                    availableItems[Random.Range(0, availableItems.Length)],
                    selectedAction
                )
            );
        }

        return new TaskBag(generatedTaskItems);
    }

    private void UpdateDisplay()
    {
        TaskItem[] taskItems = currentTaskBag.taskItems.ToArray();
        Debug.Log("---------");
        foreach (var taskItem in taskItems)
        {
            Debug.Log("Test Görev: " + taskItem.GetTaskText());
        }
        Debug.Log("---------");
    }
}
