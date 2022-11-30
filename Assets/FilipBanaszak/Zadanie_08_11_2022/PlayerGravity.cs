using UnityEngine;
using Random = UnityEngine.Random;

//script by Paweł Łukaszewski
namespace FilipBanaszak.Zadanie_08_11_2022
{
	[RequireComponent(typeof(Rigidbody))]
	public class PlayerGravity : MonoBehaviour
	{
		[SerializeField] private int samples = 100;
		[SerializeField] private float gravityForce = 10f;
		[SerializeField] private float maxRange = 10f;

		public static Vector3 LastDirection { get; private set; } = Vector3.down;
		private Rigidbody _rigid;
		private readonly RaycastHit[] _hits = new RaycastHit[1];

		private void Awake()
		{
			_rigid = GetComponent<Rigidbody>();
		}

		private void FixedUpdate()
		{
			var shortestDirection = Vector3.zero;
			var shortestDistance = maxRange;
			var didHit = false;
		
			for (var i = 0; i < samples; i++)
			{
				var tmpDirection = Random.insideUnitSphere;
				if (Physics.RaycastNonAlloc(transform.position, tmpDirection, _hits, maxRange) == 0)
					continue;
				
				didHit = true;
				
				var tmpDistance = Vector3.Distance(_hits[0].point, transform.position);
				if (tmpDistance >= shortestDistance)
					continue;
				
				shortestDirection = tmpDirection;
				shortestDistance = tmpDistance;
			}
		
			if (didHit && _rigid.SweepTest(shortestDirection, out var hit))
			{
				LastDirection = hit.point - transform.position;
				LastDirection.Normalize();
			}
			
			_rigid.AddForce(LastDirection * gravityForce);
		}
	}
}