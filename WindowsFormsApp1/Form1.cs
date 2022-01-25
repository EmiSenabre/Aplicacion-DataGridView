using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.IO;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        private int n = -1;
        public Form1()
        {
            InitializeComponent();
        }
        //Este boton abre un formulario para rellenar los datos de un nuevo producto
        private void btAñadir_Click(object sender, EventArgs e)
        {
            Agregar agrega = new Agregar();

            if (agrega.ShowDialog() == DialogResult.OK)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = agrega.Nombre;
                dataGridView1.Rows[n].Cells[1].Value = agrega.Codigo;
                dataGridView1.Rows[n].Cells[2].Value = agrega.Cantidad;
                dataGridView1.Rows[n].Cells[3].Value = agrega.Precio;
                dataGridView1.Rows[n].Cells[4].Value = agrega.Descripcion;
                dataGridView1.Rows[n].Cells[5].Value = agrega.Tipo;
            }
        }
        //Guarda el valor de una fila seleccionada
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            n = e.RowIndex;

        }
        //Si tienes una fila seleccionada o varias abre un messagebox que si lo confirmas borra dichas columnas 
        private void btBorrar_Click(object sender, EventArgs e)
        {
            /*if(n !=-2)
            {
                dataGridView1.Rows.RemoveAt(n);
            }*/
            int i = 0;

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    i++;
                }
            }
            if (i > 0)
            {
                n = -1;
                if (MessageBox.Show("¿Quiere borrar " + i + " productos de la tabla?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            dataGridView1.Rows.Remove(row);
                        }
                    }
                }     
            }
        }

        //Abre un formulario si tienes una fila seleccionada con los datos cargados donde podras modifcar estos datos
        private void btModificar_Click(object sender, EventArgs e)
        {
            if(n != -1)
            {
                Modificar modifica = new Modificar();
                modifica.Cantidad = int.Parse(dataGridView1.Rows[n].Cells[2].Value.ToString());
                modifica.Precio = double.Parse(dataGridView1.Rows[n].Cells[3].Value.ToString());
                modifica.Nombre = (string)dataGridView1.Rows[n].Cells[0].Value;
                modifica.Codigo = (string)dataGridView1.Rows[n].Cells[1].Value;
                modifica.Descripcion = (string)dataGridView1.Rows[n].Cells[4].Value;
                modifica.Tipo = (string)dataGridView1.Rows[n].Cells[5].Value;
                if (modifica.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.Rows[n].Cells[0].Value = modifica.Nombre;
                    dataGridView1.Rows[n].Cells[1].Value = modifica.Codigo;
                    dataGridView1.Rows[n].Cells[2].Value = modifica.Cantidad;
                    dataGridView1.Rows[n].Cells[3].Value = modifica.Precio;
                    dataGridView1.Rows[n].Cells[4].Value = modifica.Descripcion;
                    dataGridView1.Rows[n].Cells[5].Value = modifica.Tipo;
                }
            }
        }
        //Boton para importar un CSV
        private void btImportar_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Archivo CSV|*.csv" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string SEP = ";";

                string[] lineas = File.ReadAllLines(ofd.FileName);
                string[] cabeceras = lineas[0].Split(new[] { SEP }, StringSplitOptions.None);

                dataGridView1.Columns.Clear();
                foreach (string c in cabeceras)
                    dataGridView1.Columns.Add(c, c);

                for (int i = 1; i < lineas.Length; i++)
                {
                    string[] celdas = lineas[i].Split(new[] { SEP }, StringSplitOptions.None);
                    dataGridView1.Rows.Add(celdas);
                }
            }
        }
        //Boton para exportar nuestra tabla a CSV
        private void btExportar_Click(object sender, EventArgs e)
        {

            SaveFileDialog sfd = new SaveFileDialog() { Filter = "Archivo CSV|*.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                List<string> filas = new List<string>();

                List<string> cabeceras = new List<string>();
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    cabeceras.Add(col.HeaderText);
                }
                string SEP = ";";
                filas.Add(string.Join(SEP, cabeceras));

                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    try
                    {

                        List<string> celdas = new List<string>();
                        foreach (DataGridViewCell c in fila.Cells)
                            celdas.Add(c.Value.ToString());

                        filas.Add(string.Join(SEP, celdas));
                    }
                    catch (Exception ex) { }
                }

                File.WriteAllLines(sfd.FileName, filas);
            }
        }
        //Estas herramientas del menu tendrian las mismas funciones que sus relativos botones
        private void importarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog() { Filter = "Archivo CSV|*.csv" };
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string SEP = ";";

                string[] lineas = File.ReadAllLines(ofd.FileName);
                string[] cabeceras = lineas[0].Split(new[] { SEP }, StringSplitOptions.None);

                dataGridView1.Columns.Clear();
                foreach (string c in cabeceras)
                    dataGridView1.Columns.Add(c, c);

                for (int i = 1; i < lineas.Length; i++)
                {
                    string[] celdas = lineas[i].Split(new[] { SEP }, StringSplitOptions.None);
                    dataGridView1.Rows.Add(celdas);
                }
            }
        }

        private void exportarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog() { Filter = "Archivo CSV|*.csv" };
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                List<string> filas = new List<string>();

                List<string> cabeceras = new List<string>();
                foreach (DataGridViewColumn col in dataGridView1.Columns)
                {
                    cabeceras.Add(col.HeaderText);
                }
                string SEP = ";";
                filas.Add(string.Join(SEP, cabeceras));

                foreach (DataGridViewRow fila in dataGridView1.Rows)
                {
                    try
                    {

                        List<string> celdas = new List<string>();
                        foreach (DataGridViewCell c in fila.Cells)
                            celdas.Add(c.Value.ToString());

                        filas.Add(string.Join(SEP, celdas));
                    }
                    catch (Exception ex) { }
                }

                File.WriteAllLines(sfd.FileName, filas);
            }
        }

        private void añadirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Agregar agrega = new Agregar();

            if (agrega.ShowDialog() == DialogResult.OK)
            {
                int n = dataGridView1.Rows.Add();
                dataGridView1.Rows[n].Cells[0].Value = agrega.Nombre;
                dataGridView1.Rows[n].Cells[1].Value = agrega.Codigo;
                dataGridView1.Rows[n].Cells[2].Value = agrega.Cantidad;
                dataGridView1.Rows[n].Cells[3].Value = agrega.Precio;
                dataGridView1.Rows[n].Cells[4].Value = agrega.Descripcion;
                dataGridView1.Rows[n].Cells[5].Value = agrega.Tipo;
            }
        }

        private void borrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int i = 0;

            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                if (!row.IsNewRow)
                {
                    i++;
                }
            }
            if (i > 0)
            {
                n = -1;
                if (MessageBox.Show("¿Quiere borrar " + i + " productos de la tabla?", "Confirmar", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                    {
                        if (!row.IsNewRow)
                        {
                            dataGridView1.Rows.Remove(row);
                        }
                    }
                }
            }
        }

        private void modificarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (n != -1)
            {
                Modificar modifica = new Modificar();
                modifica.Cantidad = int.Parse(dataGridView1.Rows[n].Cells[2].Value.ToString());
                modifica.Precio = double.Parse(dataGridView1.Rows[n].Cells[3].Value.ToString());
                modifica.Nombre = (string)dataGridView1.Rows[n].Cells[0].Value;
                modifica.Codigo = (string)dataGridView1.Rows[n].Cells[1].Value;
                modifica.Descripcion = (string)dataGridView1.Rows[n].Cells[4].Value;
                modifica.Tipo = (string)dataGridView1.Rows[n].Cells[5].Value;
                if (modifica.ShowDialog() == DialogResult.OK)
                {
                    dataGridView1.Rows[n].Cells[0].Value = modifica.Nombre;
                    dataGridView1.Rows[n].Cells[1].Value = modifica.Codigo;
                    dataGridView1.Rows[n].Cells[2].Value = modifica.Cantidad;
                    dataGridView1.Rows[n].Cells[3].Value = modifica.Precio;
                    dataGridView1.Rows[n].Cells[4].Value = modifica.Descripcion;
                    dataGridView1.Rows[n].Cells[5].Value = modifica.Tipo;
                }
            }
        }
    }
}
