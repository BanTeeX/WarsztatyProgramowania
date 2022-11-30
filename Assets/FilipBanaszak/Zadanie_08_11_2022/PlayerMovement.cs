using UnityEngine;

namespace FilipBanaszak.Zadanie_08_11_2022
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerMovement : MonoBehaviour
	{
		[SerializeField] private float speed = 2;
		[SerializeField] private float accelerationTime = 1;
		[SerializeField] private float slowingTime = 0.5f;

		private Rigidbody _rigidbody;
		private float _acceleration;
		private float _slowing;

		private void Awake()
		{
			_rigidbody = GetComponent<Rigidbody>();
			_rigidbody.drag = 0;
			_acceleration = speed / accelerationTime;
			_slowing = speed / slowingTime;
		}

		private void FixedUpdate()
		{
			var direction = new Vector3(InputManager.Horizontal, 0, InputManager.Vertical);
			direction = Quaternion.FromToRotation(Vector3.down, PlayerGravity.LastDirection) * direction;

			if (direction.sqrMagnitude == 0)
			{
				_rigidbody.AddForce(_rigidbody.velocity.normalized * -_slowing, ForceMode.Acceleration);
				return;
			}
			
			if (direction.sqrMagnitude != 0)
			{
				_rigidbody.AddForce(direction.normalized * _acceleration, ForceMode.Acceleration);
			}
		}
	}
}
