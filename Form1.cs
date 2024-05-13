﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;    

namespace hollow
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            
            saveFileDialog1.Title = "儲存檔案";
           
            saveFileDialog1.Filter = "文字檔案 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
            
            saveFileDialog1.FilterIndex = 1;
           
            saveFileDialog1.InitialDirectory = "C:\\";

            
            DialogResult result = saveFileDialog1.ShowDialog();

            
            FileStream fileStream = null;

            
            if (result == DialogResult.OK)
            {
                try
                {
                   
                    string saveFileName = saveFileDialog1.FileName;

                    
                    fileStream = new FileStream(saveFileName, FileMode.Create, FileAccess.Write);
                   
                    byte[] data = Encoding.UTF8.GetBytes(rtbText.Text);
                    fileStream.Write(data, 0, data.Length);

                    

                    MessageBox.Show("檔案儲存成功。", "訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("儲存檔案時發生錯誤: " + ex.Message, "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                finally
                {
                    // 關閉資源，如果使用 using 或者直接以 File.WriteAllText 儲存文字檔，可以不需要。
                    fileStream.Close();
                }
            }
            else
            {
                MessageBox.Show("使用者取消了儲存檔案操作。", "訊息", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
            }
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            
            openFileDialog1.Title = "選擇檔案";
           
            openFileDialog1.Filter = "文字檔案 (*.txt)|*.txt|所有檔案 (*.*)|*.*";
            
            openFileDialog1.FilterIndex = 1;
            
            openFileDialog1.InitialDirectory = "C:\\";
            
            openFileDialog1.Multiselect = true;

            
            DialogResult result = openFileDialog1.ShowDialog();

            
            if (result == DialogResult.OK)
            {
                try
                {
                   
                    string selectedFileName = openFileDialog1.FileName;

                    using (FileStream fileStream = new FileStream(selectedFileName, FileMode.Open, FileAccess.Read))
                    {
                        
                        using (StreamReader streamReader = new StreamReader(fileStream, Encoding.UTF8))
                        {
                            
                            rtbText.Text = streamReader.ReadToEnd();
                        }
                    }

                    
                }
                catch (Exception ex)
                {
                    
                    MessageBox.Show("讀取檔案時發生錯誤: " + ex.Message, "錯誤訊息", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("使用者取消了選擇檔案操作。", "訊息", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation);
            }
        }
    }
}