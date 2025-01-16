using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Events;
using UnityEngine.UI;

namespace DT.UI.Core
{
    public class DataButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler
    {
        public Action<int> OnSelected;

        public Image frame;
        public Image icon;
        public Sprite idleSprite, selectedSprite;

        public UnityEvent onTabSelected, onTabDeselected;
        public int id;

        protected virtual void OnDisable()
        {
            frame.sprite = idleSprite;

            OnSelected = null;
        }

        public virtual void OnPointerClick(PointerEventData eventData)
        {
            OnSelected?.Invoke(id);
            onTabSelected?.Invoke();
            frame.sprite = selectedSprite;
        }

        public virtual void OnPointerEnter(PointerEventData eventData)
        {
        }

        public virtual void OnPointerExit(PointerEventData eventData)
        {

        }

    }

}