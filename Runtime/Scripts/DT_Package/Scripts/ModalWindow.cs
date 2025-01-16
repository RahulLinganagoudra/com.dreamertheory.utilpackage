using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[Serializable]
public class ButtonInfo
{
    public string title;
    public Color color = Color.white;
    public Action onClick;
}
namespace DT.UI.Core
{
    public class ModalWindow : MonoBehaviour
    {
        #region fields
        [Header("Header")]
        [SerializeField]
        private Transform headerArea;
        [SerializeField]
        private TextMeshProUGUI titleField;


        [Header("Content")]
        [SerializeField]
        private Transform contentArea;
        [SerializeField]
        private Transform verticalLayoutArea;
        [SerializeField]
        private Image heroImage;
        [SerializeField]
        private TextMeshProUGUI heroText;

        [Space]
        [SerializeField]
        private Transform horizontalLayoutArea;
        [SerializeField]
        private Transform iconContainer;
        [SerializeField]
        private Image iconImage;
        [SerializeField]
        private TextMeshProUGUI iconText;


        [Header("Footer")]
        [SerializeField]
        private Transform footerContainer;
        [SerializeField]
        private Button confirmButton;
        [SerializeField]
        private TextMeshProUGUI confirmButtonText;
        [SerializeField]
        private Button cancelButton;
        [SerializeField]
        private TextMeshProUGUI cancelButtonText;
        [SerializeField]
        private Button optionalButton;
        [SerializeField]
        private TextMeshProUGUI optionalButtonText;
        #endregion

        Action OnConfirm, OnCancel, OnOptional;

        public void Confirm() { OnConfirm?.Invoke(); }
        public void Cancel() { OnCancel?.Invoke(); }
        public void Optional() { OnOptional?.Invoke(); }

        public void ShowAsHero(string title, Sprite imageToShow, string message,
            ButtonInfo confirmInfo, ButtonInfo cancelInfo = null, ButtonInfo optionalInfo = null)
        {


            bool hasTitle = string.IsNullOrEmpty(title);
            headerArea.gameObject.SetActive(hasTitle);
            titleField.text = title;

            heroImage.gameObject.SetActive(imageToShow != null);
            if (imageToShow != null)
            {
                heroImage.sprite = imageToShow;
            }
            heroText.gameObject.SetActive(true);
            heroText.text = message;

            if (SetUpButton(confirmButton, confirmButtonText, confirmInfo))
                OnConfirm = confirmInfo.onClick;
            if (SetUpButton(optionalButton, optionalButtonText, optionalInfo))
                OnOptional = optionalInfo.onClick;
            if (SetUpButton(cancelButton, cancelButtonText, cancelInfo))
                OnCancel = cancelInfo.onClick;




            horizontalLayoutArea.gameObject.SetActive(false);
            verticalLayoutArea.gameObject.SetActive(true);


            Show();
        }

        public void ShowAsDialogue(string title, string message, ButtonInfo confirmInfo = null, ButtonInfo cancelInfo = null, ButtonInfo optionalInfo = null)
        {
            bool hasntTitle = string.IsNullOrEmpty(title);
            titleField.text = title;
            titleField.gameObject.SetActive(!hasntTitle);


            iconImage.gameObject.SetActive(false);
            iconText.text = message;
            iconText.gameObject.SetActive(true);

            if (SetUpButton(confirmButton, confirmButtonText, confirmInfo))
                OnConfirm = confirmInfo.onClick;
            if (SetUpButton(optionalButton, optionalButtonText, optionalInfo))
                OnOptional = optionalInfo.onClick;

            if (SetUpButton(cancelButton, cancelButtonText, cancelInfo))
                OnCancel = cancelInfo.onClick;

            horizontalLayoutArea.gameObject.SetActive(true);
            verticalLayoutArea.gameObject.SetActive(false);
            Show();
        }
        public void Hide()
        {
            horizontalLayoutArea.gameObject.SetActive(false);
            verticalLayoutArea.gameObject.SetActive(false);
            heroText.gameObject.SetActive(false);
            iconText.gameObject.SetActive(false);

            gameObject.SetActive(false);

            Time.timeScale = 1f;
        }
        void Show()
        {
            gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        bool SetUpButton(Button btn, TextMeshProUGUI btnText, ButtonInfo info)
        {
            bool hasConfirmAction = info != null;
            btn.gameObject.SetActive(hasConfirmAction);
            if (info != null)
                btn.image.color = info.color;
            btnText.text = hasConfirmAction ? info.title : "";
            return hasConfirmAction;
        }


    }
}