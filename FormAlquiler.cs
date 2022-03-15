using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Laboratorio_No._6
{
    public partial class FormAlquiler : Form
    {
        List<Alquiler> alquileres = new List<Alquiler>();
        List<Cliente> clientes = new List<Cliente>();   
        List<Vehiculo> vehiculos = new List<Vehiculo>();
        List<Mostrar> mostrar = new List<Mostrar>();    
        public FormAlquiler()
        {
            InitializeComponent();
        }

        private void GuardarAlquiler()
        {
            FileStream stream = new FileStream("Alquileres.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);

            foreach (var alquiler in alquileres)
            {
                writer.WriteLine(alquiler.nit);
                writer.WriteLine(alquiler.placa);
                writer.WriteLine(alquiler.fechaAlquiler);
                writer.WriteLine(alquiler.fechaDevolucion);
                writer.WriteLine(alquiler.kilometros);
            }
            writer.Close();
        }

        private void CargarVehiculos()
        {
            FileStream stream = new FileStream("Vehiculos.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);

            
            while (reader.Peek() > -1)
            
            {
                Vehiculo vehiculo = new Vehiculo();
                vehiculo.placa = reader.ReadLine();
                vehiculo.marca = reader.ReadLine();
                vehiculo.color = reader.ReadLine();
                vehiculo.modelo = Convert.ToInt16(reader.ReadLine());
                vehiculo.precioKilometro = Convert.ToDecimal(reader.ReadLine());

                vehiculos.Add(vehiculo);
            }
            
            reader.Close();
        }

        private void CargarClientes()
        {
            FileStream stream = new FileStream("Clientes.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);


            while (reader.Peek() > -1)

            {
               Cliente cliente = new Cliente();
                cliente.nit = reader.ReadLine();
                cliente.nombre = reader.ReadLine(); 
                cliente.direccion = reader.ReadLine();

                clientes.Add(cliente);
            }

            reader.Close();
        }

        private void CargarAlquileres()
        {
            FileStream stream = new FileStream("Alquileres.txt", FileMode.Open, FileAccess.Read);
            StreamReader reader = new StreamReader(stream);


            while (reader.Peek() > -1)

            {
                Alquiler alquiler = new Alquiler();
                alquiler.nit = reader.ReadLine();
                alquiler.placa = reader.ReadLine();
                alquiler.fechaAlquiler = Convert.ToDateTime(reader.ReadLine());
                alquiler.fechaDevolucion = Convert.ToDateTime(reader.ReadLine());
                alquiler.kilometros = Convert.ToInt16(reader.ReadLine());   

                alquileres.Add(alquiler);   
            }

            reader.Close();
        }

        private void buttonIngresar_Click(object sender, EventArgs e)
        {
            Alquiler alquiler = new Alquiler();
            alquiler.nit = textBoxNit.Text;
            alquiler.placa = textBoxPlaca.Text;
            alquiler.fechaAlquiler = dateTimePickerPrestamo.Value;
            alquiler.fechaDevolucion = dateTimePickerDevolucion.Value;
            alquiler.kilometros = Convert.ToInt16(textBoxKilometros.Text);

            alquileres.Add(alquiler);
            GuardarAlquiler();
            dataGridView1.DataSource = null;
            dataGridView1.Refresh();

            dataGridView2.DataSource = null;
            dataGridView2.Refresh();
        }
        private void CargarDatos()
        {
            dataGridView3.DataSource = null;
            dataGridView3.Refresh();
            dataGridView3.DataSource = mostrar;
            dataGridView3.Refresh();
        }

        private void buttonMostrar_Click(object sender, EventArgs e)
        {
            for(int i = 0; i < alquileres.Count; i++)
            {
                Mostrar mostrarDatos = new Mostrar();
                for(int j = 0; j <clientes.Count; j++)
                {
                    if (alquileres[i].nit == clientes[j].nit)
                    {
                        mostrarDatos.nombre = clientes[j].nombre;
                        mostrarDatos.devolucion = alquileres[i].fechaDevolucion;
                    }
                }
                for (int k = 0; k < vehiculos.Count; k++)
                {
                    if(alquileres[i].placa == vehiculos[k].placa)
                    {
                        mostrarDatos.placa = vehiculos[k].placa;
                        mostrarDatos.color = vehiculos[k].color;
                        mostrarDatos.total = vehiculos[k].precioKilometro * alquileres[i].kilometros;

                    }
                }
                mostrar.Add(mostrarDatos);
                
            }
            CargarDatos();
        }

        private void FormAlquiler_Load(object sender, EventArgs e)
        {
            CargarClientes();
            CargarVehiculos();
            dataGridView1.DataSource = vehiculos;
            dataGridView1.Refresh();

            CargarAlquileres();
            dataGridView2.DataSource = alquileres;
            dataGridView2.Refresh();
        }
    }
}
