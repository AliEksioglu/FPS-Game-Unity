using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;

public static class EventManager
{
    public static event Action OnDamage;
    public static void Event_OnDamage() { OnDamage?.Invoke(); }

    public static event Action OnFirePistol;
    public static void Event_OnFirePistol() { OnFirePistol?.Invoke(); }

    public static event Action OnHpChanged;
    public static void Event_OnHpChanged() { OnHpChanged?.Invoke(); }

    public static event Action OnCurBulletChange;
    public static void Event_OnCurBulletChange() { OnCurBulletChange?.Invoke(); }

    public static event Action OnFinishAmmo;
    public static void Event_OnFinishAmmo() { OnFinishAmmo?.Invoke(); }

    public static event Action OnDestroyDrone;
    public static void Event_OnDestroyDrone() { OnDestroyDrone?.Invoke(); }

}