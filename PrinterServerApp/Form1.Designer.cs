namespace PrinterServerApp
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            splitContainer1 = new SplitContainer();
            Loading_img = new PictureBox();
            Stop_btn = new Button();
            ErrorMsg = new Label();
            Status = new Label();
            title = new Label();
            dataGridView1 = new DataGridView();
            usernmColumn = new DataGridViewTextBoxColumn();
            ordernoColumn = new DataGridViewTextBoxColumn();
            tokuisakinmColumn = new DataGridViewTextBoxColumn();
            saledtColumn = new DataGridViewTextBoxColumn();
            labelflgColumn = new DataGridViewTextBoxColumn();
            denpyoflgColumn = new DataGridViewTextBoxColumn();
            hikaeflgColumn = new DataGridViewTextBoxColumn();
            receiptflgColumn = new DataGridViewTextBoxColumn();
            orderflgColumn = new DataGridViewTextBoxColumn();
            printflgColumn = new DataGridViewTextBoxColumn();
            printHistoryBindingSource = new BindingSource(components);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).BeginInit();
            splitContainer1.Panel1.SuspendLayout();
            splitContainer1.Panel2.SuspendLayout();
            splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)Loading_img).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).BeginInit();
            ((System.ComponentModel.ISupportInitialize)printHistoryBindingSource).BeginInit();
            SuspendLayout();
            // 
            // splitContainer1
            // 
            splitContainer1.Dock = DockStyle.Fill;
            splitContainer1.Location = new Point(0, 0);
            splitContainer1.Margin = new Padding(10);
            splitContainer1.Name = "splitContainer1";
            splitContainer1.Orientation = Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            splitContainer1.Panel1.Controls.Add(Loading_img);
            splitContainer1.Panel1.Controls.Add(Stop_btn);
            splitContainer1.Panel1.Controls.Add(ErrorMsg);
            splitContainer1.Panel1.Controls.Add(Status);
            splitContainer1.Panel1.Controls.Add(title);
            // 
            // splitContainer1.Panel2
            // 
            splitContainer1.Panel2.Controls.Add(dataGridView1);
            splitContainer1.Size = new Size(800, 450);
            splitContainer1.SplitterDistance = 225;
            splitContainer1.TabIndex = 5;
            // 
            // Loading_img
            // 
            Loading_img.Anchor = AnchorStyles.Top;
            Loading_img.Image = (Image)resources.GetObject("Loading_img.Image");
            Loading_img.Location = new Point(341, 70);
            Loading_img.MaximumSize = new Size(116, 110);
            Loading_img.Name = "Loading_img";
            Loading_img.Size = new Size(116, 110);
            Loading_img.SizeMode = PictureBoxSizeMode.StretchImage;
            Loading_img.TabIndex = 7;
            Loading_img.TabStop = false;
            Loading_img.Visible = false;
            // 
            // Stop_btn
            // 
            Stop_btn.Anchor = AnchorStyles.Top;
            Stop_btn.Cursor = Cursors.Hand;
            Stop_btn.Font = new Font("Yu Gothic UI", 16F, FontStyle.Bold, GraphicsUnit.Point);
            Stop_btn.Location = new Point(318, 70);
            Stop_btn.MaximumSize = new Size(160, 60);
            Stop_btn.Name = "Stop_btn";
            Stop_btn.Size = new Size(160, 60);
            Stop_btn.TabIndex = 6;
            Stop_btn.Text = "停止";
            Stop_btn.UseVisualStyleBackColor = true;
            Stop_btn.Click += Stop_btn_Click;
            // 
            // ErrorMsg
            // 
            ErrorMsg.BackColor = SystemColors.Control;
            ErrorMsg.Dock = DockStyle.Bottom;
            ErrorMsg.Font = new Font("Yu Gothic UI", 11.25F, FontStyle.Regular, GraphicsUnit.Point);
            ErrorMsg.Location = new Point(0, 133);
            ErrorMsg.Name = "ErrorMsg";
            ErrorMsg.Size = new Size(800, 92);
            ErrorMsg.TabIndex = 5;
            ErrorMsg.Visible = false;
            // 
            // Status
            // 
            Status.BackColor = Color.LimeGreen;
            Status.BorderStyle = BorderStyle.Fixed3D;
            Status.Font = new Font("Yu Gothic UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point);
            Status.Location = new Point(12, 9);
            Status.MaximumSize = new Size(200, 50);
            Status.Name = "Status";
            Status.Size = new Size(131, 50);
            Status.TabIndex = 3;
            Status.Text = "実行中・・・";
            Status.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // title
            // 
            title.Anchor = AnchorStyles.Top;
            title.Font = new Font("Yu Gothic UI", 30F, FontStyle.Bold, GraphicsUnit.Point);
            title.Location = new Point(12, 0);
            title.Name = "title";
            title.Size = new Size(776, 67);
            title.TabIndex = 1;
            title.Text = "自動帳票発行";
            title.TextAlign = ContentAlignment.TopCenter;
            // 
            // dataGridView1
            // 
            dataGridView1.AllowUserToAddRows = false;
            dataGridView1.AllowUserToDeleteRows = false;
            dataGridView1.AutoGenerateColumns = false;
            dataGridView1.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridView1.Columns.AddRange(new DataGridViewColumn[] { usernmColumn, ordernoColumn, tokuisakinmColumn, saledtColumn, labelflgColumn, denpyoflgColumn, hikaeflgColumn, receiptflgColumn, orderflgColumn, printflgColumn });
            dataGridView1.DataSource = printHistoryBindingSource;
            dataGridView1.Dock = DockStyle.Fill;
            dataGridView1.Location = new Point(0, 0);
            dataGridView1.Name = "dataGridView1";
            dataGridView1.ReadOnly = true;
            dataGridView1.RowTemplate.Height = 25;
            dataGridView1.Size = new Size(800, 221);
            dataGridView1.TabIndex = 0;
            // 
            // usernmColumn
            // 
            usernmColumn.DataPropertyName = "User_nm";
            usernmColumn.HeaderText = "ユーザー名";
            usernmColumn.Name = "usernmColumn";
            usernmColumn.ReadOnly = true;
            // 
            // ordernoColumn
            // 
            ordernoColumn.DataPropertyName = "Order_no";
            ordernoColumn.HeaderText = "受注番号";
            ordernoColumn.Name = "ordernoColumn";
            ordernoColumn.ReadOnly = true;
            // 
            // tokuisakinmColumn
            // 
            tokuisakinmColumn.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            tokuisakinmColumn.DataPropertyName = "Tokuisaki_nm";
            tokuisakinmColumn.HeaderText = "得意先名";
            tokuisakinmColumn.MinimumWidth = 100;
            tokuisakinmColumn.Name = "tokuisakinmColumn";
            tokuisakinmColumn.ReadOnly = true;
            // 
            // saledtColumn
            // 
            saledtColumn.DataPropertyName = "Sale_dt";
            saledtColumn.HeaderText = "売上日";
            saledtColumn.Name = "saledtColumn";
            saledtColumn.ReadOnly = true;
            // 
            // labelflgColumn
            // 
            labelflgColumn.DataPropertyName = "Label_flg";
            labelflgColumn.HeaderText = "荷札・送り状";
            labelflgColumn.Name = "labelflgColumn";
            labelflgColumn.ReadOnly = true;
            // 
            // denpyoflgColumn
            // 
            denpyoflgColumn.DataPropertyName = "Denpyo_flg";
            denpyoflgColumn.HeaderText = "売上伝票";
            denpyoflgColumn.Name = "denpyoflgColumn";
            denpyoflgColumn.ReadOnly = true;
            // 
            // hikaeflgColumn
            // 
            hikaeflgColumn.DataPropertyName = "Hikae_flg";
            hikaeflgColumn.HeaderText = "売上伝票控";
            hikaeflgColumn.Name = "hikaeflgColumn";
            hikaeflgColumn.ReadOnly = true;
            // 
            // receiptflgColumn
            // 
            receiptflgColumn.DataPropertyName = "Receipt_flg";
            receiptflgColumn.HeaderText = "領収書";
            receiptflgColumn.Name = "receiptflgColumn";
            receiptflgColumn.ReadOnly = true;
            // 
            // orderflgColumn
            // 
            orderflgColumn.DataPropertyName = "Order_flg";
            orderflgColumn.HeaderText = "注文書";
            orderflgColumn.Name = "orderflgColumn";
            orderflgColumn.ReadOnly = true;
            // 
            // printflgColumn
            // 
            printflgColumn.DataPropertyName = "Print_flg";
            printflgColumn.HeaderText = "発行";
            printflgColumn.Name = "printflgColumn";
            printflgColumn.ReadOnly = true;
            printflgColumn.Width = 60;
            // 
            // printHistoryBindingSource
            // 
            printHistoryBindingSource.DataSource = typeof(PrintHistory);
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(splitContainer1);
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "Form1";
            Text = "自動帳票発行";
            Load += Form1_Load;
            splitContainer1.Panel1.ResumeLayout(false);
            splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)splitContainer1).EndInit();
            splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)Loading_img).EndInit();
            ((System.ComponentModel.ISupportInitialize)dataGridView1).EndInit();
            ((System.ComponentModel.ISupportInitialize)printHistoryBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private SplitContainer splitContainer1;
        private PictureBox Loading_img;
        private Button Stop_btn;
        private Label ErrorMsg;
        private Label Status;
        private Label title;
        private DataGridView dataGridView1;
        private BindingSource printHistoryBindingSource;
        private DataGridViewTextBoxColumn usernmColumn;
        private DataGridViewTextBoxColumn ordernoColumn;
        private DataGridViewTextBoxColumn tokuisakinmColumn;
        private DataGridViewTextBoxColumn saledtColumn;
        private DataGridViewTextBoxColumn labelflgColumn;
        private DataGridViewTextBoxColumn denpyoflgColumn;
        private DataGridViewTextBoxColumn hikaeflgColumn;
        private DataGridViewTextBoxColumn receiptflgColumn;
        private DataGridViewTextBoxColumn orderflgColumn;
        private DataGridViewTextBoxColumn printflgColumn;
    }
}