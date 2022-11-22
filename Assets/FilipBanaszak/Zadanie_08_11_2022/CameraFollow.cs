using UnityEngine;

namespace FilipBanaszak.Zadanie_08_11_2022
{
	public class CameraFollow : MonoBehaviour
	{
		[SerializeField] private Transform _target;
		[SerializeField] private Vector3 _offset;

		private void Update()
		{
			transform.position = _target.position + _offset;
		}
	}
}