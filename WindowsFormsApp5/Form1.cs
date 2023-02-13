using System;
using System.IO;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace Security
{
    public partial class Form1 : Form
    {
        private System.Windows.Forms.Button buttonEncryptFile;
        private System.Windows.Forms.Button buttonDecryptFile;
        private System.Windows.Forms.Button buttonCreateAsmKeys;
        private System.Windows.Forms.Button buttonExportPublicKey;
        private System.Windows.Forms.Button buttonImportPublicKey;
        private System.Windows.Forms.Button buttonGetPrivateKey;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.Label label1;

        public Form1()
        {
            InitializeComponent();
        }

        CspParameters cspp = new CspParameters();
        RSACryptoServiceProvider rsa;

        // Path variables for source, encryption, and
        // decryption folders. Must end with a backslash.
        const string EncrFolder = @"c:\Encrypt\";
        const string DecrFolder = @"c:\Decrypt\";
        const string SrcFolder = @"c:\docs\";

        // Public key file
        const string PubKeyFile = @"c:\encrypt\rsaPublicKey.txt";

        // Key container name for
        // private/public key value pair.
        const string keyName = "Key01";

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.buttonEncryptFile = new System.Windows.Forms.Button();
            this.buttonDecryptFile = new System.Windows.Forms.Button();
            this.buttonCreateAsmKeys = new System.Windows.Forms.Button();
            this.buttonExportPublicKey = new System.Windows.Forms.Button();
            this.buttonImportPublicKey = new System.Windows.Forms.Button();
            this.buttonGetPrivateKey = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.qqToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.українськаToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.englishToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.режимиToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.світлийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.темнийToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.label2 = new System.Windows.Forms.Label();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // buttonEncryptFile
            // 
            this.buttonEncryptFile.BackColor = System.Drawing.Color.GhostWhite;
            this.buttonEncryptFile.FlatAppearance.BorderColor = System.Drawing.Color.GhostWhite;
            this.buttonEncryptFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonEncryptFile.Location = new System.Drawing.Point(12, 122);
            this.buttonEncryptFile.Name = "buttonEncryptFile";
            this.buttonEncryptFile.Size = new System.Drawing.Size(125, 23);
            this.buttonEncryptFile.TabIndex = 0;
            this.buttonEncryptFile.Text = "Зашифрувати файл";
            this.buttonEncryptFile.UseVisualStyleBackColor = false;
            this.buttonEncryptFile.Click += new System.EventHandler(this.buttonEncryptFile_Click);
            // 
            // buttonDecryptFile
            // 
            this.buttonDecryptFile.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonDecryptFile.Location = new System.Drawing.Point(149, 122);
            this.buttonDecryptFile.Name = "buttonDecryptFile";
            this.buttonDecryptFile.Size = new System.Drawing.Size(125, 23);
            this.buttonDecryptFile.TabIndex = 1;
            this.buttonDecryptFile.Text = "Розшифрувати файл";
            this.buttonDecryptFile.UseVisualStyleBackColor = true;
            this.buttonDecryptFile.Click += new System.EventHandler(this.buttonDecryptFile_Click);
            // 
            // buttonCreateAsmKeys
            // 
            this.buttonCreateAsmKeys.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonCreateAsmKeys.Location = new System.Drawing.Point(12, 166);
            this.buttonCreateAsmKeys.Name = "buttonCreateAsmKeys";
            this.buttonCreateAsmKeys.Size = new System.Drawing.Size(125, 23);
            this.buttonCreateAsmKeys.TabIndex = 2;
            this.buttonCreateAsmKeys.Text = "Створити ключ";
            this.buttonCreateAsmKeys.UseVisualStyleBackColor = true;
            this.buttonCreateAsmKeys.Click += new System.EventHandler(this.buttonCreateAsmKeys_Click);
            // 
            // buttonExportPublicKey
            // 
            this.buttonExportPublicKey.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonExportPublicKey.Location = new System.Drawing.Point(12, 195);
            this.buttonExportPublicKey.Name = "buttonExportPublicKey";
            this.buttonExportPublicKey.Size = new System.Drawing.Size(125, 23);
            this.buttonExportPublicKey.TabIndex = 3;
            this.buttonExportPublicKey.Text = "Експортувати ключ";
            this.buttonExportPublicKey.UseVisualStyleBackColor = true;
            this.buttonExportPublicKey.Click += new System.EventHandler(this.buttonExportPublicKey_Click);
            // 
            // buttonImportPublicKey
            // 
            this.buttonImportPublicKey.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonImportPublicKey.Location = new System.Drawing.Point(149, 166);
            this.buttonImportPublicKey.Name = "buttonImportPublicKey";
            this.buttonImportPublicKey.Size = new System.Drawing.Size(125, 23);
            this.buttonImportPublicKey.TabIndex = 4;
            this.buttonImportPublicKey.Text = "Імпортувати ключ";
            this.buttonImportPublicKey.UseVisualStyleBackColor = true;
            this.buttonImportPublicKey.Click += new System.EventHandler(this.buttonImportPublicKey_Click);
            // 
            // buttonGetPrivateKey
            // 
            this.buttonGetPrivateKey.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.buttonGetPrivateKey.Location = new System.Drawing.Point(149, 195);
            this.buttonGetPrivateKey.Name = "buttonGetPrivateKey";
            this.buttonGetPrivateKey.Size = new System.Drawing.Size(125, 23);
            this.buttonGetPrivateKey.TabIndex = 5;
            this.buttonGetPrivateKey.Text = "Приватний ключ";
            this.buttonGetPrivateKey.UseVisualStyleBackColor = true;
            this.buttonGetPrivateKey.Click += new System.EventHandler(this.buttonGetPrivateKey_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.GhostWhite;
            this.label1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.label1.Location = new System.Drawing.Point(12, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 44);
            this.label1.TabIndex = 6;
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.Color.GhostWhite;
            this.menuStrip1.GripMargin = new System.Windows.Forms.Padding(2);
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.qqToolStripMenuItem,
            this.режимиToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(289, 24);
            this.menuStrip1.TabIndex = 10;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // qqToolStripMenuItem
            // 
            this.qqToolStripMenuItem.BackColor = System.Drawing.Color.Gainsboro;
            this.qqToolStripMenuItem.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.qqToolStripMenuItem.Checked = true;
            this.qqToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.qqToolStripMenuItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.qqToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.українськаToolStripMenuItem,
            this.englishToolStripMenuItem});
            this.qqToolStripMenuItem.Name = "qqToolStripMenuItem";
            this.qqToolStripMenuItem.Size = new System.Drawing.Size(49, 20);
            this.qqToolStripMenuItem.Text = "Мова";
            this.qqToolStripMenuItem.Click += new System.EventHandler(this.qqToolStripMenuItem_Click);
            // 
            // українськаToolStripMenuItem
            // 
            this.українськаToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("українськаToolStripMenuItem.Image")));
            this.українськаToolStripMenuItem.Name = "українськаToolStripMenuItem";
            this.українськаToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.українськаToolStripMenuItem.Text = "Українська";
            this.українськаToolStripMenuItem.Click += new System.EventHandler(this.українськаToolStripMenuItem_Click);
            // 
            // englishToolStripMenuItem
            // 
            this.englishToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("englishToolStripMenuItem.Image")));
            this.englishToolStripMenuItem.Name = "englishToolStripMenuItem";
            this.englishToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.englishToolStripMenuItem.Text = "English";
            this.englishToolStripMenuItem.Click += new System.EventHandler(this.englishToolStripMenuItem_Click);
            // 
            // режимиToolStripMenuItem
            // 
            this.режимиToolStripMenuItem.BackColor = System.Drawing.Color.Gainsboro;
            this.режимиToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.світлийToolStripMenuItem,
            this.темнийToolStripMenuItem});
            this.режимиToolStripMenuItem.Name = "режимиToolStripMenuItem";
            this.режимиToolStripMenuItem.Size = new System.Drawing.Size(47, 20);
            this.режимиToolStripMenuItem.Text = "Теми";
            // 
            // світлийToolStripMenuItem
            // 
            this.світлийToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("світлийToolStripMenuItem.Image")));
            this.світлийToolStripMenuItem.Name = "світлийToolStripMenuItem";
            this.світлийToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.світлийToolStripMenuItem.Text = "Світлий";
            this.світлийToolStripMenuItem.Click += new System.EventHandler(this.світлийToolStripMenuItem_Click);
            // 
            // темнийToolStripMenuItem
            // 
            this.темнийToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("темнийToolStripMenuItem.Image")));
            this.темнийToolStripMenuItem.Name = "темнийToolStripMenuItem";
            this.темнийToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.темнийToolStripMenuItem.Text = "Темний";
            this.темнийToolStripMenuItem.Click += new System.EventHandler(this.темнийToolStripMenuItem_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Comic Sans MS", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(62, 24);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(152, 21);
            this.label2.TabIndex = 11;
            this.label2.Text = "SC Cript шифратор";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.GhostWhite;
            this.ClientSize = new System.Drawing.Size(289, 235);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.buttonGetPrivateKey);
            this.Controls.Add(this.buttonImportPublicKey);
            this.Controls.Add(this.buttonExportPublicKey);
            this.Controls.Add(this.buttonCreateAsmKeys);
            this.Controls.Add(this.buttonDecryptFile);
            this.Controls.Add(this.buttonEncryptFile);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "Form1";
            this.Text = "SC Cript";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        [STAThread]
        static void Main()
        {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            System.Windows.Forms.Application.Run(new Form1());
        }

        private void buttonCreateAsmKeys_Click(object sender, EventArgs e)
        {
            cspp.KeyContainerName = keyName;
            rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;
            if (rsa.PublicOnly == true)
                label1.Text = "Key: " + cspp.KeyContainerName + " - Public Only";
            else
                label1.Text = "Key: " + cspp.KeyContainerName + " - Full Key Pair";
        }
                    
        private void buttonEncryptFile_Click(object sender, EventArgs e)
        {
            if (rsa == null)
                MessageBox.Show("Не задано ключ");
            else
            {
                // Display a dialog box to select a file to encrypt.
                openFileDialog1.InitialDirectory = SrcFolder;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    string fName = openFileDialog1.FileName;
                    if (fName != null)
                    {
                        FileInfo fInfo = new FileInfo(fName);
                        // Pass the file name without the path.
                        string name = fInfo.FullName;
                        EncryptFile(name);
                    }
                }
            }
        }

        private void EncryptFile(string inFile)
        {
            // Create instance of Rijndael for
            // symmetric encryption of the data.
            RijndaelManaged rjndl = new RijndaelManaged();
            rjndl.KeySize = 256;
            rjndl.BlockSize = 256;
            rjndl.Mode = CipherMode.CBC;
            ICryptoTransform transform = rjndl.CreateEncryptor();

            // Use RSACryptoServiceProvider to
            // encrypt the Rijndael key.
            // rsa is previously instantiated: 
            //    rsa = new RSACryptoServiceProvider(cspp);
            byte[] keyEncrypted = rsa.Encrypt(rjndl.Key, false);

            // Create byte arrays to contain
            // the length values of the key and IV.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            int lKey = keyEncrypted.Length;
            LenK = BitConverter.GetBytes(lKey);
            int lIV = rjndl.IV.Length;
            LenIV = BitConverter.GetBytes(lIV);

            // Write the following to the FileStream
            // for the encrypted file (outFs):
            // - length of the key
            // - length of the IV
            // - encrypted key
            // - the IV
            // - the encrypted cipher content

            int startFileName = inFile.LastIndexOf("\\") + 1;
            // Change the file's extension to ".enc"
            string outFile = EncrFolder + inFile.Substring
        (startFileName, inFile.LastIndexOf(".") - startFileName) + ".enc";

            using (FileStream outFs = new FileStream(outFile, FileMode.Create))
            {
                outFs.Write(LenK, 0, 4);
                outFs.Write(LenIV, 0, 4);
                outFs.Write(keyEncrypted, 0, lKey);
                outFs.Write(rjndl.IV, 0, lIV);

                // Now write the cipher text using
                // a CryptoStream for encrypting.
                using (CryptoStream outStreamEncrypted =
        new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                {
                    // By encrypting a chunk at
                    // a time, you can save memory
                    // and accommodate large files.
                    int count = 0;
                    int offset = 0;

                    // blockSizeBytes can be any arbitrary size.
                    int blockSizeBytes = rjndl.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];
                    int bytesRead = 0;

                    using (FileStream inFs = new FileStream(inFile, FileMode.Open))
                    {
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamEncrypted.Write(data, 0, count);
                            bytesRead += blockSizeBytes;
                        }
                        while (count > 0);
                        inFs.Close();
                    }
                    outStreamEncrypted.FlushFinalBlock();
                    outStreamEncrypted.Close();
                }
                outFs.Close();
            }
        }

        private void buttonDecryptFile_Click(object sender, EventArgs e)
        {
            if (rsa == null)
                MessageBox.Show("Не задано ключ");
            else
            {
                // Display a dialog box to select the encrypted file.
                openFileDialog2.InitialDirectory = EncrFolder;
                if (openFileDialog2.ShowDialog() == DialogResult.OK)
                {
                    string fName = openFileDialog2.FileName;
                    if (fName != null)
                    {
                        FileInfo fi = new FileInfo(fName);
                        string name = fi.Name;
                        DecryptFile(name);
                    }
                }
            }
        }

        private void DecryptFile(string inFile)
        {
            // Create instance of Rijndael for
            // symmetric decryption of the data.
            RijndaelManaged rjndl = new RijndaelManaged();
            rjndl.KeySize = 256;
            rjndl.BlockSize = 256;
            rjndl.Mode = CipherMode.CBC;

            // Create byte arrays to get the length of
            // the encrypted key and IV.
            // These values were stored as 4 bytes each
            // at the beginning of the encrypted package.
            byte[] LenK = new byte[4];
            byte[] LenIV = new byte[4];

            // Construct the file name for the decrypted file.
            string outFile = DecrFolder + inFile.Substring
            (0, inFile.LastIndexOf(".")) + ".txt";

            // Use FileStream objects to read the encrypted
            // file (inFs) and save the decrypted file (outFs).
            using (FileStream inFs = new FileStream(EncrFolder + inFile, FileMode.Open))
            {
                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Seek(0, SeekOrigin.Begin);
                inFs.Read(LenK, 0, 3);
                inFs.Seek(4, SeekOrigin.Begin);
                inFs.Read(LenIV, 0, 3);

                // Convert the lengths to integer values.
                int lenK = BitConverter.ToInt32(LenK, 0);
                int lenIV = BitConverter.ToInt32(LenIV, 0);

                // Determine the start position of
                // the cipher text (startC)
                // and its length(lenC).
                int startC = lenK + lenIV + 8;
                int lenC = (int)inFs.Length - startC;

                // Create the byte arrays for
                // the encrypted Rijndael key,
                // the IV, and the cipher text.
                byte[] KeyEncrypted = new byte[lenK];
                byte[] IV = new byte[lenIV];

                // Extract the key and IV
                // starting from index 8
                // after the length values.
                inFs.Seek(8, SeekOrigin.Begin);
                inFs.Read(KeyEncrypted, 0, lenK);
                inFs.Seek(8 + lenK, SeekOrigin.Begin);
                inFs.Read(IV, 0, lenIV);
                Directory.CreateDirectory(DecrFolder);
                // Use RSACryptoServiceProvider
                // to decrypt the Rijndael key.
                byte[] KeyDecrypted = rsa.Decrypt(KeyEncrypted, false);

                // Decrypt the key.
                ICryptoTransform transform = rjndl.CreateDecryptor(KeyDecrypted, IV);
             using (FileStream outFs = new FileStream(outFile, FileMode.Create))
                {
                    int count = 0;
                    int offset = 0;
                    int blockSizeBytes = rjndl.BlockSize / 8;
                    byte[] data = new byte[blockSizeBytes];
                    inFs.Seek(startC, SeekOrigin.Begin);
                    using (CryptoStream outStreamDecrypted =
            new CryptoStream(outFs, transform, CryptoStreamMode.Write))
                    {
                        do
                        {
                            count = inFs.Read(data, 0, blockSizeBytes);
                            offset += count;
                            outStreamDecrypted.Write(data, 0, count);
                        }
                        while (count > 0);

                        outStreamDecrypted.FlushFinalBlock();
                        outStreamDecrypted.Close();
                    }
                    outFs.Close();
                }
                inFs.Close();
            }
        }

        private void buttonExportPublicKey_Click(object sender, EventArgs e)
        {
            Directory.CreateDirectory(EncrFolder);
            StreamWriter sw = new StreamWriter(PubKeyFile, false);
            sw.Write(rsa.ToXmlString(false));
            sw.Close();
        }

        private void buttonImportPublicKey_Click(object sender, EventArgs e)
        {
            StreamReader sr = new StreamReader(PubKeyFile);
            cspp.KeyContainerName = keyName;
            rsa = new RSACryptoServiceProvider(cspp);
            string keytxt = sr.ReadToEnd();
            rsa.FromXmlString(keytxt);
            rsa.PersistKeyInCsp = true;
            if (rsa.PublicOnly == true)
                label1.Text = "Key: " + cspp.KeyContainerName + " - Public Only";
            else
                label1.Text = "Key: " + cspp.KeyContainerName + " - Full Key Pair";
            sr.Close();
        }

        private void buttonGetPrivateKey_Click(object sender, EventArgs e)
        {
            cspp.KeyContainerName = keyName;

            rsa = new RSACryptoServiceProvider(cspp);
            rsa.PersistKeyInCsp = true;

            if (rsa.PublicOnly == true)
                label1.Text = "Key: " + cspp.KeyContainerName + " - Public Only";
            else
                label1.Text = "Key: " + cspp.KeyContainerName + " - Full Key Pair";
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
        private IContainer components;

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private MenuStrip menuStrip1;
        private ToolStripMenuItem qqToolStripMenuItem;

        private void qqToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private ToolStripMenuItem українськаToolStripMenuItem;
        private ToolStripMenuItem englishToolStripMenuItem;

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private ToolStripMenuItem режимиToolStripMenuItem;
        private ToolStripMenuItem світлийToolStripMenuItem;
        private ToolStripMenuItem темнийToolStripMenuItem;



        private Label label2;

        private void englishToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label2.Text = "SC Cript Encriptor";
            qqToolStripMenuItem.Text = "Language";
            українськаToolStripMenuItem.Text = "Ukrainian";
            режимиToolStripMenuItem.Text = "Mods";
            світлийToolStripMenuItem.Text = "Light";
            темнийToolStripMenuItem.Text = "Dark";
            buttonEncryptFile.Text = "Encrypt File";
            buttonDecryptFile.Text = "Decrypt File";
            buttonCreateAsmKeys.Text = "Create Key";
            buttonExportPublicKey.Text = "Export Key";
            buttonImportPublicKey.Text = "Import Key";
            buttonGetPrivateKey.Text = "Private Key";
            //messageBox.Text = "Key not set.";
        }

        private void українськаToolStripMenuItem_Click(object sender, EventArgs e)
        {
            label2.Text = "SC Cript Шифратор";
            qqToolStripMenuItem.Text = "Мова";
            українськаToolStripMenuItem.Text = "Українська";
            режимиToolStripMenuItem.Text = "Режими";
            світлийToolStripMenuItem.Text = "Світлий";
            темнийToolStripMenuItem.Text = "Темний";
            buttonEncryptFile.Text = "Зашифрувати файл";
            buttonDecryptFile.Text = "Розшифрувати файл";
            buttonCreateAsmKeys.Text = "Створити ключ";
            buttonExportPublicKey.Text = "Експортувати ключ";
            buttonImportPublicKey.Text = "Імпортувати ключ";
            buttonGetPrivateKey.Text = "Приватний ключ";
            //messageBox.Text = "Ключ не задано";
        }

        private void темнийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.Gray;
            menuStrip1.BackColor = System.Drawing.Color.Gray;
            label2.ForeColor = System.Drawing.Color.White;
            buttonEncryptFile.BackColor = System.Drawing.Color.White;
            buttonDecryptFile.BackColor = System.Drawing.Color.White;
            buttonCreateAsmKeys.BackColor = System.Drawing.Color.White;
            buttonImportPublicKey.BackColor = System.Drawing.Color.White;
            buttonExportPublicKey.BackColor = System.Drawing.Color.White;
            buttonGetPrivateKey.BackColor = System.Drawing.Color.White;

        }
        private void світлийToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.BackColor = System.Drawing.Color.White;
            menuStrip1.BackColor = System.Drawing.Color.White;
            label2.ForeColor = System.Drawing.Color.Black;
            buttonEncryptFile.BackColor = System.Drawing.Color.GhostWhite;
            buttonDecryptFile.BackColor = System.Drawing.Color.GhostWhite;
            buttonCreateAsmKeys.BackColor = System.Drawing.Color.GhostWhite;
            buttonImportPublicKey.BackColor = System.Drawing.Color.GhostWhite;
            buttonExportPublicKey.BackColor = System.Drawing.Color.GhostWhite;
            buttonGetPrivateKey.BackColor = System.Drawing.Color.GhostWhite;
        }
    }
}
//.Text = "Mods";