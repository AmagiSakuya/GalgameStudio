using UnityEngine;
using UnityEngine.UI;

namespace AdventureGame
{
    [ExecuteInEditMode]
    public class ADVGameDialog : MonoBehaviour
    {
        [SerializeField]
        private GameDialogStyleDefine m_styleDefine;

        public Image imageComp;
        public Text mainTextComp;

        public GameDialogStyleDefine StyleDefine
        {
            get { return m_styleDefine; }
            set
            {
                SetDefine(value);
                m_styleDefine = value;
            }
        }

        private void OnValidate()
        {
            StyleDefine = m_styleDefine;
        }

        void SetDefine(GameDialogStyleDefine value)
        {
            if (value == null)
            {
                imageComp.sprite = null;
                Color m_temp = imageComp.color;
                m_temp.a = 0;
                imageComp.color = m_temp;
            }
            else
            {
                //设置背景图片
                Color m_temp = imageComp.color;
                m_temp.a = 1;
                imageComp.color = m_temp;
                imageComp.sprite = value.backgroundImage;
                //设置文本颜色
                mainTextComp.color = value.mainTextFontColor;
            }
        }
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}