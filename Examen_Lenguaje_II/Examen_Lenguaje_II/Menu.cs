using System.Windows.Forms;

namespace Examen_Lenguaje_II
{
    public partial class Menu : Form
    {
        public Menu()
        {
            InitializeComponent();
        }

        private void UsuariostoolStripButton_Click(object sender, System.EventArgs e)
        {
            UsuarioForm useForm = new UsuarioForm();
            //useForm.MdiParent = this;
            useForm.Show();
        }

        private void ClientestoolStripButton_Click(object sender, System.EventArgs e)
        {
            ClientesForm clientesForm = new ClientesForm();
            //clientesForm.MdiParent = this;
            clientesForm.Show();
        }

        private void TiketstoolStripButton_Click(object sender, System.EventArgs e)
        {
            TicketsForm ticketsForm = new TicketsForm();
            //ticketsForm.MdiParent = this;
            ticketsForm.Show();
        }

        private void toolStripTabItem1_Click(object sender, System.EventArgs e)
        {

        }
    }
}
