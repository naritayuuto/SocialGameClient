using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Outgame
{
    public class ButtonIsActive : UIStackableView
    {
        [SerializeField] GameObject button;
        void Start()
        {
            bool IsEvent = EventHelper.IsEventGamePlayable(1);
            button.SetActive(IsEvent);
            //�����ŊJ�Ò���������C�x���g�����e�L�X�g�ɕ\���A����������e�L�X�g�ɂ͂��̏����L�ځB
        }
    }
}