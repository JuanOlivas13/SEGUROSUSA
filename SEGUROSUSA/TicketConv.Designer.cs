﻿namespace SEGUROSUSA
{
    partial class TicketConv
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
            this.reportViewerConv = new Microsoft.Reporting.WinForms.ReportViewer();
            this.SuspendLayout();
            // 
            // reportViewerConv
            // 
            this.reportViewerConv.Dock = System.Windows.Forms.DockStyle.Fill;
            this.reportViewerConv.LocalReport.ReportEmbeddedResource = "SEGUROSUSA.TicketConv.rdlc";
            this.reportViewerConv.Location = new System.Drawing.Point(0, 0);
            this.reportViewerConv.Name = "reportViewerConv";
            this.reportViewerConv.ServerReport.BearerToken = null;
            this.reportViewerConv.Size = new System.Drawing.Size(784, 661);
            this.reportViewerConv.TabIndex = 0;
            // 
            // TicketConv
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 661);
            this.Controls.Add(this.reportViewerConv);
            this.Name = "TicketConv";
            this.Text = "TicketConv";
            this.Load += new System.EventHandler(this.TicketConv_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewerConv;
    }
}