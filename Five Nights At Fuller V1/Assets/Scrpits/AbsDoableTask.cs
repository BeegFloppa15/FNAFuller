using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbsDoableTask : MonoBehaviour, IDoableTask
{
    public bool finished;

    public abstract void taskComplete();
}
