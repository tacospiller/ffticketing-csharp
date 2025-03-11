namespace ffTicketingCsharp
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
            _submitButton.Dispose();
            _returnButton.Dispose();
            _cts.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 30F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label1.Location = new Point(83, 194);
            label1.Name = "label1";
            label1.Size = new Size(604, 54);
            label1.TabIndex = 0;
            label1.Text = "시작하려면 엔터 종료하려면 esc";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("맑은 고딕", 30F, FontStyle.Bold, GraphicsUnit.Point, 129);
            label2.Location = new Point(83, 109);
            label2.Name = "label2";
            label2.Size = new Size(599, 54);
            label2.TabIndex = 1;
            label2.Text = "이 창을 티케팅 팝업에 맞추세요";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ControlDark;
            ClientSize = new Size(800, 450);
            Controls.Add(label2);
            Controls.Add(label1);
            Name = "Form1";
            Opacity = 0.6D;
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
    }
}
