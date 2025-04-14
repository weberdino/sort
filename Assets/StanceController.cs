using UnityEngine;

public class StanceController : MonoBehaviour
{
    #region Singletion
    public static StanceController instance;
    private IStance currentStance;
    private void Awake()
    {
        instance = this;
    }
    #endregion Singleton


    public void SetStance(IStance newStance)
    {
        if (currentStance != null)
        {
            currentStance.Exit(); 
        }

        currentStance = newStance;
        currentStance.Enter();
    }
}
