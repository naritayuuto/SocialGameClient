using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Outgame
{
    public class UIEventHomeView : UIStackableView
    {
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] GameObject button;
        protected override void AwakeCall()
        {
            ViewId = ViewID.EventHome;
            _hasPopUI = false;

            CreateView();
        }
        void CreateView()
        {
            bool IsEvent = EventHelper.IsEventGamePlayable(1);
            button.SetActive(IsEvent);
                //�����ŊJ�Ò���������C�x���g�����e�L�X�g�ɕ\���A����������e�L�X�g�ɂ͂��̏����L�ځB
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
