using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace TestDankolab.Spawner
{
    [RequireComponent(typeof(GridLayoutGroup))]
    [RequireComponent(typeof(RectTransform))]
    public class SpawnObject : MonoBehaviour
    {
        public static SpawnObject inst; //? Переделать под интерфейс?

        [SerializeField]
        private GameObject[] prefabGameCell;

        private GridLayoutGroup gridLayoutGroup;

        private RectTransform rectTransform;

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

        public Vector2 SizeCell
        {
            get { return sizeCell; }
            set { sizeCell = value; }
        }

        internal void NewGame(int width, int height, int color)
        {
            ClearField();
            gridLayoutGroup.enabled = true;
            var pole = width * height;

            //Debug.Log("Ширина: " + width + "Высота: " + height);
            //Debug.Log("Cells Summ: " + pole);

            gridLayoutGroup.cellSize = new Vector2(rectTransform.rect.width / height, rectTransform.rect.height / width);
            sizeCell = gridLayoutGroup.cellSize;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GameObject inputField = Instantiate(prefabGameCell[Random.Range(0, color)]);
                    inputField.transform.SetParent(gameObject.transform, false);
                }
            }
            GridOff();
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