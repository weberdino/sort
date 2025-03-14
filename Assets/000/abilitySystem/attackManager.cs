using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class attackManager : MonoBehaviour
{
    #region Singleton

    public bool chargeable;
    public int multihit = 1;
    public Image img;

    public static attackManager instance;

    void Awake()
    {
        instance = this;
    }

    #endregion

    public List<GameObject> attackPrefabs;
    public List<GameObject> throwPrefabs;
    public Transform parent;
    GameObject last;
    int currentIndex;

    bool _throwable;
    public bool Throwable
    {       
        get { return _throwable; }
        set
        {
            Debug.Log("throw");
            this._throwable = !this._throwable;
            if (_throwable)
            {
                SetAttack(currentIndex);
            }
        }
    }

    public void SetDefaultAttack(int index)
    {
        currentIndex = index;
        if (last != null)
        {
            Destroy(last);
        }
        last = Instantiate(attackPrefabs[index], parent);
    }

    public void SetAttack(int index)
    {
        Throwable = !Throwable;
        if (Throwable)
        {
            SetAttackThrow(index);
        }
        else
        {
            SetDefaultAttack(index);
        }
    }

    public void SetAttackThrow(int index)
    {
        currentIndex = index;
        if (last != null)
        {
            Destroy(last);
        }
        last = Instantiate(throwPrefabs[index], parent);
    }
}
