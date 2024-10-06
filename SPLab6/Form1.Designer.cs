namespace SPLab6
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
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            studentsGridView = new DataGridView();
            studentBindingSource = new BindingSource(components);
            fullname = new DataGridViewTextBoxColumn();
            studentId = new DataGridViewTextBoxColumn();
            isPresent = new DataGridViewCheckBoxColumn();
            ((System.ComponentModel.ISupportInitialize)studentsGridView).BeginInit();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).BeginInit();
            SuspendLayout();
            // 
            // studentsGridView
            // 
            studentsGridView.AutoGenerateColumns = false;
            studentsGridView.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            studentsGridView.Columns.AddRange(new DataGridViewColumn[] { fullname, studentId, isPresent });
            studentsGridView.DataSource = studentBindingSource;
            studentsGridView.Location = new Point(133, 62);
            studentsGridView.Name = "studentsGridView";
            studentsGridView.RowHeadersWidth = 51;
            studentsGridView.Size = new Size(508, 309);
            studentsGridView.TabIndex = 0;
            // 
            // studentBindingSource
            // 
            studentBindingSource.DataSource = typeof(Student);
            // 
            // fullname
            // 
            fullname.HeaderText = "Fullname";
            fullname.MinimumWidth = 6;
            fullname.Name = "fullname";
            fullname.ReadOnly = true;
            fullname.Width = 125;
            // 
            // studentId
            // 
            studentId.HeaderText = "Student ID";
            studentId.MinimumWidth = 6;
            studentId.Name = "studentId";
            studentId.ReadOnly = true;
            studentId.Width = 125;
            // 
            // isPresent
            // 
            isPresent.HeaderText = "Present";
            isPresent.MinimumWidth = 6;
            isPresent.Name = "isPresent";
            isPresent.ReadOnly = true;
            isPresent.Width = 125;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(studentsGridView);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)studentsGridView).EndInit();
            ((System.ComponentModel.ISupportInitialize)studentBindingSource).EndInit();
            ResumeLayout(false);
        }

        #endregion

        private DataGridView studentsGridView;
        private BindingSource studentBindingSource;
        private DataGridViewTextBoxColumn fullname;
        private DataGridViewTextBoxColumn studentId;
        private DataGridViewCheckBoxColumn isPresent;
    }
}
