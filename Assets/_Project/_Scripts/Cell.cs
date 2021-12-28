using UnityEngine;

namespace VoidPixel
{

    public class Cell : MonoBehaviour
    {

        [SerializeField] float tileSpawnDelay = 0.2f;

        public Cell right;
        public Cell down;
        public Cell left;
        public Cell up;

        public Fill fill;

        private void OnEnable()
        {
            GameController.slide += OnSlide;
        }

        private void OnDisable()
        {
            GameController.slide -= OnSlide;
        }

        private void OnSlide(string dir)
        {

            CellCheck();

            if (dir == "Up")
            {
                if (up != null) { return; }
                Cell currentCell = this;
                SlideUp(currentCell);
            }
            if (dir == "Right") 
            {
                if (right != null) { return; }
                Cell currentCell = this;
                SlideRight(currentCell);
            }
            if (dir == "Down") 
            {
                if (down != null) { return; }
                Cell currentCell = this;
                SlideDown(currentCell);
            }
            if (dir == "Left")
            {
                if (left != null) { return; }
                Cell currentCell = this;
                SlideLeft(currentCell);
            }

            GameController.Ticker++;
            if (GameController.Ticker == 4) { Invoke(nameof (SpawnTile), tileSpawnDelay); }

        }

        private void SpawnTile()
        {
            GameController.instance.SpawnFill();
        }

        void SlideUp(Cell currentCell)
        {
            if (currentCell.down == null) { return; }
            if (currentCell.fill != null)
            {
                Cell nextCell = currentCell.down;
                while (nextCell.down != null && nextCell.fill == null)
                {
                    nextCell = nextCell.down;
                }
                if (nextCell.fill != null)
                {
                    if (currentCell.fill.Value == nextCell.fill.Value)
                    {
                        nextCell.fill.Double();
                        nextCell.fill.transform.SetParent(currentCell.transform);
                        currentCell.fill = nextCell.fill;
                        nextCell.fill = null;
                    }
                    else if (currentCell.down.fill != nextCell.fill)
                    {
                        nextCell.fill.transform.SetParent(currentCell.down.transform);
                        currentCell.down.fill = nextCell.fill;
                        nextCell.fill = null;
                    }
                }
            }
            else
            {
                Cell nextCell = currentCell.down;
                while (nextCell.down != null && nextCell.fill == null)
                {
                    nextCell = nextCell.down;
                }
                if (nextCell.fill != null)
                {
                    nextCell.fill.transform.SetParent(currentCell.transform);
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                    SlideUp(currentCell);
                }
            }

            if (currentCell.down == null) { return; }
            SlideUp(currentCell.down);
        }

        void SlideRight(Cell currentCell)
        {
            if (currentCell.left == null) { return; }
            if (currentCell.fill != null)
            {
                Cell nextCell = currentCell.left;
                while (nextCell.left != null && nextCell.fill == null)
                {
                    nextCell = nextCell.left;
                }
                if (nextCell.fill != null)
                {
                    if (currentCell.fill.Value == nextCell.fill.Value)
                    {
                        nextCell.fill.Double();
                        nextCell.fill.transform.SetParent(currentCell.transform);
                        currentCell.fill = nextCell.fill;
                        nextCell.fill = null;
                    }
                    else if (currentCell.left.fill != nextCell.fill)
                    {
                        nextCell.fill.transform.SetParent(currentCell.left.transform);
                        currentCell.left.fill = nextCell.fill;
                        nextCell.fill = null;
                    }
                }
            }
            else
            {
                Cell nextCell = currentCell.left;
                while (nextCell.left != null && nextCell.fill == null)
                {
                    nextCell = nextCell.left;
                }
                if (nextCell.fill != null)
                {
                    nextCell.fill.transform.SetParent(currentCell.transform);
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                    SlideRight(currentCell);
                }
            }

            if (currentCell.left == null) { return; }
            SlideRight(currentCell.left);
        }

        void SlideDown(Cell currentCell)
        {
            if (currentCell.up == null) { return; }
            if (currentCell.fill != null)
            {
                Cell nextCell = currentCell.up;
                while (nextCell.up != null && nextCell.fill == null)
                {
                    nextCell = nextCell.up;
                }
                if (nextCell.fill != null)
                {
                    if (currentCell.fill.Value == nextCell.fill.Value)
                    {
                        nextCell.fill.Double();
                        nextCell.fill.transform.SetParent(currentCell.transform);
                        currentCell.fill = nextCell.fill;
                        nextCell.fill = null;
                    }
                    else if (currentCell.up.fill != nextCell.fill)
                    {
                        nextCell.fill.transform.SetParent(currentCell.up.transform);
                        currentCell.up.fill = nextCell.fill;
                        nextCell.fill = null;
                    }
                }
            }
            else
            {
                Cell nextCell = currentCell.up;
                while (nextCell.up != null && nextCell.fill == null)
                {
                    nextCell = nextCell.up;
                }
                if (nextCell.fill != null)
                {
                    nextCell.fill.transform.SetParent(currentCell.transform);
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                    SlideDown(currentCell);
                }
            }

            if (currentCell.up == null) { return; }
            SlideDown(currentCell.up);
        }

        void SlideLeft(Cell currentCell)
        {
            if (currentCell.right == null) { return; }
            if (currentCell.fill != null)
            {
                Cell nextCell = currentCell.right;
                while (nextCell.right != null && nextCell.fill == null)
                {
                    nextCell = nextCell.right;
                }
                if (nextCell.fill != null)
                {
                    if (currentCell.fill.Value == nextCell.fill.Value)
                    {
                        nextCell.fill.Double();
                        nextCell.fill.transform.SetParent(currentCell.transform);
                        currentCell.fill = nextCell.fill;
                        nextCell.fill = null;
                    }
                    else if (currentCell.right.fill != nextCell.fill)
                    {
                        nextCell.fill.transform.SetParent(currentCell.right.transform);
                        currentCell.right.fill = nextCell.fill;
                        nextCell.fill = null;
                    }
                }
            }
            else
            {
                Cell nextCell = currentCell.right;
                while (nextCell.right != null && nextCell.fill == null)
                {
                    nextCell = nextCell.right;
                }
                if (nextCell.fill != null)
                {
                    nextCell.fill.transform.SetParent(currentCell.transform);
                    currentCell.fill = nextCell.fill;
                    nextCell.fill = null;
                    SlideLeft(currentCell);
                }
            }

            if (currentCell.right == null) { return; }
            SlideLeft(currentCell.right);
        }

        public void CellCheck()
        {

            if (fill == null) { return; }

            if (up != null)
            {
                if (up.fill == null) { return;}
                if (up.fill.Value == fill.Value) { return; }
            }

            if (right != null)
            {
                if (right.fill == null) { return; }
                if (right.fill.Value == fill.Value) { return; }
            }

            if (down != null)
            {
                if (down.fill == null) { return; }
                if (down.fill.Value == fill.Value) { return; }
            }

            if (left != null)
            {
                if (left.fill == null) { return; }
                if (left.fill.Value == fill.Value) { return; }
            }

            GameController.instance.GameOverCheck();
        }
    }

}

