using System;
using UnityEngine;

namespace Scriptable_Objects
{
	public class CameraContainerAdder : MonoBehaviour
	{
		[SerializeField] CameraContainer _camContainer;

		void OnEnable()
		{
			_camContainer.Camera = GetComponent<Camera>();
		}
		
		void OnDisable()
		{
			_camContainer.Clear();
		}
	}
}
