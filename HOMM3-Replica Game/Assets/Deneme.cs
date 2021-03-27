using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deneme : MonoBehaviour
{
	struct MyStruct
	{
		public float x;
		public MyStruct(float x) {
			this.x = x;
		}
	}
	int x = 5;

	int IntChanger(int y)
	{
		y = 10;
		return y;
	}
	
	MyStruct _myStruct = new MyStruct(5f);

	MyStruct ChangeStruct(MyStruct structt)
	{
		structt.x = 10f;
		return structt;
	}
	
	void Start()
	{
		Debug.Log(IntChanger(x));
		Debug.Log(x);
	}
}