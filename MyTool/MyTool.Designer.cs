namespace MyTool
{
    partial class MyTool
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrl_bind = new System.Windows.Forms.Button();
            this.ctrl_capture = new System.Windows.Forms.Button();
            this.ctrl_win_id = new System.Windows.Forms.TextBox();
            this.ctrl_file_path = new System.Windows.Forms.TextBox();
            this.ctrl_log = new System.Windows.Forms.RichTextBox();
            this.ctrl_find_img = new System.Windows.Forms.Button();
            this.ctrl_end_pos = new System.Windows.Forms.TextBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.ctrl_start_pos = new System.Windows.Forms.TextBox();
            this.ctrl_end_pick = new System.Windows.Forms.Label();
            this.ctrl_start_pick = new System.Windows.Forms.Label();
            this.ctrl_pos = new System.Windows.Forms.TextBox();
            this.ctrl_btn_click = new System.Windows.Forms.Button();
            this.ctrl_pos_pick = new System.Windows.Forms.Label();
            this.ctrl_key = new System.Windows.Forms.TextBox();
            this.ctrl_btn_key = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.tb_cood = new System.Windows.Forms.TextBox();
            this.button4 = new System.Windows.Forms.Button();
            this.chb_direct_fly = new System.Windows.Forms.CheckBox();
            this.button5 = new System.Windows.Forms.Button();
            this.txt_str = new System.Windows.Forms.TextBox();
            this.button6 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.txt_color = new System.Windows.Forms.TextBox();
            this.button7 = new System.Windows.Forms.Button();
            this.button9 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // ctrl_bind
            // 
            this.ctrl_bind.Cursor = System.Windows.Forms.Cursors.Default;
            this.ctrl_bind.Location = new System.Drawing.Point(251, 12);
            this.ctrl_bind.Name = "ctrl_bind";
            this.ctrl_bind.Size = new System.Drawing.Size(75, 23);
            this.ctrl_bind.TabIndex = 0;
            this.ctrl_bind.Text = "绑定";
            this.ctrl_bind.UseVisualStyleBackColor = true;
            this.ctrl_bind.Click += new System.EventHandler(this.ctrl_bind_Click);
            // 
            // ctrl_capture
            // 
            this.ctrl_capture.Location = new System.Drawing.Point(692, 12);
            this.ctrl_capture.Name = "ctrl_capture";
            this.ctrl_capture.Size = new System.Drawing.Size(75, 23);
            this.ctrl_capture.TabIndex = 1;
            this.ctrl_capture.Text = "截图";
            this.ctrl_capture.UseVisualStyleBackColor = true;
            this.ctrl_capture.Click += new System.EventHandler(this.ctrl_capture_Click);
            // 
            // ctrl_win_id
            // 
            this.ctrl_win_id.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ctrl_win_id.Location = new System.Drawing.Point(85, 12);
            this.ctrl_win_id.Name = "ctrl_win_id";
            this.ctrl_win_id.ReadOnly = true;
            this.ctrl_win_id.Size = new System.Drawing.Size(141, 21);
            this.ctrl_win_id.TabIndex = 2;
            // 
            // ctrl_file_path
            // 
            this.ctrl_file_path.Location = new System.Drawing.Point(85, 103);
            this.ctrl_file_path.Name = "ctrl_file_path";
            this.ctrl_file_path.Size = new System.Drawing.Size(318, 21);
            this.ctrl_file_path.TabIndex = 3;
            this.ctrl_file_path.DoubleClick += new System.EventHandler(this.ctrl_file_path_DoubleClick);
            // 
            // ctrl_log
            // 
            this.ctrl_log.Location = new System.Drawing.Point(26, 324);
            this.ctrl_log.Name = "ctrl_log";
            this.ctrl_log.Size = new System.Drawing.Size(709, 114);
            this.ctrl_log.TabIndex = 4;
            this.ctrl_log.Text = "";
            // 
            // ctrl_find_img
            // 
            this.ctrl_find_img.Location = new System.Drawing.Point(430, 101);
            this.ctrl_find_img.Name = "ctrl_find_img";
            this.ctrl_find_img.Size = new System.Drawing.Size(75, 23);
            this.ctrl_find_img.TabIndex = 5;
            this.ctrl_find_img.Text = "查找图片";
            this.ctrl_find_img.UseVisualStyleBackColor = true;
            this.ctrl_find_img.Click += new System.EventHandler(this.ctrl_find_img_Click);
            // 
            // ctrl_end_pos
            // 
            this.ctrl_end_pos.Location = new System.Drawing.Point(546, 14);
            this.ctrl_end_pos.Name = "ctrl_end_pos";
            this.ctrl_end_pos.Size = new System.Drawing.Size(93, 21);
            this.ctrl_end_pos.TabIndex = 6;
            this.ctrl_end_pos.Text = "2000,2000";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(24, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 12);
            this.label1.TabIndex = 7;
            this.label1.Text = "窗口名";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(347, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(29, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "区域";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(24, 106);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 9;
            this.label3.Text = "图片路径";
            // 
            // ctrl_start_pos
            // 
            this.ctrl_start_pos.Location = new System.Drawing.Point(408, 14);
            this.ctrl_start_pos.Name = "ctrl_start_pos";
            this.ctrl_start_pos.Size = new System.Drawing.Size(89, 21);
            this.ctrl_start_pos.TabIndex = 10;
            this.ctrl_start_pos.Text = "0,0";
            // 
            // ctrl_end_pick
            // 
            this.ctrl_end_pick.AutoSize = true;
            this.ctrl_end_pick.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ctrl_end_pick.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctrl_end_pick.Location = new System.Drawing.Point(645, 11);
            this.ctrl_end_pick.Name = "ctrl_end_pick";
            this.ctrl_end_pick.Size = new System.Drawing.Size(22, 24);
            this.ctrl_end_pick.TabIndex = 12;
            this.ctrl_end_pick.Text = "+";
            // 
            // ctrl_start_pick
            // 
            this.ctrl_start_pick.AutoSize = true;
            this.ctrl_start_pick.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ctrl_start_pick.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctrl_start_pick.Location = new System.Drawing.Point(503, 13);
            this.ctrl_start_pick.Name = "ctrl_start_pick";
            this.ctrl_start_pick.Size = new System.Drawing.Size(22, 24);
            this.ctrl_start_pick.TabIndex = 13;
            this.ctrl_start_pick.Text = "+";
            // 
            // ctrl_pos
            // 
            this.ctrl_pos.Location = new System.Drawing.Point(85, 143);
            this.ctrl_pos.Name = "ctrl_pos";
            this.ctrl_pos.Size = new System.Drawing.Size(100, 21);
            this.ctrl_pos.TabIndex = 14;
            this.ctrl_pos.Text = "10,10";
            // 
            // ctrl_btn_click
            // 
            this.ctrl_btn_click.Location = new System.Drawing.Point(241, 141);
            this.ctrl_btn_click.Name = "ctrl_btn_click";
            this.ctrl_btn_click.Size = new System.Drawing.Size(75, 23);
            this.ctrl_btn_click.TabIndex = 15;
            this.ctrl_btn_click.Text = "左键点击";
            this.ctrl_btn_click.UseVisualStyleBackColor = true;
            this.ctrl_btn_click.Click += new System.EventHandler(this.ctrl_btn_click_Click);
            // 
            // ctrl_pos_pick
            // 
            this.ctrl_pos_pick.AutoSize = true;
            this.ctrl_pos_pick.Cursor = System.Windows.Forms.Cursors.Cross;
            this.ctrl_pos_pick.Font = new System.Drawing.Font("宋体", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.ctrl_pos_pick.Location = new System.Drawing.Point(191, 143);
            this.ctrl_pos_pick.Name = "ctrl_pos_pick";
            this.ctrl_pos_pick.Size = new System.Drawing.Size(22, 24);
            this.ctrl_pos_pick.TabIndex = 16;
            this.ctrl_pos_pick.Text = "+";
            // 
            // ctrl_key
            // 
            this.ctrl_key.Location = new System.Drawing.Point(85, 192);
            this.ctrl_key.Name = "ctrl_key";
            this.ctrl_key.ReadOnly = true;
            this.ctrl_key.Size = new System.Drawing.Size(100, 21);
            this.ctrl_key.TabIndex = 17;
            this.ctrl_key.KeyDown += new System.Windows.Forms.KeyEventHandler(this.ctrl_key_KeyDown);
            // 
            // ctrl_btn_key
            // 
            this.ctrl_btn_key.Location = new System.Drawing.Point(216, 190);
            this.ctrl_btn_key.Name = "ctrl_btn_key";
            this.ctrl_btn_key.Size = new System.Drawing.Size(75, 23);
            this.ctrl_btn_key.TabIndex = 18;
            this.ctrl_btn_key.Text = "按键";
            this.ctrl_btn_key.UseVisualStyleBackColor = true;
            this.ctrl_btn_key.Click += new System.EventHandler(this.ctrl_btn_key_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(341, 141);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 19;
            this.button1.Text = "右键点击";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(85, 240);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 20;
            this.button2.Text = "附魂截图";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(195, 240);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 21;
            this.button3.Text = "放技能";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // tb_cood
            // 
            this.tb_cood.Location = new System.Drawing.Point(341, 241);
            this.tb_cood.Name = "tb_cood";
            this.tb_cood.Size = new System.Drawing.Size(100, 21);
            this.tb_cood.TabIndex = 22;
            this.tb_cood.Text = "20,20";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(521, 239);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 23;
            this.button4.Text = "寻路";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // chb_direct_fly
            // 
            this.chb_direct_fly.AutoSize = true;
            this.chb_direct_fly.Location = new System.Drawing.Point(447, 244);
            this.chb_direct_fly.Name = "chb_direct_fly";
            this.chb_direct_fly.Size = new System.Drawing.Size(60, 16);
            this.chb_direct_fly.TabIndex = 24;
            this.chb_direct_fly.Text = "直接飞";
            this.chb_direct_fly.UseVisualStyleBackColor = true;
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(85, 283);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(100, 23);
            this.button5.TabIndex = 25;
            this.button5.Text = "附魂血量检测";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // txt_str
            // 
            this.txt_str.Location = new System.Drawing.Point(85, 60);
            this.txt_str.Name = "txt_str";
            this.txt_str.Size = new System.Drawing.Size(100, 21);
            this.txt_str.TabIndex = 26;
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(349, 58);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 27;
            this.button6.Text = "查找文字";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(48, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 12);
            this.label4.TabIndex = 28;
            this.label4.Text = "文字";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(197, 66);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 12);
            this.label5.TabIndex = 30;
            this.label5.Text = "颜色";
            // 
            // txt_color
            // 
            this.txt_color.Location = new System.Drawing.Point(232, 60);
            this.txt_color.Name = "txt_color";
            this.txt_color.Size = new System.Drawing.Size(100, 21);
            this.txt_color.TabIndex = 29;
            this.txt_color.Text = "000000-555555";
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(521, 101);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 31;
            this.button7.Text = "整理图片";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(232, 283);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(94, 23);
            this.button9.TabIndex = 33;
            this.button9.Text = "检测附魂敌人";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Click += new System.EventHandler(this.button9_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(447, 141);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(88, 23);
            this.button8.TabIndex = 34;
            this.button8.Text = "过滤字体图片";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // MyTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txt_color);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.txt_str);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.chb_direct_fly);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.tb_cood);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.ctrl_btn_key);
            this.Controls.Add(this.ctrl_key);
            this.Controls.Add(this.ctrl_pos_pick);
            this.Controls.Add(this.ctrl_btn_click);
            this.Controls.Add(this.ctrl_pos);
            this.Controls.Add(this.ctrl_start_pick);
            this.Controls.Add(this.ctrl_end_pick);
            this.Controls.Add(this.ctrl_start_pos);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.ctrl_end_pos);
            this.Controls.Add(this.ctrl_find_img);
            this.Controls.Add(this.ctrl_log);
            this.Controls.Add(this.ctrl_file_path);
            this.Controls.Add(this.ctrl_win_id);
            this.Controls.Add(this.ctrl_capture);
            this.Controls.Add(this.ctrl_bind);
            this.Name = "MyTool";
            this.Text = "小工具";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form1_FormClosed);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button ctrl_bind;
        private System.Windows.Forms.Button ctrl_capture;
        private System.Windows.Forms.TextBox ctrl_win_id;
        private System.Windows.Forms.TextBox ctrl_file_path;
        private System.Windows.Forms.RichTextBox ctrl_log;
        private System.Windows.Forms.Button ctrl_find_img;
        private System.Windows.Forms.TextBox ctrl_end_pos;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox ctrl_start_pos;
        private System.Windows.Forms.Label ctrl_end_pick;
        private System.Windows.Forms.Label ctrl_start_pick;
        private System.Windows.Forms.TextBox ctrl_pos;
        private System.Windows.Forms.Button ctrl_btn_click;
        private System.Windows.Forms.Label ctrl_pos_pick;
        private System.Windows.Forms.TextBox ctrl_key;
        private System.Windows.Forms.Button ctrl_btn_key;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.TextBox tb_cood;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.CheckBox chb_direct_fly;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.TextBox txt_str;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_color;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button9;
        private System.Windows.Forms.Button button8;
    }
}

