using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace TestDankolab.Spawner
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class SpawnObject : MonoBehaviour
    {
        public static SpawnObject inst;

        [SerializeField]
        private GameObject[] prefabGameCell;

        private GridLayoutGroup gridLayoutGroup;

        private RectTransform rectTransform;

        private int[] colorRandom;

        private void Awake()
        {
            inst = this;
        }

        private void Start()
        {
            gridLayoutGroup = GetComponentInChildren<GridLayoutGroup>();
            rectTransform = GetComponent<RectTransform>();
        }

        private Vector2 sizeCell;

        internal Vector2 SizeCell
        {
            get { return sizeCell; }
            set { sizeCell = value; }
        }

        internal void NewGame(int width, int height, int color)
        {
            ClearField();

            gridLayoutGroup.enabled = true;
            var pole = width * height;

            gridLayoutGroup.cellSize = new Vector2(rectTransform.rect.width / height, rectTransform.rect.height / width);
            sizeCell = gridLayoutGroup.cellSize;

            SotrColor(color);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GameObject inputField = Instantiate(prefabGameCell[colorRandom[Random.Range(0,colorRandom.Length)]]);
                    inputField.transform.SetParent(gameObject.transform, false);
                }
            }

            GridOff();
        }

        private void SotrColor(int color)
        {  
            colorRandom = new int[color];

            int y = 0;

            for (int i=0; i< color; i++)
            {
                colorRandom[i] = Random.Range(0, prefabGameCell.Length);
                y = colorRandom[i];

                for (int j = 0; j < i; j++)
                {
                    while (colorRandom[i] == colorRandom[j])
                    { 
                        colorRandom[i] = Random.Range(0, prefabGameCell.Length);
                        j = 0;
                        y = colorRandom[i];
                    }

                    y = colorRandom[i];
                }
            }
        }

        private async void GridOff()
        {
            await Task.Delay(200);
            gridLayoutGroup.enabled = false;
        }

        private void ClearField()
        {
            var oldCells = GameObject.FindGameObjectsWithTag("Cell");

            for (int i = 0; i < oldCells.Length; i++)
                Destroy(oldCells[i]);
        }
    }
}