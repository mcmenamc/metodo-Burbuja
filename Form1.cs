using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ProgressBar;
using System.Xml.Serialization;
using MetododoBurbuja.models;

namespace MetododoBurbuja
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private List<Sismo> ListString = new List<Sismo>();
        private Thread thread;
        private Thread thread2;
        private List<int> listNumber = new List<int>();
      
        private void Form1_Load(object sender, EventArgs e)
        {
            string path = Path.GetFullPath("..\\..\\images\\burbuja2.png");
            pictureBox1.ImageLocation = path;
        }
        private void button5_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = null;
            dataGridView2.DataSource = null;
            dataGridView3.DataSource = null;
            ListString.Clear();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            DataSet ds = new DataSet();
            string xmlFile = Path.GetFullPath("..\\..\\Documents\\datos.xml");
            ds.ReadXml(xmlFile);

            if (ds.Tables[0]!= null)
            {
                dataGridView1.DataSource = ds.Tables[0];

                foreach (DataRow dr in ds.Tables[0].Rows) {
                    ListString.Add(new Sismo
                    {
                        descripcion = dr["descripcion"].ToString(),
                        sat_unimed = dr["sat_unimed"].ToString(),
                        simbolo = dr["simbolo"].ToString()
                    });
                }
            }
        }

        private void OrdenaString()
        {
            Sismo[] arreglo = ListString.ToArray();
            for (int x = 0; x < arreglo.Length; x++)
            {
                for (int indiceActual = 0; indiceActual < arreglo.Length - 1;indiceActual++)
                {
                    int indiceSiguienteElemento = indiceActual + 1;
                    if (arreglo[indiceActual].descripcion.CompareTo(arreglo[indiceSiguienteElemento].descripcion) > 0)
                    {
                        Sismo temporal = arreglo[indiceActual];
                        arreglo[indiceActual] = arreglo[indiceSiguienteElemento];
                        arreglo[indiceSiguienteElemento] = temporal;
                    }
                }
            }
            if (dataGridView2.InvokeRequired)
                dataGridView2.Invoke(new MethodInvoker(delegate
                {
                    dataGridView2.DataSource = arreglo;
                }));
        }

        private void Advanced_BubbleString()
        {
            Sismo[] arreglo = ListString.ToArray();

            bool flag = true;
            for (int i = 1; (i <= (arreglo.Length - 1)) && flag; i++)
            {
                flag = false;
                for (int j = 0; j < (arreglo.Length - 1); j++)
                {
                    if (arreglo[j + 1].descripcion.CompareTo(arreglo[j].descripcion) > 0)
                    {
                        Sismo temp = arreglo[j];
                        arreglo[j] = arreglo[j + 1];
                        arreglo[j + 1] = temp;
                        flag = true;
                    }
                }
            }
            if (dataGridView3.InvokeRequired)
                dataGridView3.Invoke(new MethodInvoker(delegate
                {
                    dataGridView3.DataSource = arreglo;
                }));
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (ListString.Count < 1)
            {
                MessageBox.Show("No hay elementos en la lista", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            thread = new Thread(new ThreadStart(OrdenaString));
            thread2 = new Thread(new ThreadStart(Advanced_BubbleString));
            thread2.Start();
            thread.Start();
        }

        //private void RegularBubble()
        //{
        //    int[] arrrayNumber = listNumber.ToArray();
        //    int num = arrrayNumber.Length;

        //    if (num < 1)
        //    {
        //        MessageBox.Show("No hay elementos en la lista", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    for (int i = 0; i < num - 1; i++)
        //        for (int j = 0; j < num - i - 1; j++)
        //            if (arrrayNumber[j] > arrrayNumber[j + 1])
        //            {
        //                // intercambiar tmp y arr[i]
        //                int tmp = arrrayNumber[j];
        //                arrrayNumber[j] = arrrayNumber[j + 1];
        //                arrrayNumber[j + 1] = tmp;
        //            }

        //    if (listBox2.InvokeRequired)
        //        listBox2.Invoke(new MethodInvoker(delegate
        //        {
        //            listBox2.Items.Clear();
        //            foreach (int number in arrrayNumber)
        //                listBox2.Items.Add(number.ToString());
        //        }));

        //    MessageBox.Show("Termino de ordenar", "Info", MessageBoxButtons.OK);
        //}
        //private void Advanced_Bubble()
        //{
        //    int[] intArray = listNumber.ToArray();

        //    if (intArray.Length < 1)
        //    {
        //        MessageBox.Show("No hay elementos en la lista", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return;
        //    }

        //    int count = 0;
        //    bool flag = true;
        //    for (int i = 1; (i <= (intArray.Length - 1)) && flag; i++)
        //    {
        //        flag = false;
        //        for (int j = 0; j < (intArray.Length - 1); j++)
        //        {
        //            count = count + 1;
        //            if (intArray[j + 1] > intArray[j])
        //            {
        //                int temp = intArray[j];
        //                intArray[j] = intArray[j + 1];
        //                intArray[j + 1] = temp;
        //                flag = true;
        //            }
        //        }
        //    }
        //    if (listBox2.InvokeRequired)
        //        listBox2.Invoke(new MethodInvoker(delegate
        //        {
        //            listBox2.Items.Clear();
        //            foreach (int number in intArray)
        //                listBox2.Items.Add(number.ToString());
        //        }));
        //    MessageBox.Show("Termino de ordenar avanzado", "Info", MessageBoxButtons.OK);
        //}
        //private void button2_Click(object sender, EventArgs e)
        //{
        //    thread = new Thread(new ThreadStart(Advanced_BubbleString));
        //    //thread = new Thread(new ThreadStart(Advanced_Bubble));
        //    thread.Start();
        //}
    }
}
