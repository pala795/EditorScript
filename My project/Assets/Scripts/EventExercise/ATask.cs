using System;
using UnityEngine;

public abstract class ATask : MonoBehaviour
{
    public int TaskId;
    public string TaskName;
    public string Description;
    public bool IsStarted { get; private set; }
    public bool IsCompleted { get; private set; }

    // Eventi
    public event Action OnTaskStarted;
    public event Action OnTaskCompleted;

    // Metodo per avviare il task
    public void StartTask()
    {
        if (!IsStarted && !IsCompleted)
        {
            IsStarted = true;
            OnTaskStarted?.Invoke();
            Debug.Log($"{TaskName} è iniziato!");
        }
    }

    // Metodo per il completamento delle task
    public void CompleteTask()
    {
        if (IsStarted && !IsCompleted)
        {
            IsCompleted = true;
            OnTaskCompleted?.Invoke();
            Debug.Log($"{TaskName} è stato completato!");
        }
    }

    public abstract void Execute();
}


