using Prueba.Entidades;
using Prueba.Servicios;
using System;
using System.Windows.Forms;

namespace Prueba
{
    public partial class HistoriaClinicaForm : Form
    {
        private Paciente paciente;
        public HistoriaClinicaForm()
        {
            InitializeComponent();
        }


        public HistoriaClinicaForm(Paciente paciente)
        : this()
        {
            this.paciente = paciente;
            
        }
        public virtual void PoblarComboBox(ComboBox cmb,
           object datos,
           string PropiedadMostrar = "",
           string propiedadDevolver = "")
        {
            cmb.DropDownStyle = ComboBoxStyle.DropDownList;
            cmb.DataSource = datos;


            if (!string.IsNullOrEmpty(PropiedadMostrar))
            {
                cmb.DisplayMember = PropiedadMostrar;
            }

            if (!string.IsNullOrEmpty(propiedadDevolver))
            {
                cmb.ValueMember = propiedadDevolver;
            }
        }

        private void HistoriaClinicaForm_Load(object sender, EventArgs e)
        {
            PoblarComboBox(cmbMedico, MedicoServicio.ObtenerMedicosDelArchivo(string.Empty), "ApeyNom", "Matricula");
            lblPaciente.Text = paciente.ApeyNom;

            MostrarMovimientos();

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(txtIngreso.Text))
            {
                MessageBox.Show("Por favor ingrese el motivo del Ingreso del paciente");
                txtIngreso.Focus();
                return;
            }
            if (string.IsNullOrEmpty(txtDiagnostico.Text))
            {
                MessageBox.Show("Por favor ingrese el diagnostico del paciente.");
                txtDiagnostico.Focus();
                return;
            }

            if (string.IsNullOrEmpty(txtTratamiento.Text))
            {
                MessageBox.Show("Por favor ingrese el tratamiento del paciente.");
                txtTratamiento.Focus();
                return;
            }

            var nuevaHistoriaClinica = new HistoriaClinica
            {
                MotivoIngreso = txtIngreso.Text,
                Diagnostico = txtDiagnostico.Text,
                Tratamiento = txtTratamiento.Text,
                Fecha = DateTime.Now,
                IdentificacionMedico = (cmbMedico.SelectedValue).ToString(),
                IdentificacionPaciente = paciente.Ficha

            };

            HistoriaClinicaServicio.Add(nuevaHistoriaClinica);

            MessageBox.Show("Los datos se grabaron correctamente");
            btnActualizar.PerformClick();


            
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            txtIngreso.Clear();
            txtDiagnostico.Clear();
            txtTratamiento.Clear();
            btnGuardar.Enabled = true;
            MostrarMovimientos();

        }

        private void MostrarMovimientos()
        {
            dgvHistoriaClinica.DataSource = HistoriaClinicaServicio.ObtenerMovimientosDelArchivo(paciente.Ficha);


            dgvHistoriaClinica.Columns["Fecha"].Visible = true;
            dgvHistoriaClinica.Columns["Fecha"].HeaderText = "Fecha";
            dgvHistoriaClinica.Columns["Fecha"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;


            dgvHistoriaClinica.Columns["MotivoIngreso"].Visible = true;
            dgvHistoriaClinica.Columns["MotivoIngreso"].HeaderText = "Motivo Ingreso";
            dgvHistoriaClinica.Columns["MotivoIngreso"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvHistoriaClinica.Columns["Diagnostico"].Visible = true;
            dgvHistoriaClinica.Columns["Diagnostico"].HeaderText = "Diagnostico";
            dgvHistoriaClinica.Columns["Diagnostico"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvHistoriaClinica.Columns["Tratamiento"].Visible = true;
            dgvHistoriaClinica.Columns["Tratamiento"].HeaderText = "Tratamiento";
            dgvHistoriaClinica.Columns["Tratamiento"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvHistoriaClinica.Columns["IdentificacionPaciente"].Visible = true;
            dgvHistoriaClinica.Columns["IdentificacionPaciente"].HeaderText = "Identificacion del Paciente";
            dgvHistoriaClinica.Columns["IdentificacionPaciente"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;

            dgvHistoriaClinica.Columns["IdentificacionMedico"].Visible = true;
            dgvHistoriaClinica.Columns["IdentificacionMedico"].HeaderText = "Identificacion del Medico";
            dgvHistoriaClinica.Columns["IdentificacionMedico"].AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;



        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dgvHistoriaClinica_RowHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            var historiaClinica = (HistoriaClinica)dgvHistoriaClinica.Rows[e.RowIndex].DataBoundItem;
            txtDiagnostico.Text = historiaClinica.Diagnostico;
            txtIngreso.Text = historiaClinica.MotivoIngreso;
            txtTratamiento.Text = historiaClinica.Tratamiento;
            btnGuardar.Enabled = false;
        }
    }
}
