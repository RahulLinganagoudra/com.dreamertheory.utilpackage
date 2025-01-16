using UnityEngine;


namespace DT.UI.Core
{
	public abstract class Menu : MonoBehaviour
	{
		public bool opened => gameObject.activeInHierarchy;


		public virtual void Open()
		{
			if (opened) { return; }

			//PanelManager.instance.RequestFocus(this);
			Time.timeScale = 0;
			if (!opened)
			{
				gameObject.SetActive(true);

				//SimpleInput.instance.BlockPlayerInput();
			}
		}
		public virtual void OpenOnTop()
		{
			if (opened) return;
			//PanelManager.instance.RequestFocus(this, openOnTop: true);
			Time.timeScale = 0;
			gameObject.SetActive(true);
		}
		public virtual void Close()
		{
			Time.timeScale = 1;
			//if (SimpleInput.instance != null)
			//SimpleInput.instance.BlockPlayerInput(false);
			gameObject.SetActive(false);
		}

	}


}