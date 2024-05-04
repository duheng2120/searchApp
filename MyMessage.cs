namespace searchApp
{
    public class MyMessage
    {
        public static void InfoMsg(string txt)
        {
            MessageBox.Show(txt, "信息提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        public static void ErrorMsg(string txt)
        {
            MessageBox.Show(txt, " 错误提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        public static bool QuestionMsg(string txt)
        {
            if (MessageBox.Show(txt, "确认请求", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

}
