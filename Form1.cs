using System.Data;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;
namespace searchApp
{
    public partial class Search_Company : Form
    {
        public Search_Company()
        {
            InitializeComponent();
        }
        public static bool Debug = false;
        public static char split_char = ',';
        public static String[] options = { "港口中文", "港口英文", "港口代码", "可到达港口的船公司" };

        private void bt_select_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.Multiselect = false;//该值确定是否可以选择多个文件
            dialog.Title = "请选择文件";
            dialog.Filter = "表格文件(*.csv)|*.csv";
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                tbFilePath.Text = dialog.FileName;

            }
        }

        private void btRemove_Click(object sender, EventArgs e)
        {
            tbFilePath.Text = "";
        }

        private void cbOptions_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

            if (Debug)
            {
                tbFilePath.Text = "D:\\Users\\plus\\Desktop\\search\\港口-船公司.csv";
                tb_port.Text = "达曼";
            }
        }
        public class PortInfo
        {
            public string PortChinese { get; set; }
            public string PortEnglish { get; set; }
            public string PortCode { get; set; }
            public List<string> ShippingCompanies { get; set; }
        }
        //加载CSV文件
        private DataTable LoadCsv(string filePath)
        {
            DataTable dt = new DataTable();
            using (StreamReader sr = new StreamReader(filePath))
            {
                // 读取表头
                string[] headers = sr.ReadLine().Split(split_char);
                foreach (string header in headers)
                {
                    dt.Columns.Add(header);
                }

                // 读取数据
                while (!sr.EndOfStream)
                {
                    string[] rows = sr.ReadLine().Split(split_char);
                    DataRow dr = dt.NewRow();
                    for (int i = 0; i < headers.Length; i++)
                    {
                        dr[i] = rows[i];
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        //找出到达港口的船公司列表
        private List<string> GetShippingLines(DataRow row)
        {
            List<string> shippingLines = new List<string>();
            for (int i = 9; i < row.ItemArray.Length; i++) // 从第9列开始是船公司
            {
                if (row[i].ToString() != "#N/A")
                {
                    shippingLines.Add(row.Table.Columns[i].ColumnName);
                }
            }
            return shippingLines;
        }
        // 添加Levenshtein距离算法的实现
        public static int LevenshteinDistance(string s1, string s2)
        {
            var costs = new int[s2.Length + 1];
            for (int i = 0; i <= s1.Length; i++)
            {
                int lastValue = i;
                for (int j = 0; j <= s2.Length; j++)
                {
                    if (i == 0)
                        costs[j] = j;
                    else
                    {
                        if (j > 0)
                        {
                            int newValue = costs[j - 1];
                            if (s1[i - 1] != s2[j - 1])
                                newValue = Math.Min(Math.Min(newValue, lastValue), costs[j]) + 1;
                            costs[j - 1] = lastValue;
                            lastValue = newValue;
                        }
                    }
                }
                if (i > 0)
                    costs[s2.Length] = lastValue;
            }
            return costs[s2.Length];
        }

        private IEnumerable<PortInfo>? SearchPorts(DataTable table, string searchTerm)
        {
            // 首先尝试查找模糊匹配的港口
            var exactMatches = table.AsEnumerable()
                .Where(row => row[options[0]].ToString().Contains(searchTerm) ||
                        row[options[1]].ToString().Contains(searchTerm) ||
                        row[options[2]].ToString().Contains(searchTerm))
                .Select(row => new PortInfo
                {
                    PortChinese = row[options[0]].ToString(),
                    PortEnglish = row[options[1]].ToString(),
                    PortCode = row[options[2]].ToString(),
                    ShippingCompanies = GetShippingLines(row)
                });

            // 如果找到完全匹配的结果，则直接返回这些结果
            if (exactMatches.Any())
            {
                return exactMatches;
            }

            // 如果没有找到完全匹配的结果，再进行基于 Levenshtein 距离的搜索
            int maxDistance = searchTerm.Length / 2; // 可以根据需要调整这个阈值
            var similarMatches = table.AsEnumerable()
                .Where(row => options.Take(3).Any(option =>
                    LevenshteinDistance(row[option].ToString(), searchTerm) <= maxDistance))
                .Select(row => new PortInfo
                {
                    PortChinese = row[options[0]].ToString(),
                    PortEnglish = row[options[1]].ToString(),
                    PortCode = row[options[2]].ToString(),
                    ShippingCompanies = GetShippingLines(row)
                });

            return similarMatches;
        }

        //将查询结果转换为DataTable
        private DataTable ConvertToDataTable(IEnumerable<PortInfo> portInfos)
        {
            DataTable table = new DataTable();
            string num_col = "序号";
            table.Columns.Add(num_col, typeof(int)).AutoIncrement = true;
            table.Columns[num_col].AutoIncrementSeed = 1; // Start with 1
            table.Columns[num_col].AutoIncrementStep = 1; // Increment by 1

            table.Columns.Add(options[0], typeof(string));
            table.Columns.Add(options[1], typeof(string));
            table.Columns.Add(options[2], typeof(string));
            table.Columns.Add(options[3], typeof(string));

            foreach (var info in portInfos)
            {
                var row = table.NewRow();
                // No need to set the value for "序号", it will auto-increment.
                row[options[0]] = info.PortChinese;
                row[options[1]] = info.PortEnglish;
                row[options[2]] = info.PortCode;
                row[options[3]] = string.Join(", ", info.ShippingCompanies);
                table.Rows.Add(row);
            }

            return table;
        }
        //导出DataGridView内容到CSV
        private void ExportDataGridViewToCsv(string filePath)
        {
            StringBuilder csvContent = new StringBuilder();

            // 添加表头
            string[] columnNames = dataGridViewResult.Columns
                                    .Cast<DataGridViewColumn>()
                                    .Select(column => column.HeaderText)
                                    .ToArray();
            csvContent.AppendLine(string.Join(",", columnNames));

            // 添加行数据
            foreach (DataGridViewRow row in dataGridViewResult.Rows)
            {
                if (!row.IsNewRow) // 忽略未提交的新行
                {
                    string[] cells = row.Cells
                                       .Cast<DataGridViewCell>()
                                       .Select(cell => cell.Value?.ToString())
                                       .ToArray();
                    csvContent.AppendLine(string.Join(",", cells));
                }
            }

            // 保存到文件
            File.WriteAllText(filePath, csvContent.ToString(),Encoding.UTF8);
        }



        private void btSearch_Click(object sender, EventArgs e)
        {

            if (checkInfo())
            {

                String port = tb_port.Text;
                DataTable portTable = LoadCsv(tbFilePath.Text);
                IEnumerable<PortInfo> query = SearchPorts(portTable, port);
                if (query != null)
                {
                    DataTable dataTable = ConvertToDataTable(query);
                    dataGridViewResult.DataSource = dataTable;
                    dataGridViewResult.Columns[options[3]].AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;


                }
                else
                {
                    MyMessage.InfoMsg("没有找到匹配的港口");

                }
            }
        }
        private bool checkInfo()
        {
            if (tbFilePath.Text == "")
            {
                MyMessage.InfoMsg("请先选择文件");
                return false;
            }
            if (File.Exists(tbFilePath.Text) == false)
            {
                MyMessage.ErrorMsg("文件不存在");
                return false;
            }
            if (tb_port.Text == "")
            {
                MyMessage.InfoMsg("请输入港口信息");
                return false;
            }
            return true;
        }

        private void bt_save_Click(object sender, EventArgs e)
        {
            // 弹出一个保存文件对话框，让用户选择保存CSV文件的位置
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "CSV文件 (*.csv)|*.csv";
            saveFileDialog.Title = "保存为CSV文件";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                // 获取用户选择的文件路径
                string filePath = saveFileDialog.FileName;

                // 调用方法导出DataGridView内容到CSV
                ExportDataGridViewToCsv(filePath);
                MyMessage.InfoMsg("保存成功");
            }
        }
    }
}
