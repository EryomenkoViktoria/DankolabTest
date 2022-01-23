using System.Threading.Tasks;
using TestDankolab.Audio;
using TestDankolab.CheckerCells;
using TestDankolab.Spawner;
using UnityEngine;
using UnityEngine.UI;

namespace TestDankolab.GameObjectCell
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class Cell : MonoBehaviour
    {
        [SerializeField]
        private Button checkCells;

        private BoxCollider2D boxCollider2D;

        [SerializeField]
        private Transform rayUp, rayDown, rayLeft, rayRight;

        private int negativeVector = -1;
        private int positiveVector = 1;

        private string nameCell;

        private float rayDistance = 0.1f;

        private bool clickThisCell, checkDestroy;

        private void Start()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            SetNewSizeCellCollider();

            nameCell = gameObject.name.ToString();

            checkCells.onClick.AddListener(RayCastCheck);
        }

        private void SetNewSizeCellCollider()
        {
            boxCollider2D.size = SpawnObject.inst.SizeCell * 0.96f;
        }

        internal void NotFoundCellCount()
        {
            clickThisCell = false;
        }

        internal async void RayCastCheck()
        {
            if (!clickThisCell)
            {
                AudioGame.inst?.ClickButton();
                CheckCells.inst.indexCell++;

                Raycast(rayUp, positiveVector);
                Raycast(rayDown, negativeVector);
                Raycast(rayRight, positiveVector);
                Raycast(rayLeft,negativeVector) ;

                await Task.Delay(200);
                CheckCells.inst?.DestroyCells();
            }
        }

        private void Raycast(Transform directionRay, int direction)
        {
            RaycastHit2D directionHit = Physics2D.Raycast(directionRay.position, direction*directionRay.right, rayDistance);
            if (directionHit)
            {
                if (nameCell == directionHit.collider.name)
                {
                    if (directionHit.collider.gameObject.GetComponent<Cell>())
                    {
                        clickThisCell = true;

                        var gameCell = directionHit.collider.gameObject;
                        CheckCells.inst.cells.Add(gameObject.GetComponent<Cell>());
                        gameCell.GetComponent<Cell>().RayCastCheck();
                    }
                }
            }
        }

        internal async void DestroyCell()
        {
            if (!checkDestroy)
            {
                checkDestroy = true;
                await Task.Delay(200);
                Destroy(gameObject);
            }
        }
    }
}