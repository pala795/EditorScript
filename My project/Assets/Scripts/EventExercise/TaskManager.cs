using System.Collections.Generic;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    private List<ATask> taskList = new List<ATask>();
    private int currentTaskIndex = 0;

    private void Start()
    {
        taskList.AddRange(
            FindObjectsByType<ATask>(FindObjectsSortMode.None)
        );

        if (taskList.Count > 0)
        {
            StartTask();
        }
    }

    // Inizializza il task corrente
    public void StartTask()
    {
        if (currentTaskIndex < taskList.Count)
        {
            ATask currentTask = taskList[currentTaskIndex];
            currentTask.StartTask();
            currentTask.OnTaskCompleted += OnTaskCompleted;
            currentTask.Execute();
        }
    }

    // Evento che viene chiamato quando un task è completato
    private void OnTaskCompleted()
    {
        ATask currentTask = taskList[currentTaskIndex];
        Debug.Log($"Task '{currentTask.TaskName}' completato!");

        currentTaskIndex++;

        // Se ci sono task successivi, avvia il prossimo
        if (currentTaskIndex < taskList.Count)
        {
            StartTask();
        }
        else
        {
            Debug.Log("Tutti i task sono stati completati!");
        }
    }
}
