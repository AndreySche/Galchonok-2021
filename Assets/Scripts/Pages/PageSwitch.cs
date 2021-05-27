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
                    GameA gameA = EaseLod<GameA>("GameA");
                    gameA.Init(BackToMenu);
                    break;
                case Pages.GameB:
                    GameA gameB = EaseLod<GameA>("GameB");
                    gameB.Init(BackToMenu);
                    break;
                case Pages.Logo:
                    Logo logo = EaseLod<Logo>("Logo");
                    logo.Init(BackToMenu);
                    break;
                case Pages.Menu:
                    Menu menu = EaseLod<Menu>("Menu");
                    menu.Init(this);
                    break;
                case Pages.Warning:
                    Warning warning = EaseLod<Warning>("Warning");
                    warning.Init(BackToMenu);
                    break;
                default:
                    Error error = EaseLod<Error>("Error");
                    error.Init(BackToMenu);
                    break;
                    
            }
            _curtain.Hide();
        }
        
        private T EaseLod<T>(string file) where T : Component
        {
            return ResourceLoader.LoadAndInstantiateObject<T>(new ResourcePath {PathResource = "Pages/" + file}, _area, false); 
        } 
        
        private void BackToMenu() => LoadPage(Pages.Menu);
    }
}