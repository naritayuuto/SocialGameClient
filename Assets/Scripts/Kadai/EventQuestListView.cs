using MD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Outgame
{
    /// <summary>
    /// �N�G�X�g���X�g��\������r���[
    /// </summary>
    public class EventQuestListView : ListView
    {
        public delegate void Ready(int questId);
        [SerializeField] GameObject _chapterPrefab;
        [SerializeField] GameObject _questStackPrefab;
        [SerializeField] GameObject _questPrefab;

        public enum BoardType
        {
            Chapter,
            Quest
        }
        List<List<GameObject>> _childList = new List<List<GameObject>>();
        List<ListItemQuestBoard> _questList = new List<ListItemQuestBoard>();
        Ready _callback;
        int _selectedQuestId = -1;


        /// <summary>
        /// �r���[�����
        /// </summary>
        public override void Setup()
        {
            _lineList.ForEach(l => GameObject.Destroy(l));
            _itemList.Clear();
            _scrollPos = 0;

            var chapters = MasterData.Chapters;
            List<Chapter> eventChapters = new List<Chapter>(chapters);
            eventChapters.RemoveAll(e => e.QuestType.Equals(0));
            var questList = QuestListModel.QuestList.List;
            //�`���v�^�[�Ƃ��̎q���ɂȂ�N�G�X�g�����X�g�ɓ����
            for (int i = 0; i < eventChapters.Count; ++i)
            {
                var chapter = GameObject.Instantiate(_chapterPrefab, _content.RectTransform);
                var listItem = ListItemBase.ListItemSetup<ListItemChapterBoard>(i, chapter, (int evtId, int index) => OnItemClick(evtId, index));
                listItem.SetupChapterData(eventChapters[i]);
                _itemList.Add(listItem);
                _lineList.Add(listItem.gameObject);

                _childList.Add(new List<GameObject>());

                //�N�G�X�g�͔�\���ō��
                for (int q = 0; q < eventChapters[i].QuestList.Count; ++q)
                {
                    var quest = GameObject.Instantiate(_questPrefab, _content.RectTransform);
                    var listItem2 = ListItemBase.ListItemSetup<ListItemQuestBoard>(_questList.Count, quest, (int evtId, int index) => OnItemClick(evtId, index));
                    listItem2.SetupQuestData(eventChapters[i].QuestList[q].Id, questList.Where(qi => qi.QuestId == eventChapters[i].QuestList[q].Id).FirstOrDefault());
                    listItem2.gameObject.SetActive(false);

                    _questList.Add(listItem2);
                    _itemList.Add(listItem2);
                    _lineList.Add(listItem2.gameObject);

                    _childList[i].Add(listItem2.gameObject);
                }
            }

            //�T�C�Y�v�Z���čő�X�N���[���l�����߂�
            //�N�G�X�g�̓T�C�Y�ς���̂Ŗ���Čv�Z����
            _content.RectTransform.sizeDelta = new Vector2(800, (_lineList.Where(go => go.activeSelf).Count() + 1) * CardUIHeight);

            //�C�x���g�o�^
            _rect.onValueChanged.AddListener(ScrollUpdate);
        }



        public void SetReadyCallback(Ready cb)
        {
            _callback = cb;
        }

        protected override void OnItemClick(int evtId, int index)
        {
            switch ((BoardType)evtId)
            {
                //�`���v�^�[���������ꍇ�̓N�G�X�g���o��
                case BoardType.Chapter:
                    {
                        //��\���ؑ�
                        _childList[index].ForEach(c => c.SetActive(!c.activeSelf));

                        //�T�C�Y�v�Z���čő�X�N���[���l�����߂�
                        //�N�G�X�g�̓T�C�Y�ς���̂Ŗ���Čv�Z����
                        _content.RectTransform.sizeDelta = new Vector2(800, (_lineList.Where(go => go.activeSelf).Count() + 1) * CardUIHeight);
                    }
                    break;

                //�o���m�F
                case BoardType.Quest:
                    {
                        _selectedQuestId = _questList[index].QuestId;
                        UICommonDialog.OpenYesNoDialog("�o�����܂�", "�悩������OK", DialogDecide, "UIGoQuest", "UINoQuest");
                    }
                    break;
            }
        }

        void DialogDecide(int type)
        {
            if (type == 1)
            {
                _callback?.Invoke(_selectedQuestId);
            }
        }
    }
}