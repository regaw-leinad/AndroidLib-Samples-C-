using System;
using System.Windows.Forms;
using RegawMOD.Android;

namespace BuildPropDemo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        AndroidController android;
        Device device;
        
        private void Form1_Load(object sender, EventArgs e)
        {
            //Usually, you want to load this at startup, may take up to 5 seconds to initialize/set up resources/start server
            android = AndroidController.Instance;
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ALWAYS remember to call this when you're done with AndroidController.  It removes used resources
            android.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string serial;

            android.UpdateDeviceList();

            listBox1.Items.Clear();
            label2.Text = "-null-";

            if (android.HasConnectedDevices)
            {
                serial = android.ConnectedDevices[0];
                device = android.GetConnectedDevice(serial);

                //Adds all of the build.prop keys to the listbox
                foreach (string key in device.BuildProp.Keys)
                {
                    listBox1.Items.Add(key);
                }

                // OR you could do
                // listBox1.Items.AddRange(device.BuildProp.Keys.ToArray());

                //So no items are selected right away
                listBox1.SelectedIndex = -1;
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = device.BuildProp.GetProp(listBox1.SelectedItem.ToString());
        }
    }
}