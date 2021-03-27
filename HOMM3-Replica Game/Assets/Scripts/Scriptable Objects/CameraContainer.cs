using UnityEngine;

namespace Scriptable_Objects
{
	[CreateAssetMenu(fileName = "Camera Container", menuName = "Camera Container", order = 0)]
	public class CameraContainer : ScriptableObject
	{
		public Camera Camera;

		public void Clear()
		{
			Camera = null;
		}
	}
}
