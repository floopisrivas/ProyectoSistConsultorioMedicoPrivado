using Prueba.Entidades;
using System;
using System.Windows.Forms;
using Prueba.Servicios;

namespace Prueba
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            int cantidad = 0;

            if (dgvPacientes.Rows.Count > 0)
            {
                cantidad = dgvPacientes.Rows.Count + 1;
            }
            else
            {
                cantidad = 1;
            }

            if (string.IsNullOrEmpty(txtDni.Text))
            {
                MessageBox.Show("Por favor ingrese un DNI");
                txtDni.Focus();
                return;
            }
            else
            {
                if (txtDni.Text.Length < 7)
                {
                    MessageBox.Show("EL dni debe tener al menos 7 digitos");
                    txtDni.Focus();
                    return;
                }
            }


            if (string.IsNullOrEmpty(txtApellido.Text))
            {
                MessageBox.Show("Por favor ingrese un apellido");
                txtApellido.Focus();
                return;
            }


            if (string.IsNullOrEmpty(txtTelefono.Text))
            {
                MessageBox.Show("Por favor ingrese un telefono");
                txtTelefono.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtDomicilio.Text))
            {
                MessageBox.Show("Por favor ingrese un domicilio");
                txtDomicilio.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text))
            {
                MessageBox.Show("Por favor ingrese un Email");
                txtEmail.Focus();
                return;
            }


            var nuevoPaciente = new Paciente
            {

                Dni = txtDni.Text,
                Ficha = cantidad.ToString(),
                ApeyNom = txtApellido.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDomicilio.Text,
                Mail = txtEmail.Text,
                EstaEliminado = false

            };

            PacienteServicio.Grabar(nuevoPaciente);


            MessageBox.Show("Los datos se grabaron correctamente");

            btnActualizar.PerformClick();

        }

        private void MostrarMovimientos(string dni)
        {
            dgvPacientes.DataSource = PacienteServicio.ObtenerPacientesDelArchivo(dni);

            dgvPacientes.Columns["Dni"].Visible = true;
            dgvPacientes.Columns["Dni"].HeaderText = "Dni";
            dgvPacientes.Columns["Dni"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgvPacientes.Columns["Ficha"].Visible = true;
            dgvPacientes.Columns["Ficha"].HeaderText = "Ficha Médica";
            dgvPacientes.Columns["Ficha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgvPacientes.Columns["ApeyNom"].Visible = true;
            dgvPacientes.Columns["ApeyNom"].HeaderText = "Apellido y Nombre";
            dgvPacientes.Columns["ApeyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgvPacientes.Columns["Direccion"].Visible = true;
            dgvPacientes.Columns["Direccion"].HeaderText = "Direccion";
            dgvPacientes.Columns["Direccion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgvPacientes.Columns["Telefono"].Visible = true;
            dgvPacientes.Columns["Telefono"].HeaderText = "Telefono";
            dgvPacientes.Columns["Telefono"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvPacientes.Columns["Mail"].Visible = true;
            dgvPacientes.Columns["Mail"].HeaderText = "Mail";
            dgvPacientes.Columns["Mail"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            MostrarMovimientos(string.Empty);
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            txtDni.Clear();
            txtFichaMedica.Clear();
            txtApellido.Clear();
            txtDomicilio.Clear();
            txtEmail.Clear();
            txtTelefono.Clear();
            btnGuardar.Enabled = true;
            MostrarMovimientos(string.Empty);

        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvPacientes_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var paciente = (Paciente)dgvPacientes.Rows[e.RowIndex].DataBoundItem;
            var formulario = new HistoriaClinicaForm(paciente);
            formulario.ShowDialog();

        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MostrarMovimientos(txtBuscar.Text);
            
        }

        private void dgvPacientes_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var paciente = (Paciente)dgvPacientes.Rows[e.RowIndex].DataBoundItem;
            txtApellido.Text = paciente.ApeyNom;
            txtDni.Text = paciente.Dni;
            txtDomicilio.Text = paciente.Direccion;
            txtEmail.Text = paciente.Mail;
            txtTelefono.Text = paciente.Telefono;
            txtFichaMedica.Text = paciente.Ficha;
            btnGuardar.Enabled = false;
        }
    }
}
