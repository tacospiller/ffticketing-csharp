namespace ffTicketingCsharp
{
    partial class ReturnButton
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("맑은 고딕", 30F);
            label1.Location = new Point(12, 18);
            label1.Name = "label1";
            label1.Size = new Size(733, 54);
            label1.TabIndex = 0;
            label1.Text = "이 창을 구역 돌아가기 버튼에 맞추세요";
            // 
            // ReturnButton
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(label1);
            Name = "ReturnButton";
            Opacity = 0.6D;
            Text = "ReturnButton";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
    }
}