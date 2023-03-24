using Datos;
using Entidades;
using System;
using System.Windows.Forms;

namespace Examen_Lenguaje_II
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void CancelarButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void AceptarButton_Click(object sender, EventArgs e)
        {
            if (UsuarioTextBox.Text == string.Empty)
            {
                errorProvider1.SetError(UsuarioTextBox, "Ingrese un usuario ");
                UsuarioTextBox.Clear();
                UsuarioTextBox.Focus();
                return;
            }

            errorProvider1.Clear();
            if (string.IsNullOrEmpty(ContraseñaTextBox.Text))
            {
                errorProvider1.SetError(ContraseñaTextBox, "Ingrese una contraseña ");
                ContraseñaTextBox.Clear();
                ContraseñaTextBox.Focus();
                return;
            }
            errorProvider1.Clear();


            //Validar en la base de datos
            Login login = new Login(UsuarioTextBox.Text, ContraseñaTextBox.Text);
            Usuario usuario = new Usuario();
            UsuarioDB usuarioDB = new UsuarioDB();

            usuario = usuarioDB.Autenticar(login);

            if (usuario != null)
            {
                if (usuario.EstaActivo)
                {

                    System.Security.Principal.GenericIdentity identidad = new System.Security.Principal.GenericIdentity(usuario.CodigoUsuario);
                    System.Security.Principal.GenericPrincipal principal = new System.Security.Principal.GenericPrincipal(identidad, new string[] { usuario.Rol });
                    System.Threading.Thread.CurrentPrincipal = principal;


                    //Mostramos el menú
                    Menu menuFormulario = new Menu();
                    this.Hide();
                    menuFormulario.Show();
                }
                else
                {
                    MessageBox.Show("El usuario no esta activo", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

            }
            else
            {
                MessageBox.Show("Datos de usuario Incorrecto", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
