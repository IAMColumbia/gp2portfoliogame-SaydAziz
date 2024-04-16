using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIObserver
{
    public void NotifyUI(int id, int content);
    public void NotifyUI(Trinket trinket);
}
