using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public string Name,Description;
    public int damage,points;
    public int speed;
    public Transform spawnPosition;
    public List<GameObject> listadisegni;

    public void SetStats(string _name, int _damage, int _speed, List<GameObject> _listadisegni)
    {
        name = _name;
        damage = _damage;
        speed = _speed;
        listadisegni = _listadisegni;
    }


}
