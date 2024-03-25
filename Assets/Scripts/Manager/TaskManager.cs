using System.Collections.Generic;
using System.Linq;
using TMPro;
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

    [SerializeField]
    private GameObject taskListUI;

    [SerializeField]
    private GameObject taskTextPrefab;

    [SerializeField]
    private TMP_Text gainText;

    private List<GameObject> taskObjects = new List<GameObject>();

    private TaskBag currentTaskBag;

    private List<TaskItem> tutorialTasks;

    Dictionary<string, int[]> actionItems;

    public Dictionary<string, int> actionRates { get; private set; }

    public Dictionary<string, string> actionTranslate { get; private set; }

    void Start()
    {
        actionItems = new Dictionary<string, int[]>();
        actionItems["buy"] = new int[] { 0, 1, 2, 3, 4, 5 };
        actionItems["plant"] = new int[] { 3, 4, 5 };
        actionItems["harvest"] = new int[] { 6, 7, 8 };
        actionItems["sell"] = new int[] { 6, 7, 8 };

        actionRates = new Dictionary<string, int>();
        actionRates["buy"] = 1;
        actionRates["plant"] = 2;
        actionRates["harvest"] = 3;
        actionRates["sell"] = 1;

        actionTranslate = new Dictionary<string, string>();
        actionTranslate["buy"] = "satın al";
        actionTranslate["plant"] = "ek";
        actionTranslate["harvest"] = "hasat et";
        actionTranslate["sell"] = "sat";

        tutorialTasks = new List<TaskItem>
        {
            new TaskItem(1, 0, "buy"),
            new TaskItem(1, 3, "buy"),
            new TaskItem(1, 3, "plant"),
            new TaskItem(1, 6, "harvest"),
            new TaskItem(1, 6, "sell")
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
            string selectedAction = actions[Random.Range(0, actions.Length)];
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
        foreach (var taskObject in taskObjects)
            Destroy(taskObject);

        TaskItem[] taskItems = currentTaskBag.taskItems.ToArray();
        foreach (var taskItem in taskItems)
        {
            GameObject taskObject = Instantiate(taskTextPrefab, taskListUI.transform);
            TMP_Text textObject = taskObject.GetComponent<TMP_Text>();
            textObject.text = taskItem.GetTaskText();
            textObject.color = taskItem.IsDone() ? Color.green : Color.white;
            taskObjects.Add(taskObject);
        }

        gainText.text = "Exp: " + currentTaskBag.GetGainExp();
    }

    public void CheckTask(string action, int itemId)
    {
        foreach (var taskItem in currentTaskBag.taskItems)
        {
            if (taskItem.action == action && taskItem.itemId == itemId)
                taskItem.IncreaseCount();
        }

        UpdateDisplay();

        if (!currentTaskBag.IsDone())
            return;

        LevelManager.Instance.AddExp(currentTaskBag.GetGainExp());

        NextTask();
    }
}
