namespace WinForms
{
    partial class MainForm
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
            dgvUsers = new DataGridView();
            User = new Label();
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            Username = new TextBox();
            dgvSongs = new DataGridView();
            Email = new TextBox();
            Title = new TextBox();
            Artist = new TextBox();
            AddSong = new Button();
            DeleteSong = new Button();
            AddUser = new Button();
            DeleteUser = new Button();
            Pass = new Label();
            Password = new TextBox();
            helpProvider1 = new HelpProvider();
            label4 = new Label();
            label5 = new Label();
            Year = new TextBox();
            Genre = new TextBox();
            ((System.ComponentModel.ISupportInitialize)dgvUsers).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvSongs).BeginInit();
            SuspendLayout();
            // 
            // dgvUsers
            // 
            dgvUsers.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvUsers.Location = new Point(414, 185);
            dgvUsers.Name = "dgvUsers";
            dgvUsers.Size = new Size(374, 253);
            dgvUsers.TabIndex = 1;
            // 
            // User
            // 
            User.AutoSize = true;
            User.Location = new Point(488, 27);
            User.Name = "User";
            User.Size = new Size(63, 15);
            User.TabIndex = 2;
            User.Text = "Username:";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(512, 85);
            label1.Name = "label1";
            label1.Size = new Size(39, 15);
            label1.TabIndex = 3;
            label1.Text = "Email:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(100, 14);
            label2.Name = "label2";
            label2.Size = new Size(32, 15);
            label2.TabIndex = 4;
            label2.Text = "Title:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(94, 43);
            label3.Name = "label3";
            label3.Size = new Size(38, 15);
            label3.TabIndex = 5;
            label3.Text = "Artist:";
            // 
            // Username
            // 
            Username.Location = new Point(557, 24);
            Username.Name = "Username";
            Username.Size = new Size(100, 23);
            Username.TabIndex = 6;
            // 
            // dgvSongs
            // 
            dgvSongs.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSongs.Location = new Point(26, 185);
            dgvSongs.Name = "dgvSongs";
            dgvSongs.Size = new Size(356, 253);
            dgvSongs.TabIndex = 7;
            // 
            // Email
            // 
            Email.Location = new Point(557, 82);
            Email.Name = "Email";
            Email.Size = new Size(100, 23);
            Email.TabIndex = 8;
            // 
            // Title
            // 
            Title.Location = new Point(138, 6);
            Title.Name = "Title";
            Title.Size = new Size(100, 23);
            Title.TabIndex = 9;
            // 
            // Artist
            // 
            Artist.Location = new Point(138, 35);
            Artist.Name = "Artist";
            Artist.Size = new Size(100, 23);
            Artist.TabIndex = 10;
            // 
            // AddSong
            // 
            AddSong.Location = new Point(150, 123);
            AddSong.Name = "AddSong";
            AddSong.Size = new Size(75, 23);
            AddSong.TabIndex = 11;
            AddSong.Text = "Add";
            AddSong.UseVisualStyleBackColor = true;
            AddSong.Click += AddSong_Click;
            // 
            // DeleteSong
            // 
            DeleteSong.Location = new Point(150, 152);
            DeleteSong.Name = "DeleteSong";
            DeleteSong.Size = new Size(75, 23);
            DeleteSong.TabIndex = 12;
            DeleteSong.Text = "Remove";
            DeleteSong.UseVisualStyleBackColor = true;
            DeleteSong.Click += DeleteSong_Click_1;
            // 
            // AddUser
            // 
            AddUser.Location = new Point(567, 123);
            AddUser.Name = "AddUser";
            AddUser.Size = new Size(75, 23);
            AddUser.TabIndex = 13;
            AddUser.Text = "Add";
            AddUser.UseVisualStyleBackColor = true;
            AddUser.Click += AddUser_Click_1;
            // 
            // DeleteUser
            // 
            DeleteUser.Location = new Point(567, 152);
            DeleteUser.Name = "DeleteUser";
            DeleteUser.Size = new Size(75, 23);
            DeleteUser.TabIndex = 14;
            DeleteUser.Text = "Remove";
            DeleteUser.UseVisualStyleBackColor = true;
            DeleteUser.Click += DeleteUser_Click_1;
            // 
            // Pass
            // 
            Pass.AutoSize = true;
            Pass.Location = new Point(491, 56);
            Pass.Name = "Pass";
            Pass.Size = new Size(60, 15);
            Pass.TabIndex = 15;
            Pass.Text = "Password:";
            // 
            // Password
            // 
            Password.Location = new Point(557, 53);
            Password.Name = "Password";
            Password.Size = new Size(100, 23);
            Password.TabIndex = 16;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(100, 69);
            label4.Name = "label4";
            label4.Size = new Size(32, 15);
            label4.TabIndex = 17;
            label4.Text = "Year:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(91, 93);
            label5.Name = "label5";
            label5.Size = new Size(41, 15);
            label5.TabIndex = 18;
            label5.Text = "Genre:";
            // 
            // Year
            // 
            Year.Location = new Point(138, 61);
            Year.Name = "Year";
            Year.Size = new Size(100, 23);
            Year.TabIndex = 19;
            // 
            // Genre
            // 
            Genre.Location = new Point(138, 90);
            Genre.Name = "Genre";
            Genre.Size = new Size(100, 23);
            Genre.TabIndex = 20;
            // 
            // MainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 450);
            Controls.Add(Genre);
            Controls.Add(Year);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(Password);
            Controls.Add(Pass);
            Controls.Add(DeleteUser);
            Controls.Add(AddUser);
            Controls.Add(DeleteSong);
            Controls.Add(AddSong);
            Controls.Add(Artist);
            Controls.Add(Title);
            Controls.Add(Email);
            Controls.Add(dgvSongs);
            Controls.Add(Username);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(User);
            Controls.Add(dgvUsers);
            Name = "MainForm";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvUsers).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvSongs).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private DataGridView dgvUsers;
        private Label User;
        private Label label1;
        private Label label2;
        private Label label3;
        private TextBox Username;
        private DataGridView dgvSongs;
        private TextBox Email;
        private TextBox Title;
        private TextBox Artist;
        private Button AddSong;
        private Button DeleteSong;
        private Button AddUser;
        private Button DeleteUser;
        private Label Pass;
        private TextBox Password;
        private HelpProvider helpProvider1;
        private Label label4;
        private Label label5;
        private TextBox Year;
        private TextBox Genre;
    }
}
