using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoChangeManager : MonoBehaviour
{
    public AlertTime alertAmmo;
    public GameObject _ammoPanel;
    private List<GameObject> Ammos = new List<GameObject>();
    void Start()
    {
        EventManager.OnFinishAmmo += AlertShowText;
        for (int i = 0; i < _ammoPanel.transform.childCount; i++)
        {
            GameObject _bullet = _ammoPanel.transform.GetChild(i).GetChild(0).gameObject;
            Ammos.Add(_bullet);
            _bullet.SetActive(true);
        }
        
    }
    public List<GameObject> getAmmos()
    {
        return Ammos;
    }
    private void AlertShowText()
    {
        alertAmmo.ShowText();
    }
    private void OnDisable()
    {
        EventManager.OnFinishAmmo -= AlertShowText;
    }
}
