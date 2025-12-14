using UnityEngine;

public class ThrowBall : ATask
{
    private void Start()
    {
        TaskId = 1;
        TaskName = "Lancia una palla";
        Description = "Devi lanciare una palla in una cesta!";
    }

    public override void Execute()
    {
        Debug.Log(Description);
        // Aggiungere logica di gioco per lanciare una palla
    }
}
