using UnityEngine;

namespace FilipBanaszak.Zadanie_08_11_2022
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private float _speed;

		private Rigidbody _rigidbody;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			var direction = Vector3.right * InputManager.Vertical + Vector3.back * InputManager.Horizontal;
			_rigidbody.angularVelocity = direction * (_speed * 4);
		}
	}
}
