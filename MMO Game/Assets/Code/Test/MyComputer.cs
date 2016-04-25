using UnityEngine;
using System.Collections;
using UnityEngine.Events;

public class MyComputer : MonoBehaviour 
{   
  
    private float mValueField_1;
    private float mValueField_2;
    // 声明事件
    [SerializeField]
    private PassString OnAdd;
    [SerializeField]
    private PassString OnSubstract;
    [SerializeField]
    private PassString OnMutiply;
    [SerializeField]
    private PassString OnDivide;

    [SerializeField]
    private UnityEvent OnResetStatus;

    public string Value1
    {
        set
        {
            float.TryParse(value,out mValueField_1);
        }
    }
    public string Value2
    {
        set
        {
            float.TryParse(value, out mValueField_2);
        }
    }
    public void Add()
    {
        this.OnAdd.Invoke((this.mValueField_1 + this.mValueField_2).ToString());
    }
    public void Substract()
    {
        this.OnSubstract.Invoke((this.mValueField_1 - this.mValueField_2).ToString());
    }
    public void Mutiply()
    {
        this.OnMutiply.Invoke((this.mValueField_1 * this.mValueField_2).ToString());
    }
    public void Divide()
    {
        if (this.mValueField_2 == 0) return;
        this.OnDivide.Invoke((this.mValueField_1 / this.mValueField_2).ToString());
    }
    public void ResetStatus()
    {
        this.OnResetStatus.Invoke();
    }

}
