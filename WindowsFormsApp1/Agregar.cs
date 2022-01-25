using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace WindowsFormsApp1
{
    public partial class Agregar : Form
    {
        
        public string Nombre { get; set; }
        public string Codigo { get; set; }
        public int Cantidad { get; set; }
        public double Precio { get; set; }
        public string Descripcion { get; set; }
        public string Tipo { get; set; }

        public Agregar()
        {
            InitializeComponent();
        }
        //Al dar a aceptar se validara que todos los campos necesarios esten rellenados y si lo estan, enviara los datos
        private void btAceptar_Click(object sender, EventArgs e)
        {
            BorrarMensajeError();
            if(ValidarCampos() == true)
            {

                Cantidad = Convert.ToInt32(nudCantidad.Text);
                Precio = Convert.ToDouble(nudPrecio.Text);
                Nombre = Convert.ToString(txtNombre.Text);
                Codigo = Convert.ToString(txtCodigo.Text);
                Descripcion = Convert.ToString(txtDescripcion.Text);
                Tipo = Convert.ToString(cbTipo.Text);
                DialogResult = DialogResult.OK;
            }

        }
        //Funcion para validar que todos los campos necesarios estan rellenados
        private bool ValidarCampos()
        {
            bool ok = true;
            if (nudCantidad.Text.Equals(""))
            {
                ok = false;
                errorProvider1.SetError(nudCantidad, "Ingresa una cantidad");
                
            }
            if (txtCodigo.Text.Equals(""))
            {
                ok = false;
                errorProvider1.SetError(txtCodigo, "Ingresa un codigo");

            }
            if (txtNombre.Text.Equals(""))
            {
                ok = false;
                errorProvider1.SetError(txtNombre, "Ingresa un nombre");

            }
            if (nudPrecio.Text.Equals(""))
            {
                ok = false;
                errorProvider1.SetError(nudPrecio, "Ingresa un precio");

            }

            if (cbTipo.Text.Equals(""))
            {
                ok = false;
                errorProvider1.SetError(cbTipo, "Elige un tipo");

            }
            return ok;
        }
        //Funcion para borrar los errores del error provider
        private void BorrarMensajeError()
        {
            errorProvider1.SetError(nudCantidad, "");
            errorProvider1.SetError(txtCodigo, "");
            errorProvider1.SetError(txtDescripcion, "");
            errorProvider1.SetError(txtNombre, "");
            errorProvider1.SetError(nudPrecio, "");
        }
    }
}
