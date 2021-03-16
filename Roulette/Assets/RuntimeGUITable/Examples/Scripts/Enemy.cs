using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType { Normal, Boss }

[System.Serializable]
public class Enemy
{
	public string name;

	public Sprite icon;

	public int hp;

	public float power;

	public bool canSwim;

	public EnemyType type;

	public void Method()
	{
		Debug.Log("Method 2 called on " + name);
	}
    public void Swim()
    {
        Debug.Log("Swim called on " + name);
    }
    public void Attack()
    {
        Debug.Log("Attack called on " + name);
    }


}

