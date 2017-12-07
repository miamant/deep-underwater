using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

/// <summary>
/// Connects the input from touchscreen with the right joystick.
/// </summary>
public class RightJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
	public Vector2 inputVec;

	Image bgImg;
	Image joystickImg;
	public bool isDragged = false;

	void Start ()
	{
		bgImg = GetComponent<Image> ();
		joystickImg = transform.GetChild (0).GetComponent<Image> ();
	}

	/// <summary>
	/// When joystick is dragged its sprite will move accordingly and save its Vector2 position.
	/// </summary>
	/// <param name="ped">Ped.</param>
	public virtual void OnDrag (PointerEventData ped)
	{
		Vector2 pos;

		if (RectTransformUtility.ScreenPointToLocalPointInRectangle (bgImg.rectTransform
			, ped.position, ped.pressEventCamera, out pos)) {

			pos.x = (pos.x / bgImg.rectTransform.sizeDelta.x);
			pos.y = (pos.y / bgImg.rectTransform.sizeDelta.y);

			inputVec = new Vector2 (pos.x * 2, pos.y * 2);
			inputVec = (inputVec.magnitude > 1.0f) ? inputVec.normalized : inputVec;

			// Move Joystick IMG
			joystickImg.rectTransform.anchoredPosition = 
				new Vector2 (inputVec.x * (bgImg.rectTransform.sizeDelta.x / 3.5f)
					, inputVec.y * (bgImg.rectTransform.sizeDelta.y / 3.5f));
		}
	}

	/// <summary>
	/// When joystick is pressed on OnDrag will be called.
	/// </summary>
	/// <param name="ped">Ped.</param>
	public virtual void OnPointerDown (PointerEventData ped)
	{
		isDragged = true;

		OnDrag (ped);
	}

	/// <summary>
	/// When joystick is not pressed the sprite regains its zero position and so will the Vector2 for input.
	/// </summary>
	/// <param name="ped">Ped.</param>
	public virtual void OnPointerUp (PointerEventData ped)
	{
		isDragged = false;

		inputVec = Vector2.zero;
		joystickImg.rectTransform.anchoredPosition = Vector2.zero;
	}
}
