using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkUsageTracker;

namespace NetworkUsageTracker_App
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            var usageAnalyzer = new UsageAnalyzer(new UsageCollector(),new UsageListener());

            Task.Run(() => StartMonitor());
        }

        private void StartMonitor()
        {
            while(true)
            {
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
