using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using TestDankolab.Spawner;
using TestDankolab.Audio;

namespace TestDankolab.UI
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField]
        private TMP_InputField widthGrid, heightGrid, colorsGrid;

        [SerializeField]
        private Button startGameButton, clousedErrorDataPanel;

        [SerializeField]
        GameObject errorDataPanel;

        private int widthGridData;

        public int WidthGridData
        {
            get { return widthGridData; }
            set { widthGridData = value; }
        }

        private int heightGridData;

        public int HeightGridData
        {
            get { return heightGridData; }
            set { heightGridData = value; }
        }

        private int colorsGridData;

        public int ColorsGridData
        {
            get { return colorsGridData; }
            set { colorsGridData = value; }
        }

        private void Start()
        {
            startGameButton.onClick.AddListener(CreateNewDataGame);
            clousedErrorDataPanel.onClick.AddListener(ClousedErrorDataPanel);
        }

        private void CreateNewDataGame()
        {
            if (string.IsNullOrEmpty(widthGrid.text) && string.IsNullOrEmpty(heightGrid.text) && string.IsNullOrEmpty(colorsGrid.text))
                return;

            ConvertNewData();        
        }

        private void ConvertNewData()
        {
            widthGridData = Convert.ToInt32(widthGrid.text);
            heightGridData = Convert.ToInt32(heightGrid.text);
            colorsGridData = Convert.ToInt32(colorsGrid.text);

            CheckNewData();
        }

        private void CheckNewData()
        {
            if (widthGridData < 10 || widthGridData > 50 || heightGridData < 10 || heightGridData > 50 || colorsGridData < 2 || colorsGridData > 5)
                errorDataPanel.SetActive(true);
            else
                CreateGameField(widthGridData, heightGridData, colorsGridData);
        }

        private void ClousedErrorDataPanel()
        {
            errorDataPanel.SetActive(false);
        }

        private void CreateGameField(int width, int height, int color)
        {
            AudioGame.inst?.ClickButton();
            SpawnObject.inst?.NewGame(width, height, color);
        }
    }
}