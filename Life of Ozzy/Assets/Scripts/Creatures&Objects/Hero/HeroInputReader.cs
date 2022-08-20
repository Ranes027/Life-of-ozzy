using UnityEngine;
using UnityEngine.InputSystem;
using LifeOfOzzy.Model;
using LifeOfOzzy.Utils;
using LifeOfOzzy.UI;

namespace LifeOfOzzy
{
    public class HeroInputReader : MonoBehaviour
    {
        [SerializeField] private Hero _hero;
        
        private GameSession _session;

        public void OnMovement(InputAction.CallbackContext context)
        {
            var direction = context.ReadValue<Vector2>();
            _hero.SetDirection(direction);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Interact();

            }
        }

        public void OnAttack(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.Attack();
            }
        }

        public void OnThrow(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UseInventory();
            }
        }

        public void OnNextItem(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.NextItem();
            }
        }

        public void OnUsePotion(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UsePotion();
            }
        }

        public void OnOpenMenu(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                var menu = GameObject.Find("InGameMenu(Clone)");
                var settingsMenu = GameObject.Find("SettingsWindow(Clone)");
                var languageMenu = GameObject.Find("LanguageWindow(Clone)");
                if (menu == null)
                {
                    WindowUtils.CreateWindow("UI/InGameMenu");
                }
                else if (menu != null)
                {
                    var closeMenu = menu.GetComponent<InGameMenuWindow>();
                    if (settingsMenu != null)
                    {
                        var closeSettingsMenu = settingsMenu.GetComponent<AnimatedWindow>();
                        closeSettingsMenu.Close();
                        if (languageMenu != null)
                        {
                            var closeLanguageMenu = languageMenu.GetComponent<AnimatedWindow>();
                            closeLanguageMenu.Close();
                        }
                    }
                    closeMenu.Close();
                }
            }
        }        

        public void OnUseFlashlight(InputAction.CallbackContext context)
        {
            if (context.performed)
            {
                _hero.UseFlashlight();
            }
        }
    }
}

