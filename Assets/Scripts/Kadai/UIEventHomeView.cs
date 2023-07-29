using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace Outgame
{
    public class UIEventHomeView : UIStackableView
    {
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] Button button;
        protected override void AwakeCall()
        {
            ViewId = ViewID.EventHome;
            _hasPopUI = false;

            EventButtonIsActive();
        }
        void EventButtonIsActive()
        {
            List<int> eventList = EventHelper.GetAllOpenedEvent();
            for (int i = 0; i < eventList.Count; i++)
            {
                if (EventHelper.IsEventGamePlayable(eventList[i]) == true)
                {
                    button.interactable = true;
                    break;
                }
            }
        }

        public void GoHome()
        {
            UIManager.NextView(ViewID.Home);
        }
        public void GoRanking()
        {
            //UIManager.NextView(ViewID.Ranking);
        }

        public void EventQuest()
        {
            UIManager.NextView(ViewID.EventQuest);
        }
    }
}

