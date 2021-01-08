using System;
using System.Windows.Forms;
using Prueba.Entidades;
using Prueba.Servicios;

namespace Prueba
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {


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

            if (string.IsNullOrEmpty(txtMatricula.Text))
            {
                MessageBox.Show("Por favor ingrese número de su Matricula");
                txtMatricula.Focus();
                return;
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


            var nuevoMedico = new Entidades.Medico
            {

                Dni = txtDni.Text,
                Matricula = int.Parse(txtMatricula.Text),
                ApeyNom = txtApellido.Text,
                Telefono = txtTelefono.Text,
                Direccion = txtDomicilio.Text,
                Mail = txtEmail.Text,
                EstaEliminado = false

            };

            MedicoServicio.Grabar(nuevoMedico);


            MessageBox.Show("Los datos se grabaron correctamente");
            btnActualizar.PerformClick();

        }
        private void MostrarMovimientos(string dni)
        {
            dgvMedicos.DataSource = MedicoServicio.ObtenerMedicosDelArchivo(dni);

            
            dgvMedicos.Columns["Dni"].Visible = true;
            dgvMedicos.Columns["Dni"].HeaderText = "Dni";
            dgvMedicos.Columns["Dni"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgvMedicos.Columns["Matricula"].Visible = true;
            dgvMedicos.Columns["Matricula"].HeaderText = "Matricula";
            dgvMedicos.Columns["Matricula"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgvMedicos.Columns["ApeyNom"].Visible = true;
            dgvMedicos.Columns["ApeyNom"].HeaderText = "Apellido y Nombre";
            dgvMedicos.Columns["ApeyNom"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgvMedicos.Columns["Direccion"].Visible = true;
            dgvMedicos.Columns["Direccion"].HeaderText = "Direccion";
            dgvMedicos.Columns["Direccion"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgvMedicos.Columns["Telefono"].Visible = true;
            dgvMedicos.Columns["Telefono"].HeaderText = "Telefono";
            dgvMedicos.Columns["Telefono"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvMedicos.Columns["Mail"].Visible = true;
            dgvMedicos.Columns["Mail"].HeaderText = "Mail";
            dgvMedicos.Columns["Mail"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


        }

        private void Form3_Load(object sender, EventArgs e)
        {
            MostrarMovimientos(string.Empty);

        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            txtDni.Clear();
            txtMatricula.Clear();
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

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            MostrarMovimientos(txtBuscar.Text);
        }

        private void dgvMedicos_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var medico = (Medico)dgvMedicos.Rows[e.RowIndex].DataBoundItem;
            txtApellido.Text = medico.ApeyNom;
            txtDni.Text = medico.Dni;
            txtDomicilio.Text = medico.Direccion;
            txtEmail.Text = medico.Mail;
            txtTelefono.Text = medico.Telefono;
            txtMatricula.Text = medico.Mail.ToString();
            btnGuardar.Enabled = false;
        }
    }
}
