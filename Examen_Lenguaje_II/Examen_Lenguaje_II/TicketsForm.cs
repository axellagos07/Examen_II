using Datos;
using Entidades;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Examen_Lenguaje_II
{
    public partial class TicketsForm : Form
    {
        public TicketsForm()
        {
            InitializeComponent();
        }
        Cliente miCliente = null;
        ClienteDB clienteDB = new ClienteDB();
        TicketDB ticketDB = new TicketDB();
        List<Ticke> listaDetalles = new List<Ticke>();
        //ProductoDB productoDB = new ProductoDB();
        // List<Ticke> listaDetalles = new List<Ticke>();
        Ticke detalle;
        decimal subTotal = 0;
        decimal isv = 0;
        decimal totalAPagar = 0;
        decimal descuento = 0;
        decimal TT = 0;

        private void IdentidadtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(IdentidadtextBox.Text))
            {
                miCliente = new Cliente();
                miCliente = clienteDB.DevolverClientePorIdentidad(IdentidadtextBox.Text);
                NombreClientetextBox.Text = miCliente.Nombre;

            }
            else
            {
                miCliente = null;
                NombreClientetextBox.Clear();
            }
        }

        private void BuscarClientebutton_Click(object sender, System.EventArgs e)
        {
            BuscarClienteForm form = new BuscarClienteForm();
            form.ShowDialog();

            miCliente = new Cliente();
            miCliente = form.cliente;
            IdentidadtextBox.Text = miCliente.Identidad;
            NombreClientetextBox.Text = miCliente.Nombre;

        }

        private void TicketsForm_Load(object sender, System.EventArgs e)
        {
            UsuariotextBox.Text = System.Threading.Thread.CurrentPrincipal.Identity.Name;
        }

        private void CantidadtextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter && !string.IsNullOrEmpty(CantidadtextBox.Text))
            {
                Ticke detalle = new Ticke();
                //detalle.CodigoProducto = miProducto.Codigo;
                detalle.Cantidad = Convert.ToInt32(CantidadtextBox.Text);
                detalle.Precio = Convert.ToDecimal(PreciotextBox.Text);
                detalle.Total = Convert.ToUInt32(CantidadtextBox.Text) * Convert.ToInt32(PreciotextBox.Text);
                detalle.Descripcion = DescripcioncomboBox.Text;

                subTotal += detalle.Total;
                //descuento = subTotal * 0.10M;
                isv = subTotal * 0.15M;
                totalAPagar = subTotal + isv - descuento;


                listaDetalles.Add(detalle);
                DetalledataGridView.DataSource = null;
                DetalledataGridView.DataSource = listaDetalles;

                SubTotaltextBox.Text = subTotal.ToString();
                ISVtextBox.Text = isv.ToString();
                TotaltextBox.Text = totalAPagar.ToString();

                //DescripcionProductotextBox.Clear();
                //DescripcioncomboBox = null;
                PreciotextBox.Clear();
                CantidadtextBox.Clear();

            }
        }



        private void Cancelarbutton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            Ticke miTike = new Ticke();
            miTike.Fecha = FechadateTimePicker.Value;
            miTike.CodigoUsuario = System.Threading.Thread.CurrentPrincipal.Identity.Name;
            miTike.IdentidadCliente = miCliente.Identidad;//
            miTike.SubTotal = subTotal;
            miTike.ISV = isv;
            miTike.Descuento = descuento;
            miTike.Total = totalAPagar;

            bool inserto = ticketDB.Guardar(miTike, listaDetalles);

            if (inserto)
            {
                //LimpiarControles();
                IdentidadtextBox.Focus();
                MessageBox.Show("Factura Registrada Exitosamente");
            }
            else
            {
                MessageBox.Show("No se pudo registrar la factura");
            }
        }
    }
}
