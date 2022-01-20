using System.Threading.Tasks;
using TestDankolab.Audio;
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

        private string nameCell;

        private void Start()
        {
            boxCollider2D = GetComponent<BoxCollider2D>();
            SetNewSizeCellCollider();

            nameCell = gameObject.name.ToString();

            checkCells.onClick.AddListener(RayCastCheck);
        }

        private void SetNewSizeCellCollider()
        {
            boxCollider2D.size = SpawnObject.inst.SizeCell * 0.97f;
        }

        private float rayDistance = 0.1f;

        private bool clickThisCell, checkDestroy;

        internal void RayCastCheck()
        {
            if (!clickThisCell)
            {
                AudioGame.inst?.ClickButton();

                RaycastHit2D hitUp = Physics2D.Raycast(rayUp.position, rayUp.up, rayDistance);
                if (hitUp)
                {
                    if (nameCell == hitUp.collider.name)
                    {
                        if (hitUp.collider.gameObject.GetComponent<Cell>())
                        {
                            clickThisCell = true;
                            var gameCell = hitUp.collider.gameObject;
                            gameCell.GetComponent<Cell>().RayCastCheck();
                            DestroyCell();
                        }
                    }
                }

                RaycastHit2D hitDown = Physics2D.Raycast(rayDown.position, -rayDown.up, rayDistance);
                if (hitDown)
                {
                    if (nameCell == hitDown.collider.name)
                    {
                        if (hitDown.collider.gameObject.GetComponent<Cell>())
                        {
                            clickThisCell = true;
                            var gameCell = hitDown.collider.gameObject;
                            gameCell.GetComponent<Cell>().RayCastCheck();
                            DestroyCell();
                        }
                    }
                }

                RaycastHit2D hitRight = Physics2D.Raycast(rayRight.position, rayRight.right, rayDistance);
                if (hitRight)
                {
                    if (nameCell == hitRight.collider.name)
                    {
                        if (hitRight.collider.gameObject.GetComponent<Cell>())
                        {
                            clickThisCell = true;
                            var gameCell = hitRight.collider.gameObject;
                            gameCell.GetComponent<Cell>().RayCastCheck();
                            DestroyCell();
                        }
                    }
                }

                RaycastHit2D hitLeft = Physics2D.Raycast(rayLeft.position, -rayLeft.right, rayDistance);
                if (hitLeft)
                {
                    if (nameCell == hitLeft.collider.name)
                    {
                        if (hitLeft.collider.gameObject.GetComponent<Cell>())
                        {
                            clickThisCell = true;
                            var gameCell = hitLeft.collider.gameObject;
                            gameCell.GetComponent<Cell>().RayCastCheck();
                            DestroyCell();
                        }
                    }
                }
            }
        }

        private async void DestroyCell()
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