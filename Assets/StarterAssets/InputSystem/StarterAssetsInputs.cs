using UnityEngine;
#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
using UnityEngine.InputSystem;
#endif

namespace StarterAssets
{
	public class StarterAssetsInputs : MonoBehaviour
	{
		[Header("Character Input Values")]
		public Vector2 move;
		public Vector2 look;
		public bool jump;
		public bool sprint;
		public bool shoot;
		public bool shootAcid;

		[Header("Movement Settings")]
		public bool analogMovement;

		[Header("Mouse Cursor Settings")]
		public bool cursorLocked = true;
		public bool cursorInputForLook = true;
		public AudioSource pasos;
		public Vector2 pos = new Vector2(0f, 0f);

#if ENABLE_INPUT_SYSTEM && STARTER_ASSETS_PACKAGES_CHECKED
		public void OnMove(InputValue value)
		{
			//Debug.Log(value.Get<Vector2>());
			MoveInput(value.Get<Vector2>());
			pasos.Play();
			if(value.Get<Vector2>() == pos)
            {
				pasos.Stop();
            }
		}

		public void OnLook(InputValue value)
		{
			if(cursorInputForLook)
			{
				LookInput(value.Get<Vector2>());
			}
		}

		public void OnJump(InputValue value)
		{
			JumpInput(value.isPressed);
		}

		public void OnSprint(InputValue value)
		{
			SprintInput(value.isPressed);
		}

		public void OnShoot(InputValue value)
		{
			ShootInput(value.isPressed);
		}
		public void OnShootAcid(InputValue value)
		{
			ShootAcidInput(value.isPressed);
		}
#endif


		public void MoveInput(Vector2 newMoveDirection)
		{
			move = newMoveDirection;
		} 

		public void LookInput(Vector2 newLookDirection)
		{
			look = newLookDirection;
		}

		public void JumpInput(bool newJumpState)
		{
			jump = newJumpState;
		}
		public void ShootInput(bool newShootState)
		{
			shoot = newShootState;
		}
		public void ShootAcidInput(bool newShootState)
		{
			shootAcid = newShootState;
		}

		public void SprintInput(bool newSprintState)
		{
			sprint = newSprintState;
		}
		
		private void OnApplicationFocus(bool hasFocus)
		{
			SetCursorState(cursorLocked);
		}

		private void SetCursorState(bool newState)
		{
			Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
		}
	}
	
}