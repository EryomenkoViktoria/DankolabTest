using System.Collections.Generic;
using TestDankolab.GameObjectCell;
using UnityEngine;

namespace TestDankolab.CheckerCells
{
    public class CheckCells : MonoBehaviour
    {
        public static CheckCells inst;

        private void Awake()
        {
            inst = this;
        }

        internal List<Cell> cells = new List<Cell>();

        internal int indexCell { get; set; }

        internal void DestroyCells()
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
