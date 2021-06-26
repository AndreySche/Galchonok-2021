﻿using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Galchonok
{
    public class ControllerAnswers
    {
        private GameObject _area, _prefabGreen;
        private BookRound _bookRound;
        private UnityAction<bool> _callback;
        private LibraryOne _library;
        private Dictionary<int, List<AnswerRound>> _clickHistory;

        public ControllerAnswers(GameAsettings settings, GameObject area, LibraryOne library, UnityAction<bool> callback)
        {
            _area = area;
            _library = library;
            _callback = callback;
            _prefabGreen = settings.Prefabs[1]; // green
            _clickHistory = new Dictionary<int, List<AnswerRound>>();
        }

        public void Set(BookRound round, int step)
        {
            _area.transform.Destroy();
            foreach (var answer in round.Answers)
            {
                bool correct = (round.Book == answer.Book && round.Chapter == answer.Chapter);
                string title = _library.Book[answer.Book][answer.Chapter].Answers[answer.Answer];
                rgb color = GetPrefabColor(step, correct, answer);
                _area.transform.Attach(title, _prefabGreen.SetNewColor(color))
                    .GetOrAddComponent<Button>()
                    .onClick.AddListener(() => Click(step, correct, answer));
            }
        }

        private rgb GetPrefabColor(int step, bool correct, AnswerRound answer)
        {
            if (!_clickHistory.ContainsKey(step)) _clickHistory[step] = new List<AnswerRound>();
            if (Verify(step, answer)) return correct ? rgb.LightGreen : rgb.LightYellow;

            return correct ? rgb.LightGreen : rgb.White;
            //return rgb.White;
        }

        private void Click(int step, bool correct, AnswerRound answer)
        {
            if (!Verify(step, answer)) _clickHistory[step].Add(answer);
            _callback(correct);
        }

        private bool Verify(int step, AnswerRound answer)
        {
            foreach (var child in _clickHistory[step])
            {
                if (answer.Book == child.Book) return true;
            }

            return false;
        }
    }
}