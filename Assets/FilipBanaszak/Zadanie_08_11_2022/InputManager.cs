using UnityEngine;

namespace FilipBanaszak.Zadanie_08_11_2022
{
	public class InputManager : MonoBehaviour
	{
		public static float Horizontal;
		public static float Vertical;
		
		[SerializeField] private string _horizontalAxis;
		[SerializeField] private string _verticalAxis;
		
		private void Update()
		{
			Horizontal = Input.GetAxisRaw(_horizontalAxis);
			Vertical = Input.GetAxisRaw(_verticalAxis);
		}
	}
}