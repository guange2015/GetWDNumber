using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.IO;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Configuration;

namespace GetWDNumber
{
    /// <summary>
    /// Form1 的摘要说明。
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        #region 界面控件
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox comboBox1;

        private Label label6;
        private Label label7;
        private GroupBox groupBox1;
        private GroupBox groupBox2;
        private ListBox listBox1;
        #endregion

        #region 自定义变量
        private bool is_start;
        string startNum;
        string endNum;
        string partNum;
        string pasueNum;
        string[] seqList = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

        private static Mutex m = new Mutex();
        Thread myThread = null;
        private static string write_file_path =System.AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("yyyyMMdd")+".csv";

        #region country
        string[,] countryCode = {
            {"268437104","San Marino"},
            {"268435540","阿尔及利亚"},
            {"268435576","阿富汗"},
            {"268435518","阿根廷"},
            {"268435516","阿拉伯联合酋长国"},
            {"268435519","阿鲁巴"},
            {"268435507","阿曼"},
            {"268435578","阿塞拜疆"},
            {"268435548","埃及"},
            {"268435549","埃塞俄比亚"},
            {"268435473","爱尔兰"},
            {"268435601","爱沙尼亚"},
            {"268437206","安道尔"},
            {"268435541","安哥拉"},
            {"268435604","安圭拉"},
            {"268436104","安提瓜和巴布达"},
            {"268435460","奥地利"},
            {"268435488","澳大利亚"},
            {"268437304","澳门"},
            {"268435595","巴巴多斯"},
            {"268435490","巴布亚新几内亚"},
            {"268435596","巴哈马"},
            {"268435508","巴基斯坦"},
            {"268435536","巴拉圭"},
            {"268437207","巴勒斯坦领土"},
            {"268435491","巴林"},
            {"268435535","巴拿马"},
            {"268435522","巴西"},
            {"268435579","白俄罗斯"},
            {"268435602","百慕大"},
            {"268435462","保加利亚"},
            {"268435461","比利时"},
            {"268435472","冰岛"},
            {"268435521","玻利维亚"},
            {"268435592","波多黎哥"},
            {"268435479","波兰"},
            {"268436205","波斯尼亚和黑塞哥维那"},
            {"268435542","博茨瓦纳"},
            {"268435520","伯利兹"},
            {"268436704","布吉纳法索"},
            {"268435543","布隆迪"},
            {"268435503","朝鲜"},
            {"268435465","丹麦"},
            {"268435468","德国"},
            {"268435570","多哥"},
            {"268435612","多米尼加共和国"},
            {"268435605","多米尼克"},
            {"268435495","俄罗斯联邦共和国"},
            {"268435526","厄瓜多尔"},
            {"268435467","法国"},
            {"268436004","法属玻利尼西亚"},
            {"268436505","法属圭亚那"},
            {"268435509","菲律宾"},
            {"268435466","芬兰"},
            {"268436905","佛得角"},
            {"268435599","福克兰群岛（马尔维纳斯群岛）"},
            {"268435547","刚果"},
            {"268435524","哥伦比亚"},
            {"268435525","哥斯达黎加"},
            {"268435606","格林纳达"},
            {"268435470","格陵兰"},
            {"268435580","格鲁吉亚"},
            {"268435607","瓜德罗普岛"},
            {"268435496","关岛"},
            {"268435529","圭那亚"},
            {"268435581","哈萨克斯坦"},
            {"268435530","海地"},
            {"268435477","荷兰"},
            {"268435533","荷属安的列斯群岛"},
            {"268435531","洪都拉斯"},
            {"268435582","吉尔吉斯斯坦"},
            {"268435551","几内亚"},
            {"268435458","加拿大"},
            {"268435550","加纳"},
            {"268435597","捷克"},
            {"268435464","捷克共和国"},
            {"268435575","津巴布韦"},
            {"268435544","喀麦隆"},
            {"268435594","卡塔尔"},
            {"268435611","开曼群岛"},
            {"268435504","科威特"},
            {"268435603","克罗地亚"},
            {"268435553","肯尼亚"},
            {"268435904","拉托维亚"},
            {"268435583","黎巴嫩"},
            {"268435554","利比里亚"},
            {"268435555","利比亚"},
            {"268436304","立陶宛"},
            {"268435475","列支敦士登"},
            {"268435476","卢森堡"},
            {"268435565","卢旺达"},
            {"268435481","罗马尼亚"},
            {"268435556","马达加斯加"},
            {"268435600","马耳他"},
            {"268436804","马尔代夫"},
            {"268435557","马拉维"},
            {"268435505","马来西亚"},
            {"268435558","马里"},
            {"268436204","马其顿"},
            {"268435804","马绍尔群岛"},
            {"268435609","马提尼克"},
            {"268435559","毛里塔尼亚"},
            {"268435457","美国"},
            {"268435487","美属萨摩亚群岛"},
            {"268435593","美属维尔京群岛"},
            {"268435585","蒙古"},
            {"268435610","蒙特塞拉特"},
            {"268435492","孟加拉国"},
            {"268435537","秘鲁"},
            {"268435506","密克罗尼西亚群岛"},
            {"268435586","缅甸"},
            {"268435584","摩尔多瓦"},
            {"268435560","摩洛哥"},
            {"268435561","莫桑比克"},
            {"268435532","墨西哥"},
            {"268435562","纳米比亚"},
            {"268435568","南非"},
            {"268437004","尼泊尔"},
            {"268435534","尼加拉瓜"},
            {"268435563","尼日尔"},
            {"268435564","尼日利"},
            {"268435478","挪威"},
            {"268435480","葡萄牙"},
            {"268435501","日本"},
            {"268435483","瑞典"},
            {"268435484","瑞士"},
            {"268435527","萨尔瓦多"},
            {"268436604","塞尔维亚和黑山"},
            {"268435567","塞拉利昂"},
            {"268435566","塞内加尔"},
            {"268435463","塞浦路斯"},
            {"268435510","沙特阿拉伯"},
            {"268435613","圣基茨和尼维斯"},
            {"268435614","圣卢西亚"},
            {"268435615","圣文森特和格林纳丁斯"},
            {"268435512","斯里兰卡"},
            {"268435598","斯洛伐克"},
            {"268435704","斯洛文尼亚"},
            {"268435587","塔吉克斯坦"},
            {"268435514","台湾"},
            {"268435515","泰国"},
            {"268435569","坦桑尼亚"},
            {"268435616","特立尼达和多巴哥"},
            {"268435571","突尼斯"},
            {"268435485","土耳其"},
            {"268435617","土尔其和凯科斯群岛"},
            {"268435588","土库曼"},
            {"268435528","危地马拉"},
            {"268435539","委内瑞拉"},
            {"268435493","文莱达鲁萨兰"},
            {"268435572","乌干达"},
            {"268435589","乌克兰"},
            {"268435538","乌拉圭"},
            {"268435590","乌兹别克斯坦"},
            {"268435482","西班牙"},
            {"268435469","希腊"},
            {"268435497","香港"},
            {"268435552","象牙海岸"},
            {"268435511","新加坡"},
            {"268436504","新喀里多尼亚"},
            {"268435489","新西兰"},
            {"268435471","匈牙利"},
            {"268435513","叙利亚阿拉伯共和国"},
            {"268435608","牙买加"},
            {"268435577","亚美尼亚"},
            {"268435517","也门"},
            {"268437204","伊拉克"},
            {"268437205","伊朗"},
            {"268435500","以色列"},
            {"268435474","意大利"},
            {"268435498","印度"},
            {"268435499","印度尼西亚"},
            {"268435486","英国"},
            {"268435618","英属维尔京群岛"},
            {"268435502","约旦"},
            {"268435591","越南"},
            {"268435574","赞比亚"},
            {"268435573","扎伊尔"},
            {"268435546","乍得"},
            {"268436904","直布罗陀"},
            {"268435523","智利"},
            {"268435545","中非共和国"},
            {"268435494","中国"}};
        #endregion

