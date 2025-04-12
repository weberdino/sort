using UnityEngine;

public class StanceController : MonoBehaviour
{
    public static StanceController instance; // Singleton-Instanz des StanceControllers

    private IStance currentStance; // Die aktuell aktive Stance

    private void Awake()
    {
        instance = this;
    }

    public void SetStance(IStance newStance)
    {
        if (currentStance != null)
        {
            currentStance.Exit(); // Die alte Stance verlassen
        }

        currentStance = newStance; // Die neue Stance setzen
        currentStance.Enter(); // Die neue Stance aktivieren
    }
}
