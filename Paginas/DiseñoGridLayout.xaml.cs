using System;
using System.Collections.ObjectModel;
using Microsoft.Maui.Controls;

namespace AppOperaciones.Paginas
{
    public partial class DiseñoGridLayout : ContentPage
    {
        ObservableCollection<string> resultados = new ObservableCollection<string>();

        public DiseñoGridLayout()
        {
            InitializeComponent();
            lstResultados.ItemsSource = resultados;
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            int inicio, fin;
            if (!int.TryParse(txtInicio.Text, out inicio) || !int.TryParse(txtFin.Text, out fin))
            {
                DisplayAlert("Error", "Por favor, introduzca valores numéricos válidos para el inicio y el fin.", "Aceptar");
                return;
            }

            if (inicio >= fin)
            {
                DisplayAlert("Error", "El valor de inicio debe ser menor que el valor de fin.", "Aceptar");
                return;
            }

            if (Par.IsChecked || Impar.IsChecked)
            {
                string tipo = Par.IsChecked ? "Par" : "Impar";
                cmbNum1.ItemsSource = GenerarNumeros(inicio, fin, tipo);
                cmbNum2.ItemsSource = GenerarNumeros(inicio, fin, tipo);
            }
            else
            {
                DisplayAlert("Error", "Por favor, seleccione un tipo (Par/Impar).", "Aceptar");
                return;
            }
        }


        private ObservableCollection<int> GenerarNumeros(int inicio, int fin, string tipo)
        {
            ObservableCollection<int> numeros = new ObservableCollection<int>();
            for (int i = inicio; i <= fin; i++)
            {
                if ((tipo == "Par" && i % 2 == 0) || (tipo == "Impar" && i % 2 != 0))
                {
                    numeros.Add(i);
                }
            }
            return numeros;
        }

        private void RealizarOperaciones_Click(object sender, EventArgs e)
        {
            if (cmbNum1.SelectedItem == null || cmbNum2.SelectedItem == null)
            {
                DisplayAlert("Error", "Por favor, seleccione dos números.", "Aceptar");
                return;
            }

            int num1 = (int)cmbNum1.SelectedItem;
            int num2 = (int)cmbNum2.SelectedItem;

            ObservableCollection<string> operacionesResultados = new ObservableCollection<string>();

            if (Suma.IsChecked)
            {
                int suma = num1 + num2;
                operacionesResultados.Add($"La suma de {num1} + {num2} = {suma}");
            }

            if (Resta.IsChecked)
            {
                int resta = num1 - num2;
                operacionesResultados.Add($"La resta de {num1} - {num2} = {resta}");
            }

            if (Multiplicacion.IsChecked)
            {
                int multiplicacion = num1 * num2;
                operacionesResultados.Add($"La multiplicación de {num1} * {num2} = {multiplicacion}");
            }

            // Display results in the ListView
            lstResultados.ItemsSource = operacionesResultados;
        }


        private void Limpiar_Click(object sender, EventArgs e)
        {
            // Clear selections
            Par.IsChecked = false;
            Impar.IsChecked = false;
            Suma.IsChecked = false;
            Resta.IsChecked = false;
            Multiplicacion.IsChecked = false;

            // Clear text entries
            txtInicio.Text = "";
            txtFin.Text = "";

            // Reset ComboBoxes and ListView
            cmbNum1.ItemsSource = null;
            cmbNum2.ItemsSource = null;
            lstResultados.ItemsSource = null;
        }

    }
}
