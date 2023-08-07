namespace PrinterServerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);

            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (!Program.ERROR)
                {
                    e.Cancel = true; // Cancel the close event

                    //Set end parameter
                    Program.EndProgram = true;

                    //Display loading
                    Loading_img.Visible = true;
                    Status.Text = "停止中・・・";
                    Status.BackColor = Color.Orange;
                    // Btn_label.Visible = false;
                    Stop_btn.Visible = false;
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Program.DataGrid = dataGridView1;
            Program.Status = Status;
            Program.ErrMsg = ErrorMsg;
            Program.DisplayPrintHistory();

            dataGridView1.Columns["labelflgColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["denpyoflgColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["hikaeflgColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["receiptflgColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["orderflgColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dataGridView1.Columns["printflgColumn"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

        }

        //ON CLICK STOP PROGRAM
        private void Stop_btn_Click(object sender, EventArgs e)
        {
            if (Program.ERROR)
            {
                Environment.Exit(0);
            }

            //Set end parameter
            Program.EndProgram = true;

            //Display loading
            Loading_img.Visible = true;
            Status.Text = "停止中・・・";
            Status.BackColor = Color.Orange;
            Stop_btn.Visible = false;
        }
    }
}