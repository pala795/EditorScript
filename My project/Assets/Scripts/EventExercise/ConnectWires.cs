using UnityEngine;

public class ConnectWires : ATask
{
    void Start()
    {
        TaskId = 3;
        TaskName = "Collega i fili";
        Description = "Devi collegare i fili correttamente!";
    }
    public override void Execute()
    {
        Debug.Log(Description);
        // Aggiungere logica di gioco per colllegare i fili
    }
}
