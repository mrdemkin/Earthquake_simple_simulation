using UnityEngine;
using UnityEngine.UI;

namespace SimpleCGames.GDebug.Cheats
{
    public class CheatPanel : MonoBehaviour
    {
        [SerializeField] private Button m_RemoveSavesBtn;
        private void Start()
        {
#if !DEBUG_BUILD
            m_RemoveSavesBtn.gameObject.SetActive(false);
#else
            InitCheat();
#endif
        }

        public void InitCheat()
        {
            m_RemoveSavesBtn.gameObject.SetActive(true);
            m_RemoveSavesBtn.onClick.RemoveAllListeners();
            m_RemoveSavesBtn.onClick.AddListener(() =>
            {
                //YandexGame.Instance._ResetSaveProgress();
                //YandexGame.Instance._SaveProgress();
            });
        }
    }
}