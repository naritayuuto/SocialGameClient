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
            //ここで開催中だったらイベント名をテキストに表示、無かったらテキストにはその情報を記載。
        }
    }
}
