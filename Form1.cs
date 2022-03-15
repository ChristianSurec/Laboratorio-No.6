namespace Laboratorio_No._6
{
    public partial class Form1 : Form
    {
        List<Vehiculo> vehiculos = new List<Vehiculo>();
        List<Cliente> clientes = new List<Cliente>();   
        List<Alquiler> alquileres = new List<Alquiler>();
        public Form1()
        {
            InitializeComponent();
        }

        private void GuardarVehiculo()
        {
            FileStream stream = new FileStream("Vehiculos.txt", FileMode.OpenOrCreate, FileAccess.Write);           
            StreamWriter writer = new StreamWriter(stream);
            
            foreach(var vehiculo in vehiculos)
            {
                writer.WriteLine(vehiculo.placa);
                writer.WriteLine(vehiculo.marca);
                writer.WriteLine(vehiculo.color);
                writer.WriteLine(vehiculo.modelo.ToString());
                writer.WriteLine(vehiculo.precioKilometro.ToString());
            }
            writer.Close();
        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            Vehiculo vehiculo = new Vehiculo();
            vehiculo.placa = textBox1.Text;
            vehiculo.marca = textBox2.Text;
            vehiculo.color = textBox3.Text;
            vehiculo.modelo = Convert.ToInt16(textBox4.Text);
            vehiculo.precioKilometro = Convert.ToDecimal(textBox5.Text);

            int posicion = vehiculos.FindIndex(x => x.placa == vehiculo.placa);
            if (posicion == -1)
            {
                vehiculos.Add(vehiculo);
                GuardarVehiculo();
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            else
            {
                MessageBox.Show("Placa repetida");
                textBox1.Text = "";
                textBox2.Text = "";
                textBox3.Text = "";
                textBox4.Text = "";
                textBox5.Text = "";
            }
            
        }

        private void buttonAlquileres_Click(object sender, EventArgs e)
        {
            FormAlquiler formAlquiler = new FormAlquiler();
            formAlquiler.Show();
        }
    }
}