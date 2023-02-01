using System;
using System.Collections.Generic;
using UnityEngine;


namespace DefaultNamespace
{
    public class ScreenManager : Singleton<ScreenManager>
    {
        private Dictionary<string, ScreenCanvas> _screens = new Dictionary<string, ScreenCanvas>();
        private void Start()
        {
            
        }

        public void RegisterScreenCanvas(ScreenCanvas canvas)
        {
            _screens.Add(canvas.ID,canvas);
        }

        public void OpenScreen(string id)
        {
            if (_screens[id].gameObject.activeInHierarchy == false)
            {
                _screens[id].gameObject.SetActive(true);
            }

            _screens[id].ShowScreen();
        }

        public void CloseScreen(string id)
        {
            _screens[id].CloseScreen();
        }
    }
    
}