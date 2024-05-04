namespace searchApp
{
    partial class Search_Company
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
            bt_select = new Button();
            label1 = new Label();
            tbFilePath = new TextBox();
            btRemove = new Button();
            btSearch = new Button();
            lb_cn_value = new Label();
            lb_en_value = new Label();
            lb_code_value = new Label();
            tb_port = new TextBox();
            label5 = new Label();
            dataGridViewResult = new DataGridView();
            bt_save = new Button();
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).BeginInit();
            SuspendLayout();
            // 
            // bt_select
            // 
            bt_select.Location = new Point(584, 12);
            bt_select.Name = "bt_select";
            bt_select.Size = new Size(94, 29);
            bt_select.TabIndex = 0;
            bt_select.Text = "选择文件";
            bt_select.UseVisualStyleBackColor = true;
            bt_select.Click += bt_select_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(33, 18);
            label1.Name = "label1";
            label1.Size = new Size(84, 20);
            label1.TabIndex = 1;
            label1.Text = "文件路径：";
            // 
            // tbFilePath
            // 
            tbFilePath.Location = new Point(123, 15);
            tbFilePath.Name = "tbFilePath";
            tbFilePath.Size = new Size(455, 27);
            tbFilePath.TabIndex = 2;
            // 
            // btRemove
            // 
            btRemove.Location = new Point(684, 12);
            btRemove.Name = "btRemove";
            btRemove.Size = new Size(94, 29);
            btRemove.TabIndex = 5;
            btRemove.Text = "移除文件";
            btRemove.UseVisualStyleBackColor = true;
            btRemove.Click += btRemove_Click;
            // 
            // btSearch
            // 
            btSearch.Location = new Point(684, 68);
            btSearch.Name = "btSearch";
            btSearch.Size = new Size(94, 29);
            btSearch.TabIndex = 6;
            btSearch.Text = "查找";
            btSearch.UseVisualStyleBackColor = true;
            btSearch.Click += btSearch_Click;
            // 
            // lb_cn_value
            // 
            lb_cn_value.AutoSize = true;
            lb_cn_value.Location = new Point(123, 153);
            lb_cn_value.Name = "lb_cn_value";
            lb_cn_value.Size = new Size(0, 20);
            lb_cn_value.TabIndex = 12;
            // 
            // lb_en_value
            // 
            lb_en_value.AutoSize = true;
            lb_en_value.Location = new Point(198, 153);
            lb_en_value.Name = "lb_en_value";
            lb_en_value.Size = new Size(0, 20);
            lb_en_value.TabIndex = 13;
            // 
            // lb_code_value
            // 
            lb_code_value.AutoSize = true;
            lb_code_value.Location = new Point(280, 153);
            lb_code_value.Name = "lb_code_value";
            lb_code_value.Size = new Size(0, 20);
            lb_code_value.TabIndex = 14;
            // 
            // tb_port
            // 
            tb_port.Location = new Point(390, 68);
            tb_port.Name = "tb_port";
            tb_port.Size = new Size(188, 27);
            tb_port.TabIndex = 16;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(33, 71);
            label5.Name = "label5";
            label5.Size = new Size(351, 20);
            label5.TabIndex = 17;
            label5.Text = "请输入港口信息（港口中文/港口英文/港口代码）：";
            // 
            // dataGridViewResult
            // 
            dataGridViewResult.AllowUserToAddRows = false;
            dataGridViewResult.AllowUserToDeleteRows = false;
            dataGridViewResult.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dataGridViewResult.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dataGridViewResult.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewResult.Location = new Point(33, 135);
            dataGridViewResult.Name = "dataGridViewResult";
            dataGridViewResult.ReadOnly = true;
            dataGridViewResult.RowHeadersWidth = 51;
            dataGridViewResult.Size = new Size(745, 303);
            dataGridViewResult.TabIndex = 19;
            // 
            // bt_save
            // 
            bt_save.Location = new Point(33, 100);
            bt_save.Name = "bt_save";
            bt_save.Size = new Size(94, 29);
            bt_save.TabIndex = 20;
            bt_save.Text = "导出";
            bt_save.UseVisualStyleBackColor = true;
            bt_save.Click += bt_save_Click;
            // 
            // Search_Company
            // 
            AutoScaleDimensions = new SizeF(9F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(bt_save);
            Controls.Add(dataGridViewResult);
            Controls.Add(label5);
            Controls.Add(tb_port);
            Controls.Add(lb_code_value);
            Controls.Add(lb_en_value);
            Controls.Add(lb_cn_value);
            Controls.Add(btSearch);
            Controls.Add(btRemove);
            Controls.Add(tbFilePath);
            Controls.Add(label1);
            Controls.Add(bt_select);
            Name = "Search_Company";
            Text = "查找可到达港口的船公司";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dataGridViewResult).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button bt_select;
        private Label label1;
        private TextBox tbFilePath;
        private Button btRemove;
        private Button btSearch;
        private Label lb_cn_value;
        private Label lb_en_value;
        private Label lb_code_value;
        private TextBox tb_port;
        private Label label5;
        private DataGridView dataGridViewResult;
        private Button bt_save;
    }
}
