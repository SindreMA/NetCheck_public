using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.NetworkInformation;
using System.Net;
using System.Threading;
using System.Timers;

namespace NetCheck
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            //var newLine = Environment.NewLine;
            //label3.Text = string.Format("Mister du til {0}, {1}", newLine, newLine);
            //label3.Text = $"Mister du tilkoblingen til både ServerSky og Internett{newLine} samtidig."
            label3.Text = "Mister du tilkoblingen til både ServerSky og Internett \nsamtidig så er det noe med nettlinjen din.\nMister du bare kontakt med ServerSky, ta kontak \nmed ServerSky (########).";
        }

        private void SendPing()
        {
            string server = "##################";
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();

            pingSender.PingCompleted += new PingCompletedEventHandler(pingSender_Complete);

            pingSender.SendAsync(server, 5000);
        }
        private void SendPing2()
        {
            string server = "google.com";
            System.Net.NetworkInformation.Ping pingSender = new System.Net.NetworkInformation.Ping();
            byte[] packetData = Encoding.ASCII.GetBytes("................................");
            pingSender.PingCompleted += new PingCompletedEventHandler(pingSender_Complete2);
            PingOptions packetOptions = new PingOptions(50, true);

            pingSender.SendAsync(server, 5000, packetData, packetOptions);
        }

        private void pingSender_Complete(object sender, PingCompletedEventArgs e)
        {

            string co = "Tilkoblet Serversky";
            string dc = "Ingen Tilkobling";
            string time = DateTime.Now.ToLongTimeString();
            richTextBox1.Select(0, 0);
            richTextBox1.SelectionLength = 0;
            try
            {
                if (e.Reply.Status == IPStatus.Success)
                {
                    richTextBox1.SelectedText += time + ": " + co + "\n";
                }
                else
                {
                    richTextBox1.SelectedText += time + ": " + dc + "\r\n";
                }
            }
            catch
            {
                richTextBox1.SelectedText += time + ": " + dc + "\r\n";
            }
        }
        private void pingSender_Complete2(object sender, PingCompletedEventArgs e)
        {
            string co = "Tilkoblet Internett";
            string dc = "Ingen Tilkobling";
            string time = DateTime.Now.ToLongTimeString();
            richTextBox2.Select(0, 0);
            richTextBox2.SelectionLength = 0;
            try
            {
                if (e.Reply.Status == IPStatus.Success)
                {
                    richTextBox2.SelectedText += time + ": " + co + "\n";
                }
                else
                {
                    richTextBox2.SelectedText += time + ": " + dc + "\r\n";
                }
            }
            catch
            {
                richTextBox2.SelectedText += time + ": " + dc + "\r\n";
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                SendPing();
                SendPing2();
            }
            catch { }
        }



        private void button1_Click(object sender, EventArgs e)
        {
            if (timer1.Enabled)
            {
                timer1.Stop();
                button1.Text = "Start";
            }
            else
            {
                timer1.Start();
                button1.Text = "Stop";
            }
        }
    }
}
