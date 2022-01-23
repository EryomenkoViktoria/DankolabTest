using System.Collections.Generic;
using UnityEngine;

namespace TestDankolab.GameObjectCell
{
    public class CheckCells : MonoBehaviour
    {
        public static CheckCells inst;

        private void Awake()
        {
            inst = this;
        }

        public List<Cell> cells = new List<Cell>();

        public int indexCell { get; set; }

        public void DestroyCells()
        {
            if (indexCell >= 3)
            {
                for (int i = 0; i < cells.Count; i++)
                    cells[i].DestroyCell();
            }
            else
            {
                for (int i = 0; i < cells.Count; i++)
                    cells[i].NotFoundCellCount();
            }

            indexCell = 0;
            cells.Clear();
        }
    }
}