        #endregion

        #region 系统生成
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Windows 窗体设计器支持所必需的
            //
            InitializeComponent();

            setStartAndEnd(1);

            MyConfigSection config = readConfig();
            if (config != null)
            {
                this.textBox1.Text = config.PartNumber;
                if (config.PasueNumber == null || config.PasueNumber.Length <= 0)
                {
                    this.textBox2.Text = config.StartNumber;
                }
                else
                {
                    this.textBox2.Text = config.PasueNumber;
                }
                this.textBox3.Text = config.EndNumber;
                this.comboBox1.Text = config.Country;
            }

        }

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码
        /// <summary>
        /// 设计器支持所需的方法 - 不要使用代码编辑器修改
        /// 此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(2, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(157, 14);
            this.label1.TabIndex = 0;
            this.label1.Text = "序号： 例如 WMASY0815(*)";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(153, 18);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(121, 21);
            this.textBox1.TabIndex = 1;
            this.textBox1.TextChanged += new System.EventHandler(this.textBox1_TextChanged);
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(13, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(16, 23);
            this.label2.TabIndex = 2;
            this.label2.Text = "从";
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(37, 16);
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(80, 21);
            this.textBox2.TabIndex = 3;
            this.textBox2.Text = "0";
            this.textBox2.TextChanged += new System.EventHandler(this.textBox2_TextChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(163, 16);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(89, 21);
            this.textBox3.TabIndex = 5;
            this.textBox3.Text = "F";
            this.textBox3.TextChanged += new System.EventHandler(this.textBox3_TextChanged);
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(136, 19);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(16, 23);
            this.label3.TabIndex = 4;
            this.label3.Text = "到";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(191, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "开始";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(342, 279);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "停止";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(2, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "国家代码选择";
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            "San Marino",
            "阿尔巴尼亚",
            "阿尔及利亚",
            "阿富汗",
            "阿根廷",
            "阿拉伯联合酋长国",
            "阿鲁巴",
            "阿曼",
            "阿塞拜疆",
            "埃及",
            "埃塞俄比亚",
            "爱尔兰",
            "爱沙尼亚",
            "安道尔",
            "安哥拉",
            "安圭拉",
            "安提瓜和巴布达",
            "奥地利",
            "澳大利亚",
            "澳门",
            "巴巴多斯",
            "巴布亚新几内亚",
            "巴哈马",
            "巴基斯坦",
            "巴拉圭",
            "巴勒斯坦领土",
            "巴林",
            "巴拿马",
            "巴西",
            "白俄罗斯",
            "百慕大",
            "保加利亚",
            "比利时",
            "冰岛",
            "玻利维亚",
            "波多黎哥",
            "波兰",
            "波斯尼亚和黑塞哥维那",
            "博茨瓦纳",
            "伯利兹",
            "布吉纳法索",
            "布隆迪",
            "朝鲜",
            "丹麦",
            "德国",
            "多哥",
            "多米尼加共和国",
            "多米尼克",
            "俄罗斯联邦共和国",
            "厄瓜多尔",
            "法国",
            "法属玻利尼西亚",
            "法属圭亚那",
            "菲律宾",
            "芬兰",
            "佛得角",
            "福克兰群岛（马尔维纳斯群岛）",
            "刚果",
            "哥伦比亚",
            "哥斯达黎加",
            "格林纳达",
            "格陵兰",
            "格鲁吉亚",
            "瓜德罗普岛",
            "关岛",
            "圭那亚",
            "哈萨克斯坦",
            "海地",
            "荷兰",
            "荷属安的列斯群岛",
            "洪都拉斯",
            "吉尔吉斯斯坦",
            "几内亚",
            "加拿大",
            "加纳",
            "捷克",
            "捷克共和国",
            "津巴布韦",
            "喀麦隆",
            "卡塔尔",
            "开曼群岛",
            "科威特",
            "克罗地亚",
            "肯尼亚",
            "拉托维亚",
            "黎巴嫩",
            "利比里亚",
            "利比亚",
            "立陶宛",
            "列支敦士登",
            "卢森堡",
            "卢旺达",
            "罗马尼亚",
            "马达加斯加",
            "马耳他",
            "马尔代夫",
            "马拉维",
            "马来西亚",
            "马里",
            "马其顿",
            "马绍尔群岛",
            "马提尼克",
            "毛里塔尼亚",
            "美国",
            "美属萨摩亚群岛",
            "美属维尔京群岛",
            "蒙古",
            "蒙特塞拉特",
            "孟加拉国",
            "秘鲁",
            "密克罗尼西亚群岛",
            "缅甸",
            "摩尔多瓦",
            "摩洛哥",
            "莫桑比克",
            "墨西哥",
            "纳米比亚",
            "南非",
            "尼泊尔",
            "尼加拉瓜",
            "尼日尔",
            "尼日利",
            "挪威",
            "葡萄牙",
            "日本",
            "瑞典",
            "瑞士",
            "萨尔瓦多",
            "塞尔维亚和黑山",
            "塞拉利昂",
            "塞内加尔",
            "塞浦路斯",
            "沙特阿拉伯",
            "圣基茨和尼维斯",
            "圣卢西亚",
            "圣文森特和格林纳丁斯",
            "斯里兰卡",
            "斯洛伐克",
            "斯洛文尼亚",
            "塔吉克斯坦",
            "台湾",
            "泰国",
            "坦桑尼亚",
            "特立尼达和多巴哥",
            "突尼斯",
            "土耳其",
            "土尔其和凯科斯群岛",
            "土库曼",
            "危地马拉",
            "委内瑞拉",
            "文莱达鲁萨兰",
            "乌干达",
            "乌克兰",
            "乌拉圭",
            "乌兹别克斯坦",
            "西班牙",
            "希腊",
            "香港",
            "象牙海岸",
            "新加坡",
            "新喀里多尼亚",
            "新西兰",
            "匈牙利",
            "叙利亚阿拉伯共和国",
            "牙买加",
            "亚美尼亚",
            "也门",
            "伊拉克",
            "伊朗",
            "以色列",
            "意大利",
            "印度",
            "印度尼西亚",
            "英国",
            "英属维尔京群岛",
            "约旦",
            "越南",
            "赞比亚",
            "扎伊尔",
            "乍得",
            "直布罗陀",
            "智利",
            "中非共和国",
            "中国"});
            this.comboBox1.Location = new System.Drawing.Point(153, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Text = "中国";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "生成：";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(60, 51);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(0, 12);
            this.label7.TabIndex = 14;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.label6);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Location = new System.Drawing.Point(300, 4);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(288, 79);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.comboBox1);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.textBox1);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Location = new System.Drawing.Point(6, 4);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(284, 79);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 12;
            this.listBox1.Location = new System.Drawing.Point(8, 98);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(580, 172);
            this.listBox1.TabIndex = 17;
            // 
            // Form1
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(6, 14);
            this.ClientSize = new System.Drawing.Size(600, 314);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "WD序号查询";
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }
        #endregion

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Control.CheckForIllegalCrossThreadCalls = false;
            Application.Run(new Form1());
        }
        #endregion

        const string sResponseEncoding = "gb2312";
        //http://websupport.wdc.com/warranty/serialinput.asp?custtype=end&requesttype=warranty&lang=cn
        //http://websupport.wdc.com/warranty/serialinput.asp
        //确定
        private void button1_Click(object sender, System.EventArgs e)
        {
            if (is_start) { writeLog("任务正在进行中"); return; }
            is_start = true;
            this.button1.Enabled = false;
            this.button2.Enabled = true;
            ParameterizedThreadStart ParStart = new ParameterizedThreadStart(ThreadProc);
            myThread = new Thread(ParStart);
            myThread.Start(this);

        }

        public static void ThreadProc(object o)
        {
            Form1 f = (Form1)o;
            try
            {

                string serialList = string.Empty;
                List<string> listSeq = f.getWildCardSeq();
                if (listSeq == null || listSeq.Count <= 0)
                {
                    f.writeLog("数据无效，请检查数据后提交");
                    return;
                }

                int tasks = listSeq.Count / 100 + 1;
                int nowTask = 0;
                int nTask = 1;
                f.writeLog("任务开始，查询数据总数为：" + listSeq.Count + " 批次:" + tasks + " 每批次:100个");

                listSeq.Add("----------");
                string nowNum = "";
                foreach (string s in listSeq)
                {
                    bool isLastLine = false;
                    if ("----------".Equals(s))
                    {
                        isLastLine = true;
                    }
                    if (!isLastLine)
                    {
                        serialList += f.partNum + s;
                        serialList += ",";
                        nowNum = s;
                    }

                    nowTask++;
                    if (nowTask % 100 == 0 || isLastLine)
                    {
                        f.writeLog("开始查询第" + nTask + "批数据");
                        nTask++;
                        if (serialList.Length <= 0)
                        {
                            f.writeLog("数据无效，请检查数据后提交");
                            return;
                        }
                        serialList = serialList.Substring(0, serialList.Length - 1);
                        string countryCode = f.findCountryCode(f.comboBox1.Text.Trim());

                        string responseData = f.postData(countryCode, serialList);
                        List<string[]> l = parseData(responseData);

                        int nRigh = 0;
                        int nFail = 0;
                        foreach (string[] ss in l)
                        {
                            if (ss[2].Equals("有限保修期限内"))
                            {
                                string line = f.comboBox1.Text + "," + ss[0] + "," + ss[1] + "," + ss[2] + "," + ss[3];
                                Debug.WriteLine(line);
                                //f.writeLog(line);
                                Export.write2txt(write_file_path,line);
                                nRigh++;
                            }
                            else nFail++;
                        }

                        f.writeLog("第"+nTask+"批次"+"查询完毕,成功:" + nRigh + " 失败:" + nFail);

                        f.pasueNum = nowNum;

                        serialList = string.Empty;
                        nowTask = 0;
                    }
                }
            }
            catch (Exception e)
            {
                f.writeLog(e.Message);
            }
            finally
            {
                f.is_start = false;
                f.button1.Enabled = true;
                f.button2.Enabled = false;
            }



            //for (int i = 0; i < 10; i++)
            //{
            //    Console.WriteLine("ThreadPorc:{0}", i);
            //    Thread.Sleep(1000);//将当前进程阻塞指定的毫秒数  
            //}
        }

        void writeLog(string log)
        {
            m.WaitOne();
            this.listBox1.Items.Add(log);
            this.listBox1.TopIndex = this.listBox1.Items.Count - (int)(this.listBox1.Height / this.listBox1.ItemHeight);
            m.ReleaseMutex();
        }

        private string postData(string countryCode, string serialList)
        {
            CookieContainer cc = new CookieContainer();

            String sDesUrl;
            cc.Add(getCookies(out sDesUrl));
            HttpWebRequest HttpWReq =
            (HttpWebRequest)WebRequest.Create("http://websupport.wdc.com/warranty/serialinput.asp");

            HttpWReq.CookieContainer = cc;
            HttpWReq.Method = "POST";
            HttpWReq.ContentType = "application/x-www-form-urlencoded";
            string postdata = "NoErrorMessage=false&ispostback=y&cmd=continue&custtype=end&requesttype=warranty&countryobjid=" + countryCode + "&seriallist=" + serialList + "&btncontinue=%BC%CC%D0%F8";
            //string postdata =   "NoErrorMessage=false&ispostback=y&cmd=continue&custtype=end&requesttype=warranty&countryobjid=268435494&seriallist=WM1234567890%2CXW1729L1X8400053603&btncontinue=%BC%CC%D0%F8";

            byte[] byte1 = Encoding.ASCII.GetBytes(postdata);
            HttpWReq.ContentLength = byte1.Length;

            Stream poststream = HttpWReq.GetRequestStream();
            poststream.Write(byte1, 0, byte1.Length);
            poststream.Close();

            HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            // Insert code that uses the response object.
            #region 读取返回信息
            Stream responseStream = HttpWResp.GetResponseStream();
            string stringResponse = string.Empty;
            using (StreamReader responseReader = new StreamReader(
                responseStream, Encoding.GetEncoding(sResponseEncoding)))
            {
                stringResponse = responseReader.ReadToEnd();
            }
            responseStream.Close();
            //Debug.WriteLine(stringResponse);
            #endregion

            //FileStream file = File.Open(@"d:\111.txt", FileMode.OpenOrCreate);
            //byte[] data = Encoding.GetEncoding(sResponseEncoding).GetBytes(stringResponse);
            //file.Write(data, 0, data.Length);
            //file.Close();
            HttpWResp.Close();
            return stringResponse;
        }

        CookieCollection getCookies(out String sDesUrl)
        {
            sDesUrl = "";
            CookieCollection ckclReturn = new CookieCollection();
            CookieContainer cc = new CookieContainer();
            HttpWebRequest HttpWReq =
            (HttpWebRequest)WebRequest.Create("http://websupport.wdc.com/warranty/serialinput.asp?custtype=end&requesttype=warranty&lang=cn");
            HttpWReq.CookieContainer = cc;

            HttpWebResponse HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            int i = HttpWResp.ResponseUri.AbsoluteUri.IndexOf("?url=");
            if (i > 0)
            {
                sDesUrl = "http://websupport.wdc.com" + HttpWResp.ResponseUri.AbsoluteUri.Substring(i + 5);
            }


            // Cookie cookie = cc.Cookies["ASPSESSIONIDQQDDACRS"];
            ckclReturn = cc.GetCookies(HttpWResp.ResponseUri);

            HttpWResp.Close();


            HttpWReq =
            (HttpWebRequest)WebRequest.Create(sDesUrl);
            HttpWReq.CookieContainer = cc;
            HttpWResp = (HttpWebResponse)HttpWReq.GetResponse();
            HttpWResp.Close();
            HttpWResp = null;

            return ckclReturn;
        }

        string findCountryCode(string country)
        {
            for (int i = 0; i < countryCode.GetLength(0); ++i)
            {
                if (countryCode[i, 1].Equals(country)) return countryCode[i, 0];
            }
            return "";
        }

        private void setStartAndEnd(int len)
        {
            this.textBox2.Text = "";
            this.textBox3.Text = "";
            for (int i = 0; i < len; ++i)
            {
                this.textBox2.Text += "0";
                this.textBox3.Text += "F";
            }
        }

        int findSeqIndex(string s)
        {
            for (int i = 0; i < seqList.Length; ++i)
            {
                if (seqList[i].Equals(s)) return i;
            }
            return -1;
        }

        int findSeqIndex(char c)
        {
            return findSeqIndex(c.ToString());
        }

        string addNum(string s, int index, int num)
        {
            string ss;
            char[] bytes = s.ToCharArray();
            if (findSeqIndex(s[index]) + num < seqList.Length)
            {
                int n = findSeqIndex(s[index]) + num;
                bytes[index] = seqList[n].ToCharArray()[0];
                ss = new string(bytes);
            }
            else
            {
                int n = findSeqIndex(s[index]) + num - seqList.Length;
                bytes[index] = seqList[n].ToCharArray()[0];
                ss = new string(bytes);
                ss = addNum(ss, index - 1, 1);
            }
            return ss;
        }

        //-1小于  1大于 0等于
        int compareNumber(string num1, string num2)
        {
            for (int i = 0; i < startNum.Length; ++i)
            {
                if (findSeqIndex(num1[i]) > findSeqIndex(num2[i])) return 1;
                if (findSeqIndex(num1[i]) < findSeqIndex(num2[i])) return -1;
            }
            return 0;
        }

        //获取所有任务序列
        List<string> getWildCardSeq()
        {
            if (partNum == null || partNum.Length < 3) return null;
            if (startNum == null || endNum == null) return null;
            if (startNum.Length != endNum.Length) return null;
            if (compareNumber(startNum, endNum) != -1) return null;
            List<string> l = new List<string>();

            string s = startNum;
            string ss = endNum;
            l.Add(s);
            while (0 != compareNumber(s, ss))
            {
                s = addNum(s, s.Length - 1, 1);
                l.Add(s);
            }

            return l;
        }

        //开始符
        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            startNum = this.textBox2.Text.Trim();
            makeSeqList();
        }

        private void makeSeqList()
        {
            List<string> l = getWildCardSeq();
            if (l == null || l.Count <= 0) { this.label7.Text = ""; }
            else
            {
                this.label7.Text = partNum + l[0] + "---" + partNum + l[l.Count - 1];
            }
        }
        //结束符
        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            endNum = this.textBox3.Text.Trim();
            makeSeqList();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            partNum = this.textBox1.Text.Trim();
            makeSeqList();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //强制结束线程
            try
            {
                myThread.Abort();
            }
            catch (Exception e1)
            {

            }
        }

        private static List<string[]> parseData(string s)
        {
            string pattern = "<table border=\"0\".*>((.|\r|\n)*?)</table>";
            Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            MatchCollection matches = rgx.Matches(s);
            string content = "";
            if (matches.Count == 1)
            {
                foreach (Match match in matches)
                {
                    content = match.Value;
                }
            }

            pattern = "<tr>(?<word>(.|\r|\n)*?)</tr>";
            rgx = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Multiline);
            matches = rgx.Matches(content);

            List<string[]> l = new List<string[]>();
            if (matches.Count > 0)
            {
                foreach (Match match in matches)
                {
                    string trContent = match.Value;

                    string pattern1 = "<td valign=\"middle\" nowrap>(<a.*?>)*(?<word>(.|\r|\n)*?)(</a>)*</td>";
                    Regex rgx1 = new Regex(pattern1, RegexOptions.IgnoreCase | RegexOptions.Multiline);
                    MatchCollection matches1 = rgx1.Matches(trContent);

                    if (matches1.Count == 4)
                    {
                        List<string> ss = new List<string>();
                        foreach (Match match1 in matches1)
                        {
                            GroupCollection groups = match1.Groups;
                            //Debug.WriteLine(groups["word"].Value.Trim());

                            string data = groups["word"].Value.Trim();
                            if (data.EndsWith("&dagger;</a>")) //特殊处理日期
                            {
                                //取最长 11/17/2015&dagger;</a>
                                data = data.Substring(data.Length - "11/17/2015&dagger;</a>".Length - 1);
                                data = data.Replace("&dagger;</a>", "");
                                data = data.Replace(">", "");
                                data = data.Replace("\"", "");
                                data = data.Replace(")", "");
                            }
                            ss.Add(data);
                        }
                        l.Add(ss.ToArray());
                    }

                }
            }
            return l;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            saveConfig();
        }

        MyConfigSection readConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            MyConfigSection data = config.Sections["last"] as MyConfigSection;

            return data;
        }

        private void saveConfig()
        {
            Configuration config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            MyConfigSection data = new MyConfigSection();
            data.OperTime = DateTime.Now;
            data.PartNumber = this.textBox1.Text.Trim();
            data.StartNumber = this.textBox2.Text.Trim();
            data.EndNumber = this.textBox3.Text.Trim();
            data.Country = this.comboBox1.Text.Trim();
            data.PasueNumber = pasueNum;
            
            //config.SectionGroups.Remove("last");
            config.Sections.Remove("last");
            config.Sections.Add("last", data);
            config.Save(ConfigurationSaveMode.Minimal);
        }

    }
}
