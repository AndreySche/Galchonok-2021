using UnityEngine;

namespace Galchonok
{
    public class Beethoven
    {
        AudioSource _source;
        AudioClip[] _sounds;

        public Beethoven(GameObject source, AudioClip[] sounds)
        {
            _source = source.GetComponent<AudioSource>();
            _sounds = sounds;
            _source.clip = _sounds[0]; // плохо, где-то один Роман подавился чаем
        }

        public void Click(bool correct)
        {
            if (correct) return;

            _source.Play();
            Handheld.Vibrate();
        }
    }
}