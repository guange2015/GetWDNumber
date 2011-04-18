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
    /// Form1 ��ժҪ˵����
    /// </summary>
    public class Form1 : System.Windows.Forms.Form
    {
        #region ����ؼ�
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

        #region �Զ������
        private bool is_start;
        string startNum;
        string endNum;
        string partNum;
        string pasueNum;
        string[] seqList = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9", "A", "B", "C", "D", "E", "F" };

        private static Mutex m = new Mutex();
        Thread myThread = null;
        private static string write_file_path =System.AppDomain.CurrentDomain.BaseDirectory + DateTime.Now.ToString("yyyyMMdd")+".txt";

        #region country
        string[,] countryCode = {
            {"268437104","San Marino"},
            {"268435540","����������"},
            {"268435576","������"},
            {"268435518","����͢"},
            {"268435516","����������������"},
            {"268435519","��³��"},
            {"268435507","����"},
            {"268435578","�����ݽ�"},
            {"268435548","����"},
            {"268435549","���������"},
            {"268435473","������"},
            {"268435601","��ɳ����"},
            {"268437206","������"},
            {"268435541","������"},
            {"268435604","������"},
            {"268436104","����ϺͰͲ���"},
            {"268435460","�µ���"},
            {"268435488","�Ĵ�����"},
            {"268437304","����"},
            {"268435595","�ͰͶ�˹"},
            {"268435490","�Ͳ����¼�����"},
            {"268435596","�͹���"},
            {"268435508","�ͻ�˹̹"},
            {"268435536","������"},
            {"268437207","����˹̹����"},
            {"268435491","����"},
            {"268435535","������"},
            {"268435522","����"},
            {"268435579","�׶���˹"},
            {"268435602","��Ľ��"},
            {"268435462","��������"},
            {"268435461","����ʱ"},
            {"268435472","����"},
            {"268435521","����ά��"},
            {"268435592","�������"},
            {"268435479","����"},
            {"268436205","��˹���Ǻͺ�����ά��"},
            {"268435542","��������"},
            {"268435520","������"},
            {"268436704","�����ɷ���"},
            {"268435543","��¡��"},
            {"268435503","����"},
            {"268435465","����"},
            {"268435468","�¹�"},
            {"268435570","���"},
            {"268435612","������ӹ��͹�"},
            {"268435605","�������"},
            {"268435495","����˹����͹�"},
            {"268435526","��϶��"},
            {"268435467","����"},
            {"268436004","��������������"},
            {"268436505","����������"},
            {"268435509","���ɱ�"},
            {"268435466","����"},
            {"268436905","��ý�"},
            {"268435599","������Ⱥ�������ά��˹Ⱥ����"},
            {"268435547","�չ�"},
            {"268435524","���ױ���"},
            {"268435525","��˹�����"},
            {"268435606","�����ɴ�"},
            {"268435470","������"},
            {"268435580","��³����"},
            {"268435607","�ϵ����յ�"},
            {"268435496","�ص�"},
            {"268435529","������"},
            {"268435581","������˹̹"},
            {"268435530","����"},
            {"268435477","����"},
            {"268435533","����������˹Ⱥ��"},
            {"268435531","�鶼��˹"},
            {"268435582","������˹˹̹"},
            {"268435551","������"},
            {"268435458","���ô�"},
            {"268435550","����"},
            {"268435597","�ݿ�"},
            {"268435464","�ݿ˹��͹�"},
            {"268435575","��Ͳ�Τ"},
            {"268435544","����¡"},
            {"268435594","������"},
            {"268435611","����Ⱥ��"},
            {"268435504","������"},
            {"268435603","���޵���"},
            {"268435553","������"},
            {"268435904","����ά��"},
            {"268435583","�����"},
            {"268435554","��������"},
            {"268435555","������"},
            {"268436304","������"},
            {"268435475","��֧��ʿ��"},
            {"268435476","¬ɭ��"},
            {"268435565","¬����"},
            {"268435481","��������"},
            {"268435556","����˹��"},
            {"268435600","�����"},
            {"268436804","�������"},
            {"268435557","����ά"},
            {"268435505","��������"},
            {"268435558","����"},
            {"268436204","�����"},
            {"268435804","���ܶ�Ⱥ��"},
            {"268435609","�������"},
            {"268435559","ë��������"},
            {"268435457","����"},
            {"268435487","������Ħ��Ⱥ��"},
            {"268435593","����ά����Ⱥ��"},
            {"268435585","�ɹ�"},
            {"268435610","����������"},
            {"268435492","�ϼ�����"},
            {"268435537","��³"},
            {"268435506","�ܿ���������Ⱥ��"},
            {"268435586","���"},
            {"268435584","Ħ������"},
            {"268435560","Ħ���"},
            {"268435561","Īɣ�ȿ�"},
            {"268435532","ī����"},
            {"268435562","���ױ���"},
            {"268435568","�Ϸ�"},
            {"268437004","�Ჴ��"},
            {"268435534","�������"},
            {"268435563","���ն�"},
            {"268435564","������"},
            {"268435478","Ų��"},
            {"268435480","������"},
            {"268435501","�ձ�"},
            {"268435483","���"},
            {"268435484","��ʿ"},
            {"268435527","�����߶�"},
            {"268436604","����ά�Ǻͺ�ɽ"},
            {"268435567","��������"},
            {"268435566","���ڼӶ�"},
            {"268435463","����·˹"},
            {"268435510","ɳ�ذ�����"},
            {"268435613","ʥ���ĺ���ά˹"},
            {"268435614","ʥ¬����"},
            {"268435615","ʥ��ɭ�غ͸����ɶ�˹"},
            {"268435512","˹������"},
            {"268435598","˹�工��"},
            {"268435704","˹��������"},
            {"268435587","������˹̹"},
            {"268435514","̨��"},
            {"268435515","̩��"},
            {"268435569","̹ɣ����"},
            {"268435616","�������Ͷ�͸�"},
            {"268435571","ͻ��˹"},
            {"268435485","������"},
            {"268435617","������Ϳ���˹Ⱥ��"},
            {"268435588","������"},
            {"268435528","Σ������"},
            {"268435539","ί������"},
            {"268435493","������³����"},
            {"268435572","�ڸɴ�"},
            {"268435589","�ڿ���"},
            {"268435538","������"},
            {"268435590","���ȱ��˹̹"},
            {"268435482","������"},
            {"268435469","ϣ��"},
            {"268435497","���"},
            {"268435552","��������"},
            {"268435511","�¼���"},
            {"268436504","�¿��������"},
            {"268435489","������"},
            {"268435471","������"},
            {"268435513","�����ǰ��������͹�"},
            {"268435608","�����"},
            {"268435577","��������"},
            {"268435517","Ҳ��"},
            {"268437204","������"},
            {"268437205","����"},
            {"268435500","��ɫ��"},
            {"268435474","�����"},
            {"268435498","ӡ��"},
            {"268435499","ӡ��������"},
            {"268435486","Ӣ��"},
            {"268435618","Ӣ��ά����Ⱥ��"},
            {"268435502","Լ��"},
            {"268435591","Խ��"},
            {"268435574","�ޱ���"},
            {"268435573","������"},
            {"268435546","է��"},
            {"268436904","ֱ������"},
            {"268435523","����"},
            {"268435545","�зǹ��͹�"},
            {"268435494","�й�"}};
        #endregion

        #endregion

        #region ϵͳ����
        /// <summary>
        /// ����������������
        /// </summary>
        private System.ComponentModel.Container components = null;

        public Form1()
        {
            //
            // Windows ���������֧���������
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
        /// ������������ʹ�õ���Դ��
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

        #region Windows ������������ɵĴ���
        /// <summary>
        /// �����֧������ķ��� - ��Ҫʹ�ô���༭���޸�
        /// �˷��������ݡ�
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
            this.label1.Text = "��ţ� ���� WMASY0815(*)";
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
            this.label2.Text = "��";
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
            this.label3.Text = "��";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(191, 279);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 9;
            this.button1.Text = "��ʼ";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Location = new System.Drawing.Point(342, 279);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 10;
            this.button2.Text = "ֹͣ";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(2, 51);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(80, 16);
            this.label5.TabIndex = 11;
            this.label5.Text = "���Ҵ���ѡ��";
            // 
            // comboBox1
            // 
            this.comboBox1.Items.AddRange(new object[] {
            "San Marino",
            "����������",
            "����������",
            "������",
            "����͢",
            "����������������",
            "��³��",
            "����",
            "�����ݽ�",
            "����",
            "���������",
            "������",
            "��ɳ����",
            "������",
            "������",
            "������",
            "����ϺͰͲ���",
            "�µ���",
            "�Ĵ�����",
            "����",
            "�ͰͶ�˹",
            "�Ͳ����¼�����",
            "�͹���",
            "�ͻ�˹̹",
            "������",
            "����˹̹����",
            "����",
            "������",
            "����",
            "�׶���˹",
            "��Ľ��",
            "��������",
            "����ʱ",
            "����",
            "����ά��",
            "�������",
            "����",
            "��˹���Ǻͺ�����ά��",
            "��������",
            "������",
            "�����ɷ���",
            "��¡��",
            "����",
            "����",
            "�¹�",
            "���",
            "������ӹ��͹�",
            "�������",
            "����˹����͹�",
            "��϶��",
            "����",
            "��������������",
            "����������",
            "���ɱ�",
            "����",
            "��ý�",
            "������Ⱥ�������ά��˹Ⱥ����",
            "�չ�",
            "���ױ���",
            "��˹�����",
            "�����ɴ�",
            "������",
            "��³����",
            "�ϵ����յ�",
            "�ص�",
            "������",
            "������˹̹",
            "����",
            "����",
            "����������˹Ⱥ��",
            "�鶼��˹",
            "������˹˹̹",
            "������",
            "���ô�",
            "����",
            "�ݿ�",
            "�ݿ˹��͹�",
            "��Ͳ�Τ",
            "����¡",
            "������",
            "����Ⱥ��",
            "������",
            "���޵���",
            "������",
            "����ά��",
            "�����",
            "��������",
            "������",
            "������",
            "��֧��ʿ��",
            "¬ɭ��",
            "¬����",
            "��������",
            "����˹��",
            "�����",
            "�������",
            "����ά",
            "��������",
            "����",
            "�����",
            "���ܶ�Ⱥ��",
            "�������",
            "ë��������",
            "����",
            "������Ħ��Ⱥ��",
            "����ά����Ⱥ��",
            "�ɹ�",
            "����������",
            "�ϼ�����",
            "��³",
            "�ܿ���������Ⱥ��",
            "���",
            "Ħ������",
            "Ħ���",
            "Īɣ�ȿ�",
            "ī����",
            "���ױ���",
            "�Ϸ�",
            "�Ჴ��",
            "�������",
            "���ն�",
            "������",
            "Ų��",
            "������",
            "�ձ�",
            "���",
            "��ʿ",
            "�����߶�",
            "����ά�Ǻͺ�ɽ",
            "��������",
            "���ڼӶ�",
            "����·˹",
            "ɳ�ذ�����",
            "ʥ���ĺ���ά˹",
            "ʥ¬����",
            "ʥ��ɭ�غ͸����ɶ�˹",
            "˹������",
            "˹�工��",
            "˹��������",
            "������˹̹",
            "̨��",
            "̩��",
            "̹ɣ����",
            "�������Ͷ�͸�",
            "ͻ��˹",
            "������",
            "������Ϳ���˹Ⱥ��",
            "������",
            "Σ������",
            "ί������",
            "������³����",
            "�ڸɴ�",
            "�ڿ���",
            "������",
            "���ȱ��˹̹",
            "������",
            "ϣ��",
            "���",
            "��������",
            "�¼���",
            "�¿��������",
            "������",
            "������",
            "�����ǰ��������͹�",
            "�����",
            "��������",
            "Ҳ��",
            "������",
            "����",
            "��ɫ��",
            "�����",
            "ӡ��",
            "ӡ��������",
            "Ӣ��",
            "Ӣ��ά����Ⱥ��",
            "Լ��",
            "Խ��",
            "�ޱ���",
            "������",
            "է��",
            "ֱ������",
            "����",
            "�зǹ��͹�",
            "�й�"});
            this.comboBox1.Location = new System.Drawing.Point(153, 47);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(121, 20);
            this.comboBox1.TabIndex = 12;
            this.comboBox1.Text = "�й�";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 52);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(41, 12);
            this.label6.TabIndex = 13;
            this.label6.Text = "���ɣ�";
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
            this.Text = "WD��Ų�ѯ";
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
        /// Ӧ�ó��������ڵ㡣
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
        //ȷ��
        private void button1_Click(object sender, System.EventArgs e)
        {
            if (is_start) { writeLog("�������ڽ�����"); return; }
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
                    f.writeLog("������Ч���������ݺ��ύ");
                    return;
                }

                int tasks = listSeq.Count / 100 + 1;
                int nowTask = 0;
                int nTask = 1;
                f.writeLog("����ʼ����ѯ��������Ϊ��" + listSeq.Count + " ����:" + tasks + " ÿ����:100��");

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
                        f.writeLog("��ʼ��ѯ��" + nTask + "������");
                        nTask++;
                        if (serialList.Length <= 0)
                        {
                            f.writeLog("������Ч���������ݺ��ύ");
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
                            if (ss[2].Equals("���ޱ���������"))
                            {
                                string line = f.comboBox1.Text + "," + ss[0] + "," + ss[1] + "," + ss[2] + "," + ss[3];
                                Debug.WriteLine(line);
                                //f.writeLog(line);
                                Export.write2txt(write_file_path,line);
                                nRigh++;
                            }
                            else nFail++;
                        }

                        f.writeLog("��"+nTask+"����"+"��ѯ���,�ɹ�:" + nRigh + " ʧ��:" + nFail);

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
            //    Thread.Sleep(1000);//����ǰ��������ָ���ĺ�����  
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
            #region ��ȡ������Ϣ
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

        //-1С��  1���� 0����
        int compareNumber(string num1, string num2)
        {
            for (int i = 0; i < startNum.Length; ++i)
            {
                if (findSeqIndex(num1[i]) > findSeqIndex(num2[i])) return 1;
                if (findSeqIndex(num1[i]) < findSeqIndex(num2[i])) return -1;
            }
            return 0;
        }

        //��ȡ������������
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

        //��ʼ��
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
        //������
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
            //ǿ�ƽ����߳�
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
                            if (data.EndsWith("&dagger;</a>")) //���⴦������
                            {
                                //ȡ� 11/17/2015&dagger;</a>
                                data = data.Substring(data.Length - "11/17/2015&dagger;</a>".Length - 1);
                                data = data.Replace("&dagger;</a>", "");
                                data = data.Replace(">", "");
                                data = data.Replace("\"", "");
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
