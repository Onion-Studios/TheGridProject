using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string Name;
    public int damage;
    public int speed;
    public List<GameObject> listadisegni;

    public void SetStats(string _name, int _damage, int _speed, List<GameObject> _listadisegni)
    {
        name = _name;
        damage = _damage;
        speed = _speed;
        listadisegni = _listadisegni;
    }


}
