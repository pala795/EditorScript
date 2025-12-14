using UnityEngine;

public class CollectItem : ATask
{
    public void Start()
    {
        TaskId = 2;
        TaskName = "Raccogli un oggetto";
        Description = "Devi raccogliere un oggetto sparso nell'ambiente!";
    }

    public override void Execute()
    {
        Debug.Log(Description);
        // Aggiungere logica di gioco per raccogliere oggetti
    }
}
