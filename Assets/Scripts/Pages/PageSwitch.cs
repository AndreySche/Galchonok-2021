using UnityEngine;
using UnityEngine.UI;
using Tools;

namespace Galchonok
{
    class PageSwitch
    {
        private Curtain _curtain;
        private Transform _area;
        private Pages _page;

        public PageSwitch(Image curtain, Transform area)
        {
            _curtain = new Curtain(curtain);
            _area = area;
        }

        public void LoadPage(Pages page)
        {
            _page = page;
            _curtain.Show(Load);
        }

        private void Load()
        {
            _area.Destroy();
            switch (_page)
            {
                case Pages.GameA:
                    GameOne gameA = EasyLoad<GameOne>("GameС");
                    gameA.Init(BackToMenu);
                    break;
                case Pages.GameB:
                    GameOne gameB = EasyLoad<GameOne>("GameB");
                    gameB.Init(BackToMenu);
                    break;
                case Pages.Logo:
                    Logo logo = EasyLoad<Logo>("Logo");
                    logo.Init(BackToMenu);
                    break;
                case Pages.Menu:
                    Menu menu = EasyLoad<Menu>("Menu");
                    menu.Init(this);
                    break;
                case Pages.Warning:
                    Warning warning = EasyLoad<Warning>("Warning");
                    warning.Init(BackToMenu);
                    break;
                default:
                    Error error = EasyLoad<Error>("Error");
                    error.Init(BackToMenu);
                    break;
                    
            }
            _curtain.Hide();
        }
        
        private T EasyLoad<T>(string file) where T : Component
        {
            return ResourceLoader.LoadAndInstantiateObject<T>(new ResourcePath {PathResource = "Pages/" + file}, _area, false); 
        } 
        
        private void BackToMenu() => LoadPage(Pages.Menu);
    }
}