namespace WinFormUsingAsyncEnumerator {
   partial class WindowsForms {
      /// <summary>
      /// Required designer variable.
      /// </summary>
      private System.ComponentModel.IContainer components = null;

      /// <summary>
      /// Clean up any resources being used.
      /// </summary>
      /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
      protected override void Dispose(bool disposing) {
         if (disposing && (components != null)) {
            components.Dispose();
         }
         base.Dispose(disposing);
      }

      #region Windows Form Designer generated code

      /// <summary>
      /// Required method for Designer support - do not modify
      /// the contents of this method with the code editor.
      /// </summary>
      private void InitializeComponent() {
         this.m_btnStart = new System.Windows.Forms.Button();
         this.m_btnCancel = new System.Windows.Forms.Button();
         this.m_lbResults = new System.Windows.Forms.ListBox();
         this.m_chkAutoCancel = new System.Windows.Forms.CheckBox();
         this.SuspendLayout();
         // 
         // m_btnStart
         // 
         this.m_btnStart.Location = new System.Drawing.Point(12, 2);
         this.m_btnStart.Name = "m_btnStart";
         this.m_btnStart.Size = new System.Drawing.Size(75, 23);
         this.m_btnStart.TabIndex = 0;
         this.m_btnStart.Text = "&Start";
         this.m_btnStart.UseVisualStyleBackColor = true;
         this.m_btnStart.Click += new System.EventHandler(this.m_btnStart_Click);
         // 
         // m_btnCancel
         // 
         this.m_btnCancel.Enabled = false;
         this.m_btnCancel.Location = new System.Drawing.Point(93, 2);
         this.m_btnCancel.Name = "m_btnCancel";
         this.m_btnCancel.Size = new System.Drawing.Size(75, 23);
         this.m_btnCancel.TabIndex = 1;
         this.m_btnCancel.Text = "&Cancel";
         this.m_btnCancel.UseVisualStyleBackColor = true;
         this.m_btnCancel.Click += new System.EventHandler(this.m_btnCancel_Click);
         // 
         // m_lbResults
         // 
         this.m_lbResults.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                     | System.Windows.Forms.AnchorStyles.Left)
                     | System.Windows.Forms.AnchorStyles.Right)));
         this.m_lbResults.FormattingEnabled = true;
         this.m_lbResults.Location = new System.Drawing.Point(13, 31);
         this.m_lbResults.Name = "m_lbResults";
         this.m_lbResults.Size = new System.Drawing.Size(349, 108);
         this.m_lbResults.TabIndex = 2;
         // 
         // m_chkAutoCancel
         // 
         this.m_chkAutoCancel.AutoSize = true;
         this.m_chkAutoCancel.Location = new System.Drawing.Point(185, 7);
         this.m_chkAutoCancel.Name = "m_chkAutoCancel";
         this.m_chkAutoCancel.Size = new System.Drawing.Size(159, 17);
         this.m_chkAutoCancel.TabIndex = 3;
         this.m_chkAutoCancel.Text = "&Auto-cancel after 5 seconds";
         this.m_chkAutoCancel.UseVisualStyleBackColor = true;
         // 
         // WindowsFormsViaAsyncEnumerator
         // 
         this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
         this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
         this.ClientSize = new System.Drawing.Size(374, 146);
         this.Controls.Add(this.m_chkAutoCancel);
         this.Controls.Add(this.m_lbResults);
         this.Controls.Add(this.m_btnCancel);
         this.Controls.Add(this.m_btnStart);
         this.Name = "WindowsFormsViaAsyncEnumerator";
         this.Text = "Windows Form Via AsyncEnumerator";
         this.ResumeLayout(false);
         this.PerformLayout();

      }

      #endregion

      private System.Windows.Forms.Button m_btnStart;
      private System.Windows.Forms.Button m_btnCancel;
      private System.Windows.Forms.ListBox m_lbResults;
      private System.Windows.Forms.CheckBox m_chkAutoCancel;
   }
}