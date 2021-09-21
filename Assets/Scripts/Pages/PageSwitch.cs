using UnityEngine;
using UnityEngine.UI;
using Tools;
using TypeOne;

namespace Pages
{
    class PageSwitch
    {
        private Curtain _curtain;
        private Transform _area;
        private Beethoven _beethoven;
        private Page _pageEnum;

        public PageSwitch(Image curtain, Transform area, Beethoven beethoven)
        {
            _curtain = new Curtain(curtain);
            _area = area;
            _beethoven = beethoven;
        }

        public void LoadPage(Page pageEnum)
        {
            _pageEnum = pageEnum;
            _curtain.Show(Load);
        }

        private void Load()
        {
            _area.Destroy();
            switch (_pageEnum)
            {
                case Page.GameA:
                    GameOne gameA = EasyLoad<GameOne>("GameOne");
                    gameA.Init(BackToMenu, _beethoven);
                    break;
                case Page.GameB:
                    GameOne gameB = EasyLoad<GameOne>("GameOne");
                    gameB.Init(BackToMenu, _beethoven, 1);
                    break;
                case Page.Logo:
                    Logo logo = EasyLoad<Logo>("Logo");
                    logo.Init(BackToMenu, _beethoven);
                    break;
                case Page.Menu:
                    Menu menu = EasyLoad<Menu>("Menu");
                    menu.Init(this, _beethoven);
                    break;
                case Page.Warning:
                    Warning warning = EasyLoad<Warning>("Warning");
                    warning.Init(BackToMenu, _beethoven);
                    break;
                case Page.Settings:
                    Settings settings = EasyLoad<Settings>("Settings");
                    settings.Init(BackToMenu, _beethoven);
                    break;
                default:
                    Error error = EasyLoad<Error>("Error");
                    error.Init(BackToMenu, _beethoven);
                    break;
                    
            }
            _curtain.Hide();
        }
        
        private T EasyLoad<T>(string file) where T : Component
        {
            return ResourceLoader.LoadAndInstantiateObject<T>(new ResourcePath {PathResource = "Pages/" + file}, _area, false); 
        } 
        
        private void BackToMenu() => LoadPage(Page.Menu);
    }
}