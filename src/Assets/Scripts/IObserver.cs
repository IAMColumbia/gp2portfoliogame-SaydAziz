using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver 
{
    public void OnNotify(int id);
    public void OnNotify(PlayerState state);
}
