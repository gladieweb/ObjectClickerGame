using System;
using System.Collections.Generic;
using MoreMountains.Feedbacks;
using UnityEngine;
using UnityEngine.Events;

namespace DefaultNamespace
{
    public class ScreenCanvas : MonoBehaviour
    {
        public string ID;
        public UnityEvent OnCloseEvent;
        public UnityEvent OnShownEvent;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            Debug.Log("INITIALIZE CANVASSCREEN "+ ID);
            ScreenManager.Instance.RegisterScreenCanvas(this);
            this.gameObject.SetActive(false);
        }
        

        public virtual void ShowScreen()
        {
            OnShownEvent.Invoke();
        }

        public virtual void CloseScreen()
        {
            OnCloseEvent.Invoke();
        }
    }
}