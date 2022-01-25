using System.IO;
using System.Windows.Forms;
using System.Drawing;
using System;
using CourseWork.Core;
using System.Threading.Tasks;
using System.Linq;
using CourseWork.Core.Core;
using CourseWork.Models.Models;

namespace CourseWork.UI
{
    public class Main : Form
    {

        public const int RabitsCount = 30; 
        public const int WolvesCount = 50; 
        public const int SheWolfCount = 40; 


        public const int ClientWidth = 1350;
        public const int ClientHeight = 1350;
        public const int GridWidth = 1001;
        public const int GridHeight = 1001;

        public const int RowsCount = 20;
        public const int ColumnsCount = 20;
        public const int CellSize = 50;


        private DataGridView dataGridView1;
        private Button startButton;

        private Bitmap sheWolf;
        private Bitmap wolf;
        private Bitmap rabbit;

        private GameFieldManager _gameFieldManager { get; set; }
        private WolvesManager _wolvesManager{ get; set; }
        private SheWolvesManager _sheWolvesManager{ get; set; }
        private RabbitManager _rabbitManager{ get; set; }
        //private GameFieldManager _gameFieldManager { get; set; }
      //  private GameFieldManager _gameFieldManager { get; set; }

        public  Main()
        {

            Image sheWolf = Image.FromFile(@"../../../images\she-wolf.jpg");
            Image wolf = Image.FromFile(@"../../../images\wolf.jpg");
            Image rabbit = Image.FromFile(@"../../../images\rabbit.jpg");


            using (var ms = new MemoryStream())
            {
                sheWolf.Save(ms, sheWolf.RawFormat);
                var t = ms.ToArray();
                this.sheWolf = new Bitmap(new MemoryStream(t));
            }
            using (var ms = new MemoryStream())
            {
                wolf.Save(ms, wolf.RawFormat);
                var t = ms.ToArray();
                this.wolf = new Bitmap(new MemoryStream(t));
            }
            using (var ms = new MemoryStream())
            {
                rabbit.Save(ms, rabbit.RawFormat);
                var t = ms.ToArray();
                this.rabbit = new Bitmap(new MemoryStream(t));
            }


            startButton = new Button();

            startButton.Width = 100;
            startButton.Height = 100;
            startButton.Text = "start";
            startButton.Location = new Point(0, 0);

            startButton.Anchor = AnchorStyles.Top | AnchorStyles.Left;

            startButton.Click += new EventHandler(StartSimulation2);

            this.AutoSize = true;

            _rabbitManager = new RabbitManager();
            _wolvesManager = new WolvesManager();

            _sheWolvesManager = new SheWolvesManager();
            _gameFieldManager = new GameFieldManager(20, 20, RabitsCount, WolvesCount, SheWolfCount, _rabbitManager, _wolvesManager,_sheWolvesManager);
            InitializeDataGridView(null, null);
        }

       

        private async void StartSimulation2(object o, EventArgs e)
        {
            while (true)
            {


                _wolvesManager.RemoveIfNotAlive(_gameFieldManager.GameField.GameCells);
                _sheWolvesManager.RemoveIfNotAlive(_gameFieldManager.GameField.GameCells);
                // creating new game field
                var newGameCells = GameFieldManager.GenerateEmptyField(_gameFieldManager.Width, _gameFieldManager.Height);

                for (int i = 0; i < RowsCount; i++)
                {
                    for (int j = 0; j < ColumnsCount; j++)
                    {
                        if (_gameFieldManager.GameField.GameCells[i, j].Wolves.Any())
                        {
                            DrawWolf(i, j);
                            continue;
                        }


                        if (_gameFieldManager.GameField.GameCells[i,j].Rabbits.Any())
                        {
                            DrawRabbit(i, j);
                            continue;
                        }
                      
                        if (_gameFieldManager.GameField.GameCells[i,j].SheWolves.Any())
                        {
                            DrawSheWolf(i, j);
                            continue;
                        }
                    }
                }

                _wolvesManager.Hunt(_gameFieldManager.GameField.GameCells);
                _sheWolvesManager.Hunt(_gameFieldManager.GameField.GameCells);

                // reproducing game units
                _rabbitManager.Reproduce(_gameFieldManager.GameField.GameCells, newGameCells);
                _wolvesManager.Reproduce(_gameFieldManager.GameField.GameCells, newGameCells);

                // moving game units
                _rabbitManager.Move(_gameFieldManager.GameField.GameCells, newGameCells);
                _wolvesManager.Move(_gameFieldManager.GameField.GameCells, newGameCells);
                _sheWolvesManager.Move(_gameFieldManager.GameField.GameCells, newGameCells);

                _gameFieldManager.GameField.ReplaceGameCells(newGameCells);
                await Task.Delay(1000);
                //  InitializeDataGridView(null, null);

                for (int i = 0; i < RowsCount; i++)
                {
                    for (int j = 0; j < ColumnsCount; j++)
                    {
                        DataGridViewImageCell cell = (DataGridViewImageCell)dataGridView1.Rows[i].Cells[j];
                        cell.Value = null;
                    }
                }

            }
        }




        private void InitializeDataGridView(object sender,
            EventArgs e)
        {
            // this.SuspendLayout();
            SuspendLayout();
            ConfigureForm();
            SizeGrid();
            CreateColumns();
            CreateRows();

            this.ResumeLayout(false);
        }

        private void DrawWolf(int i, int j)
        {
            DataGridViewImageCell cell = (DataGridViewImageCell)dataGridView1.Rows[i].Cells[j];
            cell.Value = wolf;
        }
        private void DrawSheWolf(int i, int j)
        {
            DataGridViewImageCell cell = (DataGridViewImageCell)dataGridView1.Rows[i].Cells[j];
            cell.Value = sheWolf;
        }
        private void DrawRabbit(int i, int j)
        {
            DataGridViewImageCell cell = (DataGridViewImageCell)dataGridView1.Rows[i].Cells[j];
            cell.Value = rabbit;
        }

        private void ConfigureForm()
        {
            AutoSize = true;
      

            ClientSize = new System.Drawing.Size(ClientWidth, ClientHeight);
            Text = "Course work";

            dataGridView1 = new System.Windows.Forms.DataGridView();
            dataGridView1.Location = new Point(100, 0);
            dataGridView1.AllowUserToAddRows = false;

            Controls.Add(dataGridView1);
            Controls.Add(startButton);

        }

     


        private void CreateColumns()
        {
            DataGridViewImageColumn imageColumn;
            for (int i = 0; i < ColumnsCount; i++)
            {

                imageColumn = new DataGridViewImageColumn();

                imageColumn.Width = CellSize;

                dataGridView1.Columns.Add(imageColumn);
            }
        }

        private void CreateRows()
        {
            for (int i = 0; i < RowsCount; i++)
            {
                dataGridView1.Rows.Add(new DataGridViewRow() { Height = CellSize });

            }
        }

        private void SizeGrid()
        {
            dataGridView1.ColumnHeadersVisible = false;
            dataGridView1.RowHeadersVisible = false;
            dataGridView1.AllowUserToResizeColumns = false; ;
            dataGridView1.AllowUserToResizeRows = false;
            dataGridView1.BorderStyle = BorderStyle.None;


            dataGridView1.Width = GridWidth;
            dataGridView1.Height = GridHeight;
                     
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // Main
            // 
            this.ClientSize = new System.Drawing.Size(446, 325);
            this.Name = "Main";
            this.ResumeLayout(false);

        }

    }
}