using Datos;
using Entidades;
using System;
using System.Data;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace Examen_Lenguaje_II
{
    public partial class UsuarioForm : Form
    {
        public UsuarioForm()
        {
            InitializeComponent();
        }
        string tipoOperacion;
        DataTable dt = new DataTable();
        UsuarioDB UsuarioDB = new UsuarioDB();
        Usuario user = new Usuario();
        private void HabilitarControles()
        {
            CodigotextBox.Enabled = true;
            NombretextBox.Enabled = true;
            ContraseñatextBox.Enabled = true;
            CorreotextBox.Enabled = true;
            RolcomboBox.Enabled = true;
            EstaActivocheckBox.Enabled = true;
            Adjuntarbutton.Enabled = true;
            Guardarbutton.Enabled = true;
            Cancelarbutton.Enabled = true;
            Modificarbutton.Enabled = false;

        }
        private void DesabilitarControles()
        {
            CodigotextBox.Enabled = false;
            NombretextBox.Enabled = false;
            ContraseñatextBox.Enabled = false;
            CorreotextBox.Enabled = false;
            RolcomboBox.Enabled = false;
            EstaActivocheckBox.Enabled = false;
            Adjuntarbutton.Enabled = false;
            Guardarbutton.Enabled = false;
            Cancelarbutton.Enabled = false;
            Modificarbutton.Enabled = true;

        }

        private void LimpiarControles()
        {
            CodigotextBox.Clear();
            NombretextBox.Clear();
            ContraseñatextBox.Clear();
            CorreotextBox.Clear();
            RolcomboBox.Text = string.Empty;
            EstaActivocheckBox.Checked = false;
            FotopictureBox.Image = null;

        }

        private void Nuevobutton_Click(object sender, EventArgs e)
        {
            CodigotextBox.Focus();
            HabilitarControles();
            tipoOperacion = "Nuevo";
        }

        private void Cancelarbutton_Click(object sender, EventArgs e)
        {
            DesabilitarControles();
            LimpiarControles();

        }

        private void Guardarbutton_Click(object sender, EventArgs e)
        {
            if (tipoOperacion == "Nuevo")
            {
                if (string.IsNullOrEmpty(CodigotextBox.Text))
                {
                    errorProvider1.SetError(CodigotextBox, "Ingrese un código");
                    CodigotextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(NombretextBox.Text))
                {
                    errorProvider1.SetError(NombretextBox, "Ingrese un Nombre");
                    NombretextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(ContraseñatextBox.Text))
                {
                    errorProvider1.SetError(ContraseñatextBox, "Ingrese la contraseña");
                    ContraseñatextBox.Focus();
                    return;
                }
                errorProvider1.Clear();
                if (string.IsNullOrEmpty(RolcomboBox.Text))
                {
                    errorProvider1.SetError(RolcomboBox, "seleccione un rol");
                    RolcomboBox.Focus();
                    return;
                }
                errorProvider1.Clear();

                user.CodigoUsuario = CodigotextBox.Text;
                user.Nombre = NombretextBox.Text;
                user.Contraseña = ContraseñatextBox.Text;
                user.Rol = RolcomboBox.Text;
                user.Correo = CorreotextBox.Text;
                user.EstaActivo = EstaActivocheckBox.Checked;

                if (FotopictureBox.Image != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();

                    FotopictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    user.Foto = ms.GetBuffer();

                }
                //Insertar en la base ded datos

                bool inserto = UsuarioDB.Insertar(user);

                if (inserto)
                {
                    DesabilitarControles();
                    LimpiarControles();
                    TraerUsuarios();
                    MessageBox.Show("Registro guardado");
                }
                else
                {
                    MessageBox.Show("No se pudo efectuar el registro");
                }

            }
            else if (tipoOperacion == "Modificar")
            {
                user.CodigoUsuario = CodigotextBox.Text;
                user.Nombre = NombretextBox.Text;
                user.Contraseña = ContraseñatextBox.Text;
                user.Rol = RolcomboBox.Text;
                user.Correo = CorreotextBox.Text;
                user.EstaActivo = EstaActivocheckBox.Checked;

                if (FotopictureBox.Image != null)
                {
                    System.IO.MemoryStream ms = new System.IO.MemoryStream();
                    FotopictureBox.Image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                    user.Foto = ms.GetBuffer();

                }

                bool modifico = UsuarioDB.Editar(user);
                if (modifico)
                {
                    LimpiarControles();
                    DesabilitarControles();
                    TraerUsuarios();
                    MessageBox.Show("Registro actualizado correctamente");
                }
                else
                {
                    MessageBox.Show("No se pudo actualizar el registro");
                }
            }

        }

        private void Modificarbutton_Click(object sender, EventArgs e)
        {
            tipoOperacion = "Modificar";
            if (UsuariosdataGridView.SelectedRows.Count > 0)
            {
                CodigotextBox.Text = UsuariosdataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString();
                NombretextBox.Text = UsuariosdataGridView.CurrentRow.Cells["Nombre"].Value.ToString();
                ContraseñatextBox.Text = UsuariosdataGridView.CurrentRow.Cells["Contrasena"].Value.ToString();
                CorreotextBox.Text = UsuariosdataGridView.CurrentRow.Cells["Correo"].Value.ToString();
                RolcomboBox.Text = UsuariosdataGridView.CurrentRow.Cells["Rol"].Value.ToString();
                EstaActivocheckBox.Checked = Convert.ToBoolean(UsuariosdataGridView.CurrentRow.Cells["EstaActivo"].Value);

                byte[] miFoto = UsuarioDB.DevolverFoto(UsuariosdataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString());

                if (miFoto.Length > 0)
                {
                    MemoryStream ms = new MemoryStream(miFoto);
                    FotopictureBox.Image = System.Drawing.Image.FromStream(ms);
                }

                HabilitarControles();
            }
            else
            {
                MessageBox.Show("Debe seleccionar un registro");
            }
        }

        private void Adjuntarbutton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            DialogResult resultado = dialog.ShowDialog();

            if (resultado == DialogResult.OK)
            {
                FotopictureBox.Image = Image.FromFile(dialog.FileName);

            }
        }

        private void UsuarioForm_Load(object sender, EventArgs e)
        {
            TraerUsuarios();
        }

        private void TraerUsuarios()
        {
            dt = UsuarioDB.DevolverUsuarios();

            UsuariosdataGridView.DataSource = dt;
        }

        private void Eliminarbutton_Click(object sender, EventArgs e)
        {
            {
                if (UsuariosdataGridView.SelectedRows.Count > 0)
                {
                    DialogResult resultado = MessageBox.Show("Esta seguro de eliminar el registro", "Advertencia", MessageBoxButtons.YesNo);

                    if (resultado == DialogResult.Yes)
                    {
                        bool elimino = UsuarioDB.Eliminar(UsuariosdataGridView.CurrentRow.Cells["CodigoUsuario"].Value.ToString());

                        if (elimino)
                        {
                            LimpiarControles();
                            DesabilitarControles();
                            TraerUsuarios();
                            MessageBox.Show("Registro eliminado");
                        }
                    }

                }
                else
                {
                    MessageBox.Show("No se pudo eliminar el registro ");
                }
            }
        }
    }
}
