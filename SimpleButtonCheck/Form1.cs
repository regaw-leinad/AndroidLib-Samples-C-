using System;
using System.Windows.Forms;
using RegawMOD.Android;

namespace SimpleButtonCheck
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

        private void Button1_Click(object sender, EventArgs e)
        {
            string serial;

            //Always call UpdateDeviceList() before using AndroidController on devices to get the most updated list
            android.UpdateDeviceList();

            if (android.HasConnectedDevices)
            {
                serial = android.ConnectedDevices[0];
                device = android.GetConnectedDevice(serial);
                this.TextBox1.Text = device.SerialNumber;
            }
            else
            {
                this.TextBox1.Text = "Error - No Devices Connected";
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            //ALWAYS remember to call this when you're done with AndroidController.  It removes used resources
            android.Dispose(); 
        }
    }
}
