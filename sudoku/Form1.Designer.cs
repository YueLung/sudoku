namespace sudoku
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            this.ClearBtn = new System.Windows.Forms.Button();
            this.CalculateBtn = new System.Windows.Forms.Button();
            this.mySeparator5 = new sudoku.MySeparator();
            this.mySeparator4 = new sudoku.MySeparator();
            this.mySeparator16 = new sudoku.MySeparator();
            this.mySeparator14 = new sudoku.MySeparator();
            this.SpendTimeLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // ClearBtn
            // 
            this.ClearBtn.Font = new System.Drawing.Font("新細明體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.ClearBtn.Location = new System.Drawing.Point(785, 141);
            this.ClearBtn.Name = "ClearBtn";
            this.ClearBtn.Size = new System.Drawing.Size(119, 51);
            this.ClearBtn.TabIndex = 98;
            this.ClearBtn.Text = "清空";
            this.ClearBtn.UseVisualStyleBackColor = true;
            this.ClearBtn.Click += new System.EventHandler(this.ClearBtn_Click);
            // 
            // CalculateBtn
            // 
            this.CalculateBtn.Font = new System.Drawing.Font("新細明體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.CalculateBtn.Location = new System.Drawing.Point(785, 242);
            this.CalculateBtn.Name = "CalculateBtn";
            this.CalculateBtn.Size = new System.Drawing.Size(119, 48);
            this.CalculateBtn.TabIndex = 99;
            this.CalculateBtn.Text = "計算";
            this.CalculateBtn.UseVisualStyleBackColor = true;
            this.CalculateBtn.Click += new System.EventHandler(this.CalculateBtn_Click);
            // 
            // mySeparator5
            // 
            this.mySeparator5.isVertical = false;
            this.mySeparator5.Location = new System.Drawing.Point(27, 410);
            this.mySeparator5.Name = "mySeparator5";
            this.mySeparator5.Size = new System.Drawing.Size(707, 10);
            this.mySeparator5.TabIndex = 52;
            this.mySeparator5.Text = "mySeparator5";
            this.mySeparator5.Thickness = 4;
            // 
            // mySeparator4
            // 
            this.mySeparator4.isVertical = false;
            this.mySeparator4.Location = new System.Drawing.Point(27, 217);
            this.mySeparator4.Name = "mySeparator4";
            this.mySeparator4.Size = new System.Drawing.Size(707, 10);
            this.mySeparator4.TabIndex = 22;
            this.mySeparator4.Text = "mySeparator4";
            this.mySeparator4.Thickness = 4;
            // 
            // mySeparator16
            // 
            this.mySeparator16.isVertical = true;
            this.mySeparator16.Location = new System.Drawing.Point(242, 26);
            this.mySeparator16.Name = "mySeparator16";
            this.mySeparator16.Size = new System.Drawing.Size(10, 593);
            this.mySeparator16.TabIndex = 97;
            this.mySeparator16.Text = "mySeparator16";
            this.mySeparator16.Thickness = 4;
            // 
            // mySeparator14
            // 
            this.mySeparator14.isVertical = true;
            this.mySeparator14.Location = new System.Drawing.Point(471, 26);
            this.mySeparator14.Name = "mySeparator14";
            this.mySeparator14.Size = new System.Drawing.Size(10, 593);
            this.mySeparator14.TabIndex = 95;
            this.mySeparator14.Text = "mySeparator14";
            this.mySeparator14.Thickness = 4;
            // 
            // SpendTimeLabel
            // 
            this.SpendTimeLabel.AutoSize = true;
            this.SpendTimeLabel.Font = new System.Drawing.Font("新細明體", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.SpendTimeLabel.Location = new System.Drawing.Point(819, 309);
            this.SpendTimeLabel.Name = "SpendTimeLabel";
            this.SpendTimeLabel.Size = new System.Drawing.Size(0, 20);
            this.SpendTimeLabel.TabIndex = 100;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(926, 662);
            this.Controls.Add(this.SpendTimeLabel);
            this.Controls.Add(this.CalculateBtn);
            this.Controls.Add(this.ClearBtn);
            this.Controls.Add(this.mySeparator5);
            this.Controls.Add(this.mySeparator4);
            this.Controls.Add(this.mySeparator16);
            this.Controls.Add(this.mySeparator14);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.Text = "數獨解題";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private MySeparator mySeparator4;
        private MySeparator mySeparator5;
        private MySeparator mySeparator14;
        private MySeparator mySeparator16;
        private System.Windows.Forms.Button ClearBtn;
        private System.Windows.Forms.Button CalculateBtn;
        private System.Windows.Forms.Label SpendTimeLabel;
    }
}

